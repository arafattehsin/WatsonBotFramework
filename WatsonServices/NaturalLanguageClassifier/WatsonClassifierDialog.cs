using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

using WatsonServices.NaturalLanguageClassifier.Models;

namespace WatsonServices.NaturalLanguageClassifier
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class WatsonClassifierClassAttribute : Attribute
    {
        /// <summary>
        /// The Watson Natural Language Classifier class name.
        /// </summary>
        public readonly string ClassName;

        /// <summary>
        /// Construct the association between the Watson class and a dialog method.
        /// </summary>
        /// <param name="className">The Watson Natural Language Classifier class name.</param>
        public WatsonClassifierClassAttribute(string className)
        {
            SetField.NotNull(out this.ClassName, nameof(className), className);
        } 
    }

    /// <summary>
    /// The handler for a Watson class.
    /// </summary>
    /// <param name="context">The dialog context.</param>
    /// <param name="classifierResult">The Classifier result.</param>
    /// <returns>A task representing the completion of the class processing.</returns>
    public delegate Task ClassifierClassHandler(IDialogContext context, ClassifierResult classifierResult);

    /// <summary>
    /// The handler for a Watson class.
    /// </summary>
    /// <param name="context">The dialog context.</param>
    /// <param name="message">The dialog message.</param>
    /// <param name="classifierResult">The Classifier result.</param>
    /// <returns>A task representing the completion of the class processing.</returns>
    public delegate Task ClassifierClassActivityHandler(IDialogContext context, IAwaitable<IMessageActivity> message, ClassifierResult classifierResult);


    /// <summary>
    /// An exception for invalid class handlers.
    /// </summary>
    [Serializable]
    public sealed class InvalidClassHandlerException : InvalidOperationException
    {
        public readonly MethodInfo Method;

        public InvalidClassHandlerException(string message, MethodInfo method)
            : base(message)
        {
            SetField.NotNull(out this.Method, nameof(method), method);
        }

        private InvalidClassHandlerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }


    /// <summary>
    /// Matches a ClassifierResult object with the best scored ClassifierClass of the LuisResult.
    /// </summary>
    public class WatsonClassifierServiceResult
    {
        public WatsonClassifierServiceResult(ClassifierResult result, ClassifierClass classifierClass)
        {
            this.Result = result;
            this.BestClass = classifierClass;
        }

        public ClassifierResult Result { get; }

        public ClassifierClass BestClass { get; }
    }

    /// <summary>
    /// A dialog specialized to handle classifier results for Watson NLC.
    /// </summary>
    [Serializable]
    public class WatsonClassifierDialog<R> : IDialog<R>
    {
        protected readonly IReadOnlyList<IClassifierService> services;

        /// <summary>   Mapping from class string to the appropriate handler. </summary>
        [NonSerialized]
        protected Dictionary<string, ClassifierClassActivityHandler> handlerByClass;

        public IClassifierService[] MakeServicesFromAttributes()
        {
            var type = this.GetType();
            var watsonClassifers = type.GetCustomAttributes<WatsonClassifierAttribute>(inherit: true);
            return watsonClassifers.Select(wc => new WatsonClassifierService(wc)).Cast<IClassifierService>().ToArray();
        }

        public WatsonClassifierDialog(params IClassifierService[] services)
        {
            if (services.Length == 0)
            {
                services = MakeServicesFromAttributes();
            }

            SetField.NotNull(out this.services, nameof(services), services);
        }
        
        public virtual async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceived);
        }

        /// <summary>
        /// Calculates the best scored <see cref="ClassifierClass" /> across some <see cref="ClassifierResult" /> objects.
        /// </summary>
        /// <param name="results">Results of multiple Classifier services calls.</param>
        /// <returns>A <see cref="WatsonClassifierServiceResult" /> with the best scored <see cref="ClassifierClass" /> and related <see cref="ClassifierResult" />,
        /// or null if no one of <paramref name="results" /> contains any classifer class.</returns>
        protected virtual WatsonClassifierServiceResult BestResultFrom(IEnumerable<ClassifierResult> results)
        {
            var winners = from result in results
                          let resultWinner = result.BestClassifierClass()
                          where resultWinner != null
                          select new WatsonClassifierServiceResult(result, resultWinner);
            return winners.MaxBy(i => i.BestClass.Confidence ?? 0d);
        }


        protected virtual async Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            if (this.handlerByClass == null)
            {
                this.handlerByClass = new Dictionary<string, ClassifierClassActivityHandler>(GetHandlersByClass());
            }

            var message = await item;
            var messageText = message.Text;
            var tasks = this.services.Select(s => s.QueryAsync(messageText)).ToArray();
            var winner = this.BestResultFrom(await Task.WhenAll(tasks));

            ClassifierClassActivityHandler handler = null;
            if (winner == null || !this.handlerByClass.TryGetValue(winner.BestClass.ClassName, out handler))
            {
                handler = this.handlerByClass[string.Empty];
            }

            if (handler != null)
            {
                await handler(context, item, winner?.Result);
            }
            else
            {
                var text = $"No default class handler found.";
                throw new Exception(text);
            }

        }

        protected virtual IDictionary<string, ClassifierClassActivityHandler> GetHandlersByClass()
        {
            return WatsonClassifierDialog.EnumerateHandlers(this).ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }

    internal static class WatsonClassifierDialog
    {
        /// <summary>
        /// Enumerate the handlers based on the attributes on the dialog instance.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <returns>An enumeration of handlers.</returns>
        public static IEnumerable<KeyValuePair<string, ClassifierClassActivityHandler>> EnumerateHandlers(object dialog)
        {
            var type = dialog.GetType();
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var method in methods)
            {
                var classes = method.GetCustomAttributes<WatsonClassifierClassAttribute>(inherit: true).ToArray();
                ClassifierClassActivityHandler classifierClassHandler = null;

                try
                {
                    classifierClassHandler = (ClassifierClassActivityHandler)Delegate.CreateDelegate(typeof(ClassifierClassActivityHandler), dialog, method, throwOnBindFailure: false);
                }
                catch (ArgumentException)
                {
                    // "Cannot bind to the target method because its signature or security transparency is not compatible with that of the delegate type."
                    // https://github.com/Microsoft/BotBuilder/issues/634
                    // https://github.com/Microsoft/BotBuilder/issues/435
                }

                // fall back for compatibility
                if (classifierClassHandler == null)
                {
                    try
                    {
                        var handler = (ClassifierClassHandler)Delegate.CreateDelegate(typeof(ClassifierClassHandler), dialog, method, throwOnBindFailure: false);

                        if (handler != null)
                        {
                            // thunk from new to old delegate type
                            classifierClassHandler = (context, message, result) => handler(context, result);
                        }
                    }
                    catch (ArgumentException)
                    {
                        // "Cannot bind to the target method because its signature or security transparency is not compatible with that of the delegate type."
                        // https://github.com/Microsoft/BotBuilder/issues/634
                        // https://github.com/Microsoft/BotBuilder/issues/435
                    }
                }

                if (classifierClassHandler != null)
                {
                    var classNames = classes.Select(i => i.ClassName).DefaultIfEmpty(method.Name);

                    foreach (var className in classNames)
                    {
                        var key = string.IsNullOrWhiteSpace(className) ? string.Empty : className;
                        yield return new KeyValuePair<string, ClassifierClassActivityHandler>(className, classifierClassHandler);
                    }
                }
                else
                {
                    if (classes.Length > 0)
                    {
                        throw new InvalidIntentHandlerException(string.Join(";", classes.Select(i => i.ClassName)), method);
                    }
                }
            }
        }
    }
}
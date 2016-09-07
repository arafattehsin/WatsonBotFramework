using Microsoft.Bot.Builder.Internals.Fibers;
using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WatsonServices.NaturalLanguageClassifier.Models;

namespace WatsonServices.NaturalLanguageClassifier
{
    /// <summary>
    /// A mockable interface for the Classifier service.
    /// </summary>
    public interface IClassifierService
    {
        /// <summary>
        /// Build the query uri for the query text.
        /// </summary>
        /// <param name="text">The query text.</param>
        /// <returns>The query uri.</returns>
        Uri BuildUri(string text);

        /// <summary>
        /// Query the Classifier service using this uri.
        /// </summary>
        /// <param name="uri">The query uri.</param>
        /// <returns>The Classifier result.</returns>
        Task<ClassifierResult> QueryAsync(Uri uri);
    }


    /// <summary>
    /// Standard implementation of IClassifierService against actual Watson NLC service.
    /// </summary>
    [Serializable]
    public class WatsonClassifierService : IClassifierService
    {

        private readonly WatsonClassifierAttribute classifier;

        public WatsonClassifierService(WatsonClassifierAttribute classifier)
        {
            SetField.NotNull(out this.classifier, nameof(classifier), classifier);
        }

        /// <summary>
        /// The base URi for accessing Watson NLC.
        /// </summary>
        public static readonly Uri UriBase = new Uri("https://gateway.watsonplatform.net/natural-language-classifier/api/v1/classifiers");

        Uri IClassifierService.BuildUri(string text)
        {
            var q = HttpUtility.UrlEncode(text);
            var builder = new UriBuilder(UriBase);

            builder.Path += $"/{classifier.ClassifierID}/classify";
            builder.Query = $"text={q}";

            return builder.Uri;
        }

        async Task<ClassifierResult> IClassifierService.QueryAsync(Uri uri)
        {
            string json;
            using (HttpClient client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{classifier.UserName}:{classifier.Password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                
                json = await client.GetStringAsync(uri);
            }

            try
            {
                var result = JsonConvert.DeserializeObject<ClassifierResult>(json);
                return result;
            }
            catch (JsonException ex)
            {
                throw new ArgumentException("Unable to deserialize the LUIS response.", ex);
            }
        }
    }

    /// <summary>
    /// Classifier Service extension methods.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Query the Classifier service using this text.
        /// </summary>
        /// <param name="service">Classifier service.</param>
        /// <param name="text">The query text.</param>
        /// <returns>The Classifier result.</returns>
        public static async Task<ClassifierResult> QueryAsync(this IClassifierService service, string text)
        {
            var uri = service.BuildUri(text);
            return await service.QueryAsync(uri);
        }
    }
}
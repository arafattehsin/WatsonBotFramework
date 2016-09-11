using Microsoft.Bot.Builder.Dialogs;

using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using WatsonServices.Conversation.Models;

using System.Linq;
using System.Reflection;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace WatsonServices.Conversation
{
    [Serializable]
    public class WatsonConversationDialog<R> : IDialog<R>
    {
        protected Context watsonConversationContext;

        protected readonly WatsonConversationClient conversationClient;
        
        public WatsonConversationDialog(WatsonConversationClient watsonConversationClient = null)
        { 
            if (watsonConversationClient == null)
            {
                watsonConversationClient = MakeClientFromAttributes();
            }

            SetField.NotNull(out conversationClient, nameof(watsonConversationClient), watsonConversationClient);
        }

        public WatsonConversationClient MakeClientFromAttributes()
        {
            var type = GetType();
            var watsonClients = type.GetCustomAttributes<WatsonConversationAttribute>(inherit: true);
            return watsonClients.Select(wc => new WatsonConversationClient(
                wc.WorkspaceID, wc.UserName, wc.Password)).FirstOrDefault();
        }

        public virtual async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var activity = await argument;

            MessageRequest userMessage = new MessageRequest
            {
                Context = watsonConversationContext,
                Input = new InputData
                {
                    Text = activity.Text
                }
            };
            
            var messageResponse = await conversationClient.SendMessageAsync(userMessage);
                
            watsonConversationContext = messageResponse.Context;

            await context.PostAsync(string.Join(" ", messageResponse.Output.Text));
            
            context.Wait(MessageReceivedAsync);
        }
    }
}
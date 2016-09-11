using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using WatsonServices.Conversation;
using System;

namespace WatsonServices
{
    [Serializable]
    [WatsonConversation("a92a0922-16e9-489f-bddb-0d75ca41f41b", "eb5521c9-4fe3-4616-8f17-f28223fc363c", "t8OnTdepeqy5")]
    public class WatsonAutoDialog : WatsonConversationDialog<object>
    {
        public override async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            // Implement cache policy here
            await base.MessageReceivedAsync(context, argument);
        }
    }
}
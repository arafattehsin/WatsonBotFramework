using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WatsonServices.Controllers
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        protected int count;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var activity = await argument;

            await context.PostAsync($"{count++}: You said {activity.Text}");
            context.Wait(MessageReceivedAsync);
        }
    }

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public virtual async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null)
            {
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:

                        await Conversation.SendAsync(activity, 
                            () => new WatsonWeatherDialog());

                        break;

                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    case ActivityTypes.Ping:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;

                }
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
    }
}
using Microsoft.Bot.Connector;

using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WatsonServices.Controllers
{
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
                        
                        await Microsoft.Bot.Builder.Dialogs.Conversation.SendAsync(
                            activity, () => new WatsonServices.WatsonAutoDialog());

                        //StateClient stateClient = activity.GetStateClient();
                        //BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                        //var context = userData.GetProperty<Context>("context");

                        //MessageRequest userMessage = new MessageRequest
                        //{
                        //    Context = context,
                        //    Input = new InputData
                        //    {
                        //        Text = activity.Text
                        //    }
                        //};

                        //var conversationClient = new WatsonConversationClient(
                        //    "eb5520c8-4fe2-4616-8f16-f27223fc363c",
                        //    "t8OnHdepIqy3");

                        //var messageResponse = await conversationClient.MessageAsync(
                        //    "a92a0922-16e9-498f-bddb-0d76ca40f41b",
                        //    "2016-07-11",
                        //    userMessage);

                        //userData.SetProperty("context", messageResponse.Context);

                        //BotData response = await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);

                        //var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                        //var replyMessage = activity.CreateReply(string.Join(" ", messageResponse.Output.Text));
                        //await connector.Conversations.ReplyToActivityAsync(replyMessage);

                        //await Conversation.SendAsync(activity, 
                        //    () => new WatsonWeatherDialog());

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
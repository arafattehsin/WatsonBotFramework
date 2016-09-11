using Newtonsoft.Json;
using System.CodeDom.Compiler;

namespace WatsonServices.Conversation.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class MessageRequest
    {

        /// <summary>The input text.</summary>
        [JsonProperty("input", Required = Required.Always)]
        public InputData Input { get; set; }

        /// <summary>Whether to return more than one intent. Set to `true` to return all matching intents</summary>
        [JsonProperty("alternate_intents", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Alternate_intents { get; set; } = false;

        /// <summary>State information about the conversation.</summary>
        [JsonProperty("context", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public Context Context { get; set; } = new Context();

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static MessageRequest FromJson(string data)
        {
            return JsonConvert.DeserializeObject<MessageRequest>(data);
        }
    }

}
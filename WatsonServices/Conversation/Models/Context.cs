using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;

namespace WatsonServices.Conversation.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class Context
    {

        /// <summary>The unique identifier of the conversation.</summary>
        [JsonProperty("conversation_id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Conversation_id { get; set; }

        /// <summary>Information about the dialog.</summary>
        [JsonProperty("system", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public SystemResponse System { get; set; } = new SystemResponse();

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Context FromJson(string data)
        {
            return JsonConvert.DeserializeObject<Context>(data);
        }
    }
}
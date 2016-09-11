using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.CodeDom.Compiler;

namespace WatsonServices.Conversation.Models
{
    /// <summary>Log message details</summary>
    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class LogMessageResponse
    {

        /// <summary>Severity of the message</summary>
        [JsonProperty("level", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogMessageResponseLevel Level { get; set; }

        /// <summary>The message</summary>
        [JsonProperty("msg", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static LogMessageResponse FromJson(string data)
        {
            return JsonConvert.DeserializeObject<LogMessageResponse>(data);
        }
    }
}
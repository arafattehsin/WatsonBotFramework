using Newtonsoft.Json;
using System.CodeDom.Compiler;

namespace WatsonServices.Conversation.Models
{

    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class Error
    {

        [JsonProperty("error", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Error1 { get; set; }

        [JsonProperty("code", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Error FromJson(string data)
        {
            return JsonConvert.DeserializeObject<Error>(data);
        }
    }

}
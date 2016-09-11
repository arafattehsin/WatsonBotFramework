using Newtonsoft.Json;
using System.CodeDom.Compiler;

namespace WatsonServices.Conversation.Models
{
    /// <summary>An array of name-confidence pairs for the user input.</summary>
    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class Intent
    {

        /// <summary>The name of the recognized intent.</summary>
        [JsonProperty("intent", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Intent1 { get; set; }

        /// <summary>A decimal percentage that represents the confidence that Watson has in this intent.</summary>
        [JsonProperty("confidence", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Confidence { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Intent FromJson(string data)
        {
            return JsonConvert.DeserializeObject<Intent>(data);
        }
    }

}
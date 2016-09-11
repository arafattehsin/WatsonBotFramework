using Newtonsoft.Json;
using System.CodeDom.Compiler;

namespace WatsonServices.Conversation.Models
{

    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class InputData
    {

        /// <summary>The user's input.</summary>
        [JsonProperty("text", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static InputData FromJson(string data)
        {
            return JsonConvert.DeserializeObject<InputData>(data);
        }
    }

}
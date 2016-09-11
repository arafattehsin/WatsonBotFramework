using Newtonsoft.Json;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace WatsonServices.Conversation.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class EntityResponse
    {
        /// <summary>The recognized entity from a term in the input.</summary>
        [JsonProperty("entity", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Entity { get; set; }

        /// <summary>Zero-based character offsets that indicate where the entity value begins and ends in the input text.</summary>
        [JsonProperty("location", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ObservableCollection<int> Location { get; set; }

        /// <summary>The term in the input text that was recognized.</summary>
        [JsonProperty("value", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static EntityResponse FromJson(string data)
        {
            return JsonConvert.DeserializeObject<EntityResponse>(data);
        }
    }
}
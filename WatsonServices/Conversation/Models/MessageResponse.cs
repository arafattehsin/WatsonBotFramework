using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatsonServices.Conversation.Models
{

    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class MessageResponse
    {

        [JsonProperty("input", Required = Required.Always)]
        public InputData Input { get; set; }

        [JsonProperty("context", Required = Required.Always)]
        [Required]
        public Context Context { get; set; } = new Context();

        /// <summary>Terms from the request that are identified as entities.</summary>
        [JsonProperty("entities", Required = Required.Always)]
        [Required]
        public ObservableCollection<EntityResponse> Entities { get; set; } = new ObservableCollection<EntityResponse>();

        /// <summary>Terms from the request that are identified as intents.</summary>
        [JsonProperty("intents", Required = Required.Always)]
        [Required]
        public ObservableCollection<Intent> Intents { get; set; } = new ObservableCollection<Intent>();

        [JsonProperty("output", Required = Required.Always)]
        [Required]
        public OutputData Output { get; set; } = new OutputData();

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static MessageResponse FromJson(string data)
        {
            return JsonConvert.DeserializeObject<MessageResponse>(data);
        }
    }
}
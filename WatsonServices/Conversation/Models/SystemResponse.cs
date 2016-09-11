using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WatsonServices.Conversation.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class SystemResponse
    {

        /// <summary>An array of dialog node IDs that are in focus in the conversation.</summary>
        [JsonProperty("dialog_stack", Required = Required.Always)]
        [Required]
        public ObservableCollection<string> Dialog_stack { get; set; } = new ObservableCollection<string>();

        /// <summary>The number of cycles of user input and response in this conversation.</summary>
        [JsonProperty("dialog_turn_counter", Required = Required.Always)]
        public int Dialog_turn_counter { get; set; }

        /// <summary>The number of inputs in this conversation. This counter might be higher than the <tt>dialog_turn_counter</tt> counter when multiple inputs are needed before a response can be returned.</summary>
        [JsonProperty("dialog_request_counter", Required = Required.Always)]
        public int Dialog_request_counter { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static SystemResponse FromJson(string data)
        {
            return JsonConvert.DeserializeObject<SystemResponse>(data);
        }
    }

}
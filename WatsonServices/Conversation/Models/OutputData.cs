using Newtonsoft.Json;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WatsonServices.Conversation.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public partial class OutputData
    {

        /// <summary>Up to 50 messages logged with the request.</summary>
        [JsonProperty("log_messages", Required = Required.Always)]
        [Required]
        public ObservableCollection<LogMessageResponse> Log_messages { get; set; } = new ObservableCollection<LogMessageResponse>();

        /// <summary>Responses to the user.</summary>
        [JsonProperty("text", Required = Required.Always)]
        [Required]
        public ObservableCollection<string> Text { get; set; } = new ObservableCollection<string>();

        /// <summary>The nodes that were executed to create the response.</summary>
        [JsonProperty("nodes_visited", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ObservableCollection<string> Nodes_visited { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static OutputData FromJson(string data)
        {
            return JsonConvert.DeserializeObject<OutputData>(data);
        }
    }

}
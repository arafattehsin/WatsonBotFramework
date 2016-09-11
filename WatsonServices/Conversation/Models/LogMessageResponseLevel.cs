using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace WatsonServices.Conversation.Models
{

    [GeneratedCode("NJsonSchema", "4.6.6093.13919")]
    public enum LogMessageResponseLevel
    {
        [EnumMember(Value = "info")]
        Info = 0,

        [EnumMember(Value = "error")]
        Error = 1,

        [EnumMember(Value = "warn")]
        Warn = 2,

    }
}
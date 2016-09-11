using Microsoft.Bot.Builder.Internals.Fibers;
using System;

namespace WatsonServices.Conversation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [Serializable]
    public class WatsonConversationAttribute : Attribute
    {
        public readonly string WorkspaceID;

        public readonly string UserName;

        public readonly string Password;

        public WatsonConversationAttribute(string workspaceID, string userName, string password)
        {
            SetField.NotNull(out WorkspaceID, nameof(workspaceID), workspaceID);
            SetField.NotNull(out UserName, nameof(userName), userName);
            SetField.NotNull(out Password, nameof(password), password);
        }    
    }
}
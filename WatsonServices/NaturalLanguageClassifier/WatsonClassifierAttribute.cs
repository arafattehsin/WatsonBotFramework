using Microsoft.Bot.Builder.Internals.Fibers;
using System;

namespace WatsonServices.NaturalLanguageClassifier
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [Serializable]
    public class WatsonClassifierAttribute : Attribute
    {
        public readonly string ClassifierID;

        public readonly string UserName;

        public readonly string Password;

        public WatsonClassifierAttribute(string classifierId, string userName, string password)
        {
            SetField.NotNull(out this.ClassifierID, nameof(classifierId), classifierId);
            SetField.NotNull(out this.UserName, nameof(userName), userName);
            SetField.NotNull(out this.Password, nameof(password), password);
        }
    }
}
using Microsoft.Bot.Builder.Dialogs.Internals;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WatsonServices.NaturalLanguageClassifier.Models
{
    public class ClassifierResult
    {
        [JsonProperty("classifier_id")]
        public string ClassifierId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("top_class")]
        public string TopClass { get; set; }

        [JsonProperty("classes")]
        public IList<ClassifierClass> Classes { get; set; }
    }

    public static class ClassifierResultEx
    {
        /// <summary>
        /// Calculates the best scored <see cref="ClassifierClass" /> from a <see cref="ClassifierResult" />.
        /// </summary>
        /// <param name="result">A result of a Classifier service call.</param>
        /// <returns>The best scored <see cref="ClassifierClass" />, or null if <paramref name="result" /> doesn't contain any intents.</returns>
        public static ClassifierClass BestClassifierClass(this ClassifierResult result)
        {
            return result.Classes.MaxBy(cc => cc.Confidence ?? 0d);
        }
        
    }
}
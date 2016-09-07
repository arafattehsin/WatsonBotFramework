using Newtonsoft.Json;

namespace WatsonServices.NaturalLanguageClassifier.Models
{
    public class ClassifierClass
    {

        [JsonProperty("class_name")]
        public string ClassName { get; set; }

        [JsonProperty("confidence")]
        public double? Confidence { get; set; }
    }


    public static class ClassifierClassEx
    {
        public static string ConfidencePercentage(this ClassifierClass classifierClass)
        {
            return string.Format("{0:0%}", classifierClass.Confidence);            
        }
    }
}
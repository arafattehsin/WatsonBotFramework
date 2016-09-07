using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;
using WatsonServices.NaturalLanguageClassifier;
using WatsonServices.NaturalLanguageClassifier.Models;

namespace WatsonServices
{
    /// <summary>
    /// This dialog reproduces the sample at http://natural-language-classifier-demo.mybluemix.net/
    /// </summary>
    [WatsonClassifier("33ffe6x85-nlc-5021", "86f5aafa-1200-4abf-b86b-e7dee83c3474", "z1nii3HalDR4")]
    [Serializable]
    public class WatsonWeatherDialog : WatsonClassifierDialog<object>
    {
        [WatsonClassifierClass("conditions")]
        public async Task HandleConditionsRequest(IDialogContext context, ClassifierResult result)
        {
            var classification = result.BestClassifierClass().ClassName;
            var confidence = result.BestClassifierClass().ConfidencePercentage();

            await context.PostAsync($"Natural Language Classifier is {confidence} confident that the question submitted is talking about '{classification}'.");

            context.Wait(MessageReceived);
        }


        [WatsonClassifierClass("temperature")]
        public async Task HandleTemperatureRequest(IDialogContext context, ClassifierResult result)
        {
            var classification = result.BestClassifierClass().ClassName;
            var confidence = result.BestClassifierClass().ConfidencePercentage();

            await context.PostAsync($"Natural Language Classifier is {confidence} confident that the question submitted is talking about '{classification}'.");
            
            context.Wait(MessageReceived);
        }
        
        // In this particular example you could use a single handler method and use multiple WatsonClassifierClass attribute to avoid code duplication:
        //// [WatsonClassifierClass("conditions")]
        //// [WatsonClassifierClass("temperature")]
        //// public async Task HandleWeatherRequests(IDialogContext context, ClassifierResult result)
        //// {
        //    ...
    }
}
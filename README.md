# Watson Services for Microsoft Bot Framework

This project includes a dialog specialized to handle results from [Watson Natural Language Classifier](http://www.ibm.com/watson/developercloud/nl-classifier.html).

### Example usage
```csharp
[WatsonClassifier("33ffe6x85-nlc-3949", "86f5aafa-1200-4abf-b86b-e7dee83c3474", "z1nii3HalDR4")]
[Serializable]
public class WatsonWeatherDialog : WatsonClassifierDialog<object>
{
    [WatsonClassifierClass("conditions")]
    public async Task HandleConditionsRequest(IDialogContext context, ClassifierResult result)
    {
    }


    [WatsonClassifierClass("temperature")]
    public async Task HandleTemperatureRequest(IDialogContext context, ClassifierResult result)
    {
    }
}
```    
   

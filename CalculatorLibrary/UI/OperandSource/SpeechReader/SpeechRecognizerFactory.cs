using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;

namespace CalculatorLibrary.UI.OperandSource.SpeechReader;

public class SpeechRecognizerFactory(IOptions<SpeechRecognizerFactoryOptions> options)
{
    public SpeechRecognizer Create()
    {
        var config = SpeechConfig.FromSubscription(options.Value.SubscriptionKey, options.Value.Region);
        return new SpeechRecognizer(config);
    }
}
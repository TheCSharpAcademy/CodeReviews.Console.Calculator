namespace CalculatorLibrary.UI.OperandSource.SpeechReader;

public class SpeechRecognizerFactoryOptions
{
    public const string Key = "SpeechRecognizer";

    public string SubscriptionKey { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;
}
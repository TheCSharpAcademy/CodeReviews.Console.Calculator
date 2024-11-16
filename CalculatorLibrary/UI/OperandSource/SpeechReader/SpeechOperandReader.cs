using Microsoft.CognitiveServices.Speech;

namespace CalculatorLibrary.UI.OperandSource.SpeechReader;

public class SpeechOperandReader(SpeechRecognizer recognizer) : IOperandReader
{
    public double ReadOperand()
    {
        do
        {
            var result = recognizer.RecognizeOnceAsync().Result;
            if (result.Reason == ResultReason.Canceled)
            {
                Console.WriteLine($"CANCELED: Reason={result.Reason}");
                continue;
            }

            if (result.Reason != ResultReason.RecognizedSpeech)
            {
                Console.WriteLine("Speech could not be recognized.");
                continue;
            }

            if (!double.TryParse(result.Text.Substring(0, result.Text.Length - 1), out var answer))
            {
                Console.WriteLine($"Answer must be a double but got [{result.Text}]");
                continue;
            }

            return answer;
        } while (true);
    }
}
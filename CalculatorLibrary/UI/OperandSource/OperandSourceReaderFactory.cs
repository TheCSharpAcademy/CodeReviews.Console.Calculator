using CalculatorLibrary.ConsoleWrapper;
using CalculatorLibrary.Logic;
using CalculatorLibrary.UI.OperandSource.ConsoleReader;
using CalculatorLibrary.UI.OperandSource.HistoryReader;
using CalculatorLibrary.UI.OperandSource.SpeechReader;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorLibrary.UI.OperandSource;

public class OperandSourceReaderFactory(IServiceProvider serviceProvider)
{
    public IOperandReader Create(OperandSources source, Operations operations)
    {
        return source switch
        {
            OperandSources.Console => serviceProvider.GetService<ConsoleOperandReader>()!,
            OperandSources.History => new HistoryOperandReader(operations,
                serviceProvider.GetService<IConsoleWrapper>()!),
            OperandSources.Speech => new SpeechOperandReader(serviceProvider.GetService<SpeechRecognizerFactory>()!
                .Create()),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
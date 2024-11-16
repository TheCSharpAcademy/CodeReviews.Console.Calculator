using CalculatorLibrary.UI.OperandSource.ConsoleReader;
using CalculatorLibrary.UI.OperandSource.SpeechReader;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorLibrary.UI.OperandSource;

public static class OperandSourceReaderFactoryExtension
{
    public static void AddOperandSourceReaderFactory(this IServiceCollection services)
    {
        services.AddSingleton<SpeechRecognizerFactory>();
        services.AddSingleton<ConsoleOperandReader>();
        services.AddSingleton<OperandSourceReaderFactory>();
    }
}
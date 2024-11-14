using CalculatorLibrary.ConsoleWrapper;
using CalculatorLibrary.UI;
using CalculatorLibrary.UI.ChoiceReader;
using CalculatorLibrary.UI.Menu;
using CalculatorLibrary.UI.OperandSource;
using CalculatorLibrary.UI.OperandSource.SpeechReader;
using CalculatorLibrary.UI.Operation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(configurationBuilder =>
    {
        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>();
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<SpeechRecognizerFactoryOptions>(
            context.Configuration.GetSection(SpeechRecognizerFactoryOptions.Key));
        services.AddSingleton<MainMenu>();
        services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        services.AddSingleton<IChoiceReader, ConsoleChoiceReader>();
        services.AddSingleton<OperandSourceSelection>();
        services.AddSingleton<IKeyAwaiter, ConsoleKeyAwaiter>();
        services.AddSingleton<OperationSelection>();
        services.AddOperandSourceReaderFactory();
    });
var host = builder.Build();

var calculator = ActivatorUtilities.CreateInstance<Calculator.Application.Calculator>(host.Services);
calculator.Run();
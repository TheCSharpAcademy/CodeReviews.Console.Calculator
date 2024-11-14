using System.Text;
using CalculatorLibrary.ConsoleWrapper;
using CalculatorLibrary.UI;
using CalculatorLibrary.UI.OperandSource.ConsoleReader;
using FluentAssertions;
using NSubstitute;

namespace TestCalculator.UI.OperandSource.ConsoleReader;

public class ConsoleOperandReaderTests
{
    [Test]
    public void WillReturnDoubleWhenProvidedWithCorrectNumericString()
    {
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadLine().Returns("45.5");

        var reader = new ConsoleOperandReader(consoleWrapper, Substitute.For<IKeyAwaiter>());
        reader.ReadOperand().Should().Be(45.5);
    }

    [Test]
    public void WillKeepAskingForInputUntilValidProvided()
    {
        var consoleOutputWriter = new StringWriter(new StringBuilder());
        Console.SetOut(consoleOutputWriter);

        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadLine().Returns("a", "ab", "22.a", "45.5");
        var keyAwaiter = Substitute.For<IKeyAwaiter>();
        keyAwaiter.When(x => x.Wait()).Do(x => { });

        var reader = new ConsoleOperandReader(consoleWrapper, keyAwaiter);
        reader.ReadOperand().Should().Be(45.5);
    }
}
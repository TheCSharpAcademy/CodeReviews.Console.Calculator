using CalculatorLibrary.ConsoleWrapper;
using CalculatorLibrary.UI.ChoiceReader;
using CalculatorLibrary.UI.Menu;
using FluentAssertions;
using NSubstitute;

namespace TestCalculator.UI.ChoiceReader;

public class ConsoleChoiceReaderTests
{
    [Test]
    public void WillReturnCorrectChoice()
    {
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadKey(true).Returns(new ConsoleKeyInfo('c', ConsoleKey.None, false, false, false));
        var reader = new ConsoleChoiceReader(consoleWrapper);

        var choice = reader.GetChoice<MenuChoices>();
        choice.Should().Be(MenuChoices.StartNewCalculation);
    }
}
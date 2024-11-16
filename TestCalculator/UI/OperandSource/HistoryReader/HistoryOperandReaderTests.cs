using System.Text;
using CalculatorLibrary.ConsoleWrapper;
using CalculatorLibrary.Logic;
using CalculatorLibrary.UI.OperandSource.HistoryReader;
using FluentAssertions;
using NSubstitute;

namespace TestCalculator.UI.OperandSource.HistoryReader;

public class HistoryOperandReaderTests
{
    [Test]
    public void WillSelectResultBasedOnIndex()
    {
        var consoleOutputWriter = new StringWriter(new StringBuilder());
        Console.SetOut(consoleOutputWriter);
        var operations = new Operations
        {
            new OperationDetails(10, OperationType.Addition, 20, 10),
            new OperationDetails(10, OperationType.Addition, 30, 20),
            new OperationDetails(20, OperationType.Addition, 40, 20)
        };
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadLine().Returns("2");

        var reader = new HistoryOperandReader(operations, consoleWrapper);
        reader.ReadOperand().Should().Be(30);
    }
}
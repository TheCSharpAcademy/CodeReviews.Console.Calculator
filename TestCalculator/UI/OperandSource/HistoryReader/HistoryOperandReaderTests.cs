using System.Text;
using CalculatorLibrary.ConsoleWrapper;
using CalculatorLibrary.Logic;
using CalculatorLibrary.UI.OperandSource.HistoryReader;
using FluentAssertions;
using NSubstitute;
using LogicOperation = CalculatorLibrary.Logic.Operation;

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
            new LogicOperation(10, OperationType.Addition, 20, 10),
            new LogicOperation(10, OperationType.Addition, 30, 20),
            new LogicOperation(20, OperationType.Addition, 40, 20)
        };
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadLine().Returns("2");

        var reader = new HistoryOperandReader(operations, consoleWrapper);
        reader.ReadOperand().Should().Be(30);
    }
}
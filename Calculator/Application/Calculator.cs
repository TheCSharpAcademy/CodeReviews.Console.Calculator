using CalculatorLibrary.Logic;
using CalculatorLibrary.UI;
using CalculatorLibrary.UI.ChoiceReader;
using CalculatorLibrary.UI.Menu;
using CalculatorLibrary.UI.OperandSource;
using CalculatorLibrary.UI.Operation;

namespace Calculator.Application;

public class Calculator(
    MainMenu mainMenu,
    IChoiceReader choiceReader,
    OperandSourceSelection operandSourceSelection,
    IKeyAwaiter keyAwaiter,
    OperationSelection operationSelection,
    OperandSourceReaderFactory operandSourceReaderFactory)
{
    private Operations _performedOperations = new();

    public void Run()
    {
        var shouldQuit = false;
        do
        {
            Console.Clear();
            Console.WriteLine($"You used the calculator {_performedOperations.Count} times.");
            MenuRenderer.Render(mainMenu);
            var menuChoice = choiceReader.GetChoice<MenuChoices>();
            switch (menuChoice)
            {
                case MenuChoices.Quit:
                    shouldQuit = true;
                    break;
                case MenuChoices.StartNewCalculation:
                    RunCalculation();
                    break;
                case MenuChoices.ClearHistory:
                    ClearHistory();
                    break;
            }
        } while (!shouldQuit);

        Console.WriteLine("Thank you for using the Calculator!");
    }

    private void RunCalculation()
    {
        Console.Clear();

        OperandSources operandSourceChoice;
        OperandSourceSelectionRenderer.Render(operandSourceSelection);
        do
        {
            operandSourceChoice = choiceReader.GetChoice<OperandSources>();
        } while (operandSourceChoice == OperandSources.History && _performedOperations.Count == 0);

        var leftOperandReader = operandSourceReaderFactory.Create(operandSourceChoice, _performedOperations);

        Console.WriteLine("Enter operand:");
        var leftOperand = leftOperandReader.ReadOperand();
        double rightOperand = 0;

        OperationSelectionRenderer.Render(operationSelection);

        var operationChoice = choiceReader.GetChoice<OperationChoice>();
        var operationType = OperationUiToLogicMapper.Map(operationChoice);

        double result;
        if (operationType.RequiresTwoOperands())
        {
            OperandSources secondOperandSourceChoice;
            OperandSourceSelectionRenderer.Render(operandSourceSelection);
            do
            {
                secondOperandSourceChoice = choiceReader.GetChoice<OperandSources>();
            } while (secondOperandSourceChoice == OperandSources.History && _performedOperations.Count == 0);

            var rightOperandReader = operandSourceReaderFactory.Create(secondOperandSourceChoice, _performedOperations);

            if (operationType == OperationType.Division)
            {
                Console.WriteLine("Enter operand other than 0:");
                do
                {
                    rightOperand = rightOperandReader.ReadOperand();
                } while (rightOperand == 0);
            }
            else
            {
                Console.WriteLine("Enter operand:");
                rightOperand = rightOperandReader.ReadOperand();
            }

            result = OperationExecutor.Perform(operationType, leftOperand, rightOperand);
        }
        else
        {
            result = OperationExecutor.Perform(operationType, leftOperand);
        }

        if (operationType.RequiresTwoOperands())
        {
            if (double.IsNaN(result))
            {
                Console.WriteLine("{0:0.##} {1} {2:0.##} - This operation will result in an error");
            }
            else
            {
                _performedOperations.Add(new Operation(leftOperand, operationType, result, rightOperand));
                Console.WriteLine("Your result: {0:0.##} {1} {2:0.##} = {3:0.##}", leftOperand,
                    OperationTypeToPresentationMapper.Map(operationType), rightOperand, result);
            }
        }
        else
        {
            if (double.IsNaN(result))
            {
                Console.WriteLine("{1} {0:0.##} - This operation will result in an error");
            }
            else
            {
                _performedOperations.Add(new Operation(leftOperand, operationType, result));
                Console.WriteLine("Your result: {1} {0:0.##} = {2:0.##}", leftOperand,
                    OperationTypeToPresentationMapper.Map(operationType), result);
            }
        }

        keyAwaiter.Wait();
    }

    private void ClearHistory()
    {
        Console.Clear();
        _performedOperations.Clear();
        Console.WriteLine("History cleared. Press any key to continue...");
        keyAwaiter.Wait();
    }
}
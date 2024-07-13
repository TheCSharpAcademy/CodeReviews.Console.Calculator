using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;
class Program
{
  //ask what kind of operation they want to do -> ask if they want to use existing -> either perform a calculation that requires 1 or 2 inputs
  static void Main(string[] args)
  {
    bool endApp = false;
    int counter = 0;
    Calculation? chosenCalculation = null;
    Console.WriteLine("Console Calculator in C#\r");
    Console.WriteLine("------------------------\n");

    Calculator calculator = new();
    while (!endApp)
    {
      double cleanNum1;
      double result;
      string op = GetUserOperation();
      if (!IsValidOperationChoice(op))
      {
        Console.WriteLine("Error: Unrecognized input.");
      }
      else
      {
        bool viewCalculations = CalculationsList.ListPrompt();
        //User wants to use existing operation
        if (viewCalculations)
        {
          chosenCalculation = CalculationsList.ListMenuOptions();
          if (chosenCalculation != null)
          {
            Console.WriteLine($"Selected Calculation: {chosenCalculation.NumberOne} {chosenCalculation.Operator} {chosenCalculation.NumberTwo} = {chosenCalculation.Answer}");
            cleanNum1 = chosenCalculation.Answer;
          }
          else
          {
            cleanNum1 = GetUserOperand("Type a number, then press enter: ");
          }
        }
        else
        {
          cleanNum1 = GetUserOperand("Type a number, then press enter: ");
        }

        try
        {
          //operation requires only one input
          if (RequiresOneOperand(op))
          {
            result = calculator.DoOperation(cleanNum1, op);
            CalculationsList.AddCalculationToList(cleanNum1, result, op);
          }
          else
          {
            double cleanNum2 = GetUserOperand("Type another number, then press enter: ");
            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
            CalculationsList.AddCalculationToList(cleanNum1, cleanNum2, result, op);
          }
          if (double.IsNaN(result))
          {
            Console.WriteLine("This operation will result in a mathematical error.\n");
          }
          else
          {
            counter++;
            Console.WriteLine("Your result: {0:0.##}\n", result);
            Console.WriteLine($"Calculator has been used {counter} times this session");
          }
        }
        catch (Exception e)
        {
          Console.WriteLine("Oh no! An exception occurred trying to do the math. \n - Details: " + e.Message);
        }
        Console.WriteLine("-----------------------\n");

        Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
        if (Console.ReadLine() == "n") endApp = true;

        Console.WriteLine("\n");
      }
    }
    calculator.Finish();
    return;
  }

  public static bool IsValidOperationChoice(string op)
  {
    if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|t|e|sq|sin|c|tan)$"))
    {
      return false;
    }
    return true;
  }

  public static bool RequiresOneOperand(string op)
  {
    return Regex.IsMatch(op, "^(sq|sin|c|tan|t)");
  }

  public static string GetUserOperation()
  {
    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.WriteLine("\tt - Ten x");
    Console.WriteLine("\te - Exponent");
    Console.WriteLine("\tsq - Square Root");
    Console.WriteLine("\tsin - Sine");
    Console.WriteLine("\tc - Cosign");
    Console.WriteLine("\ttan - Tangent");
    Console.Write("Your option? ");

    return Console.ReadLine() ?? string.Empty;
  }

  public static double GetUserOperand(string prompt)
  {
    Console.WriteLine(prompt);
    string? input = Console.ReadLine();
    double operand;
    while (!double.TryParse(input, out operand))
    {
      Console.WriteLine("This is not valid input, Please enter a numeric value: ");
      input = Console.ReadLine();
    }
    return operand;
  }
}
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
  static void Main(string[] args)
  {
    bool endApp = false;
    double lastResult = double.NaN;
    // Display title as the C# console calculator app.
    Console.WriteLine("Console Calculator in C#\r");
    Console.WriteLine("------------------------\n");

    Calculator calculator = new Calculator();
    while (!endApp)
    {
      string? numInput1 = "";
      string? numInput2 = "";
      double result = 0;
      if (!double.IsNaN(lastResult))
      {
        Console.WriteLine("Do you want to use the last result? Y/N");
        string? useLastResult = Console.ReadLine();

        if (useLastResult?.ToLower() == "y")
        {
          numInput1 = lastResult.ToString();
        }
        else
        {
          Console.Write("Type a number, and then press Enter: ");
          numInput1 = Console.ReadLine();
        }
      }
      else
      {
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();
      }

      double cleanNum1 = 0;
      while (!double.TryParse(numInput1, out cleanNum1))
      {
        Console.Write("This is not valid input. Please enter a numeric value: ");
        numInput1 = Console.ReadLine();
      }

      Console.Write("Type a number, and then press Enter: ");
      numInput2 = Console.ReadLine();

      double cleanNum2 = 0;

      while (!double.TryParse(numInput2, out cleanNum2))
      {
        Console.Write("This is not valid input. Please enter a numeric value: ");
        numInput2 = Console.ReadLine();
      }

      Console.WriteLine("Choose an operator from the following list:");
      Console.WriteLine("\ta - Add");
      Console.WriteLine("\ts - Subtract");
      Console.WriteLine("\tm - Multiply");
      Console.WriteLine("\td - Divide");
      Console.WriteLine("\tp - Power");
      Console.Write("Your option? ");

      string? op = Console.ReadLine();

      if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p]"))
      {
        Console.WriteLine("Wrong input");
      }
      else
      {
        try
        {
          result = calculator.DoOperation(cleanNum1, cleanNum2, op);
          if (double.IsNaN(result))
          {
            Console.WriteLine("This operation will result in a mathematical error.\n");
          }
          else
          {
            lastResult = result;
            Console.WriteLine($"Your result: {result}");
          }
        }
        catch (Exception e)
        {
          Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
      }

      Console.WriteLine("------------------------\n");

      Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
      if (Console.ReadLine() == "n") endApp = true;

      Console.WriteLine("\n");

    }
    calculator.Finish();
    return;

  }
}
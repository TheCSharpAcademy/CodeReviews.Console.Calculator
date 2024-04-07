using CalculatorLibrary;
using CalculatorLibrary.enums;
using System.Text.RegularExpressions;

namespace CalculatorApp.BBualdo;

internal class ProgramEngine
{
  public Calculator CurrentCalculator { get; set; } = new Calculator();
  public void MainMenu()
  {
    Console.WriteLine("\nWhich operation you want to execute?");
    Console.WriteLine(@"
      a - add
      s - subtract
      m - multiply
      d - divide
      sq - square
      p - power
");

    string? userInput = Console.ReadLine();

    while (userInput == null || !Regex.IsMatch(userInput, "[a|s|m|d|sq|p]"))
    {
      Console.WriteLine("Please enter proper letter.");
      userInput = Console.ReadLine();
    }

    switch (userInput)
    {
      case "a":
        CurrentCalculator.Operation = Operations.Add;
        break;
      case "s":
        CurrentCalculator.Operation = Operations.Subtract;
        break;
      case "m":
        CurrentCalculator.Operation = Operations.Multiply;
        break;
      case "d":
        CurrentCalculator.Operation = Operations.Divide;
        break;
      case "sq":
        CurrentCalculator.Operation = Operations.Square;
        break;
      case "p":
        CurrentCalculator.Operation = Operations.Power;
        break;
      default:
        break;
    }
  }

  public double DoCalculation()
  {
    double num1; double num2;

    if (CurrentCalculator.Operation == Operations.Square)
    {
      Console.WriteLine("Enter a number to square:");
      string? sqUserInput = Console.ReadLine();

      while (!double.TryParse(sqUserInput, out num1))
      {
        Console.WriteLine("Please enter valid number.");
        sqUserInput = Console.ReadLine();
      }

      return CurrentCalculator.Square(num1);
    }

    if (CurrentCalculator.Operation == Operations.Power)
    {
      Console.WriteLine("Enter a base number to power:");
      string? powUserInput1 = Console.ReadLine();

      while (!double.TryParse(powUserInput1, out num1))
      {
        Console.WriteLine("Please enter valid number.");
        powUserInput1 = Console.ReadLine();
      }

      Console.WriteLine("\nEnter a power:");
      string? powUserInput2 = Console.ReadLine();

      while (!double.TryParse(powUserInput2, out num2))
      {
        Console.WriteLine("Please enter valid number.");
        powUserInput2 = Console.ReadLine();
      }

      return CurrentCalculator.Power(num1, num2);
    }

    Console.WriteLine("Please enter first number:");
    string? userInput1 = Console.ReadLine();

    while (!double.TryParse(userInput1, out num1))
    {
      Console.WriteLine("Please enter valid number.");
    }

    Console.WriteLine("Please enter second number:");
    string? userInput2 = Console.ReadLine();

    while (!double.TryParse(userInput2, out num2))
    {
      Console.WriteLine("Please enter valid number.");
    }

    return CurrentCalculator.Calculate(num1, num2);
  }
}
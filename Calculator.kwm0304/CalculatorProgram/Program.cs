﻿using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;
class Program
{

  static void Main(string[] args)
  {
    bool endApp = false;
    int counter = 0;

    Console.WriteLine("Console Calculator in C#\r");
    Console.WriteLine("------------------------\n");

    Calculator calculator = new();
    while (!endApp)
    {
      string? numInput1 = "";
      string? numInput2 = "";
      double result = 0;
      Calculation? chosenCalculation = null;
      bool viewCalculations = CalculationsList.ListPrompt();
      if (viewCalculations)
      {
        chosenCalculation = CalculationsList.ListMenuOptions();
        if (chosenCalculation != null)
        {
          Console.WriteLine($"Selected Calculation: {chosenCalculation.NumberOne} {chosenCalculation.Operator} {chosenCalculation.NumberTwo} = {chosenCalculation.Answer}");
        }
      }


      double cleanNum1;
      if (chosenCalculation != null)
      {
        cleanNum1 = chosenCalculation.Answer;
      }
      else
      {
        Console.Write("Type a number, and then press enter: ");
        numInput1 = Console.ReadLine();
        cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
          Console.WriteLine("This is not valid input, Please enter a numeric value: ");
          numInput1 = Console.ReadLine();
        }
      }
      Console.Write("Type another number, and then press enter: ");
      numInput2 = Console.ReadLine();

      double cleanNum2 = 0;
      while (!double.TryParse(numInput2, out cleanNum2))
      {
        Console.WriteLine("This is not valid input, Please enter a numeric value: ");
        numInput2 = Console.ReadLine();
      }

      Console.WriteLine("Choose an operator from the following list:");
      Console.WriteLine("\ta - Add");
      Console.WriteLine("\ts - Subtract");
      Console.WriteLine("\tm - Multiply");
      Console.WriteLine("\td - Divide");
      Console.WriteLine("\tt - Ten x");
      Console.WriteLine("\te - Exponent");
      Console.Write("Your option? ");

      string? op = Console.ReadLine();

      if (op == null || !Regex.IsMatch(op, "[a|s|m|d|t|e]"))
      {
        Console.WriteLine("Error: Unrecognized input.");
      }
      else
      {
        try
        {
          result = calculator.DoOperation(cleanNum1, cleanNum2, op);
          CalculationsList.AddCalculationToList(cleanNum1, cleanNum2, result, op);
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
      }
      Console.WriteLine("-----------------------\n");

      Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
      if (Console.ReadLine() == "n") endApp = true;

      Console.WriteLine("\n");
    }
    calculator.Finish();
    return;
  }

}
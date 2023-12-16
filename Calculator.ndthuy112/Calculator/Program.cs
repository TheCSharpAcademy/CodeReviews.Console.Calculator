﻿using CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        var calculatorInstance = new CalculatorEngine();
        List<CalculationModel> calculationHistory = new List<CalculationModel>();
        int timesUsed = 0;

        while (!endApp)
        {
            timesUsed += 1;

            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            CalculationModel calculationResult;

            // Ask the user to type the first number.
            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a decimal value: ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a decimal value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string op = Console.ReadLine();

            try
            {
                
                calculationResult = calculatorInstance.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(calculationResult.Answer))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", calculationResult.Answer);
                calculationHistory.Add(calculationResult);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;


            Console.WriteLine("\n"); // Friendly linespacing.
        }
        Console.WriteLine($"Calculations made: {timesUsed}");
        foreach (var calculationResult in calculationHistory)
        {
            Console.WriteLine($"First operand: {calculationResult.FirstOperand}\tSecond operand: {calculationResult.SecondOperand}\tCalculation type: {calculationResult.Type}\tAnswer: {calculationResult.Answer}");
        }
        Console.ReadLine();
        calculatorInstance.Finish();
        return;
    }
}
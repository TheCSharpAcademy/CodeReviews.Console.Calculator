using System.Text.RegularExpressions;
using CalculatorLibrary;
using CalculatorLibrary.Models;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();

        Calculation? chosenCalc = null;

        int counter = 0;

        while (!endApp)
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;
            string userDecision;

            Console.WriteLine("Do you wish to make a calculation or view the calculator history?");
            Console.WriteLine(
                "c - Calculation\n" +
                "h - History");
            userDecision = Console.ReadLine();
            while (userDecision == null || !Regex.IsMatch(userDecision, "[c|h]"))
            {
                Console.Write("Please enter a valid input");
                userDecision = Console.ReadLine();
            }

            if (userDecision == "c")
            {
                double cleanNum1 = 0;
                if (chosenCalc == null)
                {
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                }
                else
                {
                    numInput1 = chosenCalc.Result;
                    double.TryParse(numInput1, out cleanNum1);
                    Console.Write($"The first number will be set to {numInput1}, press enter to continue");
                    Console.ReadLine();
                }

                    Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tr - Root");
                Console.WriteLine("\tp - Power");
                Console.WriteLine("\th - Show History");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
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
                            Console.WriteLine("Your result: {0:0.##}", result);
                            counter += 1;
                            chosenCalc = null;
                            Console.WriteLine($"The calculator was used {counter} times.\n");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");
            }
            else
            {
                chosenCalc = calculator.ShowHistory();
            }
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); 
        }
        calculator.Finish();
        return;
    }
}
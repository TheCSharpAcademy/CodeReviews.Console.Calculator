using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        CalculatorEngine calculatorEngine = new CalculatorEngine();

        Console.WriteLine("Welcome to Console Calculator in C#!\r");
        Console.WriteLine($"{new string('-', Console.BufferWidth)}\n");

        bool programRun = true;
        while (programRun)
        {
            double firstNumber = 0;
            double secondNumber = 0;

            HelperMethods.AskForNumber("first");
            HelperMethods.ReadNumericInput(ref firstNumber, "first");

            HelperMethods.AskForNumber("second");
            HelperMethods.ReadNumericInput(ref secondNumber, "second");
            Console.WriteLine($"{new string('-', Console.BufferWidth)}");

            Console.WriteLine("\nChoose one of the options below: ");
            Console.WriteLine($"\tA - Add");
            Console.WriteLine($"\tS - Subtract");
            Console.WriteLine($"\tM - Multiply");
            Console.WriteLine($"\tD - Divide\n");
            Console.WriteLine($"{new string('-', Console.BufferWidth)}\n");
            Console.Write($"Please select your option: ");

            string? userOption = null;
            

            HelperMethods.ReadMatchingInput(ref userOption, @"^[asmd]$");
            try
            {
                double result = calculatorEngine.CalculateResult(ref firstNumber, ref secondNumber, userOption);

                if (double.IsNaN(result))
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                else
                    Console.WriteLine("Your result: {0:0.##} {1} {2:0.##} = {3:0.##}\n", firstNumber, CalculatorEngine.GetMathematicalSign(userOption), secondNumber, result);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\nOh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine($"{new string('-', Console.BufferWidth)}");
            Console.Write("Press 'E' and Enter to close the app, or press any other key and Enter to continue: ");

            userOption = Console.ReadLine();
            if (userOption != null)
            {
                if (userOption.ToLower() == "e")
                {
                    programRun = false;
                }
            }
            Console.Clear();
        }
        calculatorEngine.Deconstructor();
        Console.Write("Thank you for using this application! Press any key to exit: ");
        Console.ReadKey();
    }
}
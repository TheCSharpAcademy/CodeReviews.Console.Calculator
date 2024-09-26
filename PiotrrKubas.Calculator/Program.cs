using CalculatorLibrary;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                Console.Clear();
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\t+ - Add");
                Console.WriteLine("\t- - Subtract");
                Console.WriteLine("\t* - Multiply");
                Console.WriteLine("\t/ - Divide");
                Console.WriteLine("\th - Calculation history");
                Console.Write("Your option? ");

                string? option = Console.ReadLine();
                double result = 0;

                
                while (option == null)
                {
                    Console.WriteLine();
                }
                if (Regex.IsMatch(option, "[h]"))
                {
                    calculator.AdditionalOptions(option);
                }
                else if (!Regex.IsMatch(option, "[+|/|*|-]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        var numbers = Input();
                        result = calculator.DoOperation(numbers.cleanNum1, numbers.cleanNum2, option);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
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

            (double cleanNum1, double cleanNum2) Input()
            {
                string? numInput1 = "";
                string? numInput2 = "";

                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }
                return (cleanNum1, cleanNum2);
            }


        }
    }
}

using CalculatorLibrary;
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
                Console.WriteLine("\ts - Square root");
                Console.WriteLine("\tp - Power of");
                Console.WriteLine("\tl - Logarithm");
                Console.WriteLine("\th - Calculation history");
                Console.Write("Your option? ");

                string? option = Console.ReadLine();
                double result = 0;

                while (option == null)
                {
                    Console.WriteLine("Error, try again!");
                    option = Console.ReadLine();
                }
                if (Regex.IsMatch(option, "[h]"))
                {
                    calculator.OperationHistory();
                }
                else if (Regex.IsMatch(option, "[s|p|l]"))
                {
                    var numbers = Input(option);
                    result = calculator.AdvancedOperations(numbers.cleanNum1, numbers.cleanNum2, option);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                else if (Regex.IsMatch(option, "[+|/|*|-]"))
                {
                    var numbers = Input(option);
                    result = calculator.BasicOperations(numbers.cleanNum1, numbers.cleanNum2, option);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                else
                {
                    try
                    {
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
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

            (double cleanNum1, double cleanNum2) Input(string option)
            {
                string? numInput1 = "";
                string? numInput2 = "";
                double cleanNum1 = double.NaN;
                double cleanNum2 = double.NaN;

                switch (option)
                {
                    case "s":
                        Console.WriteLine("Enter a number that you want to be square rooted, and press Enter");
                        numInput1 = Console.ReadLine();

                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }
                        break;
                    case "p":
                        Console.WriteLine("Enter base number, and press Enter");
                        numInput1 = Console.ReadLine();
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }

                        Console.WriteLine("Enter exponent of power, and press Enter");
                        numInput2 = Console.ReadLine();
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }
                        break;
                    case "l":
                        Console.WriteLine("Enter a logarithm argument, and press Enter");
                        numInput1 = Console.ReadLine();
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }

                        Console.WriteLine("Enter a logarithm base, and press Enter");
                        numInput2 = Console.ReadLine();
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }
                        break;
                    default:
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }

                        Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }
                        break;
                }
                return (cleanNum1, cleanNum2);
            }
        }
    }
}
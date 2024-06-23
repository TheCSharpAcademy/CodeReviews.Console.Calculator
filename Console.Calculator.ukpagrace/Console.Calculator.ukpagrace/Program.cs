using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static Calculator calculator = new();
        static string usersInput = "";
        static void Main(string[] args)
        {
            bool endApp = false;
            Console.WriteLine("Console Calculator in C# \r");
            Console.WriteLine("-------------------------\n");

            while (!endApp)
            {
                ShowMenu();
                usersInput = Console.ReadLine();
                Console.Clear();
                if (usersInput == "s")
                {
                    HandleUsersHistory();
                } else {
                    PerformNewCalculation();
                }
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue\n");
                if (Console.ReadLine() == "n") endApp = true;
                Console.Clear();
                Console.WriteLine("\n");
            }
            return;
        }

        public static void ShowMenu()
        {
            Console.WriteLine("s - See History \r");
            Console.WriteLine("or any other key to perform calculations \r");
        }

        public static void HandleUsersHistory()
        {
            calculator.SeeAllCalculations();
            if (calculator.IsUsersCalculationsEmpty())
            {
                Console.WriteLine("No calculations has been performed");
            }
            else
            {
                Console.WriteLine("choose an option \r");
                Console.WriteLine("d - delete calculations \r");
                Console.WriteLine("p - perform calculations \r");

                usersInput = Console.ReadLine();
                Console.Clear();

                if (usersInput == "d")
                {
                    DeleteFromHistory();
                }
                else if (usersInput == "p")
                {
                    PerformCalculationsFromHistory();
                }
            }
        }

        public static void DeleteFromHistory()
        {
            Console.WriteLine("select an index from the list to delete a calculation");
            usersInput = Console.ReadLine();

            int index = 0;
            while (!int.TryParse(usersInput, out index))
            {
                Console.WriteLine("Enter a numerical value");
                usersInput = Console.ReadLine();
            }
            calculator.DeleteCalculation(index);
            calculator.SeeAllCalculations();
        }

        public static void ChooseArithmeticType()
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tb - Basic Arithmetic");
            Console.WriteLine("\ta - Advanced Arithmetic");
        }

        public static void PerformCalculationsFromHistory()
        {
            Console.WriteLine("select a result for the calculation");
            usersInput = Console.ReadLine();

            int firstIndex = 0;
            while (!int.TryParse(usersInput, out firstIndex) || !calculator.IsindexWithRange(firstIndex))
            {
                Console.WriteLine("Enter a numerical value or the index shown in the option\n");
                usersInput = Console.ReadLine();
            }

            double firstResult = calculator.GetUsersCalculationResult(firstIndex);
            ChooseArithmeticType();
            usersInput = Console.ReadLine();
            Console.Clear();

            if (usersInput == "b")
            {
                int secondIndex = 0;
                Console.WriteLine("select a second result for the calculation");
                usersInput = Console.ReadLine();
                while (!int.TryParse(usersInput, out secondIndex) || !calculator.IsindexWithRange(secondIndex))
                {
                    Console.WriteLine("Enter a numerical value or the index shown in the option\n");
                    usersInput = Console.ReadLine();
                }
                double secondResult = calculator.GetUsersCalculationResult(secondIndex);
                BasicArithmetic(firstResult, secondResult);
            }
            else
            {
                AdvancedArithmetic(firstResult);
            }
        }

        public static void PerformNewCalculation()
        {
            string? numInput1 = "";


            Console.WriteLine("Type a number, and then press enter");
            numInput1 = Console.ReadLine();
            double cleanNum1 = 0;

            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not a valid input.Please enter a numeric value\n");
                numInput1 = Console.ReadLine();
            }

            ChooseArithmeticType();
            usersInput = Console.ReadLine();

            if (usersInput == "b")
            {
                string? numInput2 = "";
                Console.WriteLine("Type another number, and then press Enter:");
                numInput2 = Console.ReadLine();
                double cleanNum2 = 0;

                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not a valid input.Please enter a numeric value\n");
                    numInput2 = Console.ReadLine();
                }

                BasicArithmetic(cleanNum1, cleanNum2);
            }
            else
            {
                AdvancedArithmetic(cleanNum1);
            }
        }

        public static void  AdvancedArithmetic(double num1)
        {
            Console.WriteLine("\tsqrt - Square Root");
            Console.WriteLine("\tpow - Take the Power");
            Console.WriteLine("\tx - 10 times");
            Console.WriteLine("\tcos - Cos");
            Console.WriteLine("\ttan - Tan");
            Console.WriteLine("\tsin - Sin");
            Console.Write("Your option?\n");

            string? op = Console.ReadLine();
            Console.Clear();

            if (op == null || !Regex.IsMatch(op, "^(sqrt|pow|x|cos|tan|sin)$"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    double result = calculator.PerformAdvancedArithmetic(num1, op);
                    if (double.IsNaN(result))
                    {

                        Console.WriteLine("This operation will result in a mathematical error .\n");

                    }
                    else Console.WriteLine("Your result : {0:0.##}\n", result);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occured trying to do the math.\n - Details: " + e.Message);
                }
                Console.WriteLine("---------------------------------\n");
                calculator.GetUsageCount();
            }
        }

        public static void BasicArithmetic(double num1, double num2)
        {
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option?\n");

            string? op = Console.ReadLine();
            Console.Clear();

            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    double result = calculator.PerformBasicArithmetic(num1, num2, op);
                    if (double.IsNaN(result))
                    {

                        Console.WriteLine("This operation will result in a mathematical error .\n");

                    }
                    else Console.WriteLine("Your result : {0:0.##}\n", result);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occured trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("---------------------------------\n");
            calculator.GetUsageCount();
        }
    }
}
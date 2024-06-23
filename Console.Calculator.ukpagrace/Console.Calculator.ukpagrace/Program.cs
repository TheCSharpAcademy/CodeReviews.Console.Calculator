// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static Calculator calculator = new Calculator();
        static void Main(string[] args)
        {
            bool endApp = false;
            Console.WriteLine("Console Calculator in C# \r");
            Console.WriteLine("-------------------------\n");

            
            while (!endApp)
            {
                Console.WriteLine("s - See History \r");
                Console.WriteLine("or any other key to perform calculations \r");
                string usersInput = Console.ReadLine();

                if (usersInput == "s")
                {
                    
                    calculator.seeCalculations();
                    if (calculator.isUsersCalculationsEmpty())
                    {
                        Console.WriteLine("no calculations has been performed");
                    }else
                    {
                        Console.WriteLine("choose an option \r");
                        Console.WriteLine("d - delete calculations \r");
                        Console.WriteLine("p - perform calculations \r");

                        usersInput = Console.ReadLine();

                        if(usersInput == "d")
                        {
                            Console.WriteLine("select an index from the list to delete a calculation");
                            usersInput = Console.ReadLine();

                            int index = 0;
                            while (!int.TryParse(usersInput, out index))
                            {
                                Console.WriteLine("Enter a numerical value");
                                usersInput = Console.ReadLine();
                            }
                            calculator.deleteCalculation(index);
                            calculator.seeCalculations();
                        }else if(usersInput == "p")
                        {
                            Console.WriteLine("select a the first result for the calculation");
                            usersInput = Console.ReadLine();

                            int firstIndex = 0;
                            while (!int.TryParse(usersInput, out firstIndex) || !calculator.isindexWithRange(firstIndex))
                            {
                                Console.WriteLine("Enter a numerical value or the index shown in the option");
                                usersInput = Console.ReadLine();
                            }

                            double firstResult = calculator.getUsersCalculationResult(firstIndex);



                            Console.WriteLine("select a the second result for the calculation");
                            usersInput = Console.ReadLine();

                            int secondIndex = 0;
                            while (!int.TryParse(usersInput, out secondIndex) || !calculator.isindexWithRange(secondIndex))
                            {
                                Console.WriteLine("Enter a numerical value or the index shown in the option");
                                usersInput = Console.ReadLine();
                            }


                            double secondResult = calculator.getUsersCalculationResult(secondIndex);

                            PerformCalculation(firstResult, secondResult);
                        }


                    }

                }
                else
                {


                    string? numInput1 = "";
                    string? numInput2 = "";
                    double result = 0;



                    Console.WriteLine("Type a number, and then press enter");
                    numInput1 = Console.ReadLine();

                    double cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not a valid input.Please enter a numeric value");
                        numInput1 = Console.ReadLine();
                    }


                    Console.WriteLine("Type another number, and then press Enter:");
                    numInput2 = Console.ReadLine();

                    double cleanNum2 = 0;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not a valid input.Please enter a numeric value");
                        numInput2 = Console.ReadLine();
                    }

                    PerformCalculation(cleanNum1, cleanNum2);


                }


                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue\n");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");

            }

            return;

        }


        public static void PerformCalculation(double num1, double num2)
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option?\n");


            string? op = Console.ReadLine();


            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    double result = calculator.DoOperation(num1, num2, op);
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

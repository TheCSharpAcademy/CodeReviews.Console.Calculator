using Calculator;
using Calculator.Models;
using CalculatorHelpers;
using CalculatorLibrary;

namespace CalculatorProgram
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Console Calculator\r");
            Console.WriteLine("------------------\n");

            bool endApp = false;
            CalculatorLibrary.Calculator calculator = new(); //Why does the using statement not work here?
            int timesUsed = 0;
            List<Calculation> calculations = new  List<Calculation>();
            while (!endApp)
            {
                string Input1 = "";
                string Input2 = "";
                string Operation = "";
                double result = 0;

                Console.Clear();
                Console.WriteLine("\nNew Calculation");
                Console.WriteLine("---------------");
                Console.Write("\nType the first number and press any key to enter: ");
                Input1 = Console.ReadLine();
                double cleanNum1 = 0;
                while (!double.TryParse(Input1, out cleanNum1))
                {
                    Console.Write("Please enter an integer value: ");
                    Input1 = Console.ReadLine();
                }

                Console.Write("Type the second number and press any key to enter: ");
                Input2 = Console.ReadLine();
                double cleanNum2 = 0;
                while (!double.TryParse(Input2, out cleanNum2))
                {
                    Console.Write("Please enter an integer value: ");
                    Input2 = Console.ReadLine();
                }

                Console.WriteLine("\nChoose an option for the operation to be conducted:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tp - to the Power of");

                Console.Write("\nYour option? ");
                Operation = Console.ReadLine();
                string cleanOperation = Helpers.ValidateOperation(Operation);

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, cleanOperation);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("\nThis operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        timesUsed++;
                        Helpers.SaveCalculationsToList(timesUsed, cleanNum1, cleanNum2, result, cleanOperation, calculations);
                        Console.WriteLine("\nResult : {0:0.####}\n", result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh No! An exception occurred trying to do the math.\nDetails: " + e.Message);
                }

                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine(@"Press 'q' to Quit the app 
      's' to Show previous calculations
      Any other key to continue: ");
                
                switch (Console.ReadLine().ToLower().Trim())
                {
                        case "q":
                            endApp = true;
                            break;
                        case "s":
                            Helpers.ShowPreviousCalculations(calculations);
                            Console.WriteLine(@"Delete the list? Enter 'd' to Delete the and continue
                'c' to Continue calculation from list item
                'q' to Quit the app
                Any other key to continue");
                            switch (Console.ReadLine().ToLower().Trim())
                            {
                                case "q":
                                    endApp = true;
                                    break;

                                case "c":
                                    Helpers.ContinueCalculationFromList(calculations, timesUsed);
                                    timesUsed++;
                                    Console.Write("Continue calculating (c) or Quit the app (q)? : ");
                                    string choice = Console.ReadLine();
                                    string reDo = "";
                                    do
                                    {
                                        reDo = "";
                                        switch (choice.ToLower().Trim())
                                        {
                                            case "c":
                                                break;
                                            case "q":
                                                endApp = true;
                                                break;
                                            default:
                                                Console.Write("Invalid Option. Enter your choice: ");
                                                choice = Console.ReadLine();
                                            reDo = "Not empty";
                                                break;
                                        } 
                                    } while (reDo != "");
                                    break;

                                case "d":
                                    calculations.Clear();
                                    timesUsed = 0;
                                    break;

                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                } 
            }
            Console.WriteLine($"\nGood Bye! The calculator was used {timesUsed} times");
            calculator.Finish(); //Close the JSON writer
            return;
        }
    }
}


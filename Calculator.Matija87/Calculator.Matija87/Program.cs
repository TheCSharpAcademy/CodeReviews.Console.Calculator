using CalculatorLibary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            int timesUsed = 0;
            string? mainChoice;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            Calculator calculator = new Calculator();
            MainMenu();
                        
            // Add call to close the JSON writer before return
            calculator.Finish();
            return;

            void MainMenu()
            {
                do
                {
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("\tc - Make a calculation");
                    Console.WriteLine("\tv - View previous calculations");
                    Console.WriteLine("\td - Delete previous calculations");

                    mainChoice = Console.ReadLine();

                    if (mainChoice == "v")
                    {
                        Helpers.ShowHistory();
                    }
                    else if (mainChoice == "d")
                    {
                        Helpers.DeleteHistory();
                        Console.WriteLine("\nPrevious Calculations Deleted\n");
                    }
                    else if (mainChoice == "c")
                    {
                        Console.Clear();
                        Calculate();
                    }
                    else
                    {
                        Console.WriteLine("Wrong input! Try again!\n");
                    }

                } while (mainChoice == null || mainChoice != "c" || mainChoice != "v" || mainChoice != "d");
            }

            void Calculate()
            {
                while (true)
                {
                    // Declare variables and set to empty.
                    string? numInput1 = "";
                    string? numInput2 = "";
                    double result = 0;
                                                                               
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    double cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }

                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    double cleanNum2 = 0;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
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
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            Helpers.AddToHistory(cleanNum1, cleanNum2, op, result);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    timesUsed++;

                    Console.WriteLine("------------------------\n");

                    //Display how many times calculator has been used
                    Helpers.ShowTimesUsed(timesUsed);

                    // Wait for the user to respond before closing.
                    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                    if (Console.ReadLine() == "n") Environment.Exit(0);

                    Console.Clear();
                    MainMenu();
                }
            }
        }
    }
}
using CalculatorLibrary;
using ConsoleCalculator.Models;
namespace ConsoleCalculator;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int count = 0;
        double savedResult = 0;
        bool useSaved = false;

        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        Calculator calculator = new Calculator();

        while (!endApp)
        {
            // Declare variables and set to empty or zeros.
            string choice;
            string numInput1;
            string numInput2;
            double result;
            string op;
            double cleanNum1;

            // Ask the user if he wants to use single operands or multiple perands operations.
            if (count != 0)
            {
                Console.Clear();
                Helpers.DisplayHistory();
            }
            Console.WriteLine("What are you here to do?");
            Console.WriteLine("\ts - Single Operand Operation.");
            Console.WriteLine("\tm - Multiple Operands Operation.");
            Console.WriteLine("\tx - Clear History");
            choice = Console.ReadLine();

            switch (choice.ToUpper())
            {
                case "X":
                    Helpers.ClearHistory();
                    break;
                case "S":
                    if (useSaved == true)
                    {
                        numInput1 = savedResult.ToString();
                    }
                    else
                    {
                        // Ask the user to type the first number.
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();
                    }
                    cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }

                    // Ask the user to choose an operator.
                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\tq - Square Root");
                    Console.WriteLine("\tt - 10x");
                    Console.WriteLine("\ttc - Cos(x)");
                    Console.WriteLine("\tts - Sin(x)");
                    Console.WriteLine("\ttt - Tan(x)");
                    Console.Write("Your option? ");

                    op = Console.ReadLine();
                    try
                    {
                        result = calculator.DoSingleOperation(cleanNum1, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                        Double[] arrayOperands = new double[1];
                        arrayOperands[0] = cleanNum1;
                        Helpers.SaveToHistory(arrayOperands, Helpers.AssignOperation(op), result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    count++;
                    break;
                case "M":
                    if (useSaved == true)
                    {
                        numInput1 = savedResult.ToString();
                    }
                    else
                    {
                        // Ask the user to type the first number.
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();
                    }
                    cleanNum1 = 0;
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
                    Console.WriteLine("\tp - Power");
                    Console.Write("Your option? ");

                    op = Console.ReadLine();
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                        Double[] arrayOperands = new double[2];
                        arrayOperands[0] = cleanNum1;
                        arrayOperands[1] = cleanNum2;
                        Helpers.SaveToHistory(arrayOperands, Helpers.AssignOperation(op), result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    count++;
                    break;
            }

            Console.WriteLine("------------------------\n");
           
            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app,'r' to use the result of the last operation, or press any other key and Enter to continue: ");
            string action = Console.ReadLine();
            if (action == "n") { endApp = true; }
            else if (action == "r")
            {
                if (!Helpers.NoHistory())
                {
                    savedResult = Helpers.GetLastResult();
                    useSaved = true;
                }
                else
                {
                    Console.WriteLine("no saved result to use.");
                    useSaved = false;
                }

            }
            else useSaved = false;


            Console.WriteLine("\n"); // Friendly linespacing.
        }
        // Add call to close the JSON writer before return
        calculator.Finish();
        Console.WriteLine($"The calculator was used {count} times.\n"); // tell the user how many times the calculator was used.
        Console.WriteLine("Press any key to terminate the program.\n");
        Console.ReadKey();
        return;
    }
}
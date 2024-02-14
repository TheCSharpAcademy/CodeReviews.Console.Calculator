using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                Console.WriteLine("What would you like to do?");
                Console.WriteLine("\t1 - Perform the operation");
                Console.WriteLine("\t2 - Show latest operations");
                Console.WriteLine("\t3 - Delete latest operations");
                Console.WriteLine("\t4 - Exit");

                string userChoice = Console.ReadLine();

                switch (userChoice) {
                    case "1":
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
                        Console.WriteLine("\tp - To Power");
                        Console.Write("Your option? ");

                        string op = Console.ReadLine();

                        try
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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

                        Console.WriteLine("------------------------\n");
                        break;
                    case "2":
                        calculator.ShowLatestOperations();
                        break;
                    case "3":
                        calculator.DeleteLatestOperations();
                        break;
                    case "4":
                        endApp = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
            calculator.Finish();
            return;
        }
    }
}
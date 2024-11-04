using CalculatorLibrary;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> latestCalculations = new();
            int counter = 0;
            Calculator calculator = new Calculator();
            while (true)
            {
                bool endApp = false;
                Console.WriteLine("Console Calculator in C#\r");
                Console.WriteLine("------------------------\n");
                Console.Write("""
                Select an option from the menu:
                (1) Calculate
                (2) View history
                (3) Clear history
                (4) Use a previous result
                (5) Exit

                Option: 
                """);
                string? menuOption = Console.ReadLine();
                int menuChoice;
                
                if (!int.TryParse(menuOption, out menuChoice) || menuChoice < 1 || menuChoice > 5)
                {
                    Console.WriteLine("Invalid input. Please enter a number from the menu.");
                    continue;
                }
                if (menuChoice == 5)
                    break;
                if (menuChoice == 4)
                {
                    if (latestCalculations.Count == 0)
                    {
                        Console.WriteLine("No history to use");
                        continue;
                    }
                    Console.Write("Enter the index of the result you want to use: ");
                    string? indexInput = Console.ReadLine();
                    int index;
                    if (!int.TryParse(indexInput, out index))
                    {
                        Console.WriteLine("Invalid index");
                        continue;
                    }
                    double previousResult = GetResultFromHistory(latestCalculations, index);
                    Console.WriteLine($"Using previous result: {previousResult}");

                    string? op = GetOperatorFromUser();
                    if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sq|p|x|sn|cn|tn]"))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                    }
                    double result = 0;
                    if (IsSingleOperandOperation(op))
                    {
                        result = calculator.DoOperation(previousResult, op);
                    }
                    else
                    {
                        Console.Write("\nType a number, and then press Enter: ");
                        string? numInput = Console.ReadLine();
                        double newNum = 0;
                        while (!double.TryParse(numInput, out newNum))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput = Console.ReadLine();
                        }
                        result = calculator.DoOperation(previousResult, newNum, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nYour result: {0:0.##}\n", result);
                            latestCalculations.Add(result.ToString());
                        }
                    }
                }
                if (menuChoice == 3)
                {
                    latestCalculations.Clear();
                    Console.WriteLine("History cleared.");
                    continue;
                }
                if (menuChoice == 2)
                {
                    if (latestCalculations.Count == 0)
                    {
                        Console.WriteLine("No history to display.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("History:");
                        Console.WriteLine("----------");
                        for (int i = 0; i < latestCalculations.Count; i++)
                        {
                            Console.WriteLine($"{i}: {latestCalculations[i]}");
                        }
                    }
                    continue;
                }
                if (menuChoice == 1)
                {        
                    while (!endApp)
                    {   
                        string? op = GetOperatorFromUser();
                        
                         string? numInput1 = "";
                         string? numInput2 = "";
                         double result = 0;
                             
                        // Validate input is not null, and matches the pattern
                        if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sq|p|x|sn|cn|tn]"))
                        {
                            Console.WriteLine("Error: Unrecognized input.");
                        }
                        else
                        {
                            if(IsSingleOperandOperation(op))
                            {
                                Console.Write("\nType a number, and then press Enter: ");
                                numInput1 = Console.ReadLine();

                                double cleanNum1 = 0;
                                while (!double.TryParse(numInput1, out cleanNum1))
                                {
                                    Console.Write("This is not valid input. Please enter an integer value: ");
                                    numInput1 = Console.ReadLine();
                                }
                                try
                                {
                                    result = calculator.DoOperation(cleanNum1, op);
                                    if (double.IsNaN(result))
                                    {
                                        Console.WriteLine("This operation will result in a mathematical error.\n");
                                    }
                                    else Console.WriteLine("\nYour result: {0:0.##}\n", result);
                                    latestCalculations.Add(result.ToString());
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                                }
                            }
                            else
                            {
                                // Ask the user to type the first number.
                                Console.Write("\nType a number, and then press Enter: ");
                                numInput1 = Console.ReadLine();

                                double cleanNum1 = 0;
                                while (!double.TryParse(numInput1, out cleanNum1))
                                {
                                    Console.Write("This is not valid input. Please enter an integer value: ");
                                    numInput1 = Console.ReadLine();
                                }

                                // Ask the user to type the second number.
                                Console.Write("\nType another number, and then press Enter: ");
                                numInput2 = Console.ReadLine();

                                double cleanNum2 = 0;
                                while (!double.TryParse(numInput2, out cleanNum2))
                                {
                                    Console.Write("This is not valid input. Please enter an integer value: ");
                                    numInput2 = Console.ReadLine();
                                }
                                try
                                {
                                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                                    if (double.IsNaN(result))
                                    {
                                        Console.WriteLine("This operation will result in a mathematical error.\n");
                                    }
                                    else Console.WriteLine("\nYour result: {0:0.##}\n", result);
                                    latestCalculations.Add(result.ToString());
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                                }
                            }
                            
                        }
                        counter++;
                        Console.WriteLine($"\nCalculator use count: {counter}\n");
                        Console.WriteLine("------------------------\n");

                        endApp = true;
                    }
                    continue;
                }
            }
            calculator.Finish();
        }
        static double GetResultFromHistory(List<string> history, int index)
        {
            if (index < 0 || index >= history.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid index");
            }

            return double.Parse(history[index]);
        }
        static bool IsSingleOperandOperation(string op)
        {
            return op == "sq" || op == "x" || op == "sn" || op == "cn" || op == "tn";
        }
        static string? GetOperatorFromUser()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsq - Square root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tx - 10x");
            Console.WriteLine("\tsn - Sine");
            Console.WriteLine("\tcn - Cosine");
            Console.WriteLine("\ttn - Tangent");
            Console.Write("Your option? ");
            return Console.ReadLine();
        }
    }
}
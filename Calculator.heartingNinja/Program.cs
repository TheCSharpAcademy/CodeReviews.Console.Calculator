using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            int timesUsed = 0;
            bool endApp = false;
            bool backToMenu = true;
            bool useResultNumbers = false;
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            Calculator calculator = new Calculator();

            while (!endApp)
            {
                if (backToMenu)
                {
                    Console.WriteLine($"Times the calculator was used {timesUsed}");
                    Console.WriteLine("\tS - Start");
                    Console.WriteLine("\tR - Results");
                    Console.Write("Your option? ");
                    string option = Console.ReadLine();

                    if (option.ToLower() == "s")
                    {
                        backToMenu = false;
                        useResultNumbers = false;
                        continue;
                    }

                    if (option.ToLower() == "r")
                    {
                        calculator.PrintResults();
                        Console.Write("Press 's' Enter to Start\n");
                        Console.Write("Press 'c' and Enter to clear the results\n");
                        Console.Write("Press 'r' and Enter to use these results of your number inputs, use the number in list for number to use in the calculator \n");
                        string input = Console.ReadLine();

                        if (input == "s")
                        {
                            // continue;
                        }
                        else if (input == "c")
                        {
                            calculator.ClearResults();
                        }
                        else if (input == "r")
                        {
                            if (calculator.results.Count >= 2)
                            {
                                Console.Write("choose numbers from list of results");
                                useResultNumbers = true;
                            }
                            else
                            {
                                Console.Write("need more than two results\n");
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (option.ToLower() != "r" && option.ToLower() != "s")
                    {
                        Console.WriteLine("Invalid option. Please enter 's' to Start or 'r' for Results");
                        continue;
                    }
                }

                if (!useResultNumbers)
                {
                    string numInput1 = "";
                    string numInput2 = "";
                    double result = 0;
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

                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.WriteLine("\tb - Back to Menu");
                    Console.Write("Your option? ");

                    string op = Console.ReadLine();

                    if (op.ToLower() == "b")
                    {
                        backToMenu = true;
                        continue;
                    }
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
                    timesUsed++;
                }
                else
                {
                    string numInput1 = "";
                    string numInput2 = "";
                    double result = 0;
                    Console.WriteLine("List of previous results:");

                    for (int i = 0; i < calculator.results.Count; i++)
                    {
                        Console.WriteLine("{0}: {1}", i + 1, calculator.results[i]);
                    }

                    Console.Write("Choose the index of the result to use as the first number: ");
                    string indexInput = Console.ReadLine();
                    int index = calculator.results.Count;

                    while (!int.TryParse(indexInput, out index) || index < 1 || index > calculator.results.Count)
                    {
                        Console.Write("Invalid input. Please enter a number between 1 and {0}: ", calculator.results.Count);
                        indexInput = Console.ReadLine();
                    }

                    numInput1 = calculator.results[index - 1];
                    double cleanNum1 = 0;

                    while (!double.TryParse(numInput1, out cleanNum1))

                    {
                        Console.Write("This is not valid input. Please enter a number: ");
                        numInput1 = Console.ReadLine();
                    }

                    for (int i = 0; i < calculator.results.Count; i++)
                    {
                        Console.WriteLine("{0}: {1}", i + 1, calculator.results[i]);
                    }

                    Console.Write("Choose the index of the result to use as the second number: ");
                    indexInput = Console.ReadLine();
                    index = 0;

                    while (!int.TryParse(indexInput, out index) || index < 1 || index > calculator.results.Count)
                    {
                        Console.Write("Invalid input. Please enter a number between 1 and {0}: ", calculator.results.Count);
                        indexInput = Console.ReadLine();
                    }

                    numInput2 = calculator.results[index - 1];
                    double cleanNum2 = 0;

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a number: ");
                        numInput1 = Console.ReadLine();
                    }

                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.WriteLine("\tb - Back to Menu");
                    Console.Write("Your option? ");
                    string op = Console.ReadLine();

                    if (op.ToLower() == "b")
                    {
                        backToMenu = true;
                        continue;
                    }
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

                    timesUsed++;
                    useResultNumbers = false;
                }
            }
            return;
        }
    }
}
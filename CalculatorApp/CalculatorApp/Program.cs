global using System.Net.Sockets;
using System.Text.RegularExpressions;
using CalculatorLibrary;
using System.Globalization;
public class CalculatorApp
{
    
    static void Main(string[] args)
    {   
        Calculator calculator = new Calculator();

        string? usInput;

        do
        {
            int calCount = 0;
            bool endApp = false;

            Console.WriteLine("Welcome to calculator console App");
            Console.WriteLine("___________________________________");
            Console.Clear();
            Console.WriteLine("_________\tMenu:_________");
            Console.WriteLine("\tC => Calculation");
            Console.WriteLine("\tH => Calculation History");
            Console.WriteLine("\tD => Delete calculation record");
            Console.WriteLine("\tQ => Quite calculation app");

            usInput = Console.ReadLine().ToLower().Trim();
            while (usInput == null || !Regex.IsMatch(usInput, "[c|h|d|q]"))
            {
                Console.WriteLine("Invalid option chosen. Please choose correct option: ");
                usInput = Console.ReadLine().ToLower().Trim();
            }

            switch (usInput)
            {
                case "c":
                    bool PreviousResult = false;
                    while (!endApp)
                    {
                        string? firstInput;
                        string? secondInput;

                        Console.WriteLine("Calculation selected");
                        Console.WriteLine("________________________");
                        Console.Clear();
                        double result = 0;

                        double cleanNum = 0;

                        if (!PreviousResult)
                        {
                            Console.WriteLine("Type a number and press Enter: ");
                            firstInput = Console.ReadLine().Trim();


                            while (!double.TryParse(firstInput, NumberStyles.Number, CultureInfo.InvariantCulture, out cleanNum))
                            {
                                Console.WriteLine("Invalid input value, please type a numeric value: ");
                                firstInput = Console.ReadLine().Trim();
                            }
                        }
                        else
                        {
                            cleanNum = calculator.ResultsList.Last().Answer;
                            Console.WriteLine($"Previous Result: {cleanNum}");
                        }

                        Console.WriteLine("Please a choose an operation");
                        Console.WriteLine("\t+ => Add");
                        Console.WriteLine("\t- => Subtract");
                        Console.WriteLine("\t* => Multiply");
                        Console.WriteLine("\t/ => Divide");
                        Console.WriteLine("\t^ => Power");
                        Console.WriteLine("\tr => Square root");
                        Console.WriteLine("\tcos => Cosine");
                        Console.WriteLine("\tsin => Sine");
                        Console.WriteLine("\ttan => Tangant");
                        Console.WriteLine("Your operation: ");
                        string? oper = Console.ReadLine().Trim().ToLower();

                        while (oper == null || !Regex.IsMatch(oper, @"^[\+|\-|\*|/|\^|r|c|s|t]"))
                        {
                            Console.WriteLine("Error:Unrecognized operation");
                            oper = Console.ReadLine().ToLower().Trim();
                        }

                        double cleanNum2 = 0;

                        if (Regex.IsMatch(oper, @"[\+|\-|\*|/|\^|]"))
                        {
                            Console.WriteLine("Type another number and press Enter: ");
                            secondInput = Console.ReadLine().Trim();


                            while (!double.TryParse(secondInput, NumberStyles.Number, CultureInfo.InvariantCulture, out cleanNum2))
                            {
                                Console.WriteLine("Invalid input value, please type a numeric value: ");
                                secondInput = Console.ReadLine().Trim();
                            }
                        }
                        else
                        {
                            try
                            {
                                result = calculator.OperationToDo(cleanNum, cleanNum2, oper);
                                calCount++;
                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result a mathematical error.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Your result: {0:0.##}\n", result);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Oh no!!! An exception occured trying to do math.\nDetails:", e.Message);
                            }
                        }

                        Console.WriteLine("_______________________________\n");
                        Console.WriteLine($"Calculator was used {calCount} times");
                        Console.WriteLine("Press 'x' to discard previous result and start new operation.");
                        Console.WriteLine("Press 'n' and to go back to main menu, or press any other key press Enter to continue");

                        string? endOption = Console.ReadLine().ToLower().Trim();

                        if (endOption == "n")
                        {
                            endApp = true;
                        }
                        else if (endOption != "x")
                        {
                            PreviousResult = true;
                        }
                        else
                        {
                            PreviousResult = false;
                        }
                        Console.WriteLine("\n");
                    }
                    calculator.Finish();
                    break;

                case "h":

                    Console.Clear();
                    if (calculator.ResultsList.Count == 0)
                    {
                        Console.WriteLine("--Currently there is no calculation record");
                    }
                    else
                    {
                        Console.WriteLine("-- All Calculation history---");
                        foreach (var result in calculator.ResultsList)
                        {
                            result.Display();
                        }
                    }
                    Console.ReadLine();
                    break;
                case "d":
                    Console.Clear();
                    Console.WriteLine("Are you sure you want to delete all Calculation records(Y/N)");
                    string? answer = Console.ReadLine().ToLower().Trim();

                    if (answer == "y")
                    {
                        calculator.ResultsList.Clear();
                        Console.Clear();
                        Console.WriteLine("All the calculation records deleted! (Press Enter)");
                        Console.ReadLine();
                    }
                    break;
            }
        } while (usInput != "q");
    }
}
    

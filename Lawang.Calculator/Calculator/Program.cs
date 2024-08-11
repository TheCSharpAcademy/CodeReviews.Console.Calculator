using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        string? userInput;

        Calculator calculator = new Calculator();
        do
        {
            bool endApp = false;
            int calUsed = 0;
            Console.Clear();
            Console.WriteLine("---\tMenu Options\t---");
            Console.WriteLine("-------------------");
            Console.WriteLine("Press '1' for Calculation.");
            Console.WriteLine("Press '2' for viewing and using calculation records.");
            Console.WriteLine("Press '3' for deleting calculation record.");
            Console.WriteLine("Press '4' for exiting the Program.");
            userInput = Console.ReadLine();

            while (userInput == null || !Regex.IsMatch(userInput, "[1-4]"))
            {
                Console.WriteLine("Please enter the value from 1 to 4");
                userInput = Console.ReadLine();
            }


            switch (userInput)
            {
                case "1":
                    bool usePreviousResult = false;
                    while (!endApp)
                    {
                        Console.Clear();
                        Console.WriteLine("Console Calculator in C#\r");
                        Console.WriteLine("------------------------\n");

                        string? numInput1 = "";
                        string? numInput2 = "";
                        double result = 0;

                        double cleanNum1 = 0;
                        if (!usePreviousResult)
                        {
                            Console.Write("Type a number, and then press Enter: ");
                            numInput1 = Console.ReadLine();


                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numInput1 = Console.ReadLine();
                            }
                        }
                        else
                        {
                            cleanNum1 = calculator.ResultList.Last().Answer;
                            Console.WriteLine($"Previous Result: {cleanNum1}\n");
                        }



                        Console.WriteLine("Choose an operator from the following list:");
                        Console.WriteLine("\t+ - Add");
                        Console.WriteLine("\t- - Subtract");
                        Console.WriteLine("\t* - Multiply");
                        Console.WriteLine("\t/ - Divide");
                        Console.WriteLine("\t^ - Power");
                        Console.WriteLine("\tr - square root");
                        Console.WriteLine("\tcos - cosine");
                        Console.WriteLine("\tsin - sine");
                        Console.WriteLine("\ttan - tangent");
                        Console.Write("Your option? ");

                        string? op = Console.ReadLine();

                        while (op == null || !Regex.IsMatch(op, @"[\+|\-|\*|/|r|p|s|c|t]"))
                        {
                            Console.WriteLine("Error: Unrecognized operation. Please Try Again!!");
                            op = Console.ReadLine();
                        }
                        
                        op = op.Trim().Substring(0, 1);
                        double cleanNum2 = 0;

                        if (Regex.IsMatch(op, @"[\+|\-|/|\*|\^]"))
                        {
                            Console.Write("Type another number, and then press Enter: ");
                            numInput2 = Console.ReadLine();


                            while (!double.TryParse(numInput2, out cleanNum2))
                            {
                                Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
                                numInput2 = Console.ReadLine();
                            }
                        }

                        // Ask the user to choose an operator.
                        try
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            calUsed++;
                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                            }



                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + ex.Message);
                        }


                        Console.WriteLine("------------------------\n");
                        Console.WriteLine($"Calculator was used {calUsed} times");
                        Console.WriteLine("Press 'x' to discard previous result and start new operaion: ");
                        Console.Write("Press 'n' and Enter to go to menu, or press any other key and Enter to resume working on previous result: ");

                        string? endOption = Console.ReadLine();
                        if (endOption == "n")
                        {
                            endApp = true;
                        }
                        else if (endOption != "x")
                        {
                            usePreviousResult = true;
                        }
                        else
                        {
                            usePreviousResult = false;
                        }

                        Console.WriteLine("\n");

                    }
                    calculator.Finish();
                    break;

                case "2":
                    Console.Clear();
                    if(calculator.ResultList.Count ==  0)
                    {
                        Console.WriteLine("-- Currently there is no calculation History --");
                    }
                    else
                    {
                        Console.WriteLine("-- All the calcualtion history ---\n");
                        foreach(var result in calculator.ResultList)
                        {
                            result.Display();
                        }
                    }
                    
                    Console.ReadLine();
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("Are you sure you want to delete calculation record (y/n");

                    string? answer = Console.ReadLine();

                    while (answer == null || (answer != "y" && answer != "n"))
                    {
                        Console.WriteLine("Please give the valid value (y/n)!!");
                        answer = Console.ReadLine();
                    }

                    if (answer == "y")
                    {
                        calculator.ResultList.Clear();
                        Console.Clear();
                        Console.WriteLine("All the Calculation History Deleted! (Press Enter)");
                        Console.ReadLine();
                    }
                    break;
            }
        } while (userInput != "4");


    }

}
namespace CalculatorLibrary;
using Models;

public class Helpers
{
    internal static List<Log> logs = new List<Log>();

    public static void LogOperation(double num1, double num2, char op, double result)
    {
        logs.Add(new Log
        {
            OperationCount = logs.Count+1,
            Number1 = num1,
            Operation = op,
            Number2 = num2,
            Result = result
        });
    }

    public static void ClearLogs()
    {
        logs.Clear();
    }

    public static void PrintLog()
    {
        foreach (var log in logs)
        {
            Console.WriteLine($"{log.OperationCount}. | {log.Number1} {log.Operation} {log.Number2} = {log.Result}");
        }
        Console.WriteLine();
    }
    
    public static int TotalOperations()
    {
        return logs.Count;
    }

    public static double GetPastResult()
    {
        string? userInput;
        Log selectedLog;
        double result = 0;

        Console.WriteLine("Select an operation from the list to use past result.");
        userInput = Console.ReadLine();

        if (Int32.TryParse(userInput, out int logNumber))
        {
            selectedLog = logs[logNumber - 1];
            result = Convert.ToDouble(selectedLog.Result);

            Console.WriteLine(selectedLog.Result);
        }

        return result;
    }

    public static double InputNumber(string? input)
    {
        double logResult = 0;
        bool usingPastResult = false;
     
        Console.Write("Type a number, and then press Enter: ");
        input = Console.ReadLine();

        double cleanNum = 0;
        while (!double.TryParse(input, out cleanNum))
        {
            if (input.ToLower() == "h")
            {
                if (Helpers.TotalOperations() != 0)
                {
                    Helpers.PrintLog();

                    // Select prior result
                    Console.WriteLine("Use prior result? Select Y or N:");
                    bool verifiedInput = false;

                    do
                    {
                        input = Console.ReadLine();

                        switch (input.ToLower())
                        {
                            case "y":
                                logResult = Helpers.GetPastResult();
                                usingPastResult = true;
                                verifiedInput = true;
                                break;
                            case "n":
                                verifiedInput = true;
                                break;
                            default:
                                Console.WriteLine("Please select either Y or N.");
                                break;
                        }
                    } while (!verifiedInput);
                }
                else
                {
                    Console.WriteLine("History is empty.");
                }

                if (usingPastResult == true)
                {
                    input = logResult.ToString();
                }
                else
                {
                    Console.WriteLine("\nType a number, and then press Enter: ");
                    input = Console.ReadLine();
                }
            }
            else
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                input = Console.ReadLine();
            }
        }

        return cleanNum;
    }
}

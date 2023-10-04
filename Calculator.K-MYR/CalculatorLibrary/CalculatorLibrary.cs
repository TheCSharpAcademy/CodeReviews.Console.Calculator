using Newtonsoft.Json;
using CalculatorProgram.K_MYR.Models;

namespace CalculatorLibrary;



public class Calculator
{
    JsonWriter writer;

    public int OperationsCounter { get; set; } = 0;

    internal static List<Operation> Operations = new();

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                AddToHistory(num1, num2, OperationType.Addition, result);
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                AddToHistory(num1, num2, OperationType.Subtraction, result);

                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                AddToHistory(num1, num2, OperationType.Multiplication, result);
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                while (num2 == 0)
                {
                    Console.WriteLine("Please enter a non-zero divisor!");
                    num2 = ValidateDouble(Console.ReadLine());
                }

                result = num1 / num2;
                writer.WriteValue("Divide");
                AddToHistory(num1, num2, OperationType.Division, result);
                break;
            case "r":
                result = Math.Pow(num1, 1.0 / num2);
                writer.WriteValue("Nth.Root");
                AddToHistory(num1, 1.0 / num2, OperationType.Power, result);
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Nth.Power");
                AddToHistory(num1, num2, OperationType.Power, result);
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        OperationsCounter++;
        return result;
    }

    public double ValidateDouble(string Input)
    {
        double parsedDouble = 0;

        while (!double.TryParse(Input, out parsedDouble))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            Input = Console.ReadLine();
        }
        return parsedDouble;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    internal void AddToHistory(double operand1, double operand2, OperationType operation, double result)
    {
        Operations.Add(new Operation
        {
            Operand1 = operand1,
            Operand2 = operand2,
            Type = operation,
            Result = result
        });
    }

    public double? ShowHistory()
    {
        double? requestedResult = null;

        Console.Clear();
        Console.WriteLine("History of the latest perations");
        Console.WriteLine("------------------------");

        for (int i = 0; i < Operations.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Operations[i].Operand1}  {(char)Operations[i].Type} {Operations[i].Operand2} = {Operations[i].Result}");
        }

        Console.WriteLine("------------------------");
        Console.Write("Enter 'd' to delete the history. ");
        Console.WriteLine("Choose any result for further calculations by entering the corresponding number");
        Console.WriteLine("Press enter to exit");
        string? readResult = Console.ReadLine();

        if (readResult == "d")
        {
            Operations.Clear();
        }
        else if (int.TryParse(readResult, out int index) && ((index <= Operations.Count) && index > 0))
        {
            requestedResult = Operations[index - 1].Result;
        }

        return requestedResult;
    }
}




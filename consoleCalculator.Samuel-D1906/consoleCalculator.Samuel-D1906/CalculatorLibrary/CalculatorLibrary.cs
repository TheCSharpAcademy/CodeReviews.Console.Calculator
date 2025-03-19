using System.Diagnostics;
// CalculatorLibrary.cs
using Newtonsoft.Json;

namespace CalculatorLibrary;

class Calculator
{
    // CalculatorLibrary.cs
    JsonWriter writer;

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

    public void ShowMenu()
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        Console.WriteLine("Which options do you want to make?\n");
        Console.WriteLine("\td - Do Operation");
        Console.WriteLine("\ts - Show List");
        Console.WriteLine("\tl - Delete List");
        Console.Write("Your option? ");

        string? option = Console.ReadLine();

    }
    // CalculatorLibrary.cs
    // CalculatorLibrary.cs
    public double DoOperation(double num1, double num2, string op, List<double> calculations)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Options");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }

                break;
            case "l":
                // Ask the user to enter a non-zero divisor.
                DeleteList(calculations);
                writer.WriteValue("Delete List of Calculations");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public int CountOperations(int count)
    {
        count = count + 1;
        return count;
    }

    public List<double> SaveInList (List<double> calculations ,double result)
    {
        calculations.Add(result);
        return calculations;
    }

    public void DeleteList(List<double> List)
    {
        List.Clear();
    }

    // CalculatorLibrary.cs
    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}

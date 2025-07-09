// CalculatorLibrary.cs
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{
    JsonWriter writer;
    private static int calculatorUses;
    private List<Calculation> calculations = new();
    private int nextId = 1;

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
        string operation = "";
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
                calculatorUses++;
                operation = "+";
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                calculatorUses++;
                operation = "-";
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                calculatorUses++;
                operation = "*";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    writer.WriteValue("Divide");
                    calculatorUses++;
                    operation = "/";
                }
                else
                {
                    writer.WriteValue("Divide by zero attempt");
                }
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        Calculation calculation = new(nextId, num1, num2, operation, result);
        nextId++;
        calculations.Add(calculation);
        return result;
    }
    public void Finish()
    {
        writer.WriteEndArray();
        writer.WritePropertyName("Calculator uses");
        writer.WriteValue(calculatorUses);
        writer.WriteEndObject();
        writer.Close();
    }

    public void ShowHistory()
    {
        Console.Clear();
        if (calculations.Count == 0)
        {
            Console.WriteLine("No calculations yet.");
        }
        else
        {
            Console.WriteLine("Calculation History:\n");
            foreach (var calc in calculations)
            {
                Console.WriteLine($"[{calc.Id}].\t({calc})");
            }
        }
    }

    public Calculation GetCalculationById(int id)
    {
        return calculations.FirstOrDefault(c => c.Id == id);
    }

    public bool HasHistory()
    {
        return (calculations.Count > 0); 
    }

    public void ClearHistory()
    {
        calculations.Clear();
    }

}

public class Calculation
{
    public int Id { get; set; }
    public double Operand1 { get; set; }
    public double Operand2 { get; set; }
    public string Operation { get; set; }
    public double Result { get; set; }

    public Calculation(int id, double operand1, double operand2, string operation, double result)
    {
        Id = id;
        Operand1 = operand1;
        Operand2 = operand2;
        Result = result;
        Operation = operation;
    }

    public override string ToString()
    {
        return $"{Operand1} {Operation} {Operand2} = {Result} ";
    }
}


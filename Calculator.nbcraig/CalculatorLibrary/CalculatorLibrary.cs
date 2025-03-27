using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
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

    public double HandleOperations(double num1, double num2, string op)
    {
        double result = 0;

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        // Perform operation according to user's choice
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
                // Prompt the user to enter a non zero divisor until they do so
                // We make sure at the same occasion if the input is in correct format
                while (num2.Equals(0))
                {
                    Console.WriteLine("Cannot divide by Zero(0) !");
                    Console.WriteLine("HINT: Enter a non-zero divisor!");

                    string num2Input = Console.ReadLine();

                    while (!double.TryParse(num2Input, out num2))
                    {
                        Console.WriteLine("Enter the number in the correct format!");
                        num2Input = Console.ReadLine();
                    }
                }

                result = num1 / num2;
                writer.WriteValue("Divide");
                break;
            default:
                break;
        }

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}

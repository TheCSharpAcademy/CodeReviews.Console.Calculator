using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculate
{
    JsonWriter _writer;
    public Calculate()
    {
        StreamWriter logFile = File.CreateText("Calculate.json");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile)
        {
            Formatting = Formatting.Indented
        };
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }
    public string DoMath(double num1, double num2, string operation)
    {
        double result =0;
        string output = string.Empty;
        string op = operation;

        while (!new[] {"a", "s", "m", "d"}.Contains(op))
        {
            Console.WriteLine("Not a valid input, Please enter an operation: ");
            op = Console.ReadLine();
        }

        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Operation");

        switch (op)
        {
            case "a":
                result = num1 + num2;
                output = $"{num1} + {num2} = {result}";
                _writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                output = $"{num1} - {num2} = {result}";
                _writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                result = Math.Round(result, 2);
                output = $"{num1} * {num2} = {result}";
                _writer.WriteValue("Multiply");
                break;
            case "d":
                bool valid = true;

                while (num2 == 0)
                {
                    Console.WriteLine("You can not divide by '0'. Please enter a valid number: ");
                    valid = double.TryParse(Console.ReadLine(), out num2);
                    while (!valid) 
                    {
                        Console.WriteLine("That's not a valid character, type a number: ");
                        valid = double.TryParse(Console.ReadLine(), out num2);
                    }

                }
                result = num1 / num2;
                result = Math.Round(result, 2);
                output = $"{num1} / {num2} = {result}";
                _writer.WriteValue("Divide");
                break;
        }

        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();

        return output;
    }
    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }
}

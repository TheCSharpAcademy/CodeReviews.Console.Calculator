using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
	JsonWriter writer;

	public int UsedCount { get; set; } = 0;

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

	public void ShowUsedCount()
	{
        Console.WriteLine($"Calculator has been used {UsedCount} times.");
    }

	public double DoOperation(double num1, double num2, string? op)
	{
		double result = double.NaN;

		writer.WriteStartObject();
		writer.WritePropertyName("Operand1");
		writer.WriteValue(num1);
		writer.WritePropertyName("Operand2");
		writer.WriteValue(num2);
		writer.WritePropertyName("Operation");

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
				if (num2 != 0)
				{
					result = num1 / num2;
				}
				writer.WriteValue("Divide");
				break;
			default:
				break;
		}

		writer.WritePropertyName("Result");
		writer.WriteValue(result);
		writer.WriteEndObject();

		UsedCount++;

		return result;
	}

	public double DoOperation(double num, string? op)
	{
		double result = double.NaN;

		switch (op)
		{
			case "sqrt":
				result = Math.Sqrt(num);
				break;
			case "p":
				Console.Write("Enter exponent for power: ");
				string? input = Console.ReadLine();

				int exponent;
				while (!int.TryParse(input, out exponent))
				{
					Console.Write("This is not valid input. Please enter an integer value: ");
					input = Console.ReadLine();
				}
				result = Math.Pow(num, exponent);
				break;
			case "x":
				result = 10 * num;
				break;
			case "s":
				result = Math.Sin(num);
				break;
			case "c":
				result = Math.Cos(num);
				break;
			case "t":
				result = Math.Tan(num);
				break;
			default:
				break;
		}

		UsedCount++;

		return result;
	}

	public void Finish()
	{
		writer.WriteEndArray();
		writer.WriteEndObject();
		writer.Close();
	}
}
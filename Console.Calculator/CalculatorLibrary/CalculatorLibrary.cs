using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
	JsonWriter writer;

	public int UsedCount { get; set; } = 0;
	public List<double> Calculations { get; private set; }

	public Calculator()
	{
		Calculations = new List<double>();

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

	public bool IsEmptyCalculations()
	{
		return Calculations.Count == 0;
	}

	public void ShowCalculations(bool isDecorative = true)
	{
		if (isDecorative)
			Console.Clear();

		if (Calculations.Count == 0)
            Console.WriteLine("List is empty.");
		else
		{
			Console.WriteLine("All calculation results:");
			for (int i = 0; i < Calculations.Count; i++)
			{
                Console.WriteLine($"[{i}] - {Calculations[i]}");
            }
		}

		if (isDecorative)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Press any key to continue.");
			Console.ForegroundColor = ConsoleColor.White;

			Console.ReadKey();
		}
	}

	public void ClearCalculations()
	{
		Console.Clear();
		Calculations.Clear();

		Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("All calculation results cleared.");
		Console.ForegroundColor = ConsoleColor.White;

		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("Press any key to continue.");
		Console.ForegroundColor = ConsoleColor.White;

		Console.ReadKey();
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
		Calculations.Add(result);

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
		Calculations.Add(result);

		return result;
	}

	public void Finish()
	{
		writer.WriteEndArray();
		writer.WriteEndObject();
		writer.Close();
	}
}
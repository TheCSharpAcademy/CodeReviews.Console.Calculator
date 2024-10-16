// CalculatorLibrary.cs
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
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

		public double DoOperation(double num1, double num2, string op)
		{
			double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
			writer.WriteStartObject();
			writer.WritePropertyName("Operand1");
			writer.WriteValue(num1);
			if (op != "sqrt" && op != "tenx" && op != "sin" && op != "cos" && op != "tan")
			{
				writer.WritePropertyName("Operand2");
				writer.WriteValue(num2);
			}

		
			writer.WritePropertyName("Operation");
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
					writer.WriteValue("Divide");
					break;
				case "sqrt":
					result = Math.Sqrt(num1);
					writer.WriteValue("Square Root");
					break;
				case "pow":
					result = Math.Pow(num1, num2);
					writer.WriteValue("Power");
					break;
				case "tenx":
					result = Math.Pow(10, num1);
					writer.WriteValue("10^x");
					break;
				case "sin":
					result = Math.Sin(num1);
					writer.WriteValue("Sine");
					break;
				case "cos":
					result = Math.Cos(num1);
					writer.WriteValue("Cosine");
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
}

	

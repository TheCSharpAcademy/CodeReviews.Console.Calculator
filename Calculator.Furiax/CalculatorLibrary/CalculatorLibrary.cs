using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
	public class Calculator
	{

		JsonWriter writer;
		public static List<Calculation> calculationlist = new List<Calculation>();
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
				case "p":
					result = Math.Pow(num1, num2);
					writer.WriteValue("To the Power of");
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
		public double DoOperation(double num1, string op)
		{
			double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
			writer.WriteStartObject();
			writer.WritePropertyName("Operand1");
			writer.WriteValue(num1);
			writer.WritePropertyName("Operation");
			// Use a switch statement to do the math.
			switch (op)
			{
				case "x":
					result = num1 * 10;
					writer.WriteValue("x10");
					break;
				case "r":
					result = Math.Sqrt(num1);
					writer.WriteValue("Root");
					break;
				case "sin":
					result = Math.Sin(num1);
					writer.WriteValue("Sine");
					break;
				case "cos":
					result = Math.Cos(num1);
					writer.WriteValue("Cosine");
					break;
				case "tan":
					result = Math.Tan(num1);
					writer.WriteValue("Tangent");
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

		public double GetResult(double number, string operand)
		{
			double result = 0;
			try
			{
				result = DoOperation(number, operand);
				if (double.IsNaN(result))
				{
					Console.WriteLine("This operation will result in a mathematical error. Or you entered a wrong operandator.\n");
				}
				else
				{
					Console.WriteLine("Your result: {0:0.##}\n", result);
					Calculator.AddToList(number, operand, result);
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
			}
			return result;
		}
		public double GetResult(double number1, double number2, string operand)
		{
			double result = 0;
			try
			{
				result = DoOperation(number1, number2, operand);
				if (double.IsNaN(result))
				{
					Console.WriteLine("This operation will result in a mathematical error. Or you entered a wrong operandator.\n");
				}
				else
				{
					Console.WriteLine("Your result: {0:0.##}\n", result);
					Calculator.AddToList(number1, number2, operand, result);
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
			}
			return result;
		}
		public static void AddToList(double number1, double number2, string operand, double result)
		{
			string oper = "";
			switch (operand)
			{
				case "a": oper = "+"; break;
				case "s": oper = "-"; break;
				case "m": oper = "*"; break;
				case "d": oper = "/"; break;
				case "p": oper = "^"; break;
				default: break;
			}
			calculationlist.Add(new Calculation { Number1 = number1, Number2 = number2, Operand = oper, Result = result });
		}
		public static void AddToList(double number1, string operand, double result)
		{
			string oper = "";
			switch (operand)
			{
				case "x": oper = "x10"; break;
				case "r": oper = "sqrt"; break;
				case "sin": oper = "sine"; break;
				case "cos": oper = "cosine"; break;
				case "tan": oper = "tangent"; break;

				default: break;
			}
			calculationlist.Add(new Calculation { Number1 = number1, Operand = oper, Result = result });
		}
		public static void ViewList()
		{
			Console.Clear();
			Console.WriteLine("History of calculations: ");
			Console.WriteLine("--------------------------");
			foreach (var calc in calculationlist)
			{
				if (calc.Operand == "x10" || calc.Operand == "sqrt" || calc.Operand == "sine" || calc.Operand == "cosine" || calc.Operand == "tangent")
					Console.WriteLine($"{calc.Number1} {calc.Operand} = {calc.Result}");
				else
					Console.WriteLine($"{calc.Number1} {calc.Operand} {calc.Number2} = {calc.Result}");
			}
			Console.WriteLine("--------------------------");

		}
		public static double GetLatestResult()
		{
			var output = calculationlist.LastOrDefault();
			return output.Result;
		}
		public static void ClearList()
		{
			calculationlist.Clear();
		}
		public void Finish()
		{
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.Close();
		}
	}
}
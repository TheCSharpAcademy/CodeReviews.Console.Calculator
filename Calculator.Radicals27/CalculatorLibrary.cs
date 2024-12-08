using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CalculatorLibrary
{
	public class Calculator
	{
		JsonWriter writer;
		string logFilePath = "calculatorlog.json";
		string runCounter = "runCounter";
		public int RunCount;

		public Calculator()
		{
			RunCount = GetRunCount();

			StreamWriter logFile = File.CreateText(logFilePath);  // Create the log file
			logFile.AutoFlush = true;
			writer = new JsonTextWriter(logFile);  // Initialise the json writer to the logfile
			writer.Formatting = Formatting.Indented;
			writer.WriteStartObject();

			writer.WritePropertyName(runCounter);
			writer.WriteValue(RunCount);

			writer.WritePropertyName("Operations");
			writer.WriteStartArray();
		}

		// Gets how many times the app has been run, stored in log file
		private int GetRunCount()
		{
			JObject logData;
			int runCount = 1;

			if (File.Exists(logFilePath))
			{
				string json = File.ReadAllText(logFilePath);
				logData = JObject.Parse(json);

				if (logData.ContainsKey(runCounter))
				{
					runCount = (int)logData[runCounter] + 1;
				}
				else
				{
					runCount = 1;
				}
			}

			return runCount;
		}

		public string GetSymbolForCalculation(string input)
		{
			switch (input)
			{
				case "a":
					return "+";
				case "s":
					return "-";
				case "m":
					return "x";
				case "d":
					return "/";
				case "r":
					return "√";
				case "p":
					return "^2";
				case "t":
					return "x10";
				default:
					return "invalid calculation";
			}
		}

		public double DoOperationForTwoNumbers(double num1, double num2, string op)
		{
			double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
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
					// Ask the user to enter a non-zero divisor.
					if (num2 != 0)
					{
						result = num1 / num2;
					}
					writer.WriteValue("Divide");
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

		public void Finish()
		{
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.Close();
		}

		public double DoOperationForOneNumber(double num1, string op)
		{
			double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
			writer.WriteStartObject();
			writer.WritePropertyName("Operand1");
			writer.WriteValue(num1);
			writer.WritePropertyName("Operation");

			switch (op)
			{
				case "r":
					result = MathF.Sqrt((float)num1);
					writer.WriteValue("Square Root");
					break;
				case "p":
					result = num1 * num1;
					writer.WriteValue("Power");
					break;
				case "t":
					result = num1 * 10;
					writer.WriteValue("10x");
					break;
				default:
					break;
			}

			writer.WritePropertyName("Result");
			writer.WriteValue(result);
			writer.WriteEndObject();

			return result;
		}

		public struct CalculationData
		{
			public double num1;
			public double? num2;
			public double finalResult;
			public string operand;

			public CalculationData(double cleanNum1, double? cleanNum2, string op, double result) : this()
			{
				num1 = cleanNum1;
				num2 = cleanNum2;
				operand = op;
				finalResult = result;
			}
		}
	}
}
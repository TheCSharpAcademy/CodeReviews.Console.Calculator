using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{

  JsonWriter writer;
  double lastResult = 0;
  public List<string> calculations { get; set; } = new List<string>();
  public Calculator()
  {
    StreamWriter logFile = File.CreateText("calculator.log");
    Trace.AutoFlush = true;
    writer = new JsonTextWriter(logFile);
    writer.Formatting = Formatting.Indented;
    writer.WriteStartObject();
    writer.WritePropertyName("Operations");
    writer.WriteStartArray();
  }
  public double DoOperation(double num1, double num2, string op)
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
        calculations.Add("Add");
        break;
      case "s":
        result = num1 - num2;
        writer.WriteValue("Subtract");
        calculations.Add("Subtract");
        break;
      case "m":
        result = num1 * num2;
        writer.WriteValue("Multiply");
        calculations.Add("Multiply");
        break;
      case "d":
        if (num2 != 0)
        {
          result = num1 / num2;
          writer.WriteValue("Divide");
          calculations.Add("Divide");
        }
        break;
      case "p":
        result = Math.Pow(num1, num2);
        writer.WriteValue("Power");
        calculations.Add("Power");
        break;
      default:
        break;
    }
    lastResult = result;
    writer.WritePropertyName("Result");
    writer.WriteValue(result);
    writer.WriteEndObject();
    return result;
  }

  public void Finish()
  {
    writer.WriteEndArray();
    writer.WritePropertyName("CalculationHistory");
    writer.WriteValue(String.Join(", ", calculations));
    writer.WriteEndObject();
    writer.Close();
  }

  public void RemoveSavedHistory()
  {

  }
}

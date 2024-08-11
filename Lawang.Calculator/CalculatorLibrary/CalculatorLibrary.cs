using System.Diagnostics;
using System.Formats.Asn1;
using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{

    JsonWriter writer;
    public List<Result> ResultList = new List<Result>();

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
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        switch (op)
        {
            case "+":
                result = num1 + num2;
                ResultList.Add(new Result
                {
                    Operand1 = num1,
                    Operand2 = num2,
                    Operation = op,
                    Answer = result
                });
                writer.WriteValue("Add");
                break;
            case "-":
                result = num1 - num2;
                ResultList.Add(new Result
                {
                    Operand1 = num1,
                    Operand2 = num2,
                    Operation = op,
                    Answer = result
                });
                writer.WriteValue("Subtract");
                break;
            case "*":
                result = num1 * num2;
                ResultList.Add(new Result
                {
                    Operand1 = num1,
                    Operand2 = num2,
                    Operation = op,
                    Answer = result
                });
                writer.WriteValue("Multiply");
                break;
            case "/":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    ResultList.Add(new Result
                    {
                        Operand1 = num1,
                        Operand2 = num2,
                        Operation = op,
                        Answer = result
                    });
                }
                writer.WriteValue("Divide");
                break;

            case "^":
                result = Math.Pow(num1, num2);
                ResultList.Add(new Result
                {
                    Operand1 = num1,
                    Operand2 = num2,
                    Operation = op,
                    Answer = result
                });
                writer.WriteValue("Power");
                break;

            case "r":
                result = Math.Sqrt(num1);
                ResultList.Add(new Result
                {
                    Operand1 = num1,
                    Operand2 = null,
                    Operation = "Square root",
                    Answer = result
                });
                writer.WriteValue("Square root");
                break;

            case "s":
                result = Math.Sin(num1);
                ResultList.Add(new Result
                {
                    Operand1 = num1,
                    Operand2 = null,
                    Operation = op,
                    Answer = result
                });
                writer.WriteValue("Sine");
                break;

            case "c":
                result = Math.Cos(num1);
                ResultList.Add(new Result
                {
                    Operand1 = num1,
                    Operand2 = null,
                    Operation = op,
                    Answer = result
                });

                writer.WriteValue("Cosine");
                break;

            case "t":
                result = Math.Tan(num1);
                ResultList.Add(new Result
                {
                    Operand1 = num1,
                    Operand2 = null,
                    Operation = op,
                    Answer = result
                });

                writer.WriteValue("Tangent");
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

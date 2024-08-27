global using System.Net.Sockets;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    
    public class Calculator
    {
        JsonWriter writer;

        public List<Result> ResultsList = new List<Result>();

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("Calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operation");
            writer.WriteStartArray();
           
        }
        public double OperationToDo(double firstNumber, double secondNumber, string op)
        {
            double result = double.NaN;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(firstNumber);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(secondNumber);
            writer.WritePropertyName("Operation");

            switch(op)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    ResultsList.Add(new Result
                    {
                        Operand1 = firstNumber,
                        Operand2 = secondNumber,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Add");
                    
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    ResultsList.Add(new Result
                    {
                        Operand1 = firstNumber,
                        Operand2 = secondNumber,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Subtract");
                    
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    ResultsList.Add(new Result
                    {
                        Operand1 = firstNumber,
                        Operand2 = secondNumber,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Multiply");
                    
                    break;
                case "/":
                    if(secondNumber != 0)
                    {
                        result = firstNumber / secondNumber;
                        ResultsList.Add(new Result
                        {
                            Operand1 = firstNumber,
                            Operand2 = secondNumber,
                            Operation = op,
                            Answer = result
                        });
                        writer.WriteValue("Divide");
                        
                    }
                    
                    break;
                case "^":
                    result = Math.Pow(firstNumber, secondNumber);
                    ResultsList.Add(new Result
                    {
                        Operand1 = firstNumber,
                        Operand2 = secondNumber,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Power");
                    
                    break;
                case "r":
                    result = Math.Sqrt(firstNumber);
                    ResultsList.Add(new Result
                    {
                        Operand1 = firstNumber,
                        Operand2 = null,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Square root");
                    
                    break;
                case "c":
                    result = Math.Cos(firstNumber);
                    ResultsList.Add(new Result
                    {
                        Operand1 = firstNumber,
                        Operand2 = null,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Cosine");
                    
                    break;
                case "s":
                    result = Math.Sin(firstNumber);
                    ResultsList.Add(new Result
                    {
                        Operand1 = firstNumber,
                        Operand2 = null,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Sine");
                    
                    break;
                case "t":
                    result = Math.Tan(firstNumber);
                    ResultsList.Add(new Result
                    {
                        Operand1 = firstNumber,
                        Operand2 = null,
                        Operation = op,
                        Answer = result
                    });
                    writer.WriteValue("Tangant");
                    
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

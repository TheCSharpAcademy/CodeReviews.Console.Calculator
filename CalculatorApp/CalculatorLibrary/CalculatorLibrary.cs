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
            //Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            //Trace.AutoFlush = true;
            //Trace.WriteLine("Starting Calculator Log");
            //Trace.WriteLine(string.Format("Started {0}"), System.DateTime.Now.ToString());
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
                    //Trace.WriteLine(string.Format("{0} + {1} = {2}", firstNumber, secondNumber, result));
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
                    //Trace.WriteLine(string.Format("{0} - {1} = {2}", firstNumber, secondNumber, result));
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
                    //Trace.WriteLine(string.Format("{0} * {1} = {2}", firstNumber, secondNumber, result));
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
                        //Trace.WriteLine(string.Format("{0} / {1} = {2}", firstNumber, secondNumber, result));
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
                    //Trace.WriteLine(string.Format("{0} + {1} = {2}", firstNumber, secondNumber, result));
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
                    //Trace.WriteLine(string.Format("{0} = {2}", firstNumber, result));
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
                    //Trace.WriteLine(string.Format("{0} = {2}", firstNumber, result));
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
                    //Trace.WriteLine(string.Format("{0} = {2}", firstNumber, result));
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
                    //Trace.WriteLine(string.Format("{0} = {2}", firstNumber, result));
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

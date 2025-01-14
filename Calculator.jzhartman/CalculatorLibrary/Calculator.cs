using System.Diagnostics;
using CalculatorLibrary.Models;
using Newtonsoft.Json;
using CalculatorLibrary.Enums;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public List<CalculationModel> History { get; set; }
        public int TimesUsed { get; set; }

        public Calculator()
        {
            this.History = new List<CalculationModel>();
            this.TimesUsed = 0;

            StreamWriter logFile = File.CreateText("calculator.log");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(CalculationModel calculation)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(calculation.Operand1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(calculation.Operand2);
            writer.WritePropertyName("Operation");
            writer.WriteValue(Enum.GetName(calculation.Operation));

            switch (calculation.Operation)
            {
                case Operation.Add:
                    result = calculation.Operand1 + calculation.Operand2;
                    
                    break;
                case Operation.Subtract:
                    result = calculation.Operand1 - calculation.Operand2;
                    break;
                case Operation.Multiply:
                    result = calculation.Operand1 * calculation.Operand2;
                    break;
                case Operation.Divide:
                    if (calculation.Operand2 != 0)
                    {
                        result = calculation.Operand1 / calculation.Operand2;
                    }
                    break;
                case Operation.SquareRoot:
                    if (calculation.Operand1 > 0)
                    {
                        result = Math.Sqrt(calculation.Operand1);
                    }
                    break;
                case Operation.Root:
                    result = Math.Pow(calculation.Operand1, 1.0 / calculation.Operand2);
                    break;
                case Operation.Power:
                    result = Math.Pow(calculation.Operand1, calculation.Operand2);
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

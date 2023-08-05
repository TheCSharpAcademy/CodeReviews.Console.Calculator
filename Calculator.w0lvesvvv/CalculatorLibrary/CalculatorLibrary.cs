using Newtonsoft.Json;
using System.Diagnostics;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private JsonWriter writer;
        private int usesCount;
        private List<string> listCalculations = new List<string>();

        #region CTOR
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
        #endregion



        #region PUBLIC METHODS
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
                    listCalculations.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    listCalculations.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    listCalculations.Add($"{num1} x {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    listCalculations.Add($"{num1} / {num2} = {result}");
                    break;
                case "r":
                    result = Math.Sqrt(num2);
                    writer.WriteValue("Square Root");
                    listCalculations.Add($"Sqrt({num2}) = {result}");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    listCalculations.Add($"Power({num1}, {num2}) = {result}");
                    break;
                case "x":
                    result = num2 * 10;
                    writer.WriteValue("10x");
                    listCalculations.Add($"10x {num2} = {result}");
                    break;
                case "t":
                    result = Math.Sin(num2 * Math.PI / 180.0);
                    writer.WriteValue("Trigonometry Function Sin");
                    listCalculations.Add($"Sin({num2}) = {result}");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            usesCount++;
            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public int GetUsesCount() { return usesCount; }

        public void PrintLastCalculations()
        {
            listCalculations.Reverse();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var calculation in listCalculations)
            {
                Console.WriteLine(calculation);
            }

            listCalculations.Reverse();
        }
        #endregion

        #region OLD TRACE LOG
        //public Calculator()
        //{
        //    StreamWriter logFile = File.CreateText("calculator.log");
        //    Trace.Listeners.Add(new TextWriterTraceListener(logFile));
        //    Trace.AutoFlush = true;
        //    Trace.WriteLine("Starting Calculator Log");
        //    Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
        //}

        //public double DoOperation(double num1, double num2, string op)
        //{
        //    double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        //    // Use a switch statement to do the math.
        //    switch (op)
        //    {
        //        case "a":
        //            result = num1 + num2;
        //            Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
        //            break;
        //        case "s":
        //            result = num1 - num2;
        //            Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, result));
        //            break;
        //        case "m":
        //            result = num1 * num2;
        //            Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, result));
        //            break;
        //        case "d":
        //            // Ask the user to enter a non-zero divisor.
        //            if (num2 != 0)
        //            {
        //                result = num1 / num2;
        //                Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, result));
        //            }
        //            break;
        //        // Return text for an incorrect option entry.
        //        default:
        //            break;
        //    }
        //    return result;
        //}
        #endregion
    }
}
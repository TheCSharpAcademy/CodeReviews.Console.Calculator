using System.IO;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculator.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(string.Format("Started {0}", System.DateTime.Now.ToString()));
        }

        public double Operation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.  

            // Use a switch statement to do the math.  
            switch (op)
            {
                case "1":
                    result = num1 + num2;
                    Trace.WriteLine(string.Format("{0} + {1} = {2}", num1, num2, result));
                    break;
                case "2":
                    result = num1 - num2;
                    Trace.WriteLine(string.Format("{0} - {1} = {2}", num1, num2, result));
                    break;
                case "3":
                    result = num1 * num2;
                    Trace.WriteLine(string.Format("{0} * {1} = {2}", num1, num2, result));
                    break;
                case "4":
                    // Ask the user to enter a non-zero divisor.  
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        Trace.WriteLine(string.Format("{0} / {1} = {2}", num1, num2, result));
                    }
                    break;
                // Return text for an incorrect option entry.  
                default:
                    break;
            }
            return result;
        }
    }
}

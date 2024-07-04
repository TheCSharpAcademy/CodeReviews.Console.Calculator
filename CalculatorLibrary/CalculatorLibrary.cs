using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorLibrary
{
    public class CalculatorHandler
    {
        private const string _logFileName = "calculatorlog.json";
        public CalculatorUseStatistics CalculatorUseStatistics { get; set; }

        public CalculatorHandler()
        {
            using (FileStream fs = new FileStream(_logFileName, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    CalculatorUseStatistics = JsonConvert.DeserializeObject<CalculatorUseStatistics>(sr.ReadToEnd());
                    if (CalculatorUseStatistics == null)
                    {
                        CalculatorUseStatistics = new CalculatorUseStatistics()
                        {
                            Operations = new List<Operation>(),
                            CountOfCalculations = 0,
                            LatestCalculations = new List<Operation>(),
                        };
                    }
                }
            }
        }
        public double DoOperation(double num1, double num2, string op)
        {
            CalculatorUseStatistics.CountOfCalculations++;
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            Operation operation = new Operation()
            {
                OperandLeft = num1,
                OperandRight = num2,
            };
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    operation.Operator = "Add";
                    break;
                case "s":
                    result = num1 - num2;
                    operation.Operator = "Subtract";
                    break;
                case "m":
                    result = num1 * num2;
                    operation.Operator = "Multiply";
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        operation.Operator = "Divide";
                    }
                    break;
                case "pow":
                    result = Math.Pow(num1, num2);
                    operation.Operator = "Exponentiation";
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            operation.Result = result;
            CalculatorUseStatistics.Operations.Add(operation);
            CalculatorUseStatistics.LatestCalculations.Add(operation);

            return result;
        }

        public double DoMathFunction(double x, string op)
        {
            CalculatorUseStatistics.CountOfCalculations++;
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            Operation operation = new Operation()
            {
                OperandLeft = x,
            };
            // Use a switch statement to do the math.
            switch (op)
            {
                case "root":
                    result = Math.Sqrt(x);
                    operation.Operator = "Square root";
                    break;
                case "pow10":
                    result = Math.Pow(10, x);
                    operation.Operator = "Raise 10 to the power x";
                    break;
                case "sin":
                    result = Math.Sin(DegToRad(x));
                    operation.Operator = "sin(x)";
                    break;
                case "cos":
                    result = Math.Cos(DegToRad(x));
                    operation.Operator = "cos(x)";
                    break;
                case "tan":
                    result = Math.Tan(DegToRad(x));
                    operation.Operator = "tan(x)";
                    break;
                case "cot":
                    result = 1.0 / Math.Tan(DegToRad(x));
                    operation.Operator = "cot(x)";
                    break;
                default:
                    break;
            }
            operation.Result = result;
            CalculatorUseStatistics.Operations.Add(operation);
            CalculatorUseStatistics.LatestCalculations.Add(operation);

            return result;
        }

        private double DegToRad(double deg)
        {
            return (deg * Math.PI) / 180;
        }

        public void Finish()
        {
            string calculatorUseStatistics = JsonConvert.SerializeObject(CalculatorUseStatistics, Formatting.Indented);
            using (FileStream fs = new FileStream(_logFileName, FileMode.Create))
            {
                byte[] buffer = Encoding.Default.GetBytes(calculatorUseStatistics);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        public void ClearLatestCalculations()
        {
            CalculatorUseStatistics.LatestCalculations.Clear();
        }

        public int InputNumberHandler()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    return number;
                }
                Console.WriteLine("Need input a number!");
            }
        }

        public double InputOperandHandler()
        {
            Console.Write("Type a number or symbol \"l\" for use results of latest calculations, and then press Enter: ");

            while (true)
            {
                string? input = Console.ReadLine();

                if (input != null && input == "l")
                {
                    if (CalculatorUseStatistics.LatestCalculations.Count > 0)
                    {
                        Console.WriteLine("Latest calculations list, type a number for choose result:");
                        for (int i = 0; i < CalculatorUseStatistics.LatestCalculations.Count; i++)
                        {
                            Operation operation = CalculatorUseStatistics.LatestCalculations[i];
                            Console.WriteLine($"{i + 1}. {operation.OperandLeft} {operation.Operator} {operation.OperandRight} = {operation.Result}");
                        }

                        return CalculatorUseStatistics.LatestCalculations[InputNumberHandler() - 1].Result;
                    }
                    else
                    {
                        Console.WriteLine("Latest calculations list is empty.");
                    }
                }

                if (double.TryParse(input, out double cleanNum))
                {
                    return cleanNum;
                }
                Console.Write("This is not valid input. Please enter a numeric value or symbol \"l\": ");
            }
        }
    }
}

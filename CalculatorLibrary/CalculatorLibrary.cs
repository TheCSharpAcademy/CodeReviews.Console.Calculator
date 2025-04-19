using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public List<double> Results;
        public int Counter = 0;

        public Calculator()
        {
            Results = new List<double>();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    break;

                case "s":
                    result = num1 - num2;
                    break;

                case "m":
                    result = num1 * num2;
                    break;

                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0) result = num1 / num2;
                    break;

                case "v":
                    result = (num1 + num2) / 2;
                    break;

                case "w":
                    if (num1 < 0 && num2 % 1 != 0)
                    {
                        Console.WriteLine("Cannot calculate the power of a negative base with a fractional exponent.");
                    }
                    else
                    {
                        result = Math.Pow(num1, num2);
                    }
                    break;

                case "r":
                    if (num1 >= 0)
                    {
                        result = Math.Sqrt(num1);
                    }
                    else
                    {
                        Console.WriteLine("Cannot calculate the square root of a negative number.");
                    }
                    break;

                case "n":
                    result = Math.Sin(num1);
                    break;

                case "c":
                    result = Math.Cos(num1);
                    break;

                case "t":
                    result = Math.Tan(num1);
                    break;

                case "l":
                    if (num1 > 0)
                    {
                        result = Math.Log(num1);
                    }
                    else
                    {
                        Console.WriteLine("Cannot calculate the logarithm of a non-positive number.");
                    }
                    break;

                case "h":
                    result = Math.Log10(num1);
                    break;


                default:
                    Console.WriteLine("Invalid operation.");
                    break;
            }
            // Log the operation to the JSON file
            LogOperation(num1, num2, op, result);

            Counter++;
            Results.Add(result);
            return result;
        }

        private void LogOperation(double num1, double num2, string op, double result)
        {
            var newOperation = new OperationInfo
            {
                Operand1 = num1,
                Operand2 = num2,
                Operation = op,
                Result = result
            };

            // Write to the JSON file
            string filePath = "calculatorlog.json";
            CalculatorLog log;

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    log = JsonConvert.DeserializeObject<CalculatorLog>(json) ?? new CalculatorLog { Operations = new List<OperationInfo>() };
                }
                catch (JsonException)
                {
                    Console.WriteLine("Error reading history: JSON corrupted. Starting new history.");
                    log = new CalculatorLog { Operations = new List<OperationInfo>() };
                }
            }
            else
            {
                log = new CalculatorLog { Operations = new List<OperationInfo>() };
            }

            log.Operations.Add(newOperation);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(log, Formatting.Indented));
        }
    }
}

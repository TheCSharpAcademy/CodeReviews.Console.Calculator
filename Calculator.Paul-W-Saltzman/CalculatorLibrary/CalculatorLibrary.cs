using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private JsonTextWriter writer;
        public List<PastCalculations> calculations = new List<PastCalculations>();

        public int NumberOfUses { get; private set; } // Make the NumberOfUses property private set to prevent external modification.

        public Calculator()
        {
            // Initialize the JSON log file and writer
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile)
            {
                Formatting = Formatting.Indented
            };

            // Start the JSON structure
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();

            // Initialize NumberOfUses
            NumberOfUses = 0;
        }
        public void AddUse()
        {
            NumberOfUses++;
        }


        public void ListPastCalculations(Calculator calculator)
        {
            Console.WriteLine("Past Calculations:");
            foreach (var calculation in calculator.calculations)
            {
                Console.WriteLine($"Number1: {calculation.Number1}");
                // Check if Number2 is not null or not equal to a default value (e.g., 0)
                if (calculation.Number2 != null && calculation.Number2 != 0)
                {
                    Console.WriteLine($"Number2: {calculation.Number2}");
                }
                Console.WriteLine($"Operation: {calculation.Operation}");
                Console.WriteLine($"Result: {calculation.Result}");
                Console.WriteLine();
            }
        }

        public double DoOperation(double num, string op)
        {
            string operation = "";
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand");
            writer.WriteValue(num);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "f":
                    if (num > 1)
                    {
                        result = 1;
                        for (int i = 2; i <= num; i++)
                        {
                            result *= i;
                        }
                    }
                    else if (num == 1)
                    {
                        result = 1; // Factorial of 1 is 1.
                    }
                    writer.WriteValue("Factorial");
                    operation = "Factorial";
                    break;
                case "r":
                    result = Math.Sqrt(num);
                    writer.WriteValue("SquareRoot");
                    operation = "SquareRoot";
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            // Add the calculation to the list
            calculations.Add(new PastCalculations(num, operation, result));

            // Increment NumberOfUses

            return result;
        }

        public double DoOperation(double num1, double num2, string op)
        {
            string operation = "";
            double result = double.NaN;
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
                    operation = "Add";
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    operation = "Subtract";
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    operation = ("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        operation = "Divide";
                    }
                    break;
                case "e":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Exponent");
                    operation = ("Exponent");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            // Add the calculation to the list
            calculations.Add(new PastCalculations(num1, num2, operation, result));

            // Increment NumberOfUses

            return result;
        }

        public void Finish()
        {
            // Finish writing the JSON log and close the writer
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}


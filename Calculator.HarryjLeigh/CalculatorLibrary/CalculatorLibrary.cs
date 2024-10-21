using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public int CalculationsCount { get; private set; } = 0;
        public List<double> PreviousCalculations { get; private set; } = new List<double>();
        public Calculator()
        {
            // Creates  and opens JSON file
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        // Closes JSON file
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public double DoOneNumberOperation(double num1, string op) 
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.]
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "sqrt":
                   result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
                case "10x":
                    result = num1 * 10;
                    writer.WriteValue("10x");
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
                case "tan":
                    result = Math.Tan(num1);
                // change below into function
                    writer.WriteValue("Tan");
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;
        }

        public double DoTwoNumberOperation(double num1, double num2, string op)
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
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        IncreaseCalculationsCount();
                        AddToPreviousCalculations(result);
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    IncreaseCalculationsCount();
                    AddToPreviousCalculations(result);
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;
        }

        private void IncreaseCalculationsCount() => CalculationsCount++;
        public void DeletePreviousCalculations() => PreviousCalculations.Clear();
        private void AddToPreviousCalculations(double result) => PreviousCalculations.Add(result);

        public string UsePreviousCalculations()
        {
            string? numFromPrevious = "";
            // Writes previous calculations to console to choose from
            Console.WriteLine();
            Console.Write("Previous Calculations: ");
            foreach (double result in PreviousCalculations)
            {
                if (PreviousCalculations.Last() == result) Console.Write($"{result} ");
                else Console.Write($"{result}, ");
            }

            // Checks whether choice is from previous calculations list
            while (numFromPrevious == "" || numFromPrevious == null)
            {
                Console.WriteLine();
                Console.WriteLine("Select a number to use: ");
                numFromPrevious = Console.ReadLine();

                // check if choice is valid if not asks user again for number
                while (!PreviousCalculations.Contains(Convert.ToDouble(numFromPrevious)))
                {
                    Console.WriteLine("Select a number from previous calculations: ");
                    numFromPrevious = Console.ReadLine();
                }
            }
            return numFromPrevious;
        }
    }
}

using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public string pattern = @"\A(sr|x|sin|cos|tan)\Z";
        List<string> calculations = new List<string>();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculator.json"); // StreamWriter implements a TextWriter for writing characters to a stream in a particular encoding
            logFile.AutoFlush = true; // Calls Flush() after every write, which flushes the output buffer and causes buffered data to be written
            writer = new JsonTextWriter(logFile); // Represents a writer that provides a fast, non-cached, forward-only way of generating JSON data
            writer.Formatting = Formatting.Indented; // Causes child objects to be indented
            writer.WriteStartObject(); // Writes a JSON start object ({)
            writer.WritePropertyName("Operations"); // Writes the property name of the name/value pair
            writer.WriteStartArray(); // Writes a JSON start array ([)
        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not a number" to avoid division by 0 errors
            double angleInRadians = num1 * Math.PI / 180; // Convert angle to radians for use with trigonometric functions
            writer.WriteStartObject(); // Writes a JSON start object ({)
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            // Write Operand2 if operation selected requires one
            if (!Regex.IsMatch(op, pattern))
            {
                writer.WritePropertyName("Operand2");
                writer.WriteValue(num2);
            }
            writer.WritePropertyName("Operation");


            // Switch statement to do the math
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    calculations.Add($"{num1} + {num2} = {result:0.##}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calculations.Add($"{num1} - {num2} = {result:0.##}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculations.Add($"{num1} * {num2} = {result:0.##}");
                    break;
                case "d":
                    // Make sure divisor is non-zero
                    if (num2 != 0)
                        result = num1 / num2;
                    writer.WriteValue("Divide");
                    calculations.Add($"{num1} / {num2} = {result:0.##}");
                    break;
                case "sr":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square root");
                    calculations.Add($"Square root of {num1} = {result:0.##}");
                    break;
                case "e":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Exponentiate");
                    calculations.Add($"{num1} to the power of {num2} = {result:0.##}");
                    break;
                case "x":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10x");
                    calculations.Add($"10 to the power of {num1} = {result:0.##}");
                    break;
                case "cos":
                    result = Math.Cos(angleInRadians);
                    writer.WriteValue("Cosine");
                    calculations.Add($"Cosine of {num1} = {result:0.##}");
                    break;
                case "sin":
                    result = Math.Sin(angleInRadians);
                    writer.WriteValue("Sine");
                    calculations.Add($"Sine of {num1} = {result:0.##}");
                    break;
                case "tan":
                    result = Math.Tan(angleInRadians);
                    writer.WriteValue("Tangent");
                    calculations.Add($"Tangent of {num1} = {result:0.##}");
                    break;
                // Return text displaying an incorrect option was input
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject(); // Writes a JSON end object (})

            return result;
        }

        public string GetMathOperator()
        {
            Console.WriteLine("Choose an operator from the following list:\n" +
                "\ta - Add\n" +
                "\ts - Subtract\n" +
                "\tm - Multiply\n" +
                "\td - Divide\n" +
                "\tsr - Square Root\n" +
                "\te - Exponentiate\n" +
                "\tx - 10x\n" +
                "\tsin - Sine\n" +
                "\tcos - Cosine\n" +
                "\ttan - Tangent\n" +
                "\tv - View Previous Caluclations\n" +
                "Your choice? ");

            string? op = Console.ReadLine().ToLower();

            // Validate user input is not null and matches one of the available choices
            while (op == null || !Regex.IsMatch(op, @"\A(a|s|m|d|e|sr|x|sin|cos|tan|v)\Z")) // Makes input have to match options exactly; \A matches beginning of string; \Z matches end of string, before new line;
            {
                Console.WriteLine("Invalid input. Please enter a valid operation:");
                op = Console.ReadLine().ToLower();
            }
            return op;
        }

        public double GetPreviousResults()
        {
            Console.Clear();
            if (calculations.Count == 0)
            {
                Console.WriteLine("No previous results available. Press Enter to go back to menu:");
                Console.ReadLine();
                return double.NaN;
            }

            int count = 0;
            int resultChoice;
            Console.WriteLine("Previous Calculations\n" +
                "---------------------");
            foreach (var result in calculations)
            {
                // Print each previous calculation
                Console.WriteLine($"{count + 1}. {result}");
                count++;
            }
            var indexRange = calculations.Count == 1 ? "1" : $"1-{calculations.Count}";
            Console.WriteLine("---------------------");
            Console.WriteLine($"Choose an option:\n" +
                $"\t{indexRange} - Use a previous result for new calculation\n" +
                $"\td - Delete previous calculations\n" +
                $"\tEnter - Return to main menu\n" +
                $"Your choice?");
            var selection = Console.ReadLine();
            if (int.TryParse(selection, out resultChoice)) // Check if a number was entered by user
            {
                if (resultChoice > 0 && resultChoice <= calculations.Count) // Check if provided number is an index in the calculations list
                {
                    string[] calcStrings = calculations[resultChoice - 1].Split(new char[] { ' ' }); // Grab calculation string at given index, split it at each space
                    return double.Parse(calcStrings[^1]); // Return last index of the split strings, since that's where the result of the calcuation is stored
                }
            }
            // Clear calculations list if user selects the option
            else if (selection == "d")
            {
                calculations.Clear();
                Console.Clear();
                Console.WriteLine("Previous calcuations deleted. Press Enter to return to the main menu:");
                Console.ReadLine();
            }
            return double.NaN; // Return NaN if anything but a valid calculations index is provided
        }

        public void Finish()
        {
            writer.WriteEndArray(); // Writes a JSON end array (])
            writer.WriteEndObject(); // Writes a JSON end object (})
            writer.Close(); // Closes the JSON stream
        }
    }
}

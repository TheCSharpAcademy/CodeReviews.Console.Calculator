using Newtonsoft.Json;
namespace CalculatorLibrary
{
    /// <summary>
    /// The class for the calculator itself, contains operations and helper functions
    /// </summary>
    public class Calculator
    {
        /// <value>JsonWriter instance used for writing into JSON file</value>
        JsonWriter writer;

        /// <value>List of doubles used to store recent calculations to be available later</value>
        List<double> calculationsResults = new(5);

        /// <summary>
        /// Calculator constructor
        /// </summary>
        public Calculator()
        {
            // JSON writer preparations
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        /// <summary>
        /// This method completes the math operation based on the operator and operands inputed by the user
        /// </summary>
        /// <param name="num1">First operand</param>
        /// <param name="num2">Second operand</param>
        /// <param name="op">Operator</param>
        /// <returns>The result of op operation</returns>
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
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                case "root":
                    result = Math.Pow(num1, 1.0 / num2);
                    writer.WriteValue($"{num2} root of the first number");
                    break;
                case "pow":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue($"Power of {num2}");
                    break;
                case "t":
                    result = num1 * 10;
                    writer.WriteValue("Times 10");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sinus of the first number");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosinus of the first number");
                    break;
                case "tg":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tangens of the first number");
                    break;
                case "cotg":
                    result = 1 / Math.Tan(num1);
                    writer.WriteValue("Cotangens of the first number");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        /// <summary>
        /// Finalizer method for JSON writer, closes the file
        /// </summary>
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        /// <summary>
        /// Helper method for saving the operation result to the list of recent calculations
        /// </summary>
        /// <param name="result">Desired number that will be saved</param>
        public void AddResultToMemory(double result)
        {
            if (calculationsResults.Count == calculationsResults.Capacity)
            {
                Console.WriteLine("Unable to add result to memory (memory is full).\nPlease delete memory to be able to save results.");
            }
            else
            {
                calculationsResults.Add(result);
                Console.WriteLine("Result saved to memory.");
            }
        }

        /// <summary>
        /// Method for clearing the calculator memory when it is full.
        /// </summary>
        public void DeleteMemory()
        {
            calculationsResults.Clear();
            Console.WriteLine("Memory sucessfuly deleted. Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Helper method for displaying the contents of calculationsResults list.
        /// </summary>
        public void DisplayMemory()
        {
            Console.WriteLine("Recent calculations:");
            Console.Write("------------------------\n");

            for (int i = 0; i < calculationsResults.Count; i++)
            {
                Console.WriteLine($"{i}: {calculationsResults[i].ToString()}");
            }
        }

        /// <summary>
        /// Helper method for selecting a recent calculation for use in another calculation
        /// </summary>
        /// <returns>A double typed result from the list</returns>
        public double SelectResultFromMemory()
        {
            bool validInput = false;

            Console.WriteLine("Select a number from the list below:");
            DisplayMemory();

            int parsedIndex = 0;

            while (!validInput)
            {
                string? selectedIndex = Console.ReadLine();
                bool parsable = int.TryParse(selectedIndex, out parsedIndex);

                if (!parsable)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                else if (parsedIndex < 0 ||  parsedIndex >= calculationsResults.Count)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                else
                {
                    validInput = true;
                }
            }

            return calculationsResults[parsedIndex];
        }

        /// <summary>
        /// Helper method for checking whether the list is empty or not
        /// </summary>
        /// <returns>True if calculationsResults is empty, false otherwise</returns>
        public bool IsMemoryEmpty()
        {
            return calculationsResults.Count == 0;
        }
    }
}

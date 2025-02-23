using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class CalculatorBrain
    {
        // Initialize necesarry variables
        JsonWriter jsonWriter; 
        List<Data> history = new();

        bool isFromHistory;
        double fromHistory;

        // Create and configure a new log file for storing operations in JSON format
        public CalculatorBrain()
        {            
            StreamWriter logFile = File.CreateText("calculator.log");
            logFile.AutoFlush = true;
            jsonWriter = new JsonTextWriter(logFile);
            jsonWriter.Formatting = Formatting.Indented;
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("Operations");
            jsonWriter.WriteStartArray();
        }
        // Perform the selected operation and log it in the JSON file
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            jsonWriter.WriteStartObject();

            // Log operands
            jsonWriter.WritePropertyName("Operand1");
            jsonWriter.WriteValue(num1);
            jsonWriter.WritePropertyName("Operand2");
            jsonWriter.WriteValue(num2);
            jsonWriter.WritePropertyName("Operation");

            // Switch to perform the appropriate operation
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    jsonWriter.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    jsonWriter.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    jsonWriter.WriteValue("Multiply");
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        jsonWriter.WriteValue("Divide");
                    }
                    break;
                default:
                    break;
            }

            // Log result
            jsonWriter.WritePropertyName("Result");
            jsonWriter.WriteValue(result);
            jsonWriter.WriteEndObject();

            return result;
        }
        // Add the operation details to the history list
        public void AddToHistory(double num1, double num2, string op, double result)
        {
            history.Add(new Data
            {
                Num1 = num1,
                Num2 = num2,
                Result = result,
                Operation = op
            });
        }
        // Print the history of operations
        public void PrintHistory()
        {
            Console.Clear();
            Console.WriteLine("Calculator History");
            Console.WriteLine("------------------");

            // Display all past operations from history
            for (int i = 0; i <= history.Count - 1; i++)
            {
                Console.WriteLine($"{i + 1}: {history[i].Num1} {history[i].Operation} {history[i].Num2} = {history[i].Result}");
            }
            Console.WriteLine("------------------");
            Console.WriteLine("Press \"Delete\" if you wish to delete the history or anything else to return to menu.");

            // Allow the user to clear history by pressing the Delete key
            if (Console.ReadKey().Key == ConsoleKey.Delete)
            {
                history.Clear();
            }
        }
        // Print history in a format for using past results in new operations
        public void PrintHistoryForInput()
        {
            Console.Clear();
            Console.WriteLine("Calculator History");
            Console.WriteLine("------------------");

            // Display all history entries
            for (int i = 0; i <= history.Count - 1; i++)
            {
                Console.WriteLine($"{i + 1}: {history[i].Num1} {history[i].Operation} {history[i].Num2} = {history[i].Result}");
            }
            Console.WriteLine("------------------");
        }

        // Get a result from history and prepare it for use in a new operation
        public void InputFromHistory(int index)
        {
            if (history.Count != 0)
            {
                if (index > history.Count)
                {
                    Console.WriteLine("Number can't be bigger than the history count!");
                }
                else if (index < 1)
                {
                    Console.WriteLine("Number can't be smaller than 1!");
                }
                else
                {
                    isFromHistory = true;
                    fromHistory = history[index - 1].Result;
                }
            }
            else
            {
                Console.WriteLine("History is empty!");
            }
        }
        // Get two numbers from the user for an operation (either new input or history result + second number)
        public double[] InputNumbers()
        {
            string? numInput1 = "";
            string? numInput2 = "";

            double cleanNum1 = 0;
            double cleanNum2 = 0;

            if (!isFromHistory) // If not using history, prompt for both numbers
            {
                Console.WriteLine("\nType a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                // Validate the first input
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                Console.WriteLine("\nType another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                // Validate the second input
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                double[] result = { cleanNum1, cleanNum2 };
                return result;
            }
            else // If using history, get the second number only
            {
                Console.WriteLine($"First number assigned from chosen history result. History result: {fromHistory}\n");
                Console.WriteLine("Type second number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                // Validate the second input
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                double[] result = { fromHistory, cleanNum2 };
                isFromHistory = false; // Reset the history flag
                return result;
            }
        }

        // Finish the calculation session and save the usage count in the log
        public void Finish(int calcUsed)
        {
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("Calculator Used:");
            jsonWriter.WriteValue(calcUsed);
            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndArray();
            jsonWriter.WriteEndObject();
            jsonWriter.Close();
        }
    }
}

namespace CalculatorLibrary.TheNigerianNerd
{
    using System.Diagnostics;
    using Newtonsoft.Json;
    public class Calculator
    {
        JsonWriter writer;
        int operationCount = 0; // This variable is not used in the current code but can be useful for tracking the number of operations performed.
        List<Operation> operations = new List<Operation>(); // This list is not used in the current code but can be useful for storing operations if needed.
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
        // CalculatorLibrary.cs
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Determine the operation type based on user input.
            OperationType operationType = OperationType.None;

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    operationType = OperationType.Addition;
                    result = num1 + num2;
                    writer.WriteValue(operationType);
                    break;
                case "s":
                    operationType = OperationType.Subtraction;
                    result = num1 - num2;
                    writer.WriteValue(operationType);
                    break;
                case "m":
                    operationType = OperationType.Multiplication;
                    result = num1 * num2;
                    writer.WriteValue(operationType);
                    break;
                case "d":
                    operationType = OperationType.Divison;
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue(operationType);
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            // Store the operation in the list for potential future use.
            operations.Add(new Operation(num1, num2, operationType, result));
            operationCount = operations.Count; // Increment the operation count each time an operation is performed.

            return result;
        }
        // CalculatorLibrary.cs
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
        public void ListOperations()
        {
            // Check if there are any operations to display.
            if (operations.Count == 0)
            {
                Console.WriteLine("No operations have been performed yet.");
                return;
            }
            // Display the number of operations performed.
            for (int i = 0; i < operations.Count; i++)
            {
                // Print each operation in a formatted way.
                Operation op = operations[i];
                Console.WriteLine($@"
{i + 1,-3}) Operation Summary
    ─────────────────────────────
    Operand 1 : {op.Operand1,10:N2}
    Operand 2 : {op.Operand2,10:N2}
    Operation : {op.OperationType}
    Result    : {op.Result,10:N2}
");

            }
            Console.WriteLine("To delete an item from the list, enter 'd', to carry out another calculation, enter 'r'?");
            char key = Console.ReadKey().KeyChar;

            //Return to the beginning of the calculator if user enters 'r' or 'R'
            if (char.ToLower(key) == 'r') return;

            // Validate the input to ensure it is 'd' for delete.
            while (!(char.ToLower(key) == 'd'))
            {
                Console.WriteLine("\nInvalid input. Please enter 'd' to delete an operation or any other key to exit.");
                key = Console.ReadKey().KeyChar;
            }

            // If the user chooses to delete an operation, prompt for the index.
            Console.WriteLine("\nEnter the number of the operation you want to delete:");
            int index = 0;

            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > operationCount)
            {
                Console.WriteLine($"Invalid input. Please enter a valid number within 1 and {operationCount}:");
            }
            // Delete the operation at the specified index.
            Delete(index);
        }
        //Delete index from list of operations
        private void Delete(int index)
        {
            operations.RemoveAt(index - 1);
            Console.WriteLine("Delete Successful \n ------------------------------------------------------");
            ListOperations(); // Refresh the list of operations after deletion.
        }
    }
}

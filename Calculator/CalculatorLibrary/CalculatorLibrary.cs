using Newtonsoft.Json;

namespace CalculatorLibrary
{   
    public class Calculator
    {

        JsonWriter writer;

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
                case "e":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Raise the first number to the power of the second number");
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
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public List<double> calculations = new List<double>
        {

        };
        public void AddToCalculations(int result)
        {
            calculations.Add(result);
        }

        public void PrintCalculations()
        {
            for (int i = 0; i < calculations.Count; i++)
            {
            Console.WriteLine($"{i + 1}: {calculations[i]}");
            }
        }
        public double getInput(int count)
        {
            string? choice = "";
            string? numInput = "";
            double cleanNum = 0;
            
            switch (count)
            {
                case 0:
                    Console.Write("Type a number, and then press Enter: ");
                    numInput = Console.ReadLine();
                    while (!double.TryParse(numInput, out cleanNum))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput = Console.ReadLine();
                    }
                    break;
                case >= 1:
                    Console.WriteLine("Would you like to use the result from a previous calculation as your input? Enter 'y' for yes, or any other input for no");
                    if (Console.ReadLine() == "y")
                    {
                        Console.WriteLine("Choose a result from below by typing in the list number associated with the result: ");
                        PrintCalculations();
                        choice = Console.ReadLine();
                        cleanNum = calculations[Convert.ToInt32(choice) - 1];
                    }
                    else
                    {
                        Console.WriteLine("Type a number, and then press Enter: ");
                        numInput = Console.ReadLine();
                        while (!double.TryParse(numInput, out cleanNum))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput = Console.ReadLine();
                        }
                    }
                    break;
            }
        
            return cleanNum;
        }
    }
}

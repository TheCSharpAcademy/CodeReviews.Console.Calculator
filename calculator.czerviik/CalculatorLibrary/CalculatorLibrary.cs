using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public List<double> PastResults { get; private set; }
        public static int TimesUsed { get; private set; }
        private double cleanNum = 0;
        private string numInput;

        public Calculator()
        {
            TimesUsed = 0;
            PastResults = new List<double>();
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
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Taking the Power");
                    break;
                case "t":
                    result = num1 * 10;
                    writer.WriteValue("x10");
                    break;
                case "i":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sinus");
                    break;
                case "o":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosinus");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            TimesUsed++;
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            PastResults.Add(result);
            return result;
        }
        public void Finish()
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Operations total");
            writer.WriteValue(TimesUsed);
            writer.WriteEndObject();
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
        public void SetInputNumber()
        {
            bool isValidInput = false;

            do
            {
                numInput = Console.ReadLine();
                if (numInput.ToLower() == "p")
                    if (PastResults.Any())
                    {
                        foreach (double entry in PastResults)
                        {
                            Console.WriteLine("{0} - {1}", PastResults.IndexOf(entry) + 1, entry);
                        }

                        int numInputPast;
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out numInputPast))
                        {
                            if (PastResults.Count >= numInputPast && numInputPast > 0)
                            {
                                cleanNum = PastResults[numInputPast - 1];
                                isValidInput = true;
                            }
                            else
                            {
                                Console.WriteLine("You entered a wrong number.");
                            }
                        }
                        else
                            Console.WriteLine("You didn't enter a number.");
                    }
                    else
                    {
                        Console.WriteLine("No previous results yet.");
                        Console.WriteLine("Type a number, and then press Enter: ");
                    }
                else
                {
                    while (!double.TryParse(numInput, out cleanNum))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput = Console.ReadLine();
                    }
                    isValidInput = true;
                    
                }
            } while (!isValidInput);
        }
        public double GetCleanNumber()
        {
            return cleanNum;
        }
    }  
}
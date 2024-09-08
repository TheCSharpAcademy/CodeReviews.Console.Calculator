using Newtonsoft.Json;

namespace CalculatorLibrary;

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

        public double DoOperation(double num1, double num2, string op , ref int usageCount)
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
                if (num1 >= 0)
                   {
                       result = Math.Sqrt(num1);
                   }
                writer.WriteValue("Square Root");
                    break;
                case "p":
                    result = Math.Pow(num1 , num2);
                    writer.WriteValue("Taking the Power");
                    break;
                case "w":
                    result = result = 10 * num1;
                    writer.WriteValue("10x");
                    break;
                case "sin":
                    result = Math.Sin(num1 * (Math.PI / 180.0)); 
                    writer.WriteValue("Sine");
                    break;
                case "cos":
                    result = Math.Cos(num1 * (Math.PI / 180.0));
                    writer.WriteValue("Cosine");
                    break;
                case "tan":
                    result = Math.Tan(num1 * (Math.PI / 180.0));
                    writer.WriteValue("Tangent");
                    break;
                case "u":
                    Console.WriteLine($"Calculator usage count is {usageCount} times.");
                    writer.WriteValue("UsageCount");
                    break;
                 case "l":
                     Get_Latest_Calculations();
                     Console.WriteLine("\nTo Clear the history list type option 'C'");
                     writer.WriteValue("last calc");
                     break;
                case "c":
                    DeleteGameResult(); // this delete the whole list
                    writer.WriteValue("DeletList");
                    break;
                default:     // Return text for an incorrect option entry.
                    break;
            }

           if (op != "u" && op != "l" && op !="c")  
            {
                usageCount++;
                AddGameResult(result.ToString());
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

    //  latest calculations functions.
    private static List<string> results = new List<string>();
    public static void AddGameResult(string result)
        {
           results.Add(result);
        }
    public static void DeleteGameResult()
        {
            results.Clear();
            Console.WriteLine("Result list clear now.");
        }

    public static void Get_Latest_Calculations()
    {
        if (results.Count == 0 )
        {
            Console.WriteLine("There is no calculation history yet.");
        }
        else
        {
            foreach (var result in results)
            {
                Console.WriteLine($"your latest calculation result was {result}");
            }
        }
    }
}       

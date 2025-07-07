using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        // CalculatorLibrary.cs
        JsonWriter writer;
        public int TimesUsed { get; private set; }
        public List<Calculation> calculations = [];

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

                case "p":
                    result = (int)Math.Pow(num1,num2);
                    writer.WriteValue("Power");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            AddToList(num1, num2, Symbol(op), result);
            ++TimesUsed;
            return result;
        }
        private void AddToList(double num1, double num2, string op, double result)
        {
            calculations.Add(new Calculation($"{num1} {op} {num2}", result));
        }
        public void ShowList()
        {
            int counter = 1;
            if (calculations.Count == 0)
                Console.WriteLine("The list is empty");
            foreach (Calculation c in calculations)
            {
                Console.WriteLine($"{counter++}) {c.expression} = {c.result:0.##}");
            }
        }
        public void DeleteList() => calculations.Clear();
        private string Symbol(string op) => op switch
        {
            "a" => "+",
            "s" => "-",
            "m" => "*",
            "d" => "/",
            "p" => "^",
            _ => ""
        };
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WritePropertyName("Times used");
            writer.WriteValue(TimesUsed);
            writer.WriteEndObject();
            writer.Close();

            Console.WriteLine($"The calculator was used {TimesUsed} times.");
        }
    }

    public class Calculation(string expression, double result)
    {
        public string expression = expression;
        public double result = result;
    }

    public class Input
    {
        // Returns null if the range is invalid (e.g. low > high)
        public static int? ReadInt(int low, int high)
        {
            if (low > high) return null;
            int cleanNum;
            while (!int.TryParse(Console.ReadLine(), out cleanNum)
                || cleanNum < low || cleanNum > high)
                Console.WriteLine($"This is not valid input.\n" +
                "Please enter a numeric value between {low} and {high} inclusive.");
            return cleanNum;
        }

        public static double ReadDouble()
        {
            string? input = Console.ReadLine();

            double cleanNum;
            while (!double.TryParse(input, out cleanNum))
            {
                Console.WriteLine("This is not valid input. Please enter a numeric value: ");
                input = Console.ReadLine();
            }
            return cleanNum;
        }

        public static double ReadDouble(string? input)
        {
            double cleanNum;
            while (!double.TryParse(input, out cleanNum))
            {
                Console.WriteLine("This is not valid input. Please enter a numeric value: ");
                input = Console.ReadLine();
            }
            return cleanNum;
        }
    }
}
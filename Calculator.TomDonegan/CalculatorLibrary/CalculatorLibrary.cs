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
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

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
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
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
    }

    public class Calculation
    {
        public double FirstNumber { get; set; }
        public double SecondNumber { get; set; }
        public string Operator { get; set; }
        public double Result { get; set; }
    }

    public class History
    {
        public void ShowHistory(List<Calculation> calculations)
        {
            Console.WriteLine("Calculation History (5 Max):\n");
            for (int i = 0; i < calculations.Count; i++)
            {
                string op = "";

                switch (calculations[i].Operator)
                {
                    case "a":
                        op = "+";
                        break;
                    case "s":
                        op = "-";
                        break;
                    case "m":
                        op = "x";
                        break;
                    case "d":
                        op = "/";
                        break;
                    default:
                        break;
                }
                Console.WriteLine(
                    $"Calculation {i + 1}: {calculations[i].FirstNumber}{op}{calculations[i].SecondNumber}={calculations[i].Result}"
                );
            }
            Console.WriteLine("\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public double UsePreviousResult(List<Calculation> calculations)
        {
            string selection;

            if (calculations.Count > 0)
            {
                for (int i = 0; i < calculations.Count; i++)
                {
                    Console.WriteLine($"Result {i + 1}: {calculations[i].Result}");
                }
                Console.WriteLine("Please select a previous result: ");
                selection = Console.ReadLine();
                while (Int32.Parse(selection) > calculations.Count())
                {
                    Console.WriteLine(
                        $"Option {selection} is not available. Please choose one of the listed results."
                    );

                    selection = Console.ReadLine();
                }
                Console.WriteLine($"{calculations[Int32.Parse(selection) - 1].Result} selected.");
                return calculations[Int32.Parse(selection) - 1].Result;
            }
            else
            {
                Console.WriteLine(
                    "There is currently no stored history. Please use the calculator first."
                );
                Console.WriteLine("Please type a number and press Enter: ");

                selection = Console.ReadLine();
                return double.Parse(selection);
            }
        }

        public void ManageHistory(List<Calculation> calculations)
        {
            if (calculations.Count == 6)
            {
                calculations.RemoveAt(0);
            }
        }
    }
}
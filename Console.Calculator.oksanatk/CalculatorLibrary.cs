using Newtonsoft.Json;
using Spectre.Console;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonTextWriter writer;
        public int TimesUsed { get; private set; }
        public List<Calculation> RecentCalculations { get; private set; }
        public Calculator()
        {
            TimesUsed = 0;
            RecentCalculations = new List<Calculation>();

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

            switch (op)
            {
                case "add":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;

                case "sub":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;

                case "mult":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;

                case "div":
                    while (num2 == 0)
                    {
                        Console.WriteLine("Cannot divide by 0. Please enter another number.");
                        num2 = Convert.ToDouble(Console.ReadLine());
                    }

                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                    }
                    break;

                case "sq":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;

                case "pow":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Raised to the Power of");
                    break;

                case "10x":
                    result = num1 * 10;
                    writer.WriteValue("10X");
                    break;

                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    break;

                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    break;

                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tan");
                    break;

                default:
                    Console.WriteLine("I'm sorry, I didn't understand that operation.");
                    writer.WriteValue("Invalid operation.");
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            TimesUsed++;
            RecentCalculations.Add(new Calculation(num1, op, result, num2));

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public void ClearCalculations()
        {
            RecentCalculations.Clear();
        }

        public Panel ShowPreviousCalculations(List<Calculation> previousCalcs)
        {
            Console.WriteLine();
            if (previousCalcs.Count > 0)
            {
                Table table = new Table();
                table.AddColumns("Calculation #", "Result");

                int counter = 1;
                foreach (Calculation calc in previousCalcs)
                {
                    if (calc.Num2.Equals(double.NaN))
                    {
                        table.AddRow($"Calculation {counter}", $"{OperationVisualizer(calc.Operation, calc.Num1)} = [bold yellow]{calc.Result}[/]");
                    }
                    else
                    {
                        table.AddRow($"Calculation {counter}", $"{calc.Num1} {OperationVisualizer(calc.Operation)} {calc.Num2} = [bold yellow]{calc.Result}[/]");
                    }
                    counter++;
                }
                return new Panel(table)
                            .Header("[bold yellow]Previous Calculations[/]")
                            .RoundedBorder()
                            .Padding(1, 1, 1, 1);
            }
            else
            {
                return new Panel("There are no previous calculations to view.")
                            .Header("[bold yellow]Previous Calculations[/]")
                            .RoundedBorder()
                            .Padding(1, 1, 1, 1);
            }

        }

        public string OperationVisualizer(string operation, double singleNumberOperationNum = double.NaN)
        {
            switch (operation)
            {
                case "add":
                    return "+";
                case "sub":
                    return "-";
                case "mult":
                    return "*";
                case "div":
                    return "/";
                case "sq":
                    return $"\u221A{singleNumberOperationNum}";
                case "pow":
                    return "^";
                case "10x":
                    return $"10 * {singleNumberOperationNum}";
                case "sin":
                    return $"sin({singleNumberOperationNum})";
                case "cos":
                    return $"cos({singleNumberOperationNum})";
                case "tan":
                    return $"tan({singleNumberOperationNum})";
                default:
                    return $"??{operation}?? {singleNumberOperationNum}";
            }
        }
    }
}
public record Calculation(double Num1, string Operation, double Result, double Num2 = double.NaN);


using Newtonsoft.Json;
using EnumsLibrary;
using Spectre.Console;


namespace CalculatorLibrary
{
    public class Calculator
    {
        protected  string _pastResults = "";
        
        JsonWriter writer;
        JsonWriter countWriter;
        protected int Counter { get; set; }
        protected readonly string _countLogFilePath = "UsageCountLog.json";
        public void ShowUsage()
        {
            
            Console.WriteLine(string.Format("You've used the calculator {0} {1}",Counter,(Counter > 1 ? "times":"time")));
        }

        public void UsageCounter()
        {
            Counter++;
        }
        public Calculator()
        {
             StreamWriter countLogFile = File.CreateText(_countLogFilePath);
            countLogFile.AutoFlush = true;
            countWriter = new JsonTextWriter(countLogFile);
            countWriter.Formatting = Formatting.Indented;
            countWriter.WriteStartObject();
            countWriter.WritePropertyName("Usage count");
            countWriter.WriteStartArray();
            countWriter.WriteStartObject();
            countWriter.WritePropertyName("count");

             StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        public  double DoOperation(double num1, Enums.Operation operation)
        {
            double result = double.NaN; // Default value is "not-a-number" which we use if an operation, such as division, could result in an error.
            
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            

            // Use a switch statement to do the math.
            switch (operation)
            {
                case Enums.Operation.Addition:
                    var num2 = AnsiConsole.Ask<double>("[green]Type another number, and then press Enter: [/]");
                    result = num1 + num2;
                    writer.WritePropertyName("Operand2");
                    writer.WriteValue(num2);
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("Add");
                    _pastResults += $"{num1} + {num2} = {result},";
                    break;
                case Enums.Operation.Subtraction:
                    num2 = AnsiConsole.Ask<double>("[green]Type another number, and then press Enter: [/]");
                    result = num1 - num2;
                    writer.WritePropertyName("Operand2");
                    writer.WriteValue(num2);
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("Subtract");
                    _pastResults += $"{num1} - {num2} = {result},";
                    break;
                case Enums.Operation.Multiplication:
                    num2 = AnsiConsole.Ask<double>("[green]Type another number, and then press Enter: [/]");
                    result = num1 * num2;
                    writer.WritePropertyName("Operand2");
                    writer.WriteValue(num2);
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("Multiply");
                    _pastResults += $"{num1} * {num2} = {result},";
                    break;
                case Enums.Operation.Division:
                    // Ask the user to enter a non-zero divisor.
                    num2 = AnsiConsole.Ask<double>("[green]Type another number, and then press Enter: [/]");
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        _pastResults += $"{num1} / {num2} = {result},";
                    }
                    writer.WritePropertyName("Operand2");
                    writer.WriteValue(num2);
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("Divide");
                    
                    break;
                // Return text for an incorrect option entry.
                case Enums.Operation.SquareRoot:
                    
                    result = Math.Sqrt(num1);
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("Sqrt");
                    _pastResults += $"Sqrt({num1}) = {result},";
                    break;
                case Enums.Operation.TenTimes_10x:
                    result = num1 * 10;
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("TenTimes(10X)");
                    _pastResults += $"10x{num1} = {result},";
                    break;
                case Enums.Operation.Sin:
                    result = Math.Sin(num1);
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("Sin");
                    _pastResults += $"Sin({num1})= {result},";
                    break;
                case Enums.Operation.Cos:
                    result = Math.Cos(num1);
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("Cos");
                    _pastResults += $"Cos({num1})= {result},";
                    break;
                case Enums.Operation.Tan:
                    result = Math.Tan(num1);
                    writer.WritePropertyName("Operation");
                    writer.WriteValue("Tan");
                    _pastResults += $"Tan({num1})= {result},";
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

            countWriter.WriteValue(Counter);
            countWriter.WriteEndObject();
            countWriter.WriteEndArray();
            countWriter.WriteEndObject();
            countWriter.Close();
        }
        public void ShowHistory()
        {
            Console.WriteLine("Previous calculations: ");
            string[] pastResults = _pastResults.Split(',');
            foreach (string result in pastResults)
            {
                Console.WriteLine(result);
            }
            Console.WriteLine("\n");
        }
        public void DeleteHistory()
        {
            Console.WriteLine("Deleting history...");
            _pastResults = "";
        }
        public double RetrievePastResult()
        {
            string[] pastResults = _pastResults.Split(',');
            var resultChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("[yellow]Choose the result you want to continue operations on:[/]")
                        .AddChoices(pastResults));

            return Convert.ToDouble(resultChoice.Substring(resultChoice.IndexOf('=')+1).Trim());
        }
    }
}

using Newtonsoft.Json;
using Spectre.Console;
using System.Drawing;
using static CalculatorLibrary.Enums;
namespace CalculatorLibrary
{
    public class CalculatorEngine
    {
        JsonWriter writer;

        private List<(double Operand1, double Operand2, string Operation, double Result)> HistoryList = new List<(double Operand1, double Operand2, string Operation, double Result)>();

        public CalculatorEngine()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoMathOperation(double num1, double num2, MenuOptions op)
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
                case MenuOptions.Add:
                    writer.WriteValue("Add");
                    result = num1 + num2;
                    AddHistory(num1, num2, "+", result);
                    break;
                case MenuOptions.Subtract:
                    writer.WriteValue("Subtract");
                    result = num1 - num2;
                    AddHistory(num1, num2, "-", result);
                    break;
                case MenuOptions.Multiply:
                    writer.WriteValue("Multiply");
                    result = num1 * num2;
                    AddHistory(num1, num2, "x", result);
                    break;
                case MenuOptions.Divide:
                    writer.WriteValue("Divide");
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    AddHistory(num1, num2, "/", result);
                    break;
                case MenuOptions.ToPower:
                    writer.WriteValue("ToPower");
                    result = Math.Pow(num1, num2);
                    AddHistory(num1, num2, "^", result);
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;

        }

        public double DoTrigonometricOperation(double number)
        {
            var trigOperation = AnsiConsole.Prompt(
                new SelectionPrompt<TrigonometryOptions>()
                .Title("[green]Choose an trigonometric operation from the following list:[/]")
                .AddChoices(Enum.GetValues<TrigonometryOptions>()));

            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand");
            writer.WriteValue(number);
            writer.WritePropertyName("Operation");

            switch (trigOperation)
            {
                case TrigonometryOptions.Sine:
                    writer.WriteValue("Sine");
                    result = Math.Sin(number);
                    AddHistory(double.NaN, number, "sin", result);
                    break;
                case TrigonometryOptions.Cosine:
                    writer.WriteValue("Cosine");
                    result = Math.Cos(number);
                    AddHistory(double.NaN, number, "cos", result);
                    break;
                case TrigonometryOptions.Tangent:
                    writer.WriteValue("Tangent");
                    result = Math.Tan(number);
                    AddHistory(double.NaN, number, "tan", result);
                    break;
                case TrigonometryOptions.Cotangent:
                    writer.WriteValue("Cotangent");
                    result = 1d / Math.Tan(number);
                    AddHistory(double.NaN, number, "cot", result);
                    break;
                case TrigonometryOptions.Secant:
                    writer.WriteValue("Secant");
                    result = 1d / Math.Sin(number);
                    AddHistory(double.NaN, number, "sec", result);
                    break;
                case TrigonometryOptions.Cosecant:
                    writer.WriteValue("Cosecant");
                    result = 1d / Math.Cos(number);
                    AddHistory(double.NaN, number, "csc", result);
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;
        }
        public void AddHistory(double operand1, double operand2, string op, double result)
        {
            HistoryList.Add((operand1, operand2, op, result));
        }

        public void ShowHistory()
        {
            foreach (var item in HistoryList)
            {
                Console.WriteLine($"{item.Operand1} {item.Operation} {item.Operand2} = {item.Result}");
            }
        }

        public double SelectFromHistory()
        {
            if (HistoryList.Count == 0)
            {
                return 0;
            }
            var calculationToSelect = AnsiConsole.Prompt(
                new SelectionPrompt<(double Operand1, double Operand2, string Operation, double Result)>()
                .Title("Select from History")
                .UseConverter(c => $"{c.Operand1} {c.Operation} {c.Operand2} = {c.Result}")
                .AddChoices(HistoryList));

            return calculationToSelect.Result;
        }
        public void ClearHistory()
        {
            HistoryList.Clear();
        }

        public double GetLastResult()
        {
            return HistoryList.Last().Result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
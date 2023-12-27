using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        List<string> history = new List<string>();

        public Calculator()
        {
            StreamWriter logfile = File.CreateText("calculatorlog.json");
            logfile.AutoFlush = true;
            writer = new JsonTextWriter(logfile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public double DoOperation(double num1, double num2, string operation)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (operation)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    history.Add($"{DateTime.Now} : {num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    history.Add($"{DateTime.Now} : {num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    history.Add($"{DateTime.Now} : {num1} * {num2} = {result}");
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        history.Add($"{DateTime.Now} : {num1} / {num2} = {result}");
                    }
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;
        }

        public void ShowHistory()
        {
            foreach (var calculation in history) Console.WriteLine(calculation.ToString());

            Console.WriteLine("Press x and Enter to delete the list or Enter to go back");
            if (Console.ReadLine() == "x") history.Clear();
        }
    }
}


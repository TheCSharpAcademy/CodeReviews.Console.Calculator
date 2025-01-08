using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        private int calcCount = 0;
        private List<string> calculations = new List<string>();
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
                    calcCount++;
                    calculations.Add($"{num1}+{num2}={result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calcCount++;
                    calculations.Add($"{num1}-{num2}={result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculations.Add($"{num1}*{num2}={result}");
                    calcCount++;
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    calculations.Add($"{num1}/{num2}={result}");
                    calcCount++;
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
        public int GetCount() { return calcCount; }
        public int GetHistoryCount() { return calculations.Count; }

        public void DisplayHistory()
        {
            for (int i = 0; i < calculations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {calculations[i]}");
            }
        }
        public void ClearHistory()
        {
            calculations.Clear();
        }

        public double GetResult(int index) //takes the result from the history and returns it as a double
        {
            double result = 0;
            string equation = calculations[index - 1];
            equation = equation.Substring(equation.IndexOf('=')+1);
            result = double.Parse(equation);
            return result;

        }

        public double GetInput(bool firstInput) //it takes firstInput as an argument to determine if it should display the message about using previous result
        {
            string? numInput = "";
            double cleanNum = 0;
            Console.Write("Type a number, and then press Enter: ");
            if (!firstInput)
            {
                Console.WriteLine("\nIf you want to use previous result in your calculation, type 'p' and press Enter");
            }
            numInput = Console.ReadLine();

            if (numInput == "p" && !firstInput) //second condition just to make sure it can't be used with empty history
            {
                int index = 0;
                string? indexInput = "";
                DisplayHistory();
                Console.WriteLine("Type the number of equation you want to use and press Enter: ");
                indexInput = Console.ReadLine();
                while (!int.TryParse(indexInput, out index) || index < 1 || index > GetHistoryCount())
                {
                    Console.Write("This is not valid input. Please enter a correct value: ");
                    indexInput = Console.ReadLine();
                }
                cleanNum = GetResult(index);
            }
            else
            {
                while (!double.TryParse(numInput, out cleanNum))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput = Console.ReadLine();
                }
            }
            return cleanNum;
        }
    }

}

// CalculatorLibrary.cs
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

        public double DoOperation(double num1, double num2, string op, int count, List<double> resultNumbers, bool IsReused)
        {

            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            double radians = num1 * (Math.PI / 180.0);

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
                    result = Math.Sqrt(num1 * num2);
                    writer.WriteValue("Square root");
                    break;
                case "e":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Exponentiation");
                    break;
                case "t":
                    result = num1 * 10;
                    writer.WriteValue("Tenfold");
                    break;
                case "i":
                    result = Math.Sin(radians);
                    writer.WriteValue("Sine");
                    break;
                case "c":
                    result = Math.Cos(radians);
                    writer.WriteValue("Cosine");
                    break;
                case "g":
                    result = Math.Tan(radians);
                    writer.WriteValue("Tangent");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public int ChainingCalculations(String select, List<double> resultNumbers, ref bool IsReused, ref int firstTempValue)
        {
            if (resultNumbers.Count == 0)
            {
                Console.WriteLine("No results found. Returning to the beginning.");
            }
            else
            {
                int numbering = 1;
                foreach (double saveNumber in resultNumbers)
                {
                    Console.WriteLine(numbering + ". {0:0.##}", saveNumber);
                    numbering++;
                }

                while (true)
                {
                    Console.WriteLine("Here is the recent results list. The higher the number, the more recent the result. If you want to calculate with these numbers, please press the number next to it or 'n'.");
                    if (int.TryParse(Console.ReadLine(), out firstTempValue))
                    {
                        firstTempValue -= 1; // 입력이 유효하면 1을 뺍니다.
                        IsReused = true;
                        return firstTempValue; // 변환이 성공하면 루프를 종료합니다.
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }
            }
            return -1; // 기본 반환값 추가
        }

        public void ResultRemove(List<double> resultNumbers)
        {
            if (resultNumbers.Count == 0)
            {
                Console.WriteLine("No results found. Returning to the beginning.");
                return;
            }
            else
            {
                int numbering = 1;
                foreach (double saveNumber in resultNumbers)
                {
                    Console.WriteLine(numbering + ". {0:0.##}", saveNumber);
                    numbering++;
                }
            }
            Console.WriteLine("Please enter the number to delete.");

            resultNumbers.RemoveAt(int.Parse(Console.ReadLine()) - 1);
        }

        public void UsageCount(int count)
        {
            Console.WriteLine("usage counter: " + count);
            if (count != 0)
            {
                count--;
            }
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }


    }
}
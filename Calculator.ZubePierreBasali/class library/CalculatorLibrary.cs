using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public int numberOfUse;
        JsonWriter writer;
        string operation;

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
                    operation = "Add";
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    operation = "Subtract";
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    operation = "Multiply";
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    operation = "Divide";
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            numberOfUse++;

            Data.AddData(num1, num2, result, operation);

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }

    public class Data
    {
        static int idCount = 0;
        public static List<Data> data = new List<Data>();

        public int id { get; set; }
        string operation { get; set; }
        double operand1 { get; set; }
        double operand2 { get; set; }
        public double result { get; set; }

        public static void AddData(double num1, double num2, double res, string op)
        {
            data.Add(
            new Data
            {
                id = idCount,
                operand1 = num1,
                operand2 = num2,
                result = res,
                operation = op
            }
            );
            idCount++;
        }

        public static double GetInput()
        {
            double answer = 0;
            int x = 0;
            int[] numArr = new int[x + 1];
            bool returnRet = false;
            int currentIndex = data.Count - 1;
            ConsoleKeyInfo keyInfo = new();
            do
            {
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        returnRet = true;
                        if (currentIndex <= data.Count - 1)
                        {
                            if (currentIndex < 0) currentIndex = 0;
                            answer = data[currentIndex].result;
                            currentIndex++;
                        }
                        Console.Write($"\r{new System.String(' ', Console.BufferWidth)}");
                        Console.Write($"\r{answer}");
                        Thread.Sleep(20);
                        x = 0;
                        numArr = new int[x + 1];
                        break;
                    case ConsoleKey.UpArrow:
                        returnRet = true;
                        if ((currentIndex >= 0))
                        {
                            if (currentIndex > data.Count - 1) currentIndex = data.Count - 1;
                            answer = data[currentIndex].result;
                            currentIndex--;
                        }
                        Console.Write($"\r{new System.String(' ', Console.BufferWidth)}");
                        Console.Write($"\r{answer}");
                        x = 0;
                        numArr = new int[x + 1];
                        Thread.Sleep(20);
                        break;
                    case ConsoleKey.Enter:
                        break;
                    default:

                        if (returnRet == true)
                        {
                            Console.Write($"\r{new System.String(' ', Console.BufferWidth)}");
                            Console.Write($"\r{keyInfo.KeyChar}");
                            Console.SetCursorPosition(1, Console.CursorTop);
                        }
                        int num;
                        string keyChar = keyInfo.KeyChar.ToString();
                        bool isInteger = int.TryParse(keyChar, out num);
                        if (isInteger)
                        {
                            numArr[x] = num;
                            int[] buffArr = numArr;
                            x++;
                            numArr = new int[x + 1];
                            for (int i = 0; i < buffArr.Length; i++)
                            {
                                numArr[i] = buffArr[i];
                            }
                        }
                        currentIndex = data.Count - 1;
                        returnRet = false;
                        Thread.Sleep(20);
                        break;
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            string output = "";
            if (returnRet)
            {
                return answer;
            }
            else
            {
                for (int i = 0; i < numArr.Length - 1; i++)
                {
                    output = $"{output}{numArr[i]}";
                }
                double.TryParse(output, out answer);
            }

            return answer;
        }

        public static void PrintData()
        {
            for (int i = 0; i < Data.data.Count; i++)
            {
                Console.WriteLine($"id: {data[i].id}");
                Console.WriteLine($"operand 1: {data[i].operand1}");
                Console.WriteLine($"operand 2: {data[i].operand2}");
                Console.WriteLine($"operation: {data[i].operation}");
                Console.WriteLine($"reasult: {data[i].result}");
                Console.WriteLine();
            }
        }   

        public static void DeleteData(int id)
        {
            data.Remove(data[id]);
            // Forward iteration doesn't work
            for (int i = data.Count -1; i >= id; i--)
            {
                data[i].id = i;
            }
            idCount = data.Count;
        }
    }
}
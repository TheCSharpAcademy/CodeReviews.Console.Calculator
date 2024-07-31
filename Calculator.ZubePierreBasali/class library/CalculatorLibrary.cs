using System.Collections.Generic;
using System.Collections.Immutable;
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
        static int IdCount = 0;
        public static List<Data> data = new List<Data>();

        public int Id { get; set; }
        string Operation { get; set; }
        double Operand1 { get; set; }
        double Operand2 { get; set; }
        public double Result { get; set; }

        public static void AddData(double num1, double num2, double res, string op)
        {
            data.Add(
            new Data
            {
                Id = IdCount,
                Operand1 = num1,
                Operand2 = num2,
                Result = res,
                Operation = op
            }
            );
            IdCount++;
        }

        public static string GetInput()
        {
            double answer = 0;
            int x = 0;
            string[] numArr = new string[x + 1];
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
                            answer = data[currentIndex].Result;
                            currentIndex++;
                        }
                        Console.Write($"\r{new System.String(' ', Console.BufferWidth)}");
                        Console.Write($"\r{answer}");
                        Thread.Sleep(20);
                        x = 0;
                        numArr = new string[x + 1];
                        break;
                    case ConsoleKey.UpArrow:
                        returnRet = true;
                        if ((currentIndex >= 0))
                        {
                            if (currentIndex > data.Count - 1) currentIndex = data.Count - 1;
                            answer = data[currentIndex].Result;
                            currentIndex--;
                        }
                        Console.Write($"\r{new System.String(' ', Console.BufferWidth)}");
                        Console.Write($"\r{answer}");
                        x = 0;
                        numArr = new string[x + 1];
                        Thread.Sleep(20);
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        break;
                    case ConsoleKey.Delete:
                    case ConsoleKey.Backspace:
                        Console.Write($"\r{new System.String(' ', Console.BufferWidth)}");
                        Console.SetCursorPosition(0, Console.CursorTop);
                        numArr = new string[x + 1];
                        break;

                    default:

                        if (returnRet == true)
                        {
                            Console.Write($"\r{new System.String(' ', Console.BufferWidth)}");
                            Console.Write($"\r{keyInfo.KeyChar}");
                            Console.SetCursorPosition(1, Console.CursorTop);
                        }

                        string keyChar = keyInfo.KeyChar.ToString();
                        numArr[x] = keyChar;
                        string[] buffArr = numArr;
                        x++;
                        numArr = new string[x + 1];
                        for (int i = 0; i < buffArr.Length; i++)
                        {
                            numArr[i] = buffArr[i];
                        }

                        currentIndex = data.Count - 1;
                        returnRet = false;
                        Thread.Sleep(20);
                        break;
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            return ChooseRightInput(answer, returnRet, numArr);
        }

        private static string ChooseRightInput(double answer,bool returnRet, string[] numArr)
        {
            if (returnRet)
            {
                return answer.ToString();
            }
            else
            {
                string output = "";
                for (int i = 0; i < numArr.Length - 1; i++)
                {
                    output = $"{output}{numArr[i]}";
                }
                return output;
            }
        }

        public static void PrintData()
        {
            for (int i = 0; i < Data.data.Count; i++)
            {
                Console.WriteLine($"id: {data[i].Id}");
                Console.WriteLine($"operand 1: {data[i].Operand1}");
                Console.WriteLine($"operand 2: {data[i].Operand2}");
                Console.WriteLine($"operation: {data[i].Operation}");
                Console.WriteLine($"reasult: {data[i].Result}");
                Console.WriteLine();
            }
        }   

        public static void DeleteData(int id)
        {
            if (data.Count != 0 && id >= 0 && id < data.Count)
            {
                data.Remove(data[id]);
                // Forward iteration doesn't work
                for (int i = data.Count - 1; i >= id; i--)
                {
                    data[i].Id = i;
                }
                IdCount = data.Count;
            }
        }

        public static string MainMenu()//bool endApp)
        {
            string? readResult;
            readResult = Console.ReadLine();
            Console.WriteLine();
            switch (readResult.ToLower())
            {
                case "p":
                    Data.PrintData();
                    break;
                case "d":
                    bool validInput = false;
                    int id;
                    while (!validInput)
                    {
                        Console.WriteLine($"Please select an id between 0 and {Data.data.Count - 1} then Enter,or press 'n' then Enter to quit.");
                        while (!validInput)
                        {
                            readResult = Console.ReadLine();
                            validInput = int.TryParse(readResult, out id);
                            if (validInput) { Data.DeleteData(id); }
                            else
                            {
                                Console.Write($"\r{new System.String(' ', Console.BufferWidth)}");
                                Console.SetCursorPosition(0, Console.CursorTop);
                            }
                        }
                        Console.WriteLine("press 'd' then Enter to delete another data");
                        readResult = Console.ReadLine();
                        if (readResult.ToLower() == "d") validInput = false;
                    }
                    readResult = "d";
                    // Delete data function
                    break;
                default:
                    break;
            }
            return readResult;
        }

        public static double GetNumber()
        {
            string? num;
            Console.Write("Type a number, and then press Enter: \n");
            num = GetInput();

            double cleanNum = 0;
            while (!double.TryParse(num, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                num = GetInput();
            }
            return cleanNum;
        }
    }
}
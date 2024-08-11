using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
	public class Calculator: BaseOperations
	{
        private int UsageCounter { get; set; }
        private JsonWriter writer;

        public Calculator()
		{

            StreamWriter logFile = File.CreateText($"{GetFilePath()}");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        /********* trigonometric functions *********/
        public double GetTenX(double power)
        {
            return Math.Pow(10, power);
        }

        public double GetSquareRoot(double a)
        {
            return Math.Sqrt(a);
        }
        public double GetTangent(double a)
        {
            return Math.Tan(a);
        }
        public double GetSine(double a)
        {
            return Math.Sin(a);
        }
        public double GetCosine(double a)
        {
            return Math.Cos(a);
        }
        /********* ------------------ *********/

        private string GetCurrentPath()
        {
            return Directory.GetCurrentDirectory().Replace("bin/Debug/net7.0", "");
        }
        private string GetFilePath()
        {
            string path = GetCurrentPath();
            string FileName = "calculatorlog.json";
            return $"{path}/{FileName}";
        }
        public double AskCalculationQuestion(double n)
        {
            Console.Write("Extra Calculation Y/N: ");
            if(Console.ReadLine().ToLower() == "y")
            {
                Console.WriteLine("-------Extra Calculation-------");
                Console.WriteLine("\tx - 10x");
                Console.WriteLine("\tr - Square Root");
                Console.WriteLine("-------trigonometry-------");
                Console.WriteLine("\tc - Cosine");
                Console.WriteLine("\ts - Sin");
                Console.WriteLine("\tt - Tangente");
                Console.Write("Your option? ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "x":
                        return GetTenX(n);
                    case "r":
                        return GetSquareRoot(n);
                    case "c":
                        return GetCosine(n);
                    case "s":
                        return GetSine(n);
                    case "t":
                        return GetSine(n);
                }
            }
            return double.NaN;
            
        }
        public double AskUserForNumber(string message)
        {
            Console.Write(message);
            try
            {
                string? userInput = Console.ReadLine();
                double result = AskCalculationQuestion(Convert.ToDouble(userInput));
                if (!double.IsNaN(result))
                {
                    Console.WriteLine($"The number is changed to {result}");
                    return result;
                } 
                return Convert.ToDouble(userInput);
            }
            catch
            {
                throw new FormatException("Invalid number: Ensure to enter a number");
            }

        }
        public void CountUsage()
        {
            UsageCounter++;
        }
        public int GetUsageTime()
        {
            return UsageCounter;
        }
        protected override string GetOperationName(string oper)
        {
            switch (oper)
            {
                case "m":
                    return "Multiply";
                case "s":
                    return "Substract";
                case "d":
                    return "Divide";
                case "a":
                    return "Add";
                default:
                   return "";
            }
        }
        public double Calculate(double num1, double num2, string operation)
        {
           string oper = "";
           double result = double.NaN;

           if (Regex.IsMatch(operation, "[a|s|m|d]"))
            {
                oper = GetOperation(operation);

                writer.WriteStartObject();
                writer.WritePropertyName("Operand1");
                writer.WriteValue(num1);
                writer.WritePropertyName("Operand2");
                writer.WriteValue(num2);
                writer.WritePropertyName("Operation");
            }

            Console.WriteLine("-------------Result of the operation-------------");
            switch (operation.ToLower())
            {
                case "a":
                    result = Addition(num1, num2);
                    Console.WriteLine($"Your result: {num1} {oper} {num2} = {result}");
                    writer.WriteValue(GetOperationName(operation));
                    break;
                case "s":
                    result = Substraction(num1, num2);
                    Console.WriteLine($"Your result: {num1} {oper} {num2} = {result}");
                    writer.WriteValue(GetOperationName(operation));
                    break;
                case "m":
                    result = Multiplication(num1, num2);
                    Console.WriteLine($"Your result: {num1} {oper} {num2} = {result}");
                    writer.WriteValue(GetOperationName(operation));
                    break;
                case "d":
                    while (num2 == 0)
                    {
                        num2 = AskUserForNumber("Enter a non-zero devisor: ");
                    }
                    result = Division(num1, num2);
                    Console.WriteLine($"Your result: {num1} {oper} {num2} = {result}");
                    writer.WriteValue(GetOperationName(operation));
                    break;        
            }
            Console.WriteLine("--------------------------------");
            if (Regex.IsMatch(operation, "[a|s|m|d]"))
            {
                writer.WritePropertyName("Result");
                writer.WriteValue(result);
                writer.WriteEndObject();
            }
            return result;
        }
        public void DeleteJsonFile()
        {
            string filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
                Console.WriteLine("The file has been deleted succefully");
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


using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
    public class MathOperation
    {
        public string? Operation { get; set; }
        public double Result { get; set; }
    }
    public class Calculator
    {
        public List<MathOperation> operations = new List<MathOperation>();
        JsonWriter writer;
        
        int calculatorUses = 0;
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

        public void OperationHistory()
        {
            double result;
            string? op;
            int i;
            for (i = 0; i < operations.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + operations[i].Operation);
            }

            if (operations != null)
            {
                Console.WriteLine("If you want to use one of the result as your first number, select from above numbers");
                Console.WriteLine("For basic math operations selected result will be your first number");
                Console.WriteLine("If you chose 's', selected result will be square rooted");
                Console.WriteLine("If you chose 'p', selected result will be your base number");
                Console.WriteLine("If you chose 'l', selected result will be your logarithm argument");
                Console.WriteLine("If you want to delete your game history, write \"delete\"");
                
                op = Console.ReadLine();
               
                if (int.TryParse(op, out i))
                {
                    double cleanNum1 = operations[i - 1].Result;
                    Console.WriteLine("Choose an operation between - '+', '-', '*', '/', 's', 'p' or 'l'");
                    op = Console.ReadLine();
                    while (!Regex.IsMatch(op, "[+|/|*|s|p|l|-]"))
                    {
                        Console.WriteLine("Wrong input, you have to chose between - '+', '-', '*', '/', 's', 'p' or 'l'");
                        op = Console.ReadLine();
                    }
                    Console.WriteLine("Write the second number");
                    string? num2 = Console.ReadLine();
                    double cleanNum2;

                    while (!double.TryParse(num2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        num2 = Console.ReadLine();
                    }
                    
                    if (Regex.IsMatch(op, "[s|p|l]"))
                    {
                        result = AdvancedOperations(cleanNum1, cleanNum2, op);
                        switch (op)
                        {
                            case "s":
                                result = Math.Sqrt(cleanNum1);
                                Console.WriteLine($"{cleanNum1} square rooted = {result}");
                                break;
                            case "p":
                                result = Math.Pow(cleanNum1, cleanNum2);
                                Console.WriteLine($"{cleanNum1} to the power of {cleanNum2} = {result}");
                                break;
                            case "l":
                                result = Math.Log(cleanNum1, cleanNum2);
                                Console.WriteLine($"Logarithm {cleanNum1} with the base of {cleanNum2} = {result}");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        result = BasicOperations(cleanNum1, cleanNum2, op);
                        Console.WriteLine($"{cleanNum1} {op} {cleanNum2} = {result}");
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                }
                if (string.Equals(op, "delete"))
                {
                    operations.Clear();
                }
            }
        }

        public double BasicOperations(double num1, double num2, string op)
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
                case "+":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "-":
                    result = num1 - num2;
                    writer.WriteValue("Substract");
                    break;
                case "*":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;           
                case "/":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                    }
                    break;
                default:
                    break;
            }
            operations.Add(new MathOperation {Operation = $"{num1} {op} {num2} = {result}", Result = result });
            calculatorUses++;
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public double AdvancedOperations(double num1, double num2, string op)
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
                case "s":
                    writer.WriteValue("Square root");
                    result = Math.Sqrt(num1);
                    operations.Add(new MathOperation { Operation = $"{num1} square rooted = {result}", Result = result});
                    break;
                case "p":
                    writer.WriteValue("Power of");
                    result = Math.Pow(num1, num2);
                    operations.Add(new MathOperation { Operation = $"{num1} to the power of {num2} = {result}", Result = result});
                    break;
                case "l":
                    writer.WriteValue("Logarithm");
                    result = Math.Log(num1, num2);
                    operations.Add(new MathOperation { Operation = $"Logarith {num1} with the base of {num2} = {result}", Result = result });
                    break;
                default:
                    break;
            }
            calculatorUses++;
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Calculator uses");
            writer.WriteValue(calculatorUses);
            writer.WriteEndObject();
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}

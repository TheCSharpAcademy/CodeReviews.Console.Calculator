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

        public void AdditionalOptions(string op)
        {
            switch (op)
            {
                case "h":
                    int i;
                    for (i = 0; i < operations.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + operations[i].Operation);
                    }

                    if (operations != null)
                    {
                        Console.WriteLine("If you want to use one of the result as your first number, select from above numbers");
                        Console.WriteLine("If you want to delete your game history, write \"delete\"");
                        op = Console.ReadLine();
                        if (int.TryParse(op, out i))
                        {
                            double cleanNum1 = operations[i - 1].Result;
                            Console.WriteLine("Write the second number");
                            string? num2 = Console.ReadLine();
                            double cleanNum2;
                            while (!double.TryParse(num2, out cleanNum2))
                            {
                                Console.Write("This is not valid input. Please enter an integer value: ");
                                num2 = Console.ReadLine();
                            }
                            Console.WriteLine("Choose an operation between - '+', '-', '*' and '/'");
                            op = Console.ReadLine();
                            while (!Regex.IsMatch(op, "[+|/|*|-]"))
                            {
                                Console.WriteLine("Wron input, you have to chose between - '+', '-', '*' and '/'");
                                op = Console.ReadLine();
                            }

                            Console.WriteLine($"{cleanNum1} {op} {cleanNum2} = {DoOperation(cleanNum1, cleanNum2, op)}");
                        }
                        if (op == "delete")
                        {
                            operations.Clear();
                        }
                    }
                    break;
                default:
                    break;
            }

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

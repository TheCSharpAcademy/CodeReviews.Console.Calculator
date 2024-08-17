
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Option
    {
        static Operation Op = new Operation();
        static List<string> listItems = new List<string>();

        JsonWriter writer;
        public Option()
        {
            StreamWriter logFile = File.CreateText("calculator.log");

            // to output operation in JSON
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public void GetOperands(string choice)
        {
            // for displaying calculator history
            if (choice == "5")
            {
                Op.DisplayHistory(listItems);
                return;
            }

            try
            {
                Console.WriteLine("Enter 1st value: ");
                double num1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter 2nd value: ");
                double num2 = Convert.ToDouble(Console.ReadLine());
                SelectOption(choice, num1, num2);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input, enter numbers.");
            }
        }

        public void SelectOption(string selection, double n1, double n2)
        {
            string resultMessage;
            double resultValue = 0;

            // to output operation in JSON
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(n1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(n2);
            writer.WritePropertyName("Operation");

            switch (selection)
            {
                case "1":
                    resultValue = Op.Addition(n1, n2);
                    resultMessage = $"Result of sum {n1} + {n2} = {resultValue}";
                    writer.WriteValue("Add");
                    break;
                case "2":
                    resultValue = Op.Difference(n1, n2);
                    resultMessage = $"Result of sub {n1} - {n2} = {resultValue}";
                    writer.WriteValue("Sub");
                    break;
                case "3":
                    resultValue = Op.Multiplication(n1, n2);
                    resultMessage = $"Result of mul {n1} * {n2} = {resultValue}";
                    writer.WriteValue("Mul");
                    break;
                case "4":
                    // ensure divisor is not zero
                    while (n2 == 0)
                    {
                        Console.WriteLine("Divisor can't be zero.");
                        Console.WriteLine("Choose divisor again: ");
                        n2 = Convert.ToDouble(Console.ReadLine());
                    }
                    resultValue = Op.Division(n1, n2);
                    resultMessage = $"Result of div {n1} / {n2} = {resultValue}";
                    writer.WriteValue("Div");
                    break;
                default:
                    resultMessage = "Invalid selection.";
                    writer.WriteValue("Invalid");
                    break;
            }

            writer.WritePropertyName("Result");
            writer.WriteValue(resultValue);
            writer.WriteEndObject();
            Operation.Display(resultMessage);
            listItems.Add(resultMessage);
        }

        public void JsonFinish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
    internal class Operation
    {
        public double Addition(double num1, double num2)
        {
            return num1 + num2;
        }

        public double Difference(double n1, double n2)
        {
            return n1 - n2;
        }

        public double Multiplication(double a, double b)
        {
            return a * b;
        }

        public double Division(double x, double y)
        {
            return x / y;
        }

        public static void Display(string res)
        {
            Console.WriteLine(res);
        }

        public void DisplayHistory(List<string> listItems)
        {
            if (listItems.Count == 0)
            {
                Console.WriteLine("History Empty");
                return;
            }

            Console.WriteLine("Result history:");

            foreach (string i in listItems)
            {
                Console.WriteLine(i);
            }
        }

    }

}

using Newtonsoft.Json;
namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        int operationCount = 0;
        public List<Operation> operationsHistory = new();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorLog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, string op, double num2 = 1)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            string operand = "";
            bool twoNumbers = true;

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    operand = "+";
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    operand = "-";
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    operand = "*";
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        operand = "/";
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    operand = "^";
                    break;
                case "sin":
                    result = Math.Sin(num1 * Math.PI / 180);
                    writer.WriteValue("Sin");
                    operand = "Sin";
                    twoNumbers = false;
                    break;
                case "cos":
                    result = Math.Cos(num1 * Math.PI / 180);
                    writer.WriteValue("Cos");
                    operand = "Cos";
                    twoNumbers = false;
                    break;
                case "log":
                    result = Math.Log(num1);
                    writer.WriteValue("Log");
                    operand = "Log";
                    twoNumbers = false;
                    break;
                case "sqr":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square root");
                    operand = "Sqr";
                    twoNumbers = false;
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            try
            {
                writer.WriteValue(result);
            }
            catch
            {
                writer.WriteValue("Non numerical result");
            }
            writer.WriteEndObject();
            operationCount++;
            if (twoNumbers)
            {
                Operation temp = new Operation($"{num1} {operand} {num2} = ", result, operationCount);
                operationsHistory.Add(temp);
            }
            else
            {
                Operation temp = new Operation($"{operand} {num1} = ", result, operationCount);
                operationsHistory.Add(temp);
            }

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
        public void showRecentOperations()
        {

            Console.WriteLine(@"Recent Operations
--------------------------
Id    Operation      Result
---------------------------");
            if (operationsHistory.Count < 5)
            {
                for (int i = operationsHistory.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine($"a{i,-5} {operationsHistory[i].operationExpression,-15} {operationsHistory[i].result:0.##}");
                }
            }
            else
            {
                for (int i = operationsHistory.Count - 1; i >= operationsHistory.Count - 5; i--)
                {
                    Console.WriteLine($"a{i,-5} {operationsHistory[i].operationExpression,-15} {operationsHistory[i].result:0.##}");

                }
            }
        }
        public void ClearHistory()
        { //method to clear history data from memory, but the log file stays
            bool validInput = true;
            string? input = "";
            do
            {
                Console.WriteLine("This will clear all operation history are you sure? Y/N");
                input = Console.ReadLine();
                if (input == "Y" || input == "N")
                    validInput = true;
                else validInput = false;

            } while (!validInput);
            if (input == "Y")
            {
                this.operationsHistory.Clear();
                this.operationCount = 0;
                return;
            }

            else return;

        }
    }

    public class Operation
    {
        public int id;
        public string operationExpression;
        public double result;
        public Operation(string _operationExpression, double _result, int _id)
        {
            this.id = _id;
            this.operationExpression = _operationExpression;
            this.result = _result;

        }


    }
}
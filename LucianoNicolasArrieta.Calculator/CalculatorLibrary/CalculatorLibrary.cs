using Newtonsoft.Json;
namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        List<string> previousCalculations = new List<string>();
        List<double> previousResults = new List<double>();

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
                    previousCalculations.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    previousCalculations.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    previousCalculations.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        previousCalculations.Add($"{num1} / {num2} = {result}");
                    }
                    writer.WriteValue("Divide");
                    break;
                case "sr":
                    if (num1 >= 0)
                    {
                        result = Math.Sqrt(num1);
                        previousCalculations.Add($"sqrt({num1}) = {result}");
                    }
                    writer.WriteValue("Square Root (num1)");
                    break;
                case "pw":
                    result = Math.Pow(num1, num2);
                    previousCalculations.Add($"{num1} ^ {num2} = {result}");
                    writer.WriteValue("Power");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    previousCalculations.Add($"sin({num1}) = {result}");
                    writer.WriteValue("Sin (num1)");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    previousCalculations.Add($"cos({num1}) = {result}");
                    writer.WriteValue("Cos (num1)");
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    previousCalculations.Add($"tan({num1}) = {result}");
                    writer.WriteValue("Tan (num1)");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            previousResults.Add(result);

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public void PrintLastestCalculations()
        {
            int i = 0;
            Console.WriteLine("------------------------");
            foreach (string calculation in previousCalculations)
            {
                i++;
                Console.WriteLine($"{i}. {calculation}");
            }
            Console.WriteLine("------------------------");
        }

        public double UseAPreviousResult()
        {
            if (!previousCalculations.Any())
            {
                Console.Write("There is no previous calculations done. Please enter a number: ");
                double number;
                while (!double.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Please enter a number.");
                }
                return number;
            } else
            {
                this.PrintLastestCalculations();
                bool validNumber = false;
                int choosenResult = 0;
                string choosen_input;
                while (!validNumber)
                {
                    Console.Write("Please, enter the number with the result you want to use in the calculator: ");
                    choosen_input = Console.ReadLine();
                    if (int.TryParse(choosen_input, out choosenResult))
                    {
                        validNumber = choosenResult <= previousResults.Count();
                    }
                }
                return previousResults[choosenResult - 1];
            }
        }
        
        public void DeleteCalculationHistory()
        {
            previousCalculations.Clear();
            previousResults.Clear();
            Console.WriteLine("Calculation history was deleted successfully!");
        }
    }
}
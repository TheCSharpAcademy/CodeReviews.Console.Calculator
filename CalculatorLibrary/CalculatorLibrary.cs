using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculation
    {
        public string Operation { get; set; }
        public double Result { get; set; }
    }
    public class Calculator
    {
        JsonWriter writer;

        // Store how many calculations were done.
        public int Count { get; private set; }
        public List<Calculation> calculations = new();


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
            double result = double.NaN; // Default value is "not-a-number" if an Operation, such as division, could Result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "1":
                    result = num1 + num2;
                    calculations.Add(new Calculation { Operation = $"{num1} + {num2}", Result = result });
                    writer.WriteValue("Add");
                    break;
                case "2":
                    result = num1 - num2;
                    calculations.Add(new Calculation { Operation = $"{num1} - {num2}", Result = result });
                    writer.WriteValue("Subtract");
                    break;
                case "3":
                    result = num1 * num2;
                    calculations.Add(new Calculation { Operation = $"{num1} * {num2}", Result = result });
                    writer.WriteValue("Multiply");
                    break;
                case "4":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        calculations.Add(new Calculation { Operation = $"{num1} / {num2}", Result = result });
                        writer.WriteValue("Divide");
                    }
                    break;
                case "5":
                    result = Math.Pow(num1, num2);
                    calculations.Add(new Calculation { Operation = $"{num1}^{num2}", Result = result });
                    writer.WriteValue("To the Power Of");
                    break;
                case "6":
                    result = Math.Sqrt(num1);
                    calculations.Add(new Calculation { Operation = $"sqrt({num1})", Result = result });
                    writer.WriteValue("SquareRoot");
                    break;
                case "7":
                    result = 10 * num1;
                    calculations.Add(new Calculation { Operation = $"10*{num1}", Result = result });
                    writer.WriteValue("Multiply by 10");
                    break;
                case "8":
                    result = Math.Sin(num1);
                    calculations.Add(new Calculation { Operation = $"Sin({num1})", Result = result });
                    writer.WriteValue("Sin");
                    break;
                case "9":
                    result = Math.Cos(num1);
                    calculations.Add(new Calculation { Operation = $"Cos({num1})", Result = result });
                    writer.WriteValue("Cos");
                    break;
                case "10":
                    result = Math.Tan(num1);
                    calculations.Add(new Calculation { Operation = $"Tan({num1})", Result = result });
                    writer.WriteValue("Tan");
                    break;
                case "11":
                    result = Math.Asin(num1);
                    calculations.Add(new Calculation { Operation = $"Asin({num1})", Result = result });
                    writer.WriteValue("Asin");
                    break;
                case "12":
                    result = Math.Acos(num1);
                    calculations.Add(new Calculation { Operation = $"Acos({num1})", Result = result });
                    writer.WriteValue("Acos");
                    break;
                case "13":
                    result = Math.Atan(num1);
                    calculations.Add(new Calculation { Operation = $"Atan({num1})", Result = result });
                    writer.WriteValue("Atan");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }





            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            this.Count++;
            return result;
        }

        public void ShowCount()
        {
            Console.WriteLine($"Total number of Operations perfomed: {this.Count}");
        }

        public void ShowCalculations()
        // Display all calculations that have been done.
        {
            for (int i = 0; i < calculations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {calculations[i].Operation} = {calculations[i].Result}");
            }

        }
        public void ClearCalculations()
        // Clear the list of calculations.
        {
            if (calculations.Count == 0)
                Console.WriteLine("No calculations to clear.");
            else
            {
                calculations.Clear();
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

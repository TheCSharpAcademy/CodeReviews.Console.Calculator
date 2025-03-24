using Newtonsoft.Json;
// CalculatorLibrary.cs

namespace consoleCalculator.Samuel_D1906.CalculatorLibrary;

class Calculator
{
    // CalculatorLibrary.cs
    private readonly JsonWriter _writer;

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile);
        _writer.Formatting = Formatting.Indented;
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }
    // CalculatorLibrary.cs
    // CalculatorLibrary.cs
    public double DoOperation(double num1, double num2, string op, List<double> calculations)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Options");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                _writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                _writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                _writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    _writer.WriteValue("Divide");
                }
                break;
            case "sq" :
                result = Math.Sqrt(num1);
                _writer.WriteValue("Square Root");
                break;
            case "p" :
                result = Math.Pow(num1, 10);
                _writer.WriteValue("Power to 10");
                break;
            case "sin" :
                result = Math.Sin(num1);
                _writer.WriteValue("Trigonometry functions Sin");
                break;
            case "cos" :
                result = Math.Cos(num1);
                _writer.WriteValue("Trigonometry functions Cos");
                break;
            case "tan" :
                result = Math.Tan(num1);
                _writer.WriteValue("Trigonometry functions tan");
                break;
            default:
                break;
        }
        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();

        return result;
    }

    public int CountOperations(int count)
    {
        count = count + 1;
        return count;
    }

    public List<double> SaveInList (List<double> calculations ,double result)
    {
        calculations.Add(result);
        return calculations;
    }

    public static void ShowList(List<double> calculations)
    {
        Console.WriteLine("-----------------");
        Console.WriteLine("\n");
        calculations.ForEach(item => Console.Write(item + ","));
        Console.WriteLine("\n");
        Console.WriteLine("-----------------");
    }

    public static void DeleteList(List<double> list) {
        Console.WriteLine("List sucessfully deleted!\n");
        list.Clear();
    }
    

    // CalculatorLibrary.cs
    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }
}

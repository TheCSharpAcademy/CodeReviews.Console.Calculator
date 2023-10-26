using Newtonsoft.Json;
namespace CalculatorLibrary;
public class Calculator
{
    JsonWriter writer;

    public static Dictionary<int, Calculation> historyResults = new Dictionary<int, Calculation>();
    static string[] calculationOptions = new string[] { "a", "s", "m", "d", "r", "p", "x", "i", "c", "t" };
    public static int operationNumber;
    public static double cleanFirstNumber;
    public static double cleanSecondNumber;
    public static bool useResult;


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

    public struct Calculation
    {
        public string body;
        public double result;
    }

    public static string GetNumber(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    public static bool ValidateNumber(string numInput)
    {
        return double.TryParse(numInput, out double _);
    }

    public static bool ValidateUserNumber(string userInput)
    {
        bool result;
        if (ValidateNumber(userInput) && (int.Parse(userInput) < 0 || int.Parse(userInput) > historyResults.Keys.Count))
        {
            Console.WriteLine("No results for this number. Try again!");
            result = false;
        }
        else if (userInput == "d" || userInput == "" || ValidateNumber(userInput))
        {
            result = true;
        }
        else
        {
            Console.WriteLine("Invalid input. Try again!");
            result = false;
        }
        return result;
    }

    public static string GetCalculationOption()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("For operations 'Square Root', '10x', 'Sin', 'Cos', 'Tan', only the first entered number will be used");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tr - Square Root");
        Console.WriteLine("\tp - Power");
        Console.WriteLine("\tx - 10x");
        Console.WriteLine("\ti - Sin");
        Console.WriteLine("\tc - Cos");
        Console.WriteLine("\tt - Tan");

        string option = Console.ReadLine();

        while (!calculationOptions.Contains(option))
        {
            Console.WriteLine("Invalid input. Try again!");
            option = Console.ReadLine();

        }
        return option;
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
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                break;
            case "r":
                result = Math.Sqrt(num1);
                writer.WriteValue("Square Root");
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power");
                break;
            case "x":
                result = Math.Log10(num1);
                writer.WriteValue("10x");
                break;
            case "i":
                result = Math.Sin(num1);
                writer.WriteValue("Sin");
                break;
            case "c":
                result = Math.Cos(num1);
                writer.WriteValue("Cos");
                break;
            case "t":
                result = Math.Tan(num1);
                writer.WriteValue("Tan");
                break;

            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }




    public static void AddOperation(double firstNumber, double secondNumber, double resultCalculation)
    {
        operationNumber++;
        Calculation current = new Calculation() { body = ($"{firstNumber} + {secondNumber}"), result = resultCalculation };
        historyResults.Add(operationNumber, current);
    }

    public static int GetCountOperations()
    {
        var countOperations = historyResults.Count;

        return countOperations;
    }

    public static void PrintHistory()
    {
        foreach (var result in historyResults)
        {
            Console.WriteLine($"{result.Key}. {(result.Value.body)} = {(result.Value.result)}");
        }
    }


    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}
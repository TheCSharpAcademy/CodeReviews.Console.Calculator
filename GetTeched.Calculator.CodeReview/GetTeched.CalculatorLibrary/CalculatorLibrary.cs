using Newtonsoft.Json;
namespace CalculatorLibrary;
public class Calculator
{
    JsonWriter writer;
    int calculatorUsage;
    public Calculator()
    {
        JsonParse jsonParse = new();
        try
        {
            calculatorUsage = jsonParse.GetCalculatorUsageStats();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine($"\nNo calculator usage value found, setting value to 0. Press any key to continue.\n");
            calculatorUsage = 0;
            Console.ReadLine();
        }
        
        StreamWriter logFile = File.CreateText("calculator.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }
    public double StandardDoOperation(double firstNumber, double secondNumber, string operation)
    {
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(firstNumber);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(secondNumber);
        writer.WritePropertyName("OperationType");

        switch (operation)
        {
            case "a":
                result = firstNumber + secondNumber;
                writer.WriteValue("Add");
                calculatorUsage += 1;
                break;
            case "s":
                result = firstNumber - secondNumber;
                writer.WriteValue("Subtract");
                calculatorUsage += 1;
                break;
            case "m":
                result = firstNumber * secondNumber;
                writer.WriteValue("Multiply");
                calculatorUsage += 1;
                break;
            case "d":
                if (secondNumber != 0)
                {
                    result = firstNumber / secondNumber;
                    writer.WriteValue("Divide");
                    calculatorUsage += 1;
                }
                break;
            case "p":
                result = Math.Pow(firstNumber, secondNumber);
                writer.WriteValue("Power of X");
                calculatorUsage += 1;
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        return result;
    }

    public double AdvanceDoOperation(double number, string  operation)
    {
        double result = double.NaN;
        double radians = number * Math.PI / 180;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(number);
        writer.WritePropertyName("OperationType");

        switch (operation)
        {      
            case "q":
                result = Math.Sqrt(number);
                writer.WriteValue("Square Root");
                calculatorUsage += 1;
                break;
            case "x":
                result = Math.Pow(number, 10);
                writer.WriteValue("Power of 10");
                calculatorUsage += 1;
                break;
            case "sin":
                result = Math.Sin(radians);
                writer.WriteValue("Sin");
                calculatorUsage += 1;
                break;
            case "cos":
                result = Math.Cos(radians);
                writer.WriteValue("Cos");
                calculatorUsage += 1;
                break;
            case "tan":
                result = Math.Tan(radians);
                writer.WriteValue("Tan");
                calculatorUsage += 1;
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        return result;
    }
    public void Finish()
    {
        //JsonParse jsonParse = new();

        writer.WriteEndArray();
        writer.WritePropertyName("Usage");
        writer.WriteValue(calculatorUsage);
        writer.WriteEndObject();
        writer.Close();

        //jsonParse.CalculationHistory();
    }
    public void Start()
    {
        StreamWriter logFile = File.CreateText("calculator.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }   
}


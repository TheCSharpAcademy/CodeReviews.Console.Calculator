using Newtonsoft.Json;
namespace CalculatorLibrary;
public class Calculator
{
    JsonWriter writer;
    int calculatorUsage;
    public Calculator()
    {
        JsonParse jsonParse = new();

        calculatorUsage = jsonParse.GetCalculatorUsageStats();
        StreamWriter logFile = File.CreateText("calculator.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }
    public double DoOperation(double firstNumber, double secondNumber, string operation)
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

        writer.WriteEndArray();
        writer.WritePropertyName("Usage");
        writer.WriteValue(calculatorUsage);
        writer.WriteEndObject();
        writer.Close();
    }

    //public int GetCalculatorUsageStats()
    //{
    //    int result = 0;
    //    List<string> previousCalculations = new();

    //    string path = Path.Combine(Environment.CurrentDirectory, "calculator.json");
    //    string json = File.ReadAllText(path);
    //    CalculatorUsageData _calculatorUsage = JsonConvert.DeserializeObject<CalculatorUsageData>(json);
    //    result = Int32.Parse(_calculatorUsage.Usage.ToString());
    //    foreach (var operations in _calculatorUsage.Operations)
    //    {
    //        switch (operations.OperationType)
    //        {
    //            case "Add":
    //                previousCalculations.Add($"{operations.Operand1} + {operations.Operand2} = {operations.Result}");
    //                break;
    //            case "Subtract":
    //                previousCalculations.Add($"{operations.Operand1} - {operations.Operand2} = {operations.Result}");
    //                break;
    //            case "Multiply":
    //                previousCalculations.Add($"{operations.Operand1} X {operations.Operand2} = {operations.Result}");
    //                break;
    //            case "Divide":
    //                previousCalculations.Add($"{operations.Operand1} / {operations.Operand2} = {operations.Result}");
    //                break;
    //        }
    //    }

    //    foreach (string calculations in previousCalculations)
    //    {
    //        Console.WriteLine(calculations);
    //    }
    //    Console.ReadLine();
    //    return result;
    //}

    //public class JSONArray
    //{
    //    public double Operand1;
    //    public double Operand2;
    //    public string OperationType;
    //    public double Result;
    //}

    //public class CalculatorUsageData()
    //{
    //    public List<JSONArray> Operations;
    //    public int Usage;
    //}
}


﻿using Newtonsoft.Json;
namespace CalculatorLibrary;

public class JsonParse
{
    public class JSONArray
    {
        public double Operand1;
        public double Operand2;
        public string OperationType;
        public double Result;
    }
    public class JSONRoot()
    {
        public List<JSONArray> Operations;
        public int Usage;
    }
    public class Caclulations()
    {
        public double firstNumber { get; set; }
        public double secondNumber { get; set; }
        public double result { get; set; }
    }
    public int GetCalculatorUsageStats()
    {
        int result = 0; ;
        string path = Path.Combine(Environment.CurrentDirectory, "calculator.json");
        string json = File.ReadAllText(path);
        JSONRoot root = JsonConvert.DeserializeObject<JSONRoot>(json);
        result = root.Usage;
        return result;
    }
    public List<string> CalculationHistory()
    {
        List<string> previousCalculations = new();

        string path = Path.Combine(Environment.CurrentDirectory, "calculator.json");
        string json = File.ReadAllText(path);
        JSONRoot root = JsonConvert.DeserializeObject<JSONRoot>(json);

        foreach (var operations in root.Operations)
        {
            switch (operations.OperationType)
            {
                case "Add":
                    previousCalculations.Add($"{operations.Operand1} + {operations.Operand2} = {operations.Result}");
                    break;
                case "Subtract":
                    previousCalculations.Add($"{operations.Operand1} - {operations.Operand2} = {operations.Result}");
                    break;
                case "Multiply":
                    previousCalculations.Add($"{operations.Operand1} X {operations.Operand2} = {operations.Result}");
                    break;
                case "Divide":
                    previousCalculations.Add($"{operations.Operand1} / {operations.Operand2} = {operations.Result}");
                    break;
            }
        }
        return previousCalculations;
    }
}

using Newtonsoft.Json;
using static CalculatorLibrary.CalculationsRecord;

namespace CalculatorLibrary;

    public class Calculator
{
    private List<OperationRecord> operationsList;
    public int operationId;
    JsonWriter writer;

    public Calculator()
    {
        operationsList = new List<OperationRecord>();
        operationId = 1;
        //JSON
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1A, double num2A, string operation)
    {
        string operationA = "";
        double resultA = double.NaN;
        //JSON
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1A);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2A);
        writer.WritePropertyName("Operation");

        switch (operation)
        {
            case "+":
                resultA = num1A + num2A;
                operationA = "+";
                writer.WriteValue("Add");
                break;
            case "-":
                resultA = num1A - num2A;
                operationA = "-";
                writer.WriteValue("Subtract");
                break;
            case "*":
                resultA = num1A * num2A;
                operationA = "*";
                writer.WriteValue("Multiply");
                break;
            case "/":
                resultA = num1A / num2A;
                operationA = "/";
                writer.WriteValue("Divide");
                break;
            case "s":
                resultA = Math.Sqrt(num1A);
                operationA = "SquareRoot";
                writer.WriteValue("SquareRoot");
                break;
            case "p":
                resultA = num1A;
                for(int i = 1; i < num2A; i++)
                { 
                    resultA *= num1A;
                }
                operationA = "powerOf";
                writer.WriteValue("Power of");
                break;
            case "i":
                resultA = Math.Sin(num1A * Math.PI / 180);
                operationA = "Sin";
                writer.WriteValue("Sin");
                break;
            case "o":
                resultA = Math.Cos(num1A * Math.PI / 180);
                operationA = "Cos";
                writer.WriteValue("Cos");
                break;
            case "y":
                resultA = Math.Tan(num1A * Math.PI / 180);
                writer.WriteValue("Tan");
                break;
            case "m":
                resultA = num1A * 10;
                operationA = "Tan";
                writer.WriteValue("Multiply by 10");
                break;
            default:
                break;
        }
        //JSON
        writer.WritePropertyName("Result");
        writer.WriteValue(resultA);
        writer.WriteEndObject();

        OperationRecord operationRecord = new OperationRecord
        {
            Id = operationId,
            NumA1 = num1A,
            Operation = operationA,
            NumA2 = num2A,
            ResultA = resultA
        };
        operationsList.Add(operationRecord);
        operationId++;
        return resultA;
    }

    public List<OperationRecord> GetOperationsList()
    {
        return operationsList;
    }

    public void ClearOperationsList()
    {
        operationsList.Clear();
    }

    public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

    public void UIOperationHandler(double num1, double num2, double result, bool endApp, string userOption)
    {
        Console.Clear();

        num1 = GetFirstNumber(num1, userOption);


        Console.WriteLine(@$"Chose an Operator to perform the calculation.
            + - Addition Operation
            - - Subtraction Operation
            * - Multiplication Operation
            / - Division Operation
            s - Square root
            p - Power of
            m - Multiply by 10
            i - Sine (trigonometry function)
            o - Cosine (trigonometry function)
            y - Tangent (trigonometry function)");

        string validOperators = "+-*/spioym";
        string operation = Console.ReadLine();

        while (string.IsNullOrEmpty(operation) || !validOperators.Contains(operation))
        {
            Console.Write("Invalid Operator! Remember, chose an Operator (+, -, *, /, s) to perform the calculation.");
            operation = Console.ReadLine();
        }

            num2 = GetSecondNumber(num2, operation);
        
        try
        {
            result = DoOperation(num1, num2, operation);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine($"Your result is: {result}. Press any key to continue.");
                Console.ReadLine();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
        Console.Clear();
    }

    public double GetFirstNumber(double num1, string userOption)
    {
        if (userOption != "b")
        {
            Console.WriteLine("Type a Number and then Press Enter to continue");
            string input1 = Console.ReadLine();

            while (!double.TryParse(input1, out num1))
            {
                Console.WriteLine("Input should be numeric! Type a number:");
                input1 = Console.ReadLine();
            }
        }
        return num1;
    }


    public double GetSecondNumber(double num2, string operation)
    {
        if (operation != "s" && operation != "i" && operation != "o" && operation != "y" && operation != "m")
        {
            Console.WriteLine("Type a Number and then Press Enter to continue");
            string input2 = Console.ReadLine();

            while (!double.TryParse(input2, out num2))
            {
                Console.WriteLine("Input should be numeric! Type a number:");
                input2 = Console.ReadLine();
            }
            while(num2 == 0 && operation == "/")
            {
                Console.WriteLine("Can't divide by 0! Type another number");
                num2 = int.Parse(Console.ReadLine());
            }
        }
        return num2;
    }

    public double GetResultFromList(int idChosen)
    {
        double result = 0;
        foreach(OperationRecord record in operationsList)
        {
            if (record.Id == idChosen)
            {
                result = record.ResultA;
            }
        }
        return result;
    }
}

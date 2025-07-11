using System.ComponentModel.Design;

namespace CalculatorLibrary;
public class Calculator
{
    private static int calculatorUses;
    private List<Calculation> calculations = new();
    private int nextId = 1;

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        string operation = "";
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                calculatorUses++;
                operation = "+";
                break;
            case "s":
                result = num1 - num2;
                calculatorUses++;
                operation = "-";
                break;
            case "m":
                result = num1 * num2;
                calculatorUses++;
                operation = "*";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    calculatorUses++;
                    operation = "/";
                }
                break;
            case "r":
                result = Math.Sqrt(num1);
                operation = "√";
                break;
            case "p":
                result = Math.Pow(num1, num2);
                operation = $"^{num2}";
                break;
            case "t":
                result = Math.Pow(10, num1);
                operation = "10^";
                break;
            case "sin":
                result = Math.Sin(num1);
                operation = "sin";
                break;
            case "cos":
                result = Math.Cos(num1);
                operation = "cos";
                break;
            case "tan":
                result = Math.Tan(num1);
                operation = "tan";
                break;
        }

        calculations.Add(new Calculation(nextId++, num1, num2, operation, result));
        return result;
    }

    public void ShowHistory()
    {
        Console.Clear();
        if (calculations.Count == 0)
        {
            Console.WriteLine("No calculations yet.");
        }
        else
        {
            Console.WriteLine("Calculation History:\n");
            foreach (var calc in calculations)
            {
                Console.WriteLine($"[{calc.Id}].\t({calc})");
            }
        }
    }

    public Calculation GetCalculationById(int id)
    {
        return calculations.FirstOrDefault(c => c.Id == id);
    }

    public bool HasHistory()
    {
        return (calculations.Count > 0); 
    }

    public void ClearHistory()
    {
        calculations.Clear();
    }

}

public class Calculation
{
    public int Id { get; set; }
    public double Operand1 { get; set; }
    public double Operand2 { get; set; }
    public string Operation { get; set; }
    public double Result { get; set; }

    public Calculation(int id, double operand1, double operand2, string operation, double result)
    {
        Id = id;
        Operand1 = operand1;
        Operand2 = operand2;
        Result = result;
        Operation = operation;
    }

    public override string ToString()
    {
        if (Operation == "sin" || Operation == "cos" || Operation == "tan" || Operation == "√" || Operation == "10^")
            return $"{Operation}({Operand1}) = {Result}";
        if (Operation.StartsWith("^"))
            return $"{Operand1}^{Operand2} = {Result}";
        return $"{Operand1} {Operation} {Operand2} = {Result}";
    }

}


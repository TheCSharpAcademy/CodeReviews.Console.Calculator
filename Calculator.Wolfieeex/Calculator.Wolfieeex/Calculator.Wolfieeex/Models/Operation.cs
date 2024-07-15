namespace Calculator.Wolfieeex.Models;
internal class Operation
{
    public string Name
    {
        get; set;
    }
    public string OperandOne
    {
        get; set;
    }
    public string OperandTwo
    {
        get; set;
    }
    public string ResultingOperand
    {
        get; set;
    }
}

internal class OperationalDetails
{
    public static Dictionary<string, Operation> menuOptions = new Dictionary<string, Operation>()
    {
        { "a", new Operation { Name = "Addition", OperandOne = "Addent", OperandTwo = "second Addent", ResultingOperand = "Sum" } },
        { "s", new Operation { Name = "Subtraction", OperandOne = "Minuend", OperandTwo = "Subtrahend", ResultingOperand = "Difference" } },
        { "m", new Operation { Name = "Multiplication", OperandOne = "Multiplicand", OperandTwo = "Multiplicator", ResultingOperand = "Product" } },
        { "d", new Operation { Name = "Division", OperandOne = "Divisor", OperandTwo = "Dividient", ResultingOperand = "Quotient" } },
        { "t", new Operation { Name = "Trigonometry"} },
        { "p10", new Operation { Name = "Power of ten", OperandOne = "Multiplicand", OperandTwo = "Exponent of power with 10 base", ResultingOperand = "Value" } },
        { "p", new Operation { Name = "Power", OperandOne = "Base", OperandTwo = "Exponent", ResultingOperand = "Value" } },
        { "sr", new Operation { Name = "Square root", OperandOne = "Radicand", ResultingOperand = "Square root" } }
    };

    public static Dictionary<string, Operation> trigonometryOptions = new Dictionary<string, Operation>()
    {
        { "s", new Operation { Name = "Sine", OperandOne = "Angle (in degrees)",  ResultingOperand = "Value" } },
        { "c", new Operation { Name = "Cosine", OperandOne = "Angle (in degrees)",  ResultingOperand = "Value" } },
        { "t", new Operation { Name = "Tangent", OperandOne = "Angle (in degrees)",  ResultingOperand = "Value" } },
        { "as", new Operation { Name = "Arcsine", OperandOne = "Sine value",  ResultingOperand = "Angle (in degrees)" } },
        { "ac", new Operation { Name = "Arccosine", OperandOne = "Cosine value",  ResultingOperand = "Angle (in degrees)" } },
        { "at", new Operation { Name = "Arctangent", OperandOne = "Tangent value",  ResultingOperand = "Angle (in degrees)" } },
    };
}



namespace CalculatorProgram;

public class Calculator
{
    public double Num1 { get; set; }
    public double Num2 { get; set; }
    public double Result { get; set; }
    public string OperationType { get; set; }

    //here I am setting Num2 as an optional parameter, for use in the DoOperation method
    public Calculator(double num1, string operationType, double num2 = double.NaN)
    {
        Num1 = num1;
        Num2 = num2;
        OperationType = operationType;
        Result = double.NaN;
    }

    public void DoOperation()
    {
        if (!double.IsNaN(Num2))
        {
            switch (OperationType)
            {
                case "a": Result = Num1 + Num2; break;
                case "s": Result = Num1 - Num2; break;
                case "m": Result = Num1 * Num2; break;
                case "d": if (Num2 != 0) Result = Num1 / Num2; break;
                case "p": Result = Math.Pow(Num1, Num2); break;
                default: break;
            }
        }

        else
        {
            switch (OperationType)
            {
                case "r": Result = Math.Sqrt(Num1); break;
                case "i": Result = Math.Sin(Num1); break;
                case "c": Result = Math.Cos(Num1); break;
                case "t": Result = Math.Tan(Num1); break;
                case "x": Result = Num1 * 10; break;
                default: break;
            }
        }
    }
}
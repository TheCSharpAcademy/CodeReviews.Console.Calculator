using CalculatorLibrary.enums;

namespace CalculatorLibrary;

public class Calculator
{
  public Operations Operation { get; set; }

  public Calculator() { }

  public double Calculate(double num1, double num2)
  {
    double result = double.NaN;

    switch (Operation)
    {
      case Operations.Add:
        result = num1 + num2;
        break;
      case Operations.Subtract:
        result = num1 - num2;
        break;
      case Operations.Multiply:
        result = num1 * num2;
        break;
      case Operations.Divide:
        while (num2 == 0)
        {
          Console.WriteLine("Please enter non-zero number.");
          num2 = Convert.ToDouble(Console.ReadLine());
        }
        result = num1 / num2;
        break;
      default:
        break;
    }
    return result;
  }

  public double Square(double num)
  {
    return Math.Sqrt(num);
  }

  public double Power(double num, double pow)
  {
    return Math.Pow(num, pow);
  }
}
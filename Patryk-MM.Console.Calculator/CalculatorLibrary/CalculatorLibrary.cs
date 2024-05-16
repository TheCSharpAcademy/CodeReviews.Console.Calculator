using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CalculatorLibrary;
public class Calculator {
    private const string FilePath = "calculator.json";
    public int UseCount { get; set; }
    public List<Calculation> Calculations { get; set; } = new List<Calculation>();
    public Calculator() {
        ClearJsonFile();
    }

    private void ClearJsonFile() {
        try {
            File.WriteAllText(FilePath, "");
        } catch (IOException e) {
            Console.WriteLine("An error occurred while clearing the file: " + e.Message);
        }
    }

    public Calculation HandleTwoParameterOperation(Calculation calc) {
        switch (calc.Operation) {
            case '+': // Addition
                calc.Result = calc.Num1 + calc.Num2;
                break;
            case '-': // Subtraction
                calc.Result = calc.Num1 + calc.Num2;
                break;
            case '*': // Multiplication
                calc.Result = calc.Num1 + calc.Num2;
                break;
            case '/': // Division
                while (calc.Num2 == 0) {
                    Console.Write("Please provide a non-zero divisor:");
                    double temp;

                    while (!double.TryParse(Console.ReadLine(), out temp)) {
                        Console.Write("Please input a valid numeric value: ");
                    }
                    calc.Num2 = temp;
                }
                calc.Result = calc.Num1 / calc.Num2;
                break;
            case 'E': // Exponentiation
                calc.Result = Math.Pow(calc.Num1, calc.Num2);
                break;
        }
        Console.WriteLine($"\rResult: {calc.Result}");

        return calc;
    }

    public Calculation HandleSingleParameterOperation(Calculation calc) {

        switch (calc.Operation) {
            case 'R': // Square Root
                calc.Result = Math.Sqrt(calc.Num1);
                break;
            case 'P': // 10^x
                calc.Result = Math.Pow(10, calc.Num1);
                break;
            case 'S': // Sine
                calc.Result = Math.Sin(calc.Num1);
                break;
            case 'C': // Cosine
                calc.Result = Math.Cos(calc.Num1);
                break;
            case 'T': // Tangent
                calc.Result = Math.Tan(calc.Num1);
                break;
            default:
                throw new ArgumentException("Invalid operation");
        }
        Console.WriteLine($"\rResult: {calc.Result}");
        return calc;
    }

}

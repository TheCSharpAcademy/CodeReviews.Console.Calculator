using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator {
    private const string FilePath = "calculator.json";
    public int UseCount { get; set; }
    public List<Calculation> Calculations { get; set; } = new List<Calculation>();
    public Calculator() {
        ClearJsonFile();
    }

    public void WriteToJson() {
        using(StreamWriter sw = new(FilePath)) {
            string json = JsonConvert.SerializeObject(Calculations, Formatting.Indented);
            sw.WriteLine(json);
        }
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
                calc.Result = calc.Num1 - calc.Num2;
                break;
            case '*': // Multiplication
                calc.Result = calc.Num1 * calc.Num2;
                break;
            case '/': // Division
                while (calc.Num2 == 0) {
                    TextFormat.Write("Please provide a non-zero divisor: ", ConsoleColor.Red);
                    double temp;

                    while (!double.TryParse(Console.ReadLine(), out temp)) {
                        TextFormat.Write("Please input a valid numeric value: ", ConsoleColor.Red);
                    }
                    calc.Num2 = temp;
                }
                calc.Result = calc.Num1 / calc.Num2;
                break;
            case 'E': // Exponentiation
                calc.Result = Math.Pow(calc.Num1, calc.Num2);
                break;
        }
        TextFormat.WriteLine($"\rResult: {calc.Result}", ConsoleColor.Cyan);
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
        TextFormat.WriteLine($"\rResult: {calc.Result}", ConsoleColor.Cyan);
        return calc;
    }


}

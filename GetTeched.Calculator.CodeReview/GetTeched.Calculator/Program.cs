using System.Text.RegularExpressions;
namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApplication = false;

        CalculatorMenu calculatorMenu = new();
        string regexPattern = "^((q|x|sin|cos|tan),)*(q|x|sin|cos|tan)$";
        while (!endApplication)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            string operation = calculatorMenu.CalculatorOptions();
            if (Regex.IsMatch(operation, "^((l|u),)*(l|u)$"))
            {
                endApplication = calculatorMenu.CalculatorOperation(0, 0, operation);
            }
            else if (Regex.IsMatch(operation, regexPattern))
            {
                double[] numbers = calculatorMenu.InputValues(true);
                endApplication = calculatorMenu.CalculatorOperation(numbers[0], 0, operation);
            }
            else
            {
                double[] numbers = calculatorMenu.InputValues(false);
                endApplication = calculatorMenu.CalculatorOperation(numbers[0], numbers[1], operation);
            }
            
        }
        return;
    }
}



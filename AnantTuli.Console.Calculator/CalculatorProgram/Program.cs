using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static double GetNumericalInput()
        {
            Console.Write("Type a number, and then press Enter: ");

            string? numInput = Console.ReadLine();
            double cleanNum;

            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput = Console.ReadLine();
            }

            return cleanNum;
        }

        static void Main(string[] args)
        {
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            CalculationData calculationData = new CalculationData();
            calculationData.Init();

            Calculator calculator = new Calculator(calculationData);

            while (!endApp)
            {
                double result = 0;

                Console.WriteLine($@"
                Choose an operator from the following list:
                    a - Add
                    s - Subtract
                    m - Multiply
                    d - Divide
                    p - x^y
                    r - Square root
                    x - 10x
                    n - Sin(x)
                    c - Cos(x)
                    ----
                    1 - View Calculator used count
                    2 - Delete calculations history
                    3 - View history
                    ----
                    Your option?");

                string? op = Console.ReadLine();

                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|x|n|c|1|2|3]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        bool isBinaryOperation = Regex.IsMatch(op, "[a|s|m|d|p]");
                        bool isUnaryOperation = Regex.IsMatch(op, "[r|x|n|c]");
                        bool isOperation = isBinaryOperation || isUnaryOperation;

                        if (isOperation)
                        {
                            double num1;
                            num1 = GetNumericalInput();

                            if (isBinaryOperation)
                            {
                                result = calculator.DoBinaryOperation(num1, GetNumericalInput(), op);
                            }
                            else if (isUnaryOperation)
                            {
                                result = calculator.DoUnaryOperation(num1, op);
                            }

                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                            }
                        }
                        else
                        {
                            switch (op)
                            {
                                case "1":
                                    Console.WriteLine($"\n\tCalculator launched {calculator.calculationData.numCalculatorLaunches} times\n");
                                    break;
                                case "2":
                                    calculator.calculationData.DeleteCalculationHistory();
                                    Console.WriteLine($"\nHistory cleared");
                                    break;
                                case "3":
                                    CalculationData calcData = calculator.calculationData;
                                    for (int i = 0; i < calcData.calculations.Count; i++)
                                    {
                                        Calculation calculation = calcData.calculations[i];
                                        string printedCalculation = calculation.nums.Length > 1 ?
                                            string.Join($" {calculation.mathematicalOperator} ", calculation.nums) :
                                            $"{calculation.mathematicalOperator} {calculation.nums[0]}\t=\t{calculation.result}";

                                        Console.WriteLine($"#{i + 1}\t\t{printedCalculation}");
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n")
                {
                    endApp = true;
                }

                Console.WriteLine("\n");
            }

            calculator.Finish();
            return;
        }
    }
}
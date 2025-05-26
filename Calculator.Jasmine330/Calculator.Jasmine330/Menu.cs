using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace Calculator.Jasmine330
{
    internal class Menu
    {
        private readonly CalculatorLibrary.Calculator _calculator;
        private readonly List<double> _answers;
        private int _operationCount;
        public Menu()
        {
            _calculator = new CalculatorLibrary.Calculator();
            _answers = new List<double>();

            _operationCount = 0;
        }

        public void Run()
        {
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("_____________________________");

            while (!endApp)
            {
                _operationCount++;
                DisplayMenu();
                string? op = GetOperatorChoice();

                if (Helpers.IsValidOperator(op))
                {
                    PerformCalculation(op);
                }
                else
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }

                Console.WriteLine("-----------------------------------------------");

                if (ShouldEndApplication())
                {
                    endApp = true;
                }
                else
                {
                    AskToViewResults();
                }
                Console.WriteLine("\n");
            }

            DisplayUsageCount();
            _calculator.Finish();
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine(@"Choose an operator from the following list:  
            a    - Add  
            s    - Subtract  
            m    - Multiply  
            d    - Divide  
            sqrt - Square Root  
            pow  - Power (x^y)  
            10x  - 10 to the power of x  
            trig - Trigonometric functions  

            Your Option?");
        }

        private string? GetOperatorChoice()
        {
            return Console.ReadLine()?.ToLower();
        }

        private void PerformCalculation(string op)
        {
            try
            {
                double[] numbers = Helpers.GetInputs(op);
                double cleanNum1 = numbers[0];
                double cleanNum2 = (numbers.Length > 1) ? numbers[1] : 0;

                double result = _calculator.DoOperation(cleanNum1, cleanNum2, op);
                Helpers.ValidateResult(result);

                Console.WriteLine($"Your result: {result}");
                _answers.Add(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oh no! An exception occurred trying to do the math. \n Details: {e.Message}");
            }
        }

        private bool ShouldEndApplication()
        {
            Console.Write("Press 'n' and Enter to close the app. To continue just press Enter: ");
            return Console.ReadLine()?.TrimEnd().ToLower() == "n";
        }

        private void AskToViewResults()
        {
            Console.Write("Would you like to view the results? (y/n): ");
            string? view = Console.ReadLine()?.TrimEnd().ToLower();
            if (view == "y")
            {
                Helpers.DisplayResults(_answers);
            }
        }

        private void DisplayUsageCount()
        {
            Console.WriteLine($"You used the calculator {_operationCount} times.");
        }
    }
}

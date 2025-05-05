using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace Calculator.Jasmine330
{
    internal class Menu
    {
        public Menu()
        {
            bool endApp = false;
            int count = 0;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("_____________________________");

            CalculatorLibrary.Calculator calculator = new CalculatorLibrary.Calculator();
            List<double> answers = new List<double>();

            while (!endApp)
            {
                count++;
                double result = 0;
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

                string? op = Console.ReadLine();

                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sqrt|pow|10x|trig]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        double[] numbers = Helpers.GetInputs(op);
                        double cleanNum1 = numbers[0];
                        double cleanNum2 = numbers[1];

                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        Helpers.ValidateResult(result);
                        answers.Add(result); 
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n Details: " + e.Message);
                    }
                }

                Console.WriteLine("________________________________________");
                
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
                Console.WriteLine("\n");

                Console.Write("Would you like to view the results? (y/n): ");
                string view = Console.ReadLine()?.ToLower();
                if (view == "y")
                {
                    Helpers.DisplayResults(answers);
                }

                Console.WriteLine("\n__________________________________________");
            }

            Console.WriteLine($"You  used the calculator {count} times.");
            calculator.Finish();
            return;
        }
    }
}

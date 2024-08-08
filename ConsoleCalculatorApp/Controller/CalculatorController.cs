using CalculatorLibrary;
using Console_Calculator_App.ConsoleCalculatorApp.Model;
using Console_Calculator_App.ConsoleCalculatorApp.View;

namespace Console_Calculator_App.ConsoleCalculatorApp.Controller
{
    internal class CalculatorController
    {
        private static readonly Calculator _calculator = new Calculator();

        public static void Run()
        {
            MathProblem mathProblem = new MathProblem();
            do
            {
                Menu.DisplayFirstNum();
                mathProblem.Input1= Console.ReadLine()!;
                mathProblem.Num1 = ParseInput(mathProblem.Input1);

                Menu.DisplaySecondNum();
                mathProblem.Input2 = Console.ReadLine()!;
                mathProblem.Num2 = ParseInput(mathProblem.Input2);

                Menu.DisplayOperation();
                mathProblem.Operation = Console.ReadLine()!.ToLower();
                mathProblem.Answer = _calculator.CalculateAnswer(
                                                        mathProblem.Operation, 
                                                        mathProblem.Num1,
                                                        mathProblem.Num2);

                if (!float.IsNaN(mathProblem.Answer))
                {
                    Menu.DisplayAnswer(mathProblem.Answer);
                }
                else
                {
                    Menu.DisplayError();
                }

                Menu.DisplayEnd();
                string input = Console.ReadLine()!;
                if (input == "n")
                {
                    _calculator.Finish();
                    System.Environment.Exit(0);
                }

            } while (true);
        }
        private static float ParseInput(string input)
        {
            float num;

            do
            {
                try
                {
                    num = float.Parse(input);
                    break;
                }
                catch (Exception)
                {
                    Menu.DisplayInvalidInput();
                    input = Console.ReadLine()!;
                }
            } while (true);

            return num;
        }

    }
}

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
            do
            {
                MathProblem mathProblem = new MathProblem();
                Menu.FirstNum();
                mathProblem.Num1 = ParseInput(Console.ReadLine()!);

                Menu.SecondNum();
                mathProblem.Num2 = ParseInput(Console.ReadLine()!);

                Menu.Operation();
                mathProblem.Operation = Console.ReadLine()!.ToLower();
                mathProblem.Answer = _calculator.CalculateAnswer(
                                                        mathProblem.Operation, 
                                                        mathProblem.Num1,
                                                        mathProblem.Num2);

                if (!float.IsNaN(mathProblem.Answer))
                {
                    Menu.Answer(mathProblem.Answer);
                }
                else
                {
                    Menu.Error();
                }

                Menu.End();
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
                    Menu.InvalidInput();
                    input = Console.ReadLine()!;
                }
            } while (true);

            return num;
        }

    }
}

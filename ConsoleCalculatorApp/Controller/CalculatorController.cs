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
            IList<MathProblem> mathProblems = new List<MathProblem>();
            do
            {
                MathProblem mathProblem = new MathProblem();
                
                Menu.Title(mathProblems.Count());
                Menu.Options();
                string input = Console.ReadLine()!;
                if (input == "n")
                {
                    _calculator.Finish();
                    System.Environment.Exit(0);
                }
                else if (input == "d")
                {
                    mathProblems = new List<MathProblem>();
                }
                else if (input == "u")
                {
                    Menu.ViewList(mathProblems);
                    Menu.PickAResult(1);
                    input = Console.ReadLine()!;
                    if (input == "r")
                    {
                        continue;
                    }
                    else if (Int32.TryParse(input, out _) == true)
                    {
                        mathProblem.Num1 = mathProblems[ParseIndex(input, mathProblems.Count)].Answer;
                    }
                    if (mathProblem.Num1 != float.NaN)
                    {
                        Menu.PickAResult(2);
                        input = Console.ReadLine()!;
                        if (input == "r")
                        {
                            continue;
                        }
                        else if (Int32.TryParse(input, out _) == true)
                        {
                            mathProblem.Num2 = mathProblems[ParseIndex(input, mathProblems.Count)].Answer;
                        }
                    }
                }

                if (float.IsNaN(mathProblem.Num1))
                {
                    Menu.FirstNum();
                    mathProblem.Num1 = ParseInput(Console.ReadLine()!);
                }

                if (float.IsNaN(mathProblem.Num2))
                {
                    Menu.SecondNum();
                    mathProblem.Num2 = ParseInput(Console.ReadLine()!);
                }

                Menu.Operation();
                mathProblem.Operation = Console.ReadLine()!.ToLower();
                mathProblem.Answer = _calculator.CalculateAnswer(mathProblem.Operation, mathProblem.Num1, mathProblem.Num2);

                mathProblems.Add(mathProblem);

                if (!float.IsNaN(mathProblem.Answer))
                {
                    Menu.Answer(mathProblem.Answer);
                }
                else
                {
                    Menu.Error();
                }

                Menu.End();
                Console.ReadLine();

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

        private static int ParseIndex(string input, int max)
        {
            int num;

            do
            {
                try
                {
                    num = Int32.Parse(input);
                    if (num >= max || num < 0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    break;
                }
                catch (Exception)
                {
                    Menu.InvalidIndex(max);
                    input = Console.ReadLine()!;
                }
            } while (true);

            return num;
        }

    }
}

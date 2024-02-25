namespace ConsoleUI
{
    public static class ConsoleHelper
    {
        public static string GetStringFromUser(string message)
        {
            string output;
            string userInput;
            do
            {
                Console.Write($"{message}: ");
                userInput = Console.ReadLine();
            } while (!IsValidInput(userInput, InputType.CHOICE));

            output = userInput.Trim().ToLower();
            return output;
        }
        public static double GetNumberFromUser(string message)
        {
            double output;
            string userInput;
            do
            {
                Console.Write($"{message}: ");
                userInput = Console.ReadLine();
            } while (!IsValidInput(userInput, InputType.NUMBER));
            output = userInput.CastToDouble();
            return output;
        }
        public static bool IsValidInput(string input, InputType option)
        {
            bool result = false;
            if (!input.IsEmptyOrNull())
            {
                switch (option)
                {
                    case InputType.NUMBER:
                        {
                            result = double.TryParse(input, out double temp);
                            break;
                        }
                    case InputType.CHOICE:
                        {
                            result = input.IsValidChoice();
                            break;
                        }
                }
            }
            return result;

        }
        public static bool IsEmptyOrNull(this string input)
        {
            return input == null || input == string.Empty;
        }
        public static bool IsValidChoice(this string input)
        {
            bool result = false;
            if (char.TryParse(input.Trim(), out char choice))
            {
                result = true;
            }
            return result;

        }
        public static double CastToDouble(this string input)
        {
            double.TryParse(input, out double result);
            return result;
        }
        public static double GetNumberOrUsePickedHistoryResult(string message, double? historyResult = null)
        {
            double result;

            if (historyResult == null)
            {
                result = ConsoleHelper.GetNumberFromUser(message);
            }
            else
            {
                Console.Write($"{message} - or Enter 'ans' to use picked result from result: ");
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == "ans")
                {
                    result = historyResult.Value;
                }
                else
                {
                    while (!double.TryParse(answer, out result))
                    {
                        Console.Write($"{message}: ");
                        answer = Console.ReadLine();
                    }
                }
            }

            return result;
        }

    }
}

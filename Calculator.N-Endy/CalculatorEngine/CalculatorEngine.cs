using System.Text.RegularExpressions;
using Calculator.N_Endy.UserInteractionRepository;

namespace Calculator.N_Endy.CalculatorEngine
{
    public class CalculatorEngine
    {
        private readonly IUserInteraction _userInteraction;

        public CalculatorEngine(IUserInteraction userInteraction)
        {
            _userInteraction = userInteraction;
        }
        public void Run()
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            double numInput1 = 0;
            double numInput2 = 0;

            // Display Introductory message
            _userInteraction.DisplayIntroductoryMessage();

            // Ask user for first number
            numInput1 = _userInteraction.GetNumberFromUser();

            // Ask user for second number
            numInput2 = _userInteraction.GetNumberFromUser();

            // Ask user for operator
            string op = "";
            do
            {
                op = _userInteraction.GetOperatorFromUser();
            } while (! ValidateOperator(op));
        }


        // Validate operator input is not null and matches the pattern
        public bool ValidateOperator(string op)
        {
            if (op == null || Regex.IsMatch(op, "[a|s|m|d]"))
            {
                _userInteraction.ShowMessage("Error: Invalid operator\n");
                return false;
            }
            else
            {
                return true;
            }
        }
        // Ask user for first number
        // Ask user for second number
        // Ask user for operator
        // Calculate and display result
        // Ask to play again
    }
}
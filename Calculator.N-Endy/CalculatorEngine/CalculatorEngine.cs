using System.Text.RegularExpressions;
using Calculator.N_Endy.UserInteractionRepository;
using MyCalculator = CalculatorLibrary.Calculator;

namespace Calculator.N_Endy.CalculatorEngine
{
    public class CalculatorEngine
    {
        private readonly IUserInteraction _userInteraction;
        private readonly MyCalculator _calculator;
        private int _numOfAppRuns;

        public CalculatorEngine(IUserInteraction userInteraction, MyCalculator calculator)
        {
            _userInteraction = userInteraction;
            _calculator = calculator;

        }

        private bool endApp;

        public void Run()
        {
            // Check if "apprun.txt" file exists.
            if (File.Exists("apprun.txt"))
            {
                // Read the number of app runs from the file.
                string runsText = File.ReadAllText("apprun.txt");
                int.TryParse(runsText, out _numOfAppRuns);
            }
            else
            {
                _numOfAppRuns = 0;
            }
            // Increment the number of app runs
            _numOfAppRuns++;
            // Display number of app runs.
            _userInteraction.ShowMessage("Number of app runs: " + _numOfAppRuns + "\n");

            while (!endApp)
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
                } while (!ValidateOperator(op));

                // Calculate and Display result
                DisplayResult(op, numInput1, numInput2);

                _userInteraction.ShowMessage("--------------------------------\n\n");

                // Ask to continue
                _userInteraction.ShowMessage("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");

                string continueCalculation = _userInteraction.GetInputFromUser();
                if (continueCalculation == "n") endApp = true;
            }
            
            // Store updated number of app runs in the "apprun.txt" file.
            File.WriteAllText("apprun.txt", _numOfAppRuns.ToString());

            // Add call to close the JSON writer.
            _calculator.Finish();

            return;
        }

        // Validate operator input is not null and matches the pattern
        public bool ValidateOperator(string op)
        {
            if (op == null || ! Regex.IsMatch(op, "[a|s|m|d]"))
            {
                _userInteraction.ShowMessage("Error: Invalid operator\n");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DisplayResult(string op, double num1, double num2)
        {
            try
            {
                double result = _calculator.DoOperation(num1, num2, op);
                if (double.IsNaN(result))
                    _userInteraction.ShowMessage("This operation will result in a mathematical error.\n");
                else
                    _userInteraction.ShowMessage("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                _userInteraction.ShowMessage("Oh no! An exception occurred while trying to do the math.\n - Details: " + e.Message);
            }
        }
    }
}
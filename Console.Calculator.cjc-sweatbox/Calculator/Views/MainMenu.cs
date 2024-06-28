// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.MainMenu
// -------------------------------------------------------------------------------------------------
// The main menu console view of the application.
// -------------------------------------------------------------------------------------------------
using System.Text;
using CalculatorLibrary;
using CalculatorLibrary.Constants;
using CalculatorLibrary.Models;
using CalculatorProgram.Enums;
using CalculatorProgram.Utilities;

namespace CalculatorProgram.Views;

internal class MainMenu
{
    #region Constants

    private const string PageTitle = "Main Menu";

    #endregion
    #region Variables

    private readonly Calculator _calculator;

    #endregion
    #region Constructors

    public MainMenu()
    {
        _calculator = new Calculator();
    }

    #endregion
    #region Properties
    
    internal static string MenuText
    {
        get
        {
            var sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine($"{Application.Title}: {PageTitle}");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("");
            sb.AppendLine("N - New calculation");
            sb.AppendLine("R - Recall a result and perform a new calculation");
            sb.AppendLine("V - View history");
            sb.AppendLine("C - Clear history");
            sb.AppendLine("Q - Quit the application");
            sb.AppendLine("");
            
            return sb.ToString();
        }
    }

    #endregion
    #region Methods: Internal

    internal ProgramStatus Show()
    {
        Console.Clear();
        Console.Write(MenuText);
        
        var option = UserInputReader.GetChar("Enter your selection: ", AllowedChars.MainMenuInput);

        return PerformOption(option);
    }

    internal void Close()
    {
        _calculator.Finish();
    }

    #endregion
    #region Methods: Private

    private ProgramStatus PerformOption(char option)
    {
        // Retain Started unless explicitly change (i.e. quit chosen).
        var output = ProgramStatus.Started;

        // Normalise input option.
        option = char.ToLower(option);

        switch (option)
        {
            case 'n':
                // New calculation.
                var newCalculation = CalculationPage.Show();
                newCalculation = _calculator.DoOperation(newCalculation);
                ShowResult(newCalculation);
                break;
            case 'r':
                // Recall calculation.
                if (_calculator.HasHistoryItems)
                {
                    var firstNumber = RecallPage.Show(_calculator.History);
                    var recallCalculation = CalculationPage.Show(firstNumber);
                    recallCalculation = _calculator.DoOperation(recallCalculation);
                    ShowResult(recallCalculation);
                }
                else
                {
                    MessagePage.Show("Recall", "No history items to recall.");
                }
                break;
            case 'v':
                // View calculation history.
                if (_calculator.HasHistoryItems)
                {
                    HistoryPage.Show(_calculator.History);
                }
                else
                {
                    MessagePage.Show("History", "No history items to view.");
                }
                break;
            case 'c':
                // Clear calculation history.
                _calculator.ClearHistory();
                MessagePage.Show("History", "History cleared.");
                break;
            case 'q':
                // Quit.
                output = ProgramStatus.Stopped;
                break;
            default:
                MessagePage.Show("Error", "Invalid option selected.");
                break;
        }

        return output;
    }

    private void ShowResult(Calculation calculation)
    {
        var sb = new StringBuilder();
        sb.AppendLine(calculation.ToString());
        sb.AppendLine();
        sb.Append($"Calculations performed: {_calculator.UsageCount}");
        MessagePage.Show("Result", sb.ToString());
    }

    #endregion
}

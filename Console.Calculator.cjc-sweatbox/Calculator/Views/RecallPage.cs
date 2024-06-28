// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.RecallPage
// -------------------------------------------------------------------------------------------------
// The recall page console view of the application.
// -------------------------------------------------------------------------------------------------
using System.Text;
using CalculatorLibrary.Constants;
using CalculatorLibrary.Models;
using CalculatorProgram.Utilities;

namespace CalculatorProgram.Views;

internal class RecallPage
{
    #region Constants

    private const string PageTitle = "Recall";

    #endregion
    #region Methods: Internal Static

    internal static double Show(List<Calculation> history)
    {
        Console.Clear();
        Console.Write(MenuText(history));

        // Note: Id is 1-based, and index is 0-based. So take 1 from user input.
        var option = UserInputReader.GetInt("Enter the id of result to recall: ", 1, history.Count) - 1;

        return history[option].Result;
    }

    #endregion
    #region Methods: Private Static

    private static string MenuText(List<Calculation> history)
    {
        var sb = new StringBuilder();
        sb.AppendLine("----------------------------------------");
        sb.AppendLine($"{Application.Title}: {PageTitle}");
        sb.AppendLine("----------------------------------------");
        sb.AppendLine("");

        // Note: We can only get here by having items in the history.
        for (int i = 0; i < history.Count; i++)
        {
            sb.AppendLine($"{i + 1}: {history[i]}");
        }
        sb.AppendLine("");

        return sb.ToString();
    }

    #endregion
}

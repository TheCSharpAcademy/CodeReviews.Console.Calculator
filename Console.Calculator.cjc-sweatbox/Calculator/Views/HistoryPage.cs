// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.HistoryPage
// -------------------------------------------------------------------------------------------------
// The history page console view of the application.
// -------------------------------------------------------------------------------------------------
using System.Text;
using CalculatorLibrary.Models;

namespace CalculatorProgram.Views;

internal class HistoryPage : BasePage
{
    #region Constants

    private const string PageTitle = "History";

    #endregion
    #region Methods: Internal Static

    internal static void Show(List<Calculation> history)
    {
        WriteHeader(PageTitle);

        Console.Write(GetMenuText(history));

        WriteFooter();

        // Await user input to contine.
        Console.Read();
    }

    #endregion
    #region Methods: Private Static

    private static string GetMenuText(List<Calculation> history)
    {
        var sb = new StringBuilder();
        if (history.Count > 0)
        {
            foreach (var item in history)
            {
                sb.AppendLine(item.ToString());
            }
        }
        else
        {
            sb.AppendLine("History empty.");
        }

        return sb.ToString();
    }

    #endregion
}

// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.HistoryMenu
// -------------------------------------------------------------------------------------------------
// The history page console view of the application.
// -------------------------------------------------------------------------------------------------
using System.Text;
using CalculatorLibrary.Constants;
using CalculatorLibrary.Models;

namespace CalculatorProgram.Views;

internal class HistoryPage
{
    #region Constants

    private const string PageTitle = "History";

    #endregion
    #region Methods: Internal Static

    internal static void Show(List<Calculation> history)
    {
        Console.Clear();
        Console.Write(MenuText(history));
        Console.WriteLine("Press any key to continue...");
        Console.Read();
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
        sb.AppendLine("");
        
        return sb.ToString();
    }

    #endregion
}

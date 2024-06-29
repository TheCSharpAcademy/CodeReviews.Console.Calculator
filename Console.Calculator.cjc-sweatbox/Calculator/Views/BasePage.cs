// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.BasePage
// -------------------------------------------------------------------------------------------------
// The base class for a console view page.
// -------------------------------------------------------------------------------------------------
using System.Text;
using CalculatorLibrary.Constants;

namespace CalculatorProgram.Views;

internal class BasePage
{
    #region Methods: Protected Static

    private static string GetHeaderText(string title)
    {
        var sb = new StringBuilder();
        sb.AppendLine("----------------------------------------");
        sb.AppendLine($"{Application.Title}: {title}");
        sb.AppendLine("----------------------------------------");
        sb.AppendLine();
        return sb.ToString();
    }

    protected static void WriteFooter()
    {
        Console.Write($"{Environment.NewLine}Press any key to continue...");
    }

    protected static void WriteHeader(string title)
    {
        Console.Clear();
        Console.Write(GetHeaderText(title));
    }

    #endregion
}

// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.MessagePage
// -------------------------------------------------------------------------------------------------
// A page which displays a parameterised message and title.
// -------------------------------------------------------------------------------------------------

namespace CalculatorProgram.Views;

internal class MessagePage : BasePage
{
    #region Methods: Internal Static

    internal static void Show(string title, string message)
    {
        Console.Clear();

        WriteHeader(title);

        Console.WriteLine(message);

        WriteFooter();

        // Await user confirmation to continue.
        Console.Read();
    }

    #endregion
}

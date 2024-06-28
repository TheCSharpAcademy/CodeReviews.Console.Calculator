// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.MessagePage
// -------------------------------------------------------------------------------------------------
// A page which displays a parameterised message and title.
// -------------------------------------------------------------------------------------------------
using CalculatorLibrary.Constants;

namespace CalculatorProgram.Views;

internal static class MessagePage
{
    #region Methods: Internal Static

    internal static void Show(string title, string message)
    {
        // Clear the console.
        Console.Clear();

        // Write the header.
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"{Application.Title}: {title}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");

        // Write the message.
        Console.WriteLine(message);

        // Await user confirmation to continue.
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.Read();
    }

    #endregion
}

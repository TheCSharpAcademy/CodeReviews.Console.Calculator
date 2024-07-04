// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Program
// -------------------------------------------------------------------------------------------------
// The insertion point of the console application.
// -------------------------------------------------------------------------------------------------
using CalculatorProgram.Enums;
using CalculatorProgram.Views;

namespace CalculatorProgram;

internal class Program
{
    #region Methods: Private Static

    private static void Main(string[] args)
    {
        var status = ProgramStatus.Started;

        var mainMenu = new MainMenu();
        while (status != ProgramStatus.Stopped)
        {
            status = mainMenu.Show();
        }
        mainMenu.Close();

        return;
    }

    #endregion
}

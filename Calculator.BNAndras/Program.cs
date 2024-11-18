using Calculator.BNAndras.CalculatorProgram.Models;

namespace Calculator.BNAndras.CalculatorProgram;

class Program
{
    static void Main()
    {
        MenuController.ShowWelcomeScreen();

        double previousResult = double.NaN;
        while (true)
        {
            MenuOperation menuOperation = MenuController.GetMenuOperation();

            if (menuOperation is MenuOperation.ReuseLastResult)
            {
                previousResult = MenuController.ReuseLastResult();
                menuOperation = MenuOperation.CalculateValue;
            }

            switch (menuOperation)
            {
                case MenuOperation.Quit:
                    MenuController.Quit();
                    break;

                case MenuOperation.ShowHistory:
                    MenuController.ShowHistory();
                    break;

                case MenuOperation.ClearHistory:
                    MenuController.ClearHistory();
                    break;

                case MenuOperation.DisplayOperationCount:
                    MenuController.DisplayOperationCount();
                    break;

                case MenuOperation.CalculateValue:
                    MenuController.CalculateValue(ref previousResult);
                    break;
            }

            MenuController.ClearDisplay();
        }
    }
}
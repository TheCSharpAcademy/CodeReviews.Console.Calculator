using CalculatorLibrary;

namespace Calculator;

internal class Program
{
    static void Main(string[] args)
    {
        bool stop = false;

        while (!stop)
        {
            Menu.DisplayWelcomeMessage();
            Utility.ReadUserNumberInput();
            Menu.DisplayOptionMenu();            
            stop = Menu.DisplayAgainOption();
            Menu.DisplayUseCount();
        }
        //BrainCount.SaveBrainCount(Brain.count);
    }
}

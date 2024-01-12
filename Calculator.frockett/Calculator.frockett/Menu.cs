using CalculatorLibrary;

namespace CalculatorProgram.frockett;

internal class Menu
{
    ListFunctions listFunctions = new ListFunctions();

    bool endApp = false;

    public void ShowMenu()
    {
        while (!endApp)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Please select an option: ");
            Console.WriteLine("\t1. New Calculation");
            Console.WriteLine("\t2. View History");
            Console.WriteLine("\t3. Clear History");
            Console.WriteLine("\t4. Exit Application");

            string? readResult = Console.ReadLine();
            int menuSelection = 0;
            while (!int.TryParse(readResult, out menuSelection))
            {
                Console.WriteLine("Input a valid integer menu selection");
                readResult = Console.ReadLine();
            }

            switch (menuSelection)
            {
                case 1:
                    Program.ProcessCalcInput();
                    break;
                case 2:
                    listFunctions.PrintList();
                    break;
                case 3:
                    listFunctions.ClearList();
                    break;
                case 4:
                    endApp = true;
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("Invalid input, please enter a valid integer menu selection");
                    break;
            }
        }

        // Add call to close JSON writer before return
        //calculator.Finish();
        return;
    }

    public void ExitProgram()
    {
        //Calculator.Finish();
        return;
    }
}
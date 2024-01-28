using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main()
    {
        Calculator calculator = new();

        List<double> resultHistoryValue = [];
        List<string> resultHistoryDisplay = [];

        int calculatorUsedCount = 0;
        double cleanNum1, cleanNum2, result;

        string? readResult, numInput1, numInput2, op;
        string currentResultDisplay;
        char operationSymbol;

        bool validInput;
        bool mainMenuRunning = true;
        bool calculationScreenRunning;

        MainMenu();

        void MainMenu()
        {
            do
            {
                DisplayMenu();

                MenuInput();
            } while (mainMenuRunning);
        }

        void CalculatorScreen()
        {
            calculationScreenRunning = true;

            do
            {
                DisplayCalculatorHeader();

                CalculatorInput();

                DisplayOperations();

                CompleteOperation();

                ContinueCalculatingInput();
            } while (calculationScreenRunning);
        }

        void HistoryScreen()
        {
            DisplayHistory();

            HistoryInput();
        }

        //Used in MainMenu
        void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the C# Calculator!");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("\n1. Calculator\n2. History\n3. Exit");
            Console.WriteLine("Type a number and press Enter.");
        }

        void MenuInput()
        {
            readResult = Console.ReadLine();
            if (readResult != null)
            {
                switch (readResult)
                {
                    case "1":
                        CalculatorScreen();
                        break;
                    case "2":
                        HistoryScreen();
                        break;
                    case "3":
                        calculator.Finish();
                        mainMenuRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Try Again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Try Again.");
            }
        }

        //Used in CalculatorScreen
        void DisplayCalculatorHeader()
        {
            Console.Clear();
            Console.WriteLine("Console Calculator in C#");
            Console.WriteLine("------------------------\n");
        }

        void CalculatorInput()
        {
            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
            }

            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }
        }

        void DisplayOperations()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\t1 - Add\n\t2 - Subtract\n\t3 - Multiply\n\t4 - Divide");
            Console.Write("Your option? ");
        }

        void CompleteOperation()
        {
            //This does too much but struggling to separate.
            validInput = false;
            do
            {
                op = Console.ReadLine();

                if (op != null)
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op, calculatorUsedCount);

                    try
                    {
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            SetOperationSymbol();

                            if (validInput)
                            {
                                calculatorUsedCount++;

                                DisplayResult();

                                SaveResult();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            } while (!validInput);
        }

        void SetOperationSymbol()
        {
            validInput = true;
            operationSymbol = ' ';

            switch (op)
            {
                case "1":
                    operationSymbol = '+';
                    break;
                case "2":
                    operationSymbol = '-';
                    break;
                case "3":
                    operationSymbol = 'x';
                    break;
                case "4":
                    operationSymbol = '/';
                    break;
                default:
                    validInput = false;
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }

        void DisplayResult()
        {
            currentResultDisplay = $"{calculatorUsedCount}: {cleanNum1} {operationSymbol} {cleanNum2} = {result:0.##}";

            Console.WriteLine("Your result:");
            Console.WriteLine(currentResultDisplay);
        }

        void SaveResult()
        {
            resultHistoryValue.Add(result);
            resultHistoryDisplay.Add(currentResultDisplay);
        }

        void ContinueCalculatingInput()
        {
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Do another calculation? (y/n)");

            validInput = false;
            while (!validInput)
            {
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    ContinueCalculatingSelection();
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
        }

        void ContinueCalculatingSelection()
        {
            switch (readResult.Trim().ToLower())
            {
                case "y":
                    validInput = true;
                    break;
                case "n":
                    calculationScreenRunning = false;
                    validInput = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }

        //Used in HistoryScreen
        void DisplayHistory()
        {
            Console.Clear();
            Console.WriteLine("History\n---------------");

            foreach (string storedResult in resultHistoryDisplay)
            {
                Console.WriteLine(storedResult);
            }
        }

        void HistoryInput()
        {
            Console.WriteLine("Press Enter to return to menu. OR type \'d\' to delete history.");

            validInput = false;
            do
            {
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    HistorySelection();
                }
            } while (!validInput);
        }

        void HistorySelection()
        {
            switch (readResult.Trim().ToLower())
            {
                case "": //Back to menu
                    validInput = true;
                    break;
                case "d":
                    DeleteHistory();
                    validInput = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Try Again.");
                    break;
            }
        }

        void DeleteHistory()
        {
            resultHistoryDisplay.Clear();
            resultHistoryValue.Clear();
            calculatorUsedCount = 0;

            Console.WriteLine("History deleted. Press Enter to return to menu");
            Console.ReadLine();
        }
    }
}
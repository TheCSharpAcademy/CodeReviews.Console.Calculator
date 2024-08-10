using System.Globalization;

namespace Calculator;

using SimpleMenuLibrary;
using SimpleLoggerLibrary;

public class Calculator
{
    public static Menu MainMenu { get; set; } = new();
    private static Menu HistoryMenu { get; set; } = new();
    private static List<string[]> CalculationHistory { get; set; } = new();
    private static double[] Operands { get; set; } = [0, 0];
    private static double _result;
    private const string Log = @"logfile.json";
    private const string AppName = @"Calculator Application";
    private static readonly LoggerLibrary Logger = new();
    private static string _selection = "";

    public Calculator()
    {
        // Main Calculator Menu
        MainMenu.Title = $"{AppName}".PadLeft(30);
        MainMenu.AddOption(["x + y = z", "a"]);
        MainMenu.AddOption(["x - y = z", "s"]);
        MainMenu.AddOption(["x * y = z", "x"]);
        MainMenu.AddOption(["x ÷ y = z", "d"]);
        MainMenu.AddOption(["sqrt(x) = z", "sqrt"]);
        MainMenu.AddOption(["x^y = z", "pow"]);
        MainMenu.AddOption(["10x = z", "10x"]);
        MainMenu.AddOption(["sin(x) = z", "sin"]);
        MainMenu.AddOption(["cos(x) = z", "cos"]);
        MainMenu.AddOption(["tan(x) = z", "tan"]);
        MainMenu.AddOption(["x = z, z = x", "swapxz"]);
        MainMenu.AddOption(["x = y, y = x", "swapxy"]);
        MainMenu.AddOption(["Set x", "set"]);
        MainMenu.AddOption(["Set x, y", "setxy"]);
        MainMenu.AddOption(["History Menu", "history"]);
        MainMenu.AddOption(["Exit Program", "exit"]);

        // Calculator History Menu
        HistoryMenu.Title = $" ".PadLeft(35);
        HistoryMenu.AddOption(["Clear history", "clear"]);
        HistoryMenu.AddOption(["x, y, z from history", "set"]);
        HistoryMenu.AddOption(["Main Menu", "exit"]);

        // Start Logger
        Logger.ApplicationName = AppName;
        Logger.FileName = Log;
        Logger.OpenLog();
    }

    private double GetOperand()
    {
        string? numInput = Console.ReadLine();

        double cleanNum;
        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput = Console.ReadLine();
        }
        return cleanNum;
    }

    public void GetOperands()
    {
        Console.Clear();
        Calculator.MainMenu.ShowMenu();
        Calculator.ShowMemory();
        Console.Write("Enter X, press Enter: ");
        double numx = GetOperand();
        Operands[0] = numx;

        Console.Clear();
        Calculator.MainMenu.ShowMenu();
        Calculator.ShowMemory();
        Console.Write("Enter Y, press Enter: ");
        double numy = GetOperand();
        Operands[1] = numy;
    }

    public void DoOperation()
    {
        try
        {
            switch (_selection)
            {
                case "a":
                    _result = Operands[0] + Operands[1];
                    AddHistory("a", Operands[0].ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.ToString(CultureInfo.InvariantCulture));
                    break;
                case "s":
                    _result = Operands[0] - Operands[1];
                    AddHistory("s", Operands[0].ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.ToString(CultureInfo.InvariantCulture));
                    break;
                case "x":
                    _result = Operands[0] * Operands[1];
                    AddHistory("x", Operands[0].ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.ToString(CultureInfo.InvariantCulture));
                    break;
                case "d":
                    if (Operands[1] != 0)
                    {
                        _result = Operands[0] / Operands[1];
                        AddHistory("d", Operands[0].ToString(CultureInfo.InvariantCulture), Operands[1].
                            ToString(CultureInfo.InvariantCulture), _result.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        Console.WriteLine("Attempted to divide by Zero! Try again.");
                    }
                    break;
                case "sqrt":
                    _result = Math.Sqrt(Operands[0]);
                    AddHistory("sqrt", Operands[0].
                        ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "pow":
                    _result = Math.Pow(Operands[0], Operands[1]);
                    AddHistory("pow", Operands[0].
                        ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "10x":
                    _result = Operands[0] * 10;
                    AddHistory("10x", Operands[0].
                        ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "sin":
                    _result = Math.Sin(Operands[0]);
                    AddHistory("sin", Operands[0].
                        ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "cos":
                    _result = Math.Cos(Operands[0]);
                    AddHistory("cos", Operands[0].
                        ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "tan":
                    _result = Math.Tan(Operands[0]);
                    AddHistory("tan", Operands[0].
                        ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "swapxz":
                    (Operands[0], _result) = (_result, Operands[0]);
                    break;
                case "swapxy":
                    (Operands[0], Operands[1]) = (Operands[1], Operands[0]);
                    break;
                case "history":
                    ShowHistory();
                    HistoryMenu.ShowMenu(clear: false);
                    var s = HistoryMenu.Prompt();
                    switch (s)
                    {
                        case "clear":
                            ClearHistory();
                            break;
                        case "set":
                            bool keepgoing = false;
                            int response;
                            while (!keepgoing)
                            {
                                Console.Write("Please enter the line you would like to set:");
                                string? r = Console.ReadLine();
                                if (int.TryParse(r, out response))
                                {
                                    keepgoing = true;
                                    SetFromHistory(response, out Operands[0], out Operands[1], out double num);
                                    _result = num;
                                }
                            }
                            break;
                        case "exit":
                            break;
                    }
                    break;
                case "clear":
                    _result = 0;
                    Operands[0] = 0;
                    Operands[1] = 0;
                    break;
                case "setxy":
                    GetOperands();
                    break;
                case "set":
                    Console.Write("Enter X, press Enter: ");
                    double numx = GetOperand();
                    Operands[0] = numx;
                    break;
                case "exit":
                    Logger.CloseLog();
                    Environment.Exit(0);
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            Console.ReadKey();
        }

    }

    public static void GetSelection()
    {
        _selection = MainMenu.Prompt($"Enter Operation (1/{CalculationHistory.Count + 1}): ");
    }

    public static void ShowMemory()
    {

        Console.WriteLine($"Memory: x = {Operands[0]}  |  y = {Operands[1]}  |  z = {_result}".ToUpper());
        Console.WriteLine("".PadRight(40, '-'));

    }

    private static void ShowHistory()
    {
        Console.Clear();
        List<string[]> his = CalculationHistory;
        for (int i = his.Count - 1; i >= 0; i--)
        {
            string n1 = his[i][1];
            string n2 = his[i][2];
            string res = his[i][3];
            Console.Write($"{i}.\t");
            Console.Write($"x={n1}, ");
            Console.Write($"y={n2}, ");
            Console.Write($"z={res}\n");
        }
    }

    private static void ClearHistory()
    {
        CalculationHistory.Clear();
        Console.Write("History cleared! Press any key to continue...");
        Console.ReadKey();
    }

    private static void SetFromHistory(int history, out double x, out double y, out double z)
    {
        x = Convert.ToDouble(CalculationHistory[history][1]);
        y = Convert.ToDouble(CalculationHistory[history][2]);
        z = Convert.ToDouble(CalculationHistory[history][3]);
    }

    private static void AddHistory(params String[] eventData)
    {
        foreach (Option option in MainMenu.Options)
        {
            if (option.Symbol == eventData[0])
            {
                Logger.WriteLogEntry(eventData);
                CalculationHistory.Add(eventData);
            }
        }
    }
}
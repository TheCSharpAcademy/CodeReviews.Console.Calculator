using System.Globalization;

namespace Calculator;

using SimpleMenuLibrary;
using SimpleLoggerLibrary;
using System.Text;

public class Calculator
{
    public static Menu MainMenu = new($"{AppName}");
    private static Menu HistoryMenu = new($"{AppName} - History");
    private static List<string[]> CalculationHistory = new();
    private static double[] Operands = [0, 0];
    private static double _result;
    private const string Log = @"logfile.json";
    private const string AppName = @"Calculator Application";
    private static readonly LoggerLibrary Logger = new();
    private static string? _selection = "";

    public Calculator()
    {
        // Main Calculator Menu
        MainMenu.AddMenuOption(new Menu.Option(description: "x + y = z",
        selector: "a"));
        MainMenu.AddMenuOption(new Menu.Option(description: "x - y = z",
        selector: "s"));
        MainMenu.AddMenuOption(new Menu.Option(description: "x * y = z",
        selector: "x"));
        MainMenu.AddMenuOption(new Menu.Option(description: "x ÷ y = z",
        selector: "d"));
        MainMenu.AddMenuOption(new Menu.Option(description: "sqrt(x) = z",
        selector: "sqrt"));
        MainMenu.AddMenuOption(new Menu.Option(description: "x^y = z",
        selector: "pow"));
        MainMenu.AddMenuOption(new Menu.Option(description: "10x = z",
        selector: "10x"));
        MainMenu.AddMenuOption(new Menu.Option(description: "sin(x) = z",
        selector: "sin"));
        MainMenu.AddMenuOption(new Menu.Option(description: "cos(x) = z",
        selector: "cos"));
        MainMenu.AddMenuOption(new Menu.Option(description: "tan(x) = z",
        selector: "tan"));
        MainMenu.AddMenuOption(new Menu.Option(description: "x = z, z = x",
        selector: "swapxz"));
        MainMenu.AddMenuOption(new Menu.Option(description: "x = y, y = x",
        selector: "swapxy"));
        MainMenu.AddMenuOption(new Menu.Option(description: "Set x",
        selector: "set"));
        MainMenu.AddMenuOption(new Menu.Option(description: "Set x, y",
        selector: "setxy"));
        MainMenu.AddMenuOption(new Menu.Option(description: "History Menu",
        selector: "history"));
        MainMenu.AddMenuOption(new Menu.Option(description: "Exit Program",
        selector: "exit"));

        // Calculator History Menu
        HistoryMenu.AddMenuOption(new Menu.Option(description:
        "Clear history", selector: "clear"));
        HistoryMenu.AddMenuOption(new Menu.Option(description:
        "x, y, z from history", selector: "set"));
        HistoryMenu.AddMenuOption(new Menu.Option(description:
        "Main Menu", selector: "exit"));

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
            Console.Write(
                "This is not valid input. Please enter a numeric value: ");
            numInput = Console.ReadLine();
        }
        return cleanNum;
    }

    public void GetOperands()
    {
        double numx;
        double numy;

        Console.Clear();
        Calculator.MainMenu.ShowMenu(footerContent: new List<string>
        { $"{Calculator.GetMemory()}" });
        while (!double.TryParse(MainMenu.Prompt("Enter X, press Enter: "),
        out numx)) ;
        Operands[0] = numx;

        Console.Clear();
        Calculator.MainMenu.ShowMenu(footerContent: new List<string>
        { $"{Calculator.GetMemory()}" });
        while (!double.TryParse(MainMenu.Prompt("Enter Y, press Enter: "),
        out numy)) ;
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
                    AddHistory("a", Operands[0].
                    ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "s":
                    _result = Operands[0] - Operands[1];
                    AddHistory("s", Operands[0].
                    ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "x":
                    _result = Operands[0] * Operands[1];
                    AddHistory("x", Operands[0].
                    ToString(CultureInfo.InvariantCulture), Operands[1].
                        ToString(CultureInfo.InvariantCulture), _result.
                        ToString(CultureInfo.InvariantCulture));
                    break;
                case "d":
                    if (Operands[1] != 0)
                    {
                        _result = Operands[0] / Operands[1];
                        AddHistory("d", Operands[0].
                        ToString(CultureInfo.InvariantCulture), Operands[1].
                            ToString(CultureInfo.InvariantCulture), _result.
                            ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        Console.WriteLine(
                            "Attempted to divide by Zero! Try again.");
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
                    HistoryMenu.ShowMenu(clear: true,
                    footerContent: GetHistory());
                    var s = HistoryMenu.Prompt();
                    switch (s)
                    {
                        case "clear":
                            ClearHistory();
                            break;
                        case "set":
                            bool keepgoing = false;
                            while (!keepgoing)
                            {
                                var r = HistoryMenu.Prompt(promptText:"Please enter the line you would like to set:");
                                if (!int.TryParse(r, out var response) || (response  > CalculationHistory.Count - 1) ) continue;
                                keepgoing = true;
                                SetFromHistory(response, out Operands[0],
                                out Operands[1], out double num);
                                _result = num;
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
                    double numx;
                    while(!double.TryParse(MainMenu.Prompt(promptText:"Enter X, press Enter: "), out numx));
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
            Console.WriteLine
            ("Oh no! An exception occurred trying to do the math.\n" +
            " - Details: " + e.Message);
            Console.ReadKey();
        }

    }

    public static void GetSelection()
    {
        _selection = MainMenu.
        Prompt($"Enter Operation (1/{CalculationHistory.Count + 1}): ");
    }

    public static string GetMemory()
    {

        return
        $"Memory: x = {Operands[0]}  |  y = {Operands[1]}  |  z = {_result}".
        ToUpper();

    }

    private static List<string> GetHistory()
    {
        List<string[]> his = CalculationHistory;
        var history = new List<string>();
        for (int i = his.Count - 1; i >= 0; i--)
        {
            var line = new StringBuilder();
            string n1 = his[i][1];
            string n2 = his[i][2];
            string res = his[i][3];
            line.AppendFormat($"{i}.\t");
            line.AppendFormat($"x={n1}, ");
            line.AppendFormat($"y={n2}, ");
            line.AppendFormat($"z={res}\n");
            history.Add(line.ToString());
        }
        return history;
    }

    private static void ClearHistory()
    {
        CalculationHistory.Clear();
        Console.Write("History cleared! Press any key to continue...");
        Console.ReadKey();
    }

    private static void SetFromHistory(int history, out double x, out double y,
     out double z)
    {
        x = Convert.ToDouble(CalculationHistory[history][1]);
        y = Convert.ToDouble(CalculationHistory[history][2]);
        z = Convert.ToDouble(CalculationHistory[history][3]);
    }

    private static void AddHistory(params string[] eventData)
    {
        foreach (var option in MainMenu.GetMenuOptions().Where(option =>
        option.Selector == eventData[0]))
        {
            Logger.WriteLogEntry(eventData);
            CalculationHistory.Add(eventData);
        }
    }
}
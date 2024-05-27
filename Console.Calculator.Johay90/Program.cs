using CalculatorLibrary;
using System.Text.RegularExpressions;

var userInteractions = new UserInteractions();
var calculator = new Calculator();
var calculationsList = new List<Calculations>();
int count = 0;

userInteractions.Intro();

while (true)
{
    bool usePreviousCalc = false;
    string? option = "";
    double cleanNum1 = 0, cleanNum2 = 0;

    // only show previous calculation menu if we have calculations already
    if (calculationsList.Count > 0) option = userInteractions.CalculationsOptions();

    switch (option?.ToLower())
    {
        case "p" when calculationsList.Count > 0:
            {
                usePreviousCalc = true;
                userInteractions.DisplayCalculations(calculationsList);
                int selectedIndex = userInteractions.SelectCalculation(calculationsList);
                cleanNum1 = calculationsList[selectedIndex].num1;
                cleanNum2 = calculationsList[selectedIndex].num2;
                break;
            }
        case "d" when calculationsList.Count > 0:
            userInteractions.ClearCalculations(calculationsList);
            break;
    }

    if (!usePreviousCalc)
    {
        Console.Write("Type a number, and then press Enter: ");
        cleanNum1 = userInteractions.GetNumInput(Console.ReadLine());

        Console.Write("Type another number, and then press Enter: ");
        cleanNum2 = userInteractions.GetNumInput(Console.ReadLine());
    }

    string? op = userInteractions.ChooseOperation();

    if (op == null || !Regex.IsMatch(op, "[a|s|m|d]")) userInteractions.InvalidInput();

    else
    {
        try
        {
            double result = calculator.DoOperation(cleanNum1, cleanNum2, op);

            if (double.IsNaN(result)) userInteractions.InvalidInput();

            else
            {
                count++;
                calculationsList.Add(new Calculations(cleanNum1, cleanNum2, op, result));
                Console.WriteLine("Your result: {0:0.##}\n", result);
                userInteractions.TimesRan(count);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
    }
    userInteractions.EndOptions();
    Console.Clear();
}

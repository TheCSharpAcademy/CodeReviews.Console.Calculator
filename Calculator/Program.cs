using CalculatorLibrary;

//Program Variables
bool endApp = false;
int counter = 0;
CalcLib calculator = new();
await calculator.OpenFile();

while (!endApp)
{    
    int menuSelection = 0;
    Console.Clear();
    Console.WriteLine("""
        Console Calculator in C#
        ------------------------
        1 - New Calculation
        2 - Calculate From History
        3 - Clear History
        4 - Quit
        -------------------------
        """);
    ShowStats();
    Console.Write("Your Selection: ");
    menuSelection = (int)ValidateNumber();

    switch (menuSelection)
    {
        case 1:
            CalculationProcess();
            counter++;
            await calculator.SaveFile();
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadLine();
            break;

        case 2:
            if (calculator.PreviousCalculations.Any())
            {
                Console.Clear();
                int lineSelection;
                double result;

                ShowStats();
                Console.Write("Select line number to use result: ");
                lineSelection = (int)ValidateNumber();
                result = calculator.PreviousCalculations.ElementAt(lineSelection - 1).Result;
                CalculationProcess(result);

                counter++;
                await calculator.SaveFile();
                Console.WriteLine("Press Any Key to Continue");
                Console.ReadLine();
            }
            break;

        case 3:
            calculator.ClearHistory();            
            continue;            

        case 4:
            endApp = true;
            Console.Clear();
            break;

        default:
            break;
    }
}
return;

void CalculationProcess(double firstNumber = 0)
{
    double operand1;
    double operand2;
    string? op;
    double result = 0;

    Console.Clear();
    Console.WriteLine($"""
	Console Calculator in C#
	------------------------
	""");
    //Operator selection
    Console.WriteLine("""
        Choose Operation to perform:
        A - Add
        S - Subtract
        M - Multiply
        D - Divide
        SQ - Square Root
        P - Power
        SIN - Sine of an angle
        COS - Cosine of an angle
        TAN - Tangent of an angle
        """);
    Console.Write("Menu Selection: ");
    op = ValidateCalcMenu();
    Console.Clear();

    switch (op.Trim().ToLower())
    {
        case "sq":
            Console.Write("Enter Number for Square Root: ");
            operand1 = ValidateNumber();
            operand2 = 0;
            break;

        case "sin":
            Console.Write("Enter angle degree to return the Sine: ");
            operand1 = ValidateNumber();
            operand2 = 0;
            break;

        case "cos":
            Console.Write("Enter angle degree to return the Cosine: ");
            operand1 = ValidateNumber();
            operand2 = 0;
            break;

        case "tan":
            Console.Write("Enter angle degree to return the Tangent: ");
            operand1 = ValidateNumber();
            operand2 = 0;
            break;

        default:
            if (firstNumber == 0)
            {
                //First number input
                Console.Write("Enter the first number: ");
                operand1 = ValidateNumber();
            }
            else
            {
                operand1 = firstNumber;
                Console.WriteLine($"First number: {operand1}");
            }
            //Second number input
            Console.Write("Enter the second number: ");
            operand2 = ValidateNumber();
            break;
    }

    try
    {
        result = calculator.DoOperation(operand1, operand2, op);

        if (double.IsNaN(result))
            Console.WriteLine("This operation will result in a mathematical error.");
        else
        {
            CalcLib.Calculation currentCalc = calculator.PreviousCalculations.Last();
            Console.Clear();
            DisplayCalculation(currentCalc);
            Console.WriteLine($"{currentCalc.Result:0.###}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception occured whilst calculating: {ex.Message}");
    }
}

void ShowStats()
{
    Console.WriteLine($"""
    Calculations this session: { counter}
    Calculations Total: { calculator.PreviousCalculations.Count()}
    """);

    if (calculator.PreviousCalculations.Any())
    {
        Console.WriteLine("""
		    ----------------------
		    Previous Calculations:
		    ----------------------
		    """);

        int count = 0;
        var prevList = calculator.PreviousCalculations.GetEnumerator();
        while (prevList.MoveNext())
        {
            count++;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{count}: ");
            Console.ResetColor();
            DisplayCalculation(prevList.Current);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{prevList.Current.Result:0.###}");
            Console.ResetColor();
        }
    }
Console.WriteLine("----------------------");
}

void DisplayCalculation(CalcLib.Calculation calc)
{
    switch (calc.Operation)
    {
        case "Square Root":        
            Console.Write($"{calc.Operation} of {calc.Operand1:0.##} = ");
            break;
        case "Power":
            Console.Write($"{calc.Operand1} to the Power of {calc.Operand2} = ");
            break;
        case "Sine":
        case "Cosine":
        case "Tangent":
            Console.Write($"{calc.Operation} of {calc.Operand1:0.##} degrees = ");
            break;
        default:
            Console.Write($"{calc.Operand1:0.##} {calc.Operation} {calc.Operand2:0.##} = ");
            break;
    }    
}


double ValidateNumber()
{
    string? input = Console.ReadLine();
	double validNumber;
    while (!double.TryParse(input, out validNumber))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Value must be a number: ");
        Console.ResetColor();
        input = Console.ReadLine();
    }
    return validNumber;
}

string ValidateCalcMenu()
{
    string[] menuOptions = { "a", "s", "m", "d", "sq", "p", "sin", "cos", "tan" };
    string? input = Console.ReadLine();

    while (string.IsNullOrEmpty(input) || !menuOptions.Contains(input.Trim().ToLower()))
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("Please select a valid menu option: ");
		Console.ResetColor();
		input = Console.ReadLine();
	}
	return input;
}
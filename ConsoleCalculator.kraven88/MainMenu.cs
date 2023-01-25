using CalculatorLibrary;
using CalculatorLibrary.Models;

namespace ConsoleCalculator.kraven88;

internal class MainMenu
{
    string filePath;
    List<Equation> equations;
    Calculator calc;
    double memory = double.NaN;

    public MainMenu(string FilePath, List<Equation> Equations, Calculator Calc)
    {
        filePath = FilePath;
        equations = Equations;
        calc = Calc;
    }

    internal void SelectionScreen()
    {
        GreetingMessage();
        var isRunning = true;

        while (isRunning)
        {
            DisplayMenuItems();
            isRunning = TakeUserInput("12q") switch
            {
                '1' => NewEquasion(calc),
                '2' => PreviousEquations(),
                'q' => false,
            };
        }
    }
    
    // This method remains largely unchanged from the original MS tutorial. On purpose. No, I don't like it.
    private bool NewEquasion(Calculator calculator)     
    {
        var nextEquasion = true;

        while (nextEquasion)
        {
            Console.Clear();
            // Declare variables and initialize with zero;
            var numText1 = "";
            var numText2 = "";
            var result = 0.0;

            // Ask the user to type the first number;
            Console.Write("Type a number, then press Enter. ");
            if (double.IsNaN(memory) == false) 
                Console.Write("Type MR to recall from memory. ");

            numText1 = Console.ReadLine()!.Trim();
            if (numText1.ToUpper() == "MR")
                numText1 = memory.ToString();

            var num1 = 0.0;
            while (double.TryParse(numText1, out num1) == false)
            {
                Console.Write("Invalid number. Please enter a numeric value: ");
                numText1 = Console.ReadLine()!.Trim();
            }

            // Ask the user to type the second number;
            Console.Write("Type another number, then press Enter. ");
            if (double.IsNaN(memory) == false)
                Console.Write("Type MR to recall from memory. ");

            numText2 = Console.ReadLine()!.Trim();
            if (numText2.ToUpper() == "MR")
                numText2 = memory.ToString();

            var num2 = 0.0;
            while (double.TryParse(numText2, out num2) == false)
            {
                Console.Write("Invalid number. Please enter a numeric value: ");
                numText2 = Console.ReadLine()!.Trim();
            }

            // Ask the user to choose an option;
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");

            Console.WriteLine();
            Console.WriteLine("\tp - A to the power of B");
            Console.WriteLine("\tr - Ath root of B");

            Console.WriteLine();
            Console.WriteLine("\tsin - Sine of A");
            Console.WriteLine("\tcos - Cosine of A and B");
            Console.WriteLine("\ttg - Tangent of A and B");
            Console.WriteLine("\tctg - Cotangent of A and B");
            Console.Write("\nYour choice: ");

            var operation = Console.ReadLine()!.Trim();

            try
            {
                result = calculator.DoOperation(num1, num2, operation);

                if (double.IsNaN(result))
                    Console.WriteLine("This operation will result in mathematical error.\n");
                else
                {
                    calculator.calulatorUseCount++;
                    Console.WriteLine($"Your result: {result:0.##}. You used the Calculator {CountFormatter(calculator.calulatorUseCount)}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oh no! An exception occured while doing the math. \nDetails: {ex.Message}");
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing the app;
            Console.Write("Do you want another equation? (Y/N): ");
            if (Console.ReadLine().ToLower() == "n")
                nextEquasion = false;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        
        return true;
    }

    private bool PreviousEquations()
    {
        Console.Clear();
        Console.WriteLine("List of previous equations");
        Console.WriteLine("--------------------------");

        for (int i = 0; i < equations.Count; i++)
        {
            Console.WriteLine($"  {i + 1}.\t{equations[i]}");
        }

        Console.WriteLine("\nType to select the option:");
        Console.WriteLine("  delete     - Deletes all previous equations");
        Console.WriteLine("  M+[number] - Add the result of a specific equation to memory, ex: M+1");

        Console.WriteLine("\n  Any other key to go back");
        Console.WriteLine("------------------------------");

        var selected = Console.ReadLine()!.ToUpper();
        if (selected == "DELETE")
        {
            DataAccess.DeleteEquations(equations, filePath);
        }
        else if (selected.StartsWith("M+") && int.TryParse(selected.Substring(2), out int i))
        {
            memory = equations[i - 1].Result;
        }

        return true;
    }

    private char TakeUserInput(string availableOptions)
    {
        var input = Console.ReadKey(true).KeyChar;
        if (availableOptions.Contains(input) == false)
        {
            Console.WriteLine("Invalid input. Please choose from the options listed above");
            input = TakeUserInput(availableOptions);
        }
        return input;
    }

    private void DisplayMenuItems()
    {
        Console.Clear();
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("\t1 - New equation");
        Console.WriteLine("\t2 - Previous equations");
        Console.WriteLine("\tQ - Quit");
        Console.WriteLine();
    }

    internal void GreetingMessage()
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Wecome to Console Calculator App by kraven88");
        Console.WriteLine("--------------------------------------------");
        Console.ReadLine();
    }

    private string CountFormatter(int count)
    {
        var output = $"{count} time";
        if (count > 1) output += "s";
        return output;
    }
}

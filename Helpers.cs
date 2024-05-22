
using CalculatorProgram;


internal class Helpers
{
    internal static List<Engine> engine = new();

    internal static void PrintCalculations()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Calculator History:");

        foreach (var cal in engine)
        {
            if(cal.Type == CalculationType.SquareRoot || cal.Type == CalculationType.Tangent || cal.Type == CalculationType.Sine ||cal.Type == CalculationType.Cosine){
                Console.WriteLine($"{cal.Date} - {cal.Num1} {cal.Type} = {cal.TotalResult}"); 
            }
            else{
                Console.WriteLine($"{cal.Date} - {cal.Num1} {cal.Type} {cal.Num2} = {cal.TotalResult}");
            }
        }
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Program.Menu(); 
    }

    internal static void AddHistory(CalculationType calculationType, double num1, double num2, double totalResult)
    {

        engine.Add(new Engine
        {
            Date = DateTime.Now,
            Num1 = num1, 
            Num2 = num2, 
            TotalResult = totalResult,
            Type = calculationType,
        });

    }

    internal static void DeleteHistory()
    {
        engine.Clear();
    }

    internal static int CountCalculator(int count){
        Console.WriteLine($"You used the calculator: {count} times");
        Console.ReadLine(); 
        Program.Menu(); 
        return count; 
    }

    internal static double Recalculate(double result)
    {
        double num1; 
        result = 0;

        var lastCalculation = engine[^1];

        num1 = lastCalculation.TotalResult; 

        Console.WriteLine($"Your last result was: {num1}");
        Console.ReadLine(); 

        var(operation, count) = Logic.SetOp(); 

        double cleanNum2 = 0; 

        if(!(operation.Equals("r") || operation.Equals("sin") || operation.Equals("cos") || operation.Equals("tan"))){
            cleanNum2 = Logic.SetSecondNum(); 
        }
        
        Logic.TotalOperation(num1, cleanNum2, operation); 

        Console.WriteLine("------------------------\n");

        Console.Write("press 'c' to continue operating, or press any other key and Enter to go to the menu: ");

        Console.WriteLine("\n\n------------------------\n");

        string? readResult = Console.ReadLine();

        switch (readResult)
        {
            case "c":

                Console.WriteLine("You want to continue operating with the last result? Press 'Y' for continue operating with the last result, type any other key to make new operations"); 

               if (Console.ReadLine() == "y")
                {
                    Helpers.Recalculate(result);
                }
                else{
                    Program.Menu(); 
                }

                break;
        }

        Console.WriteLine("\n");
        Console.Clear();

        return result; 
    }
    
}

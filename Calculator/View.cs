namespace Calculator;

using CalculatorLibrary;

public class View
{
    public void ShowHeader()
    {
        Console.WriteLine("C# Console Calculator");
        Console.WriteLine("---------------------");
    }
    
    public double GetNumberFromUser()
    {
        var success = false;
        var number = 0.0;
        do
        {
            Console.Write("Type a number, then press Enter: ");
            success = Double.TryParse(Console.ReadLine(), out number);
        } while (!success);
        return number;
    }
    
    public Mode GetFunctionFromUser()
    {
        Console.WriteLine("Choose an option from the following list.");
        Console.WriteLine("A - Add");
        Console.WriteLine("S - Subtract");
        Console.WriteLine("M - Multiply");
        Console.WriteLine("D - Divide");
        Console.Write("Your option: ");
        while (true)
        {
            switch (Console.ReadLine())
            { 
                case "A" or "a":
                    return Mode.Add;
                case "S" or "s":
                    return Mode.Subtract;
                case "M" or "m":
                    return Mode.Multiply;
                case "D" or "d":
                    return Mode.Divide;
                default:
                    Console.WriteLine("Your input is invalid.");
                    break;
            }
        }
    }
    
    public void ShowResult(Mode function, double operand1, double operand2, double result)
    {
        switch (function)
        {
            case Mode.Add:
                Console.WriteLine($"The result is: {operand1} + {operand2} = {result}");
                break;
            case Mode.Subtract:
                Console.WriteLine($"The result is: {operand1} - {operand2} = {result}");
                break;
            case Mode.Multiply:
                Console.WriteLine($"The result is: {operand1} * {operand2} = {result}");
                break;
            case Mode.Divide:
                Console.WriteLine($"The result is: {operand1} / {operand2} = {result}");
                break;
        }
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public bool PromptUserToQuit()
    {
        Console.WriteLine("Press \"n\" and Enter to quit, any other symbol to continue.");
        var option = Console.ReadLine();
        if (option == null) return false;
        if (option.ToUpper() == "N") return true;
        return false;
    }
}
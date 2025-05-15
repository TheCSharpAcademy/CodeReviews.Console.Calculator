using System.Text.RegularExpressions;
namespace Calculator_kilozdazolik;

public class Helpers
{
    private static readonly List<Calculation> Calculations = new();
    
    public double ValidateNumInput(string input)
    {
        double cleanNum;
        while (!double.TryParse(input, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            input = Console.ReadLine();
        }
        return cleanNum;
    }

    public void AddToHistory(int index, double operand1, double operand2, string operation, double result)
    {
        Calculations.Add(new Calculation
        {
            Index = index,
            Operand1 = operand1,
            Operand2 = operand2,
            Operation = operation,
            Result = result
        });
    }
    
    public void PrintHistory()
    {
        Console.Clear();
        Console.WriteLine("Calculation History");
        Console.WriteLine("-------------------------");
        Console.WriteLine("If you want to change the list type:");
        Console.WriteLine("es - Edit Selected");
        Console.WriteLine("ds - Delete Selected");
        Console.WriteLine("d - Delete All");
        Console.WriteLine("r - Return");
        Console.WriteLine("-------------------------");
        foreach (var calc in Calculations)
        {
            string operationSymbol = calc.Operation switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "×",
                "d" => "/",
                "sq" => "√",
                "si" => "sin",
                "c" => "cos",
                "t" => "tan",
                "p" => "10^",
                _ => "?"
            };

            Console.WriteLine(calc.Operation is "sq" or "si" or "c" or "t" or "p"
                ? $"{calc.Index}: {operationSymbol}({calc.Operand1}) = {calc.Result}"
                : $"{calc.Index}: {calc.Operand1} {operationSymbol} {calc.Operand2} = {calc.Result}");
        }
        Console.Write("Your option ");
        string input = Console.ReadLine();
        while ((string.IsNullOrEmpty(input)))
        {
            Console.WriteLine("Please enter a valid option");
            input = Console.ReadLine();
        }

        switch (input)
        {
            case "es":
                EditSelectedElement();
                break;
            case "ds":
                DeleteSelectedElement();
                break;
            case "d":
                DeleteList();
                break;
        }
        Console.WriteLine("-------------------------");
    }

    private void EditSelectedElement()
    {
        Console.WriteLine("Please write the Index number you want to edit");
        string input = Console.ReadLine();

        if (int.TryParse(input, out var inputIndex))
        {
            bool found = false;

            foreach (var el in Calculations)
            {
                double operandToEdit, newOperand;
                if (el.Index == inputIndex)
                {
                    Console.WriteLine($"Element with Index {inputIndex} selected.");

                    if ((Regex.IsMatch(el.Operation, "^(sq|si|c|t|p)$")))
                    {
                        Console.WriteLine("Type the new number");
                        input = Console.ReadLine();
                        newOperand = ValidateNumInput(input);
                        el.Operand1 = newOperand;
                    }
                    else
                    {
                        Console.WriteLine("Which operand you want to edit? (1 or 2)");
                        operandToEdit = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Type the new number");
                        input = Console.ReadLine();
                        newOperand = ValidateNumInput(input);
                        
                        if (operandToEdit == 1)
                        {
                            el.Operand1 = newOperand;
                        }
                        else
                        {
                            el.Operand2 = newOperand;
                        }
                    }

                    el.Result = Calculator.DoOperation(el.Operand1, el.Operand2, el.Operation);
                    Console.WriteLine($"Your result: {el.Result:0.##}");
                    
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Invalid index: no such calculation found.");
            }
        }
    }

    private void DeleteSelectedElement()
    {
        Console.WriteLine("Please write the Index number you want to delete");

        string input = Console.ReadLine();
        int inputIndex;

        if (int.TryParse(input, out inputIndex))
        {
            bool found = false;

            for (int i = 0; i < Calculations.Count; i++)
            {
                if (Calculations[i].Index == inputIndex)
                {
                    Calculations.RemoveAt(i);
                    Console.WriteLine($"Element with Index {inputIndex} deleted.");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Invalid index: no such calculation found.");
            }
        }
    }


    private void DeleteList()
    {
        Calculations.Clear();
    }

}
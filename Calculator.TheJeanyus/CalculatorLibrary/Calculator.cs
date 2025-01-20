using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    readonly JsonWriter writer;
    public int NumberOfCalcs { get; set; }
    int PrintCount { get; set; } = 5;
    static List<string> Calcs { get; } = [];
    static List<double> Results { get; } = [];
    
    public Calculator(int printCnt = 5)                                                                         //Constructor for new calculator, intializes the JSON writer
    {
        PrintCount = printCnt;
        StreamWriter logFile = File.CreateText("calculator.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public static CalculatorOperations.OperationData GetOperation(string? input)                                //Get an operation from the user and return relevant data
    {
        CalculatorOperations.OperationData value;
        while (input == null || !CalculatorOperations.OpList.TryGetValue(input, out value))
        {
            Console.WriteLine("Error: Unrecognized input. Please try again.");
            input = Console.ReadLine();
        }
        return value;        
    }

    public static double GetOperand(string? input, CalculatorOperations.Operation op, int whichOperand = 0)     //Get and operand for the desired operation
    {
        double result = 0;
        bool validInput = false;
        double multiplier = 1;

        while (!validInput)
        {
            //Process permitted string augments first
            if (input != null && input.Contains("ans")) input = GetHistory(input);
            else if (input != null && input.Length >= 3 && input[^2..] == "pi") { multiplier = Math.PI; input = input[0..^2]; }
            //Process input that should be numeric, first that it parses then for specific errors
            if (!double.TryParse(input, out result)) input = RepeatInput("This is not valid input. Please enter a numeric value: ");
            else if (op == CalculatorOperations.Operation.Division && result == 0 && whichOperand == 1) 
                input = RepeatInput("Cannot divide by zero. Enter a non-zero number: ");
            else if (op == CalculatorOperations.Operation.SquareRoot && result < 0) 
                input = RepeatInput("Cannot take the root of a negative number. Please enter a positive number: ");
            else validInput = true;
        }

        return result * multiplier;
    }

    public double DoOperation(double[] nums, CalculatorOperations.OperationData opData)                         //Perform an operation with provided operands
    {
        double result = double.NaN;
        
        writer.WriteStartObject();
        for (int i = 0; i < nums.Length; i++)
        {
            writer.WritePropertyName($"Operand{i}");
            writer.WriteValue(nums[i]);
        }
        writer.WritePropertyName("Operation");
        writer.WriteValue($"{opData.Op}");

        result = opData.Calc(nums);
        
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        if (!double.IsNaN(result))
        {
            NumberOfCalcs++;
            Results.Add(result);
            Calcs.Add(opData.Record(nums));
        }

        return result;
    }
    
    public static string GetHistory(string str)                                                                 //Get a result from history to use as an operand
    {
        str = str[0..2];
        if (int.TryParse(str, out int index) && index >= 0 && Results.Count >= index) return Results.ElementAt(index-1).ToString();
        else return "";
    }

    public void PrintHistory()                                                                                  //Print a history of past calculations
    {
        Console.WriteLine($"History ({(PrintCount < NumberOfCalcs ? PrintCount : NumberOfCalcs)} of {NumberOfCalcs})".PadRight(15));      //Move cursor to write on the right side of the screen (or below current calcs?)
        Console.WriteLine("------------------------");
        for (int i = Results.Count < PrintCount ? 0 : Results.Count - PrintCount; i < Results.Count; i++)
        {
            Console.Write($"{i + 1}: ");        //Label the item
            Console.Write(Calcs[i]);            //Represent the operation and operands as text
            Console.WriteLine("{0:0.##}", Results[i]);      //Show the result
        }
        Console.WriteLine();
    }

    public static string? RepeatInput(string str)                                                               //Hekper to ask for new input if there is an error
    {
        Console.Write(str);
        return Console.ReadLine();
    }

    public void Finish()                                                                                        //End JSON Writing
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }      
}
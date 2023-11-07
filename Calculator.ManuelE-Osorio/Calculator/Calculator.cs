using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int calculatorUsedTimes = 0;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        List<double> calculationList = new(); 
        Calculator calculator = new();

        while (!endApp)
        {
            string numInput1 = "";
            string numInput2 = "";
            string numListInput = "";
            double result = 0;

            Console.WriteLine($"The calculator has been used {calculatorUsedTimes} times");
            Console.Write("Type a number, and then press Enter or use (p) for a previous calculation (d) to delete the history: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            
            while (!(double.TryParse(numInput1, out cleanNum1) || (calculationList.Count>0 && numInput1=="p")))
            {
                if (numInput1=="d")
                {
                    calculationList.Clear();
                    Console.WriteLine("Previous results succesfully deleted.");
                    Console.Write("Type a number, and then press Enter: ");
                }
                else if (numInput1 == "p" && calculationList.Count<1)
                {
                    Console.Write("There are no previous calculations. Please enter a value:");
                }
                else
                {
                    Console.Write("This is not a valid input. Please enter a value: ");
                }
                numInput1 = Console.ReadLine();
            }

            if (numInput1 == "p")
            {
                for (int i=0; i<calculationList.Count; i++)
                {
                    Console.WriteLine($"{i}) {calculationList[i]}");
                }
                
                Console.Write("Select the previous result number: ");
                numListInput = Console.ReadLine();
                int cleanListInput=0;
                while (!int.TryParse(numListInput, out cleanListInput))
                { 
                    Console.Write("This is not valid input. Please enter a valid value: ");
                    numListInput = Console.ReadLine();
                }
                cleanNum1 = calculationList[cleanListInput];
            }

            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a value: ");
                numInput2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsqrt - a*Sqrt(b)");
            Console.WriteLine("\tpow - Power");
            Console.WriteLine("\t10x - Power to the 10x");
            Console.WriteLine("\tcos - a Cos(b)");
            Console.WriteLine("\tsin - a Sin(b)");
            Console.Write("Your option? ");

            string op = Console.ReadLine();

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op); 
                calculationList.Add(result);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            calculatorUsedTimes++;
            Console.WriteLine("\n"); 
        }
        calculator.Finish();
        return;
    }
}

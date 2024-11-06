using System.Text.RegularExpressions;
// Program.cs
using CalculatorLibrary;
// Program.cs



class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        // Program.cs
        Calculator calculator = new Calculator();

        List<string> pastCalc = new List<string>();
        int timesUsed = 0;

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;
            double cleanNum1 = 0; 
            double cleanNum2 = 0;



            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power of");
            Console.WriteLine("\tr - Square root");
            Console.WriteLine("\tt - 10x");
            Console.WriteLine("\tc - cosine");
            Console.WriteLine("\tsn - sine");
            Console.WriteLine("\tta - tangent");
            Console.WriteLine("\tct - cotanget");
            Console.WriteLine("\tse - secant");
            Console.WriteLine("\tcose - cosecant");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();



                // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|r|t|c|sn|ta|ct|se|cose]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    // Program.cs
                    // Program.cs
                    if ((op == "a") || (op == "s") || (op == "m") || (op == "d"))
                    {
                        Console.WriteLine("Do you want to use a previous result for your calculation? y or n");
                        string sult = Console.ReadLine().ToLower();
                        if (sult == "y")
                        {
                            if (pastCalc.Count == 0)
                            {
                                Console.WriteLine("you have no items in your history");
                                // Ask the user to type the first number.
                                Console.Write("Type a number, and then press Enter: ");
                                numInput1 = Console.ReadLine();


                                while (!double.TryParse(numInput1, out cleanNum1))
                                {
                                    Console.Write("This is not valid input. Please enter a numeric value: ");
                                    numInput1 = Console.ReadLine();
                                }

                            }
                            else
                            {
                                foreach (string item in pastCalc)
                                {
                                    int index = pastCalc.IndexOf(item);
                                    Console.WriteLine($"{index}: {item} ");
                                }
                                Console.WriteLine("Which result do you want to use?");
                                string ? dex = Console.ReadLine();
                                int indexCalc = int.Parse(dex);
                                string calcToUse = pastCalc[indexCalc];
                                int indexOfEqual = calcToUse.IndexOf("=");
                                int lenghtCalc = calcToUse.Length;
                                string resultClean = calcToUse.Substring(indexOfEqual+1).Trim();
                                double resClean = double.Parse(resultClean);
                                cleanNum1 = resClean;


                            }
                        }
                        else if (sult == "n")
                        {
                        // Ask the user to type the first number.
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }

                        }
                        else { 
                            Console.WriteLine("Not a valid choice");
                            // Ask the user to type the first number.
                            Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();


                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }

                    }

                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }

                    }
                    else if (op == "p")
                    {
                        Console.WriteLine("Do you want to use a previous result for your calculation? y or n");
                        string sult = Console.ReadLine().ToLower();
                        if (sult == "y")
                        {
                            if (pastCalc.Count == 0)
                            {
                                Console.WriteLine("you have no items in your history");
                                // Ask the user to type the first number.
                                Console.Write("Type a number, and then press Enter: ");
                                numInput1 = Console.ReadLine();


                                while (!double.TryParse(numInput1, out cleanNum1))
                                {
                                    Console.Write("This is not valid input. Please enter a numeric value: ");
                                    numInput1 = Console.ReadLine();
                                }

                            }
                            else
                            {
                                foreach (string item in pastCalc)
                                {
                                    int index = pastCalc.IndexOf(item);
                                    Console.WriteLine($"{index}: {item} ");
                                }
                                Console.WriteLine("Which result do you want to use?");
                                string? dex = Console.ReadLine();
                                int indexCalc = int.Parse(dex);
                                string calcToUse = pastCalc[indexCalc];
                                int indexOfEqual = calcToUse.IndexOf("=");
                                int lenghtCalc = calcToUse.Length;
                                string resultClean = calcToUse.Substring(indexOfEqual+1).Trim();
                                double resClean = double.Parse(resultClean);
                                cleanNum1 = resClean;


                            }
                        }
                        else if (sult == "n")
                        {
// Ask the user to type the first number.
                            Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }
                        }
                        else
                        {
                            Console.WriteLine("Not a valid choice");
                            // Ask the user to type the first number.
                            Console.Write("Type a number, and then press Enter: ");
                            numInput1 = Console.ReadLine();


                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numInput1 = Console.ReadLine();
                            }

                        }



                        // Ask the user to type the second number.
                        Console.Write("Type your exponent number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }
                    }
                    else if ((op == "r") || (op == "t") || (op == "c") || (op == "sn") || (op == "ta") || (op == "ct") || (op == "se") || (op == "cose"))
                    {
                        Console.WriteLine("Do you want to use a previous result for your calculation? y or n");
                        string sult = Console.ReadLine().ToLower();
                        if (sult == "y")
                        {
                            if (pastCalc.Count == 0)
                            {
                                Console.WriteLine("you have no items in your history");
                                // Ask the user to type the first number.
                                Console.Write("Type a number, and then press Enter: ");
                                numInput1 = Console.ReadLine();


                                while (!double.TryParse(numInput1, out cleanNum1))
                                {
                                    Console.Write("This is not valid input. Please enter a numeric value: ");
                                    numInput1 = Console.ReadLine();
                                }

                            }
                            else
                            {
                                foreach (string item in pastCalc)
                                {
                                    int index = pastCalc.IndexOf(item);
                                    Console.WriteLine($"{index}: {item} ");
                                }
                                Console.WriteLine("Which result do you want to use?");
                                string? dex = Console.ReadLine();
                                int indexCalc = int.Parse(dex);
                                string calcToUse = pastCalc[indexCalc];
                                int indexOfEqual = calcToUse.IndexOf("=");
                                int lenghtCalc = calcToUse.Length;
                                string resultClean = calcToUse.Substring(indexOfEqual+1).Trim();
                                double resClean = double.Parse(resultClean);
                                cleanNum1 = resClean;

                            }
                        }
                        else if (sult == "n")
                        {
// Ask the user to type the first number.
                            Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }
                        }
                        else
                        {
                            Console.WriteLine("input not valid");
                            // Ask the user to type the first number.
                            Console.Write("Type a number, and then press Enter: ");
                            numInput1 = Console.ReadLine();


                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numInput1 = Console.ReadLine();
                            }

                        }


                    }
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            pastCalc.Add($" {cleanNum1} {op} {cleanNum2} = {result}");
            timesUsed++;
            Console.Write("Press 'n' and Enter to close the app, press 'h' to view your history, press 't' to view how many times you used the calculator or press any other key and Enter to continue: ");
            string ? newR = Console.ReadLine().ToLower();
            if (newR == "n") endApp = true;
            else if ((newR == "h"))
            {
                foreach (var item in pastCalc)
                {
                    int index = pastCalc.IndexOf(item);
                    Console.WriteLine($"{index}: {item} ");
                }
                Console.WriteLine("Do you want to erase all your history? y or n");
                if (Console.ReadLine() == "y")
                {
                    pastCalc.Clear();
                    continue;
                }
                else continue;

            }
            else if  (newR == "t") {Console.WriteLine($"you used the app  {timesUsed} times"); continue; }
            else
            {
                
                continue;
            }
                
            Console.WriteLine("\n"); // Friendly linespacing.
            // Program.cs
            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }

    }
}

using Calculator.Models;

namespace CalculatorHelpers
{
    public class Helpers
    {
        static List<Calculation> calculations = new List<Calculation>();
        internal static void SaveCalculationsToList(int order, double num1, double num2, double result, string op, List<Calculation> cals)
        {
            cals.Add(new Calculation
            {
                orderNo = order,
                firstNo = num1,
                secondNo = num2,
                result = Math.Round(result,4),
                operation = op
            });
        }

        internal static void ShowPreviousCalculations(List<Calculation> cals)
        {
            Console.Clear();
            Console.WriteLine("Previous Calculations");
            Console.WriteLine("-----------------------");
            foreach (var calculation in cals)
            {
                string OperationSymbol = calculation.operation switch
                {
                    "a" => "+",
                    "s" => "-",
                    "m" => "*",
                    "d" => "/",
                    "p" => "^",
                };
                Console.WriteLine($"\t{calculation.orderNo}: {calculation.firstNo} {OperationSymbol} {calculation.secondNo} = {calculation.result}");
            }
            Console.WriteLine("\n");
        }

        internal static string ValidateOperation(string Operation)
        {
            while(Operation.ToLower().Trim() != "a" &&
                  Operation.ToLower().Trim() != "s" &&
                  Operation.ToLower().Trim() != "m" &&
                  Operation.ToLower().Trim() != "d" &&
                  Operation.ToLower().Trim() != "p")
            {
                Console.Write("Invalid option. Choose again: ");
                Operation = Console.ReadLine();
            }
            return Operation;
        }

        internal static void ContinueCalculationFromList(List<Calculation> cals, int order)
        {
            Console.Write("\nEnter a list item to continue calculation: ");
            string listItem = (Console.ReadLine());
            int cleanListItem = 0;
            while (!int.TryParse(listItem, out cleanListItem) || (Convert.ToInt32(listItem) > cals.Count))
            {
                Console.Write("Invalid input. Try again: ");
                listItem = Console.ReadLine();
            }
            Console.WriteLine("\nNew Calculation");
            Console.WriteLine("---------------");
            double Input1 = cals[cleanListItem-1].result;
            Console.WriteLine("First number: " +Input1);
            Console.Write("Type the second number and press any key to enter: ");
            string Input2 = Console.ReadLine();
            double cleanNum2 = 0;
            while (!double.TryParse(Input2, out cleanNum2))
            {
                Console.WriteLine("Please enter an integer value: ");
                Input2 = Console.ReadLine();
            }

            Console.WriteLine("\nChoose an option for the operation to be conducted:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - to the Power of");

            Console.Write("\nYour option? ");
            string operation = Console.ReadLine();
            string cleanOperation = ValidateOperation(operation);

            try
            {
                double result = double.NaN;
                switch (cleanOperation)
                    {
                        case "a":
                            result = Input1 + cleanNum2;
                            break;
                        case "s":
                            result = Input1 - cleanNum2;
                            break;
                        case "m":
                            result = Input1 * cleanNum2;
                            break;
                        case "d":
                            if (cleanNum2 != 0)
                            {
                                result = Input1 / cleanNum2;
                            }
                            break;
                        case "p":
                            result = Math.Pow(Input1, cleanNum2);
                            break;
                        default:
                            break;
                } 

                if (double.IsNaN(result))
                {
                    Console.WriteLine("\nThis operation will result in a mathematical error.\n");
                }
                else
                {
                    order++;
                    Helpers.SaveCalculationsToList(order, Input1, cleanNum2, result, cleanOperation, cals);
                    Console.WriteLine("\nResult : {0:0.####}\n", result);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh No! An exception occurred trying to do the math.\nDetails: " + e.Message);
            }

        }
    }
}

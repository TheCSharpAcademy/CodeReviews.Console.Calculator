
namespace Calculator.Lonchanick
{
    public class Calculator
    {
        LogClass log = new LogClass();

        public void ShowContent() {log.PrintContent();}
        public void CloseLog() { log.CloseLog(); } 
        public void DoOperation(double num1, double num2, string op)
        {
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    double result = num1 + num2;
                    //writer.WriteValue("Add");
                    Operations aux = new Operations(num1, num2, result, "Add");
                    log.AddOperation(aux);
                    Console.WriteLine($"Your result: {num1} + {num2} = " + result);
                    break;
                case "s":
                    result = num1 - num2;
                    //writer.WriteValue("Subtract");
                    Operations aux2 = new Operations(num1, num2, result, "Subtract");
                    log.AddOperation(aux2);
                    Console.WriteLine($"Your result: {num1} - {num2} = " + result);
                    break;
                case "m":
                    result = num1 * num2;
                    //writer.WriteValue("Multiply");
                    Operations aux3 = new Operations(num1, num2, result, "Multiply");
                    log.AddOperation(aux3);
                    Console.WriteLine($"Your result: {num1} * {num2} = " + result);
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    while (num2 == 0)
                    {
                        Console.WriteLine("Enter a non-zero divisor: ");
                        num2 = Convert.ToInt32(Console.ReadLine());
                    }
                    result = num1 / num2;
                    //Console.WriteLine($"Your result: {num1} / {num2} = " + result);
                    //writer.WriteValue("Divide");
                    Operations aux4 = new Operations(num1, num2, result, "Divide");
                    log.AddOperation(aux4);
                    Console.WriteLine($"Your result: {num1} / {num2} = " + result);
                    break;
                case "l":
                    log.PrintContent();
                    break;
                default:
                    break;
            }
        }
    }

}

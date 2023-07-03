namespace Calculator.Mo3ses
{
    public static class Menu
    {
        public static void StartMenu(bool isListValue){
            Console.WriteLine("-----------------------Calculator-----------------------");
            Console.WriteLine("What math operation do you want to do:");
            Console.WriteLine("1 - Add");
            Console.WriteLine("2 - Subtract");
            Console.WriteLine("3 - Multiply");
            Console.WriteLine("4 - Divide");
            Console.WriteLine("5 - Square Root");
            Console.WriteLine("6 - Taking the Power of a number");
            Console.WriteLine("7 - Ten Times");
            Console.WriteLine("8 - Trigonometry functions");
            if (isListValue)
            {
                Console.WriteLine("9 - Last Calcs");
                Console.WriteLine("10 - Delete Last Calcs List");
            }
            Console.WriteLine("11 - Quit Program");
            Console.WriteLine("--------------------------------------------------------");
            
        }
        public static bool MenuAnswer(int id, double value1, double value2, MathOperations calculator){
            double result = 0;
            switch (id)
                {
                    case 1:
                        result = calculator.Sum(value1, value2);
                       Console.WriteLine($"Your result: {value1} + {value2} = {result}");
                        break;
                    case 2:
                        result = calculator.Subtract(value1, value2);
                        Console.WriteLine($"Your result: {value1} - {value2} = {result}");
                        break;
                    case 3:
                        result = calculator.Multiply(value1, value2);
                        Console.WriteLine($"Your result: {value1} * {value2} = {result}");
                        break;
                    case 4:
                        result = calculator.Divide(value1, value2);
                        Console.WriteLine($"Your result: {value1} / {value2} = {result}");
                        break;
                    case 5:
                        result = calculator.SquareRoot(value1);
                        Console.WriteLine($"Your result: √{value1} = {result}");
                        break;
                    case 6:
                        result = calculator.TakingPower(value1, value2);
                        Console.WriteLine($"Your result: ({value1} ^ {value2}) = {result}");
                        break;
                    case 7:
                       result =  calculator.TenTimes(value1);
                       Console.WriteLine($"Your result: ({value1} * 10) = {result}");
                        break;
                    case 8:
                        TrigonometryMenu(calculator);
                        break;
                    case 9:
                        calculator.ListOperations();
                        break;
                    case 10:
                        calculator.DeleteOperationList();
                    break;
                    case 11:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Option, Try Again.");
                        break;
                }
                if (id == 9){
                    return false;
                }else{
                    return true;
                }
        }
        public static void TrigonometryMenu(MathOperations calculator){
            Console.WriteLine("1 - Sine");
            Console.WriteLine("2 - Cosine");
            Console.WriteLine("3 - Tangent");
            int answer = Convert.ToInt32(Console.ReadLine());
            double value1 = 0;

            switch (answer)
            {
                case 1:
                    Console.WriteLine("Write the angle value (radians):");
                    value1 = Convert.ToDouble(Console.ReadLine());
                    double result = calculator.Sine(value1);
                    Console.WriteLine($"Your result: sin({value1}) = {result}");
                    break;
                case 2:
                    Console.WriteLine("Write the angle value (radians):");
                    value1 = Convert.ToDouble(Console.ReadLine());
                    double result1 = calculator.Cosine(value1);
                    Console.WriteLine($"Your result: cos({value1}) = {result1}");
                    break;
                case 3:
                    Console.WriteLine("Write the angle value (radians):");
                    value1 = Convert.ToDouble(Console.ReadLine());
                    double result2 = calculator.Tangent(value1);
                    Console.WriteLine($"Your result: tan({value1}) = {result2}");
                    break;
                default:
                    Console.WriteLine("Invalid Option, Try Again.");
                    TrigonometryMenu(calculator);
                break;
            }

        }
    }
}

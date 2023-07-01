using System;

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
            //Console.WriteLine("5 - Taking The Power");
            //Console.WriteLine("6 - x10");
            //Console.WriteLine("7 - Take Square Root");
            //Console.WriteLine("8 - Trigonometry functions");
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
                    case 9:
                        calculator.ListOperations();
                        break;
                    case 10:
                        calculator.DeleteOperationList();
                    break;
                    case 11:
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
    }
}

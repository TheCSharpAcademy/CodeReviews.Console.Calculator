using System;

namespace Calculator.Mo3ses
{
    public static class Menu
    {
        public static void StartMenu(){
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
            Console.WriteLine("9 - Quit Program");
            Console.WriteLine("--------------------------------------------------------");
            Console.Write("Your option? ");
        }
    }
}

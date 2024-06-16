using Objects;

namespace Helpers
{

    public class UsesListFunctions
    {
        public static void PrintUses()
        {

            Console.Clear();
            Console.WriteLine("Past Uses Log");
            Console.WriteLine("------------------------ \n");


            if (Objects.Uses.uses.Count() == 0)
            {
                Console.WriteLine("List empty.");
            }

            int iteration = 1;
            for (int i = Objects.Uses.uses.Count() - 1; i >= 0; i--, iteration++)
            {
                if (Objects.Uses.uses[i].Op != "Square Root")
                {
                    Console.WriteLine($"{iteration}. \t{Objects.Uses.uses[i].Operand1} {Objects.Uses.uses[i].Op} {Objects.Uses.uses[i].Operand2} = {Objects.Uses.uses[i].Result} \n");
                }
                else
                {
                    Console.WriteLine($"{iteration}. \t{Objects.Uses.uses[i].Operand1} {Objects.Uses.uses[i].Op} = {Objects.Uses.uses[i].Result} \n");
                }
               
            }
        }

        public static void DeleteUses()
        {
            Objects.Uses.uses.Clear();

            Console.WriteLine("List emptied.");
        }

        public static void AddToHistory(double operand1, double operand2, double result, string op = "no op applied")
        {
            switch (op)
            {
                case "m":
                    op = "*";
                    break;
                case "s":
                    op = "-";
                    break;
                case "a":
                    op = "+";
                    break;
                case "d":
                    op = "/";
                    break;
                case "p":
                    op = "^";
                    break;
                case "r":
                    op = "Square Root";
                    break;
            }

            Objects.Uses.uses.Add(new CalcUse
            {
                Operand1 = operand1,
                Operand2 = operand2,
                Op = op,
                Result = result
            });
        }
    }
}

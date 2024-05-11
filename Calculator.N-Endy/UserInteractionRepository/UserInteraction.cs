namespace Calculator.N_Endy.UserInteractionRepository
{
    public class UserInteraction : IUserInteraction
    {
        public void DisplayIntroductoryMessage()
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowMessage(string message, double result)
        {
            Console.WriteLine(message, result);
        }

        public double GetNumberFromUser()
        {
            Console.Write("Type a number, and then press 'Enter': ");
            string? numInput = Console.ReadLine();
            
            double cleanNum = 0;

            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.WriteLine("Please enter a valid number.");
                numInput = Console.ReadLine();
            }

            return cleanNum;
        }

        public string GetOperatorFromUser()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            return op;
        }

        public string GetInputFromUser()
        {
            return Console.ReadLine();
        }
    }
}
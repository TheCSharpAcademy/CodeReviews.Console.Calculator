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

        public string GetNumberFromUser()
        {
            throw new NotImplementedException();
        }

        public double CleanNumberFromUser()
        {
            throw new NotImplementedException();
        }

        public string GetOperatorFromUser()
        {
            throw new NotImplementedException();
        }
    }
}
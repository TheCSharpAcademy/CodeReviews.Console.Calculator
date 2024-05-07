namespace Calculator.N_Endy.UserInteractionRepository
{
    public interface IUserInteraction
    {
        void DisplayIntroductoryMessage();
        void ShowMessage(string message);
        string GetNumberFromUser();
        double CleanNumberFromUser();
        string GetOperatorFromUser();
    }
}
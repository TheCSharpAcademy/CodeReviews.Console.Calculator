namespace Calculator.N_Endy.UserInteractionRepository
{
    public interface IUserInteraction
    {
        void DisplayIntroductoryMessage();
        void ShowMessage(string message);
        void ShowMessage(string message, double result);
        double GetNumberFromUser();
        string GetOperatorFromUser();
        string GetInputFromUser();
    }
}
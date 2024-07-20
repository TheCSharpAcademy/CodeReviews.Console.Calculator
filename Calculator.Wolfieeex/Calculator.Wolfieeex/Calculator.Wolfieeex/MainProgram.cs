namespace Calculator.Wolfieeex;

class MainProgram
{
    static void Main(string[] args)
    {
        ScreenEngine screenEngine = new ScreenEngine();

        try
        {
            screenEngine.RunScreens();
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n\nAn exception occurred trying to run the program.\n - Details: " + ex.Message + ".\nThis application will close now.");
        }


        Console.Write("Thank you for using this application! Press any key to exit: ");
        Console.ReadKey();
    }
}


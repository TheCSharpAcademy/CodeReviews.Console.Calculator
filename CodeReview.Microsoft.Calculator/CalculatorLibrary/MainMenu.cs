namespace CalculatorLibrary
{
    public class MainMenu
    {
        public static void StartingMenu()
        {
            bool continueApp = true;
            Calculator calculator = new Calculator();
            calculator.LoadData();
            do
            {
                Console.Clear();
                Console.WriteLine("Choose from the options below:");
                Console.WriteLine();

                Console.WriteLine("1 - Calculator");
                Console.WriteLine("2 - Print previous calculations");
                Console.WriteLine("3 - Delete previous calculations");
                Console.WriteLine("e - Exit");

                Console.WriteLine();
                Console.WriteLine("---------------------------------------------");


                var menuSelection = Console.ReadLine().ToUpper().Trim();

                switch (menuSelection)
                {
                    case "1":
                        calculator.StartCalculator();
                        break;

                    case "2":
                        calculator.PrintCalculations();
                        break;

                    case "3":
                        calculator.Delete();
                        break;

                    case "E":
                        continueApp = false;
                        break;
                    default:
                        break;
                }
            } while (continueApp);
            Console.WriteLine("Calculator Exited");
        }
    }
}

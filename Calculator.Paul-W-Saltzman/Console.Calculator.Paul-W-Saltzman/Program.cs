using CalculatorLibrary;


namespace CalculatorProgram
{
    class Program
    {

        static void Main(string[] args)
        {
            bool endApp = false;
            Calculator calculator = new Calculator();

            while (!endApp)
            {
                Menu.ShowMenu(calculator);

                endApp = Helpers.EndApp();
            }
            calculator.Finish();
            return;
        }
    }
}
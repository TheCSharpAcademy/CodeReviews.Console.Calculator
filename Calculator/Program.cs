namespace Calculator;
using CalculatorLibrary;

public class Program
{
    public static void Main(string[] args)
    {
        View view = new();
        Calculator calc = new();
        
        view.ShowHeader();
        using (calc)
        {
            do
            {
                var num1 = view.GetNumberFromUser();
                var num2 = view.GetNumberFromUser();
                var f = view.GetFunctionFromUser();
                try
                {
                    var res = calc.ComputeResult(f, num1, num2);
                
                    if (Double.IsNaN(res) || Double.IsInfinity(res))
                    {
                        view.ShowMessage("This operation resulted in an arithmetic error.");
                    }
                    else
                    {
                        view.ShowResult(f, num1, num2, res);
                    }
                }
                catch (Exception e)
                {
                    view.ShowMessage("Oh no! An exception occurred trying to do the math.");
                }

            } while (view.PromptUserToQuit() != true);
        }
    }
}









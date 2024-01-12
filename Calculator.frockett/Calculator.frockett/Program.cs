using CalculatorLibrary;
using System.ComponentModel.Design;
using System.Security.AccessControl;

namespace CalculatorProgram.frockett;

internal class Program
{
    static void Main(string[] args)
    {
        Engine menu = new Engine();
        menu.ShowMenu();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Calculator_App.ConsoleCalculatorApp.Model
{
    internal class MathProblem
    {
        internal float Answer { get; set; } = float.NaN;
        internal float Num1 { get; set; }
        internal float Num2 { get; set; }
        internal string Operation { get; set; } = string.Empty;
    }
}

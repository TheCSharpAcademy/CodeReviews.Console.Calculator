using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary;

internal class History
{
    public double FirstOperand { get; set; }
    public double SecondOperand { get; set; }
    public string Operator { get; set; }
    public double Sum { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary.Models;
public class Equasion
{
    required public double A { get; set; }
    required public double B { get; set; }
    required public string Operation { get; set; }
    public double Result { get; set; } = double.NaN;

    public override string ToString()
    {
        var sign = Operation switch
        {
            "Add" => '+',
            "Subtract" => '-',
            "Multiply" => '*',
            "Divide" => '/'
        };
        return $"{A} {sign} {B} = {Result:0.##}";
    }
}

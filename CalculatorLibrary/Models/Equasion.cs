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
            "Add" => "+",
            "Subtract" => "-",
            "Multiply" => "*",
            "Divide" => "/",
            "Power" => "^",
            "Root" => "\u221A",
            "Sine" => "sin",
            "Cosine" => "cos",
            "Tangent" => "tg",
            "Cotangent" => "ctg"
        };
        if (sign.Length > 1)
        {
            return $"{sign} {A} = {Result:0.##}";
        }
        return $"{A} {sign} {B} = {Result:0.##}";
    }
}

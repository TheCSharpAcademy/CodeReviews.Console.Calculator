using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patryk_MM.Console.Calculator {
    public class Calculator {
        public static double DoOperation(double x, double y, string op) {
            double result = double.NaN; //

            //Use switch statement to do the math
            switch (op) {
                case "a":
                    result = x + y; break;
                case "s":
                    result = x - y; break;
                case "m":
                    result = x * y; break;
                case "d":
                    if (x != 0) result = x / y;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}

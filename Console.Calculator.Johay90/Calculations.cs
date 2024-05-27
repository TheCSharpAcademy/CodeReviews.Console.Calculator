public class Calculations
{
    public double num1;
    public double num2;
    private string op;
    private double res;

    public Calculations(double num1, double num2, string op, double res)
    {
        this.num1 = num1;
        this.num2 = num2;
        this.op = op;
        this.res = res;
    }

    public override string ToString()
    {
        string opFormat = "";

        switch (op)
        {
            case "a":
                opFormat = "+";
                break;
            case "s":
                opFormat = "-";
                break;
            case "m":
                opFormat = "*";
                break;
            case "d":
                opFormat = "/";
                break;
        }

        return $"{num1} {opFormat} {num2} = {res}";
    }
}
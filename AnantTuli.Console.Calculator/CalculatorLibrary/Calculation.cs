public class Calculation
{
    public string op;
    public double[] nums;
    public double result;
    public string mathematicalOperator;

    public Calculation(string op, double[] nums, double result, string mathematicalOperator)
    {
        this.op = op;
        this.nums = nums;
        this.result = result;
        this.mathematicalOperator = mathematicalOperator;
    }
}
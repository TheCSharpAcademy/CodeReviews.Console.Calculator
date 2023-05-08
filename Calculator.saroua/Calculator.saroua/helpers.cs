namespace Calculator.saroua
{
    internal class helpers
    {
        //Method that returns the amount of calculator uses incremented by one
        internal static int IncrementTotalUse(int runAmount)
        {
            int totalUse = runAmount;
            totalUse++;
            return totalUse;
        }
    }
}

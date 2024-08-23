namespace CalculatorProgram
{
    class CalculatorCounter
    {
        // Private field to store counter value
        private int _counter;

        // Method to increment the counter
        internal void IncrementCounter()
        {
            _counter++;
        }

        // Property to get current counter value
        internal int CounterValue
        {
            get { return _counter; }
        }
    }
}

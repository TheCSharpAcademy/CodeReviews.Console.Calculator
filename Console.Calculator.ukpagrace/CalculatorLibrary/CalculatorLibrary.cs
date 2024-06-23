namespace CalculatorLibrary
{
    
    public class Calculator
    {
        private int calculatorUsageCount = 0;
        struct UsersResult {
            public string previousCalculations;
            public double result;
        
        }
        private List<UsersResult> usersCalculations = new List<UsersResult>();
        public Calculator()
        {
        }
        public double DoOperation(double num1, double num2, string operation)
        {
            double result = double.NaN;

            switch (operation)
            {
                case "a":
                    result = num1 + num2;
                    UsersResult addition = new UsersResult();
                    addition.previousCalculations = $"{num1} + {num2}";
                    addition.result = result;
                    usersCalculations.Add(addition);
                    break;
                case "s":
                    result = num1 - num2;
                    UsersResult substraction = new UsersResult();
                    substraction.previousCalculations = $"{num1} + {num2}";
                    substraction.result = result;
                    usersCalculations.Add(substraction);
                    break;
                case "m":
                    result = num1 * num2;
                    UsersResult multiplication = new UsersResult();
                    multiplication.previousCalculations = $"{num1} + {num2}";
                    multiplication.result = result;
                    usersCalculations.Add(multiplication);
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        
                        result = num1 / num2;
                        UsersResult division = new UsersResult();
                        division.previousCalculations = $"{num1} + {num2}";
                        division.result = result;
                        usersCalculations.Add(division);

                    }
                    break;
                default:
                    break;
            }

            calculatorUsageCount++;
            return result;
        }


        public void GetUsageCount()
        {
           Console.WriteLine($"Calculator usage count is {calculatorUsageCount}");
        }


        public void deleteCalculation(int index)
        {

            usersCalculations.RemoveAt(index);
        }
        public void seeCalculations()
        {
            int count = 0;
            foreach (UsersResult calculation in usersCalculations)
            {
                Console.WriteLine($"{count} - {calculation.previousCalculations}");
                count++;

            }
        }

        public bool isUsersCalculationsEmpty()
        {
            return !usersCalculations.Any();
        }


        public bool isindexWithRange(int index)
        {
            bool result = true;
            if (index < 0) result = false;
            if (index > usersCalculations.Count) result = false;
            return result;
            
        }

        public double getUsersCalculationResult(int index)
        {
            return usersCalculations[index].result;
        }

    }
}

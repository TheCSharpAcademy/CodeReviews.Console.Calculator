namespace CalculatorLibrary {

    public class Calculator
    {
        private int calculatorUsageCount = 0;
        struct UsersResult {
            public string previousCalculations;
            public double result;
        }
        private List<UsersResult> usersCalculations = [];
        public Calculator() { }

        public double PerformAdvancedArithmetic(double num1, string operation)
        {
            double result = double.NaN;
            switch (operation) 
            {
                case "sqrt":
                    result = num1 * num1;
                    UpdateUsersCalculations("sqrt", num1, result);
                    break;
                case "pow":
                    result = Math.Pow(num1, 2.0);
                    UpdateUsersCalculations("^2", num1, result);
                    break;
                case "x":
                    result = num1 * 10;
                    UpdateUsersCalculations("10x", num1, result);
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    UpdateUsersCalculations("cos", num1, result);
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    UpdateUsersCalculations("sin", num1, result);
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    UpdateUsersCalculations("tan", num1, result);
                    break;
                default:
                    break;
            }

            calculatorUsageCount++;
            return result;
        }

        public double PerformBasicArithmetic(double num1, double num2, string operation)
        {
            double result = double.NaN;

            switch (operation)
            {
                case "a":
                    result = num1 + num2;
                    UpdateUsersCalculations("+", num1, num2, result);
                    break;
                case "s":
                    result = num1 - num2;
                    UpdateUsersCalculations("-", num1, num2, result);
                    break;
                case "m":
                    result = num1 * num2;
                    UpdateUsersCalculations("*", num1, num2, result);
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        UpdateUsersCalculations("/", num1, num2, result);
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

        public void DeleteCalculation(int index)
        {

            usersCalculations.RemoveAt(index);
        }
        public void SeeAllCalculations()
        {
            int count = 0;
            foreach (UsersResult calculation in usersCalculations)
            {
                Console.WriteLine($"{count} - {calculation.previousCalculations}");
                count++;
            }
        }

        public bool IsUsersCalculationsEmpty()
        {
            return !usersCalculations.Any();
        }

        public bool IsindexWithRange(int index)
        {
            return index < usersCalculations.Count && index >= 0;
        }

        public double GetUsersCalculationResult(int index)
        {
            return usersCalculations[index].result;
        }

        public void UpdateUsersCalculations(string operation, double num1, double num2, double result)
        {
            UsersResult userResult = new UsersResult();
            userResult.previousCalculations = $"{num1} {operation} {num2} = {result}";
            userResult.result = result;
            usersCalculations.Add(userResult);
        }

        public void UpdateUsersCalculations(string operation, double num1, double result)
        {
            UsersResult userResult = new UsersResult();
            userResult.previousCalculations = $"{operation} {num1}  = {result}";
            userResult.result = result;
            usersCalculations.Add(userResult);
        }
    }
}
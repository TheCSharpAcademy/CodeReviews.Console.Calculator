using System.Diagnostics;
using static MyFirstProgram.Helper;

namespace MyFirstProgram

{
    internal class GameEngine
    {

        internal void AdditionGame(List<int> difficulty, string name)
        {
            try
            {




                int maxDifficulty = difficulty[0];
                int totalOperations = difficulty[1];
                var random = new Random();
                var score = 0;
                Stopwatch timeMeasure = new Stopwatch();

                int firstNumber;
                int secondNumber;

                timeMeasure.Start();
                for (int i = 0; i < totalOperations; i++)
                {
                    Console.Clear();


                    firstNumber = random.Next(1, maxDifficulty);
                    secondNumber = random.Next(1, maxDifficulty);

                    Console.WriteLine($"{firstNumber} + {secondNumber}");

                    var result = Console.ReadLine();

                    if (result != "")
                    {

                        if (int.Parse(result) == firstNumber + secondNumber)
                        {
                            Console.WriteLine("Your answer was correct! Type any key for the next question");
                            score++;
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                            Console.ReadLine();
                        }


                    }
                    else
                    {
                        Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                        Console.ReadLine();
                    }
                    if (i == totalOperations - 1)
                    {
                        timeMeasure.Stop();
                        Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");
                        Helper.AddToHistory(score, "Game: Adition", name, timeMeasure.Elapsed.TotalSeconds.ToString());
                        Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalSeconds} Seconds");
                        Console.ReadLine();

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
           
            }

    internal void SubstractionGame(List<int> difficulty, string name)
        {
            int maxDifficulty = difficulty[0];
            int totalOperations = difficulty[1];

            var random = new Random();
            var score = 0;
            Stopwatch timeMeasure = new Stopwatch();

            int firstNumber;
            int secondNumber;

            timeMeasure.Start();

            for (int i = 0; i < totalOperations; i++)
            {
                Console.Clear();

                firstNumber = random.Next(1, maxDifficulty);
                secondNumber = random.Next(1, maxDifficulty);

                Console.WriteLine($"{firstNumber} - {secondNumber}");
                var result = Console.ReadLine();

                if (result != "")
                {

                    if (int.Parse(result) == firstNumber - secondNumber)
                    {
                        Console.WriteLine("Your answer was correct! Type any key for the next question");
                        score++;
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                        Console.ReadLine();
                    }



                }
                else
                {
                    Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                    Console.ReadLine();
                }
                if (i == totalOperations - 1)
                {
                    timeMeasure.Stop();
                    Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");
                    Helper.AddToHistory(score, "Game: Substraction", name, timeMeasure.Elapsed.TotalSeconds.ToString());
                    Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalSeconds} Seconds");
                    Console.ReadLine();
                }
            }


        }

        internal void MultiplicationGame(List<int> difficulty, string name)
        {
            int maxDifficulty = difficulty[0];
            int totalOperations = difficulty[1];

            var random = new Random();
            var score = 0;
            Stopwatch timeMeasure = new Stopwatch();

            int firstNumber;
            int secondNumber;


            timeMeasure.Start();
            for (int i = 0; i < totalOperations; i++)
            {
                Console.Clear();

                firstNumber = random.Next(1, maxDifficulty);
                secondNumber = random.Next(1, maxDifficulty);

                Console.WriteLine($"{firstNumber} * {secondNumber}");

                var result = Console.ReadLine();

                if (result != "")
                {

                    if (int.Parse(result) == firstNumber * secondNumber)
                    {
                        Console.WriteLine("Your answer was correct! Type any key for the next question");
                        score++;
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                        Console.ReadLine();
                    }


                }
                else
                {
                    Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                    Console.ReadLine();
                }
                if (i == totalOperations - 1)
                {
                    timeMeasure.Stop();
                    Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");
                    Helper.AddToHistory(score, "Game: Multiplication", name, timeMeasure.Elapsed.TotalSeconds.ToString());
                    Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalSeconds} Seconds");
                    Console.ReadLine();



                }
            }


        }

        internal void DivisionGame(List<int> difficulty, string name, bool randomop = false)
        {
            int maxDifficulty = difficulty[0];
            int totalOperations = difficulty[1];

            var random = new Random();
            var score = 0;
            Stopwatch timeMeasure = new Stopwatch();

            int firstNumber;
            int secondNumber;


            timeMeasure.Start();
            for (int i = 0; i < totalOperations; i++)
            {
                Console.Clear();

                firstNumber = random.Next(1, maxDifficulty);
                secondNumber = random.Next(1, maxDifficulty);

                while (firstNumber % secondNumber != 0)
                {
                    firstNumber = random.Next(1, maxDifficulty);
                    secondNumber = random.Next(1, maxDifficulty);
                }

                Console.WriteLine($"{firstNumber} / {secondNumber}");

                var result = Console.ReadLine();

                if (result != "")
                {

                    if (int.Parse(result) == firstNumber / secondNumber)
                    {
                        Console.WriteLine("Your answer was correct! Type any key for the next question");
                        score++;
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                        Console.ReadLine();
                    }


                }
                else
                {
                    Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                    Console.ReadLine();
                }
                if (i == totalOperations - 1)
                {
                    timeMeasure.Stop();
                    Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");


                    Helper.AddToHistory(score, "Game: Division", name, timeMeasure.Elapsed.TotalSeconds.ToString());
                    Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalSeconds} Seconds");
                    Console.ReadLine();

                }
            }

        }

        internal void PreviousGame()
        {
            Console.Clear();
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name} - Date: {game.Date} - Type: {game.Type} Score: {game.Score} pts, Time: {game.Time} seconds");
            }

            Console.WriteLine("Press any key to go back to the main menu.");
            Console.ReadLine();
            Console.Clear();

        }



        internal void RandomGames(List<int> difficulty, string name)
        {
            var random = new Random();

            var score = 0;
            Console.Clear();
            Stopwatch timeMeasure = new Stopwatch();
            timeMeasure.Start();
            int totalOperations = difficulty[1];
            for (int i = 0; i < totalOperations; i++)
            {
                // 1 ADDITION - 2 SUBSTRACTION - 3 MULTIPLICATION - 4 DIVISION
                var operators = random.Next(1, 5);

                if (operators == 1)
                {

                    if (RandomAdditionGame(difficulty, name))
                    {
                        score++;
                    }
                }
                else if (operators == 2)
                {
                    if (RandomSubstractionGame(difficulty, name))
                    {
                        score++;
                    }

                }
                else if (operators == 3)
                {
                    if (RandomMultiplicationGame(difficulty, name))
                    {
                        score++;
                    }

                }
                else if (operators == 4)
                {
                    if (RandomDivisionGame(difficulty, name))
                    {
                        score++;
                    }
                }
            }
            timeMeasure.Stop();
            Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalSeconds} Seconds");
            Helper.AddToHistory(score, "Game: RANDOM", name, timeMeasure.Elapsed.TotalSeconds.ToString());

        }




        internal bool RandomAdditionGame(List<int> difficulty, string name)
        {

            int maxDifficulty = difficulty[0];
            var random = new Random();
            

            int firstNumber;
            int secondNumber;

            firstNumber = random.Next(1, maxDifficulty);
            secondNumber = random.Next(1, maxDifficulty);

            Console.WriteLine($"{firstNumber} + {secondNumber}");

            var result = Console.ReadLine();
            if (result != "")
            {

                if (int.Parse(result) == firstNumber + secondNumber)
                {
                    Console.WriteLine("Your answer was correct! Type any key for the next question");
                    
                    Console.ReadLine();
                    return true;

                }
                else
                {
                    Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                    Console.ReadLine();
                    return false;
                }
            }

            else
            {

                Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                Console.ReadLine();
                return false;

            }



        }

        internal bool RandomSubstractionGame(List<int> difficulty, string name)
        {

            int maxDifficulty = difficulty[0];
            
            var random = new Random();
            
            int firstNumber;
            int secondNumber;

            firstNumber = random.Next(1, maxDifficulty);
            secondNumber = random.Next(1, maxDifficulty);

            Console.WriteLine($"{firstNumber} - {secondNumber}");

            var result = Console.ReadLine();

            if (result != "")
            {

                if (int.Parse(result) == firstNumber - secondNumber)
                {
                    Console.WriteLine("Your answer was correct! Type any key for the next question");
                    
                    Console.ReadLine();
                    return true;

                }
                else
                {
                    Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                    Console.ReadLine();
                    return false;
                }
            }

            else
            {

                Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                Console.ReadLine();
                return false;

            }



        }


        internal bool RandomMultiplicationGame(List<int> difficulty, string name)
        {

            int maxDifficulty = difficulty[0];
            
            var random = new Random();

            int firstNumber;
            int secondNumber;

            firstNumber = random.Next(1, maxDifficulty);
            secondNumber = random.Next(1, maxDifficulty);

            Console.WriteLine($"{firstNumber} * {secondNumber}");

            var result = Console.ReadLine();
            if (result != "")
            {

                if (int.Parse(result) == firstNumber * secondNumber)
                {
                    Console.WriteLine("Your answer was correct! Type any key for the next question");
                    Console.ReadLine();
                    return true;


                }
                else
                {
                    Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                    Console.ReadLine();
                    return false;
                }
            }

            else
            {

                Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                Console.ReadLine();
                return false;

            }



        }


        internal bool RandomDivisionGame(List<int> difficulty, string name)
        {

            int maxDifficulty = difficulty[0];
            
            var random = new Random();

            int firstNumber;
            int secondNumber;

            firstNumber = random.Next(1, maxDifficulty);
            secondNumber = random.Next(1, maxDifficulty);

            while (firstNumber % secondNumber != 0)
            {
                firstNumber = random.Next(1, maxDifficulty);
                secondNumber = random.Next(1, maxDifficulty);
            }

            Console.WriteLine($"{firstNumber} / {secondNumber}");

            var result = Console.ReadLine();

            if (result != "")
            {

                if (int.Parse(result) == firstNumber / secondNumber)
                {
                    Console.WriteLine("Your answer was correct! Type any key for the next question");
                    Console.ReadLine();
                    return true;
                }
                else
                {
                    Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                    Console.ReadLine();
                    return false;
                }




            }
            else
            {
                Console.WriteLine("Your answer was incorrect. Type any key for the next question");
                Console.ReadLine();
                return false;
            }
        }


    }
}





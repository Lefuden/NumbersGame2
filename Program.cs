namespace NumbersGame2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int guesses = 5;
            int userInput = 1;
            Random random = new Random();
            int maxRoll = 20;
            int secret = random.Next(1,maxRoll);
            List<int> numbers = new();

            Console.WriteLine($"For testing purposes, remove later! #{secret}");
            Console.WriteLine($"I'm thinking of a number between 1 and {maxRoll}. Can you guess which? I'll give you {guesses} attempts.");
            Console.Write("> ");

            while (true)
            {
                userInput = ErrorHandlingInt(userInput, numbers, maxRoll);
                numbers.Add(userInput);
                guesses--;

                int roundEnd = GameLoop(userInput, secret, guesses);

                if (roundEnd == 2)
                {
                    Console.WriteLine("You did it! This achievement will surely be passed down to future generations as the greatest moment of your life.");
                    break;
                }
                if (roundEnd == 1)
                {
                    numbers.Add(secret);
                    guesses = 5;
                    maxRoll *= 2;
                    Console.Clear();
                    Console.WriteLine($"Regrettably, you didn't manage to guess my number in {guesses} attempts.");
                    Console.WriteLine("\nWould you like to spice it up a bit? I'll pick a new number and you can guess again." + 
                                      $"\nTo make it more interesting I'll pick between 1 and {maxRoll}." +
                                      "\nI won't use any numbers you've already guessed or the same one I had." +
                                      "\n\nHere are the previous numbers used: ");

                    foreach (var i in numbers)
                    {
                        Console.Write($"{i} ");
                    }
                    secret = NewNumber(secret, numbers, maxRoll);
                    
                    Console.WriteLine($"\nFor testing purposes, remove later! #{secret}");
                    Console.WriteLine("Would you like to try again? [yes] or [no]");
                    Console.Write("> ");
                    ErrorHandlingString(guesses);
                }
                Console.Write("> ");
            }
        }

        static void ErrorHandlingString(int guesses)
        {
            while (true)
            {
                string userAnswer = Console.ReadLine().ToLower();
                if (userAnswer == "no")
                {
                    Console.WriteLine("Thanks for playing, better luck next time.");
                    Environment.Exit(0);
                }
                else if (userAnswer == "yes")
                {
                    Console.WriteLine($"Good luck, you have {guesses} attempts again.");
                    break;
                }
                Console.WriteLine("Try again, answer yes or no.");
                Console.Write("> ");
            }
        }

        static int ErrorHandlingInt(int i, List<int> n, int max) //maybe redo this a bit, too much nestling.
        {
            while (true)
            {
                try
                {
                    i = int.Parse(Console.ReadLine());

                    while (n.Contains(i) || i <= 0 || i > max)
                    {
                        if (i <= 0 || i > max)
                        {
                            Console.WriteLine("Guess within the range, please.");
                        }
                        else
                        {
                            Console.WriteLine("You've already guessed this before, try a different number.");
                        }
                        Console.Write("> ");
                        i = int.Parse(Console.ReadLine());    
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("You need to enter a number, try again!");
                    Console.Write("> ");
                }
            }
            return i;
        }

        static int NewNumber(int i, List<int> n, int max)
        {
            while (n.Contains(i))
            {
                Random random = new();
                i = random.Next(1,max);
            }
            return i;
        }

        static int GameLoop(int userInput, int secret, int guesses)
        {
            if (userInput < secret && guesses > 0)
            {
                Console.WriteLine($"Too low! You have {guesses} attempts remaining.");
            }
            else if (userInput > secret && guesses > 0)
            {
                Console.WriteLine($"Too high! You have {guesses} attempts remaining.");
            }
            else if (guesses == 0 && userInput != secret)
            {
                return 1;
            }
            else if (userInput == secret)
            {
                return 2;
            }
            return 0;
        }
    }
}

//add total # of guesses to display.
//clean up integer error handling.
//clean up terminal.
//add more interesting output during game loop. not just "too high" / "too low".
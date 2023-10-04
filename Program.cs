namespace NumbersGame2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int guesses = 5;
            int increaseGuess = 7;
            int sumGuesses = 0;
            int userInput = 1;
            Random random = new Random();
            int maxRoll = 20;
            int secret = random.Next(1,maxRoll);
            List<int> numbers = new();

            Console.WriteLine($"For testing purposes, remove later! #{secret}"); // <----- !!!

            IntroLogo();
            Console.WriteLine($"I'm thinking of a number between 1 and {maxRoll}. Can you guess which? I'll give you {guesses} attempts.");
            Console.Write("> ");

            while (true)
            {
                userInput = ErrorHandlingInt(userInput, numbers, maxRoll);
                numbers.Add(userInput);
                guesses--;
                sumGuesses++;

                int roundEnd = GameLoop(userInput, secret, guesses);

                if (roundEnd == 2)
                {
                    Console.Clear();
                    Console.WriteLine("You did it! This achievement will surely be passed down to future generations as the greatest moment of your life.");
                    switch (sumGuesses)
                    {
                        case <= 1:
                            Console.WriteLine($"It only took you {sumGuesses} attempt! You must be some kind of computer psychic.. or you cheated!");
                            break;
                        case > 15:
                            Console.WriteLine($"It took {sumGuesses} attempts, but you got there in the end. Good job sticking it out!");
                            break;
                        default: Console.WriteLine($"It only took you {sumGuesses} attempts!");
                            break;
                    }
                    break;
                }
                if (roundEnd == 1)
                {
                    numbers.Add(secret);
                    guesses = increaseGuess;
                    increaseGuess += 2;
                    maxRoll *= 2;
                    Console.Clear();
                    Console.WriteLine($"Regrettably, you didn't manage to guess my number in {sumGuesses} attempts.");
                    Console.WriteLine("\nWould you like to spice it up a bit? I'll pick a new number and you can guess again." + 
                                      $"\nTo make it more interesting I'll pick between 1 and {maxRoll}." +
                                      "\nI won't use any numbers you've already guessed or the same one I had." +
                                      "\n\nHere are the previous numbers used: ");

                    ListNumbers(numbers);
                    Console.WriteLine("\nWould you like to try again? [yes] or [no]");
                    Console.Write("> ");
                    ErrorHandlingString(guesses, numbers);
                    secret = NewNumber(secret, numbers, maxRoll);

                    Console.WriteLine($"\nFor testing purposes, remove later! #{secret}"); // <----- !!!
                }
                Console.Write("> ");
            }
        }

        static void ErrorHandlingString(int guesses, List<int> numbers)
        {
            while (true)
            {
                string userAnswer = Console.ReadLine().ToLower();
                if (userAnswer == "no")
                {
                    Console.Clear();
                    Console.WriteLine("Thanks for playing, better luck next time.");
                    Environment.Exit(0);
                }
                else if (userAnswer == "yes")
                {
                    Console.Clear();
                    ListNumbers(numbers);
                    Console.WriteLine($"\nGood luck, you have {guesses} attempts.");
                    break;
                }
                Console.WriteLine("\nTry again, answer yes or no.");
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
                            Console.WriteLine("\nGuess within the range, please.");
                        }
                        else
                        {
                            Console.WriteLine("\nYou've already guessed this before, try a different number.");
                        }
                        Console.Write("> ");
                        i = int.Parse(Console.ReadLine());    
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nYou need to enter a number, try again!");
                    Console.Write("> ");
                }
            }
            return i;
        }

        static void ListNumbers(List<int> numbers)
        {
            int j = 0;
            numbers.Sort();
            foreach (var i in numbers)
            {
                Console.Write($"{i} ");
                j++;
                if (j % 10 == 0)
                {
                    Console.Write("\n");
                }
            }
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
            Random random = new();
            int i = random.Next(1,5);

            if (userInput < secret && guesses > 0)
            {
                switch (i)
                {
                    case 1: Console.WriteLine($"\nToo low!");
                        break;
                    case 2: Console.WriteLine($"\nOh no no, it's too low!");
                        break;
                    case 3: Console.WriteLine("\nYou're off the mark, too low!");
                        break;
                    case 4: Console.WriteLine("\nTry a bit higher perhaps?");
                        break;
                    case 5: Console.WriteLine("\nGo higher, you're not there yet.");
                        break;
                    default:
                        Console.WriteLine("\nYour guess is a bit low!");
                        break;
                }

                switch (guesses)
                {
                    case <= 1: Console.WriteLine($"You have only {guesses} attempt remaining.");
                        break;
                    default:  Console.WriteLine($"You have {guesses} attempts remaining.");
                        break;
                }
            }
            if (userInput > secret && guesses > 0)
            {
                switch (i)
                {
                    case 1: Console.WriteLine("\nToo high!");
                        break;
                    case 2: Console.WriteLine("\nYou're too high.");
                        break;
                    case 3: Console.WriteLine("\nTry lower next time.");
                        break;
                    case 4: Console.WriteLine("\nOh no no, it's too high!");
                        break;
                    case 5: Console.WriteLine("\nNope, too high.");
                        break;
                    default: Console.WriteLine("\nYour guess is a bit high!");
                        break;
                }
                switch (guesses)
                {
                    case <= 1: Console.WriteLine($"You have only {guesses} attempt remaining.");
                        break;
                    default: Console.WriteLine($"You have {guesses} attempts remaining.");
                        break;
                }
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

        static void IntroLogo()
        {
            Console.WriteLine(" _   _ _   ____  _________ ___________ _____   ___  ___  ___ _____ ");
            Console.WriteLine("| \\ | | | | |  \\/  || ___ \\  ___| ___ \\  __ \\ / _ \\ |  \\/  ||  ___|");
            Console.WriteLine("|  \\| | | | | .  . || |_/ / |__ | |_/ / |  \\// /_\\ \\| .  . || |__  ");
            Console.WriteLine("| . ` | | | | |\\/| || ___ \\  __||    /| | __ |  _  || |\\/| ||  __| ");
            Console.WriteLine("| |\\  | |_| | |  | || |_/ / |___| |\\ \\| |_\\ \\| | | || |  | || |___ ");
            Console.WriteLine("\\_| \\_/\\___/\\_|  |_/\\____/\\____/\\_| \\_|\\____/\\_| |_/\\_|  |_/\\____/ \n\n");
        }
    }
}

//clean up integer error handling?
//clean up terminal.
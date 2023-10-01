namespace NumbersGame2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int guesses = 5;
            int userInput = 0;
            Random random = new Random();
            int secret = random.Next(1,21);
            List<int> numbers = new List<int>
            {
                secret
            };

            Console.WriteLine("Välkommen! Jag tänker på ett tal mellan 1 och 20. Kan du gissa vilket? Du får fem försök.");
            Console.Write("> ");
            while (true)
            {
                userInput = ErrorHandling(userInput);
                numbers.Add(userInput);
                guesses--;

                if (userInput < secret && guesses > 0)
                {
                    Console.WriteLine($"Du gissade för lågt! Du har {guesses} gissningar kvar.");
                }
                else if (userInput > secret && guesses > 0)
                {
                    Console.WriteLine($"Du gissade för högt! Du har {guesses} gissningar kvar.");
                }
                else if (guesses == 0)
                {
                    Console.WriteLine("Tyvärr, du lyckades inte gissa talet på fem försök!");
                    break;
                }
                else
                {
                    Console.WriteLine("Wohoo! Du klarade det!");
                    break;
                }
                Console.Write("> ");
            }
        }
        static int ErrorHandling(int i)
        {
            while (true)
            {
                try
                {
                    int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Du måste ange ett tal, försök igen!");
                    Console.Write("> ");
                }
            }
            return i;
        }
    }
}
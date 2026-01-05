using System;

namespace Howest.Prog.Cia.CurrencySwapper.Cons
{
    class Program
    {
        private const decimal EurToUsdRate = 1.0813771M; //1 EUR is altijd 1,08 USD
        
        static void Main(string[] args)
        {
            Console.WriteLine("Currency Converter\n==============");
            Console.Write("Enter amount (EUR): ");
            string userInput = Console.ReadLine();

            decimal amount;
            if (!decimal.TryParse(userInput, out amount) || amount < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter a positive amount!");
            }
            else
            {
                decimal targetAmount = amount * EurToUsdRate;

                Console.WriteLine($"{amount} EUR = {targetAmount:N2} USD");
            }
        }
    }
}

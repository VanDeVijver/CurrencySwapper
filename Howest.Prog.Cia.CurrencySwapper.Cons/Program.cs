using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using Howest.Prog.Cia.CurrencySwapper.Core.Validation;
using Howest.Prog.Cia.CurrencySwapper.Infrastructure.Realtime;
using System;

namespace Howest.Prog.Cia.CurrencySwapper.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            IRateService rateService = new ExchangeRatesApiClient();
            AmountValidator validator = new AmountValidator();
            CurrencyConverter converter = new CurrencyConverter(rateService);

            Console.WriteLine("Currency Converter\n==============");
            Console.WriteLine("-- enter non-number to exit. --\n");
            bool numberEntered;

            do
            {
                Console.Write("Enter amount (EUR): ");
                string userInput = Console.ReadLine();
                numberEntered = decimal.TryParse(userInput, out decimal amount);
                if (numberEntered)
                {
                    var validationResult = validator.Validate(amount);
                    if (validationResult.IsValid)
                    {
                        decimal convertedAmount = converter.Convert(amount, "EUR", "USD");

                        Console.WriteLine($"{amount} EUR = {convertedAmount:N2} USD");
                    }
                    else
                    {
                        ShowError(validationResult.ErrorMessage);
                    }
                }
            } while (numberEntered);
        }

        static void ShowError(string errorMessage)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ForegroundColor = currentColor;
        }
    }
}

namespace Howest.Prog.Cia.CurrencySwapper.Core
{
    public class AmountValidator
    {
        public const string AmountMustBePositive = "Amount must be a positive number";

        public virtual ValidationResult Validate(decimal amount)
        {
            var result = new ValidationResult();

            if (amount < 0)
            {
                result.ErrorMessage = AmountMustBePositive;
            }
            else
            {
                result.IsValid = true;
            }
            return result;
        }
    }
}

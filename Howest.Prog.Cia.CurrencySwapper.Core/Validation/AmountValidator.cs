namespace Howest.Prog.Cia.CurrencySwapper.Core.Validation
{
    public class AmountValidator
    {
        public const string AmountMustBePositive = "Amount must be a positive number";

        public ValidationResult Validate(decimal amount)
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

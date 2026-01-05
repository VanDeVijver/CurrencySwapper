using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;
using Howest.Prog.Cia.CurrencySwapper.Core.Validation;
using Howest.Prog.Cia.CurrencySwapper.Infrastructure.Realtime;
using System.Windows;

namespace Howest.Prog.Cia.CurrencySwapper.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AmountValidator validator;
        private readonly CurrencyConverter converter;
        private readonly IRateService rateService;

        public MainWindow()
        {
            rateService = new ExchangeRatesApiClient();
            validator = new AmountValidator();
            converter = new CurrencyConverter(rateService);

            InitializeComponent();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtInput.Text;

            if (decimal.TryParse(userInput, out decimal amount))
            {
                var validationResult = validator.Validate(amount);
                if (validationResult.IsValid)
                {
                    decimal convertedAmount = converter.Convert(amount, "EUR", "USD");

                    txtOutput.Text = $"{amount} EUR = {convertedAmount:N2} USD";
                }
                else
                {
                    ShowErrorDialog(validationResult.ErrorMessage);
                }
            }
            else
            {
                ShowErrorDialog($"Your input '{userInput}' must be a number");
            }
        }

        private void ShowErrorDialog(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

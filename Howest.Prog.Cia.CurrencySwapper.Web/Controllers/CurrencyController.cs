using Howest.Prog.Cia.CurrencySwapper.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Howest.Prog.Cia.CurrencySwapper.Core.Validation;
using Howest.Prog.Cia.CurrencySwapper.Core.Domain;
using Howest.Prog.Cia.CurrencySwapper.Core.Infrastructure;

namespace Howest.Prog.Cia.CurrencySwapper.Web.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly AmountValidator _validator;
        private readonly CurrencyConverter _converter;

        public CurrencyController(AmountValidator validator, CurrencyConverter converter)
        {
            _validator = validator;
            _converter = converter;
        }
       

        public IActionResult Convert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Convert(ConvertViewModel model)
        {
            decimal amount = model.Amount;

            var validationResult = _validator.Validate(amount);
            if (validationResult.IsValid)
            {
                decimal convertedAmount = _converter.Convert(amount, "EUR", "USD");
                model.ConvertedAmount = convertedAmount;
                model.ShowResult = true;
                return View(model);
            }
            else
            {
                ModelState.AddModelError(nameof(ConvertViewModel.Amount), validationResult.ErrorMessage);
                return View(model);
            }
        }

    }
}

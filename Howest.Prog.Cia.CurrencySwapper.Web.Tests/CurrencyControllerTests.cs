using Howest.Prog.Cia.CurrencySwapper.Core;
using Howest.Prog.Cia.CurrencySwapper.Web.Controllers;
using Howest.Prog.Cia.CurrencySwapper.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace Howest.Prog.Cia.CurrencySwapper.Web.Tests
{
    public class CurrencyControllerTests
    {
        [Fact]
        public void Convert_Returns_View()
        {
            //arrange
            var controller = new CurrencyController(null, null);

            //act
            IActionResult result = controller.Convert();

            //assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Convert_InvalidAmount_HasModelErrorForAmount()
        {
            //arrange
            var inputModel = new ConvertViewModel();
            var validator = new FakeAmountValidator();
            var controller = new CurrencyController(validator, null);

            //act
            IActionResult result = controller.Convert(inputModel);

            //assert
            Assert.True(controller.ModelState.ContainsKey(nameof(ConvertViewModel.Amount)));
        }

    }

    public class FakeAmountValidator : AmountValidator
    {
        public override ValidationResult Validate(decimal amount)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = "Fake Error" };
        }
    }

}

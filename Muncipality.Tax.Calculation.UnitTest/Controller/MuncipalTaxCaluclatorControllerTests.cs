using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Muncipality.Tax.Calculation.Api.Controllers;
using Muncipality.Tax.Calculation.UnitTest.Fixture;
using System;
using Xunit;

namespace Muncipality.Tax.Calculation.UnitTest.Controller
{
    public class MuncipalTaxCaluclatorControllerTests : IClassFixture<MuncipalTaxCaluclatorControllerFixture>
    {
        private readonly MuncipalTaxCaluclatorControllerFixture _fixture;
        private readonly MunicipalityTaxCalculationController _controller;

        public MuncipalTaxCaluclatorControllerTests(MuncipalTaxCaluclatorControllerFixture fixture)
        {
            _fixture = fixture;
            _controller = new MunicipalityTaxCalculationController(fixture._taxCalulator, _fixture.controllerLoggerMock.Object);
        }

        #region CreateEndPoint

        [Fact]
        public async void GivenValidMuncipalityDetails_WhenInvokedApi_Return200()
        {
            var input = _fixture.GetJsonData("Sweden");
            var output = await _controller.Post(input) as ObjectResult;
            Assert.NotNull(output);
            Assert.Equal(output.StatusCode.Value, StatusCodes.Status200OK);
            _fixture.taxCalculatorLoggerMock.Verify(
            m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((object v, Type _) => v.ToString().Contains("Create tax for muncipality")),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Invalid json")]
        public async void GivenInvalidMuncipalityDetails_WhenInvokedApi_ReturnBadRequest(string data)
        {
            var input = data;
            var output = await _controller.Post(input) as ObjectResult;
            Assert.NotNull(output);
            Assert.Equal(output.StatusCode.Value, StatusCodes.Status400BadRequest);
        }

        #endregion

        #region GetEndPoint

        [Theory]
        [InlineData("Denmark", "07/07/2020")]//Testcase for dailyTax
        [InlineData("Denmark", "14/07/2020")]//Testcase for weeklyTax
        [InlineData("Denmark", "02/05/2020")]//Testcase for MonthlyTax
        [InlineData("Denmark", "09/09/2020")]//Testcase for YearlyTax
        public async void GivenValidMuncipalityAndDate_WhenGetEndpointIsInvoked_ReturntaxValue(string muncipality, string data)
        {
            var input = _fixture.GetJsonData(muncipality);
            await _controller.Post(input);
            var output = await _controller.Get(muncipality, data) as ObjectResult;
            Assert.NotNull(output);
            Assert.Equal(output.StatusCode.Value, StatusCodes.Status200OK);
        }

        [Theory]
        [InlineData("California", "07/07/2020")]
        [InlineData("Denmark", "Invalid Input")]
        [InlineData("Denmark", null)]
        [InlineData(null, null)]
        public async void GivenInvalidMuncipalityAndDate_WhenGetEndpointIsInvoked_ReturnBadRequest(string muncipality, string data)
        {
            if (data == "Denmark")
            {
                var input = _fixture.GetJsonData(muncipality);
                await _controller.Post(input);
            }
            var output = await _controller.Get(muncipality, data) as ObjectResult;
            Assert.NotNull(output);
            Assert.Equal(output.StatusCode.Value, StatusCodes.Status400BadRequest);
        }

        #endregion
    }
}

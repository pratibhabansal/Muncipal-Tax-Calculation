using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Muncipality.Tax.Calculation.Api.Business.Manager;
using Muncipality.Tax.Calculation.Api.Controllers;
using Muncipality.Tax.Calculation.Api.StrategyPattern;
using System.IO;

namespace Muncipality.Tax.Calculation.UnitTest.Fixture
{
    public class MuncipalTaxCaluclatorControllerFixture
    {
        public readonly Mock<ILogger<MunicipalityTaxCalculationController>> controllerLoggerMock;
        public readonly Mock<ILogger<TaxCalculatorManager>> taxCalculatorLoggerMock;
        public readonly TaxCalculatorManager _taxCalulator;
        public readonly TaxCalculationStrategy strategy;
        public IMemoryCache InMemoryCache;

        public MuncipalTaxCaluclatorControllerFixture()
        {
            controllerLoggerMock = new Mock<ILogger<MunicipalityTaxCalculationController>>();
            taxCalculatorLoggerMock = new Mock<ILogger<TaxCalculatorManager>>();
            InMemoryCache = new MemoryCache(new MemoryCacheOptions { });
            strategy = new TaxCalculationStrategy();
            _taxCalulator = new TaxCalculatorManager(taxCalculatorLoggerMock.Object, InMemoryCache, strategy);
        }

        public string GetJsonData(string muncipality)
        {
            using StreamReader r = new StreamReader(muncipality + "Tax.json");
            string json = r.ReadToEnd();
            return json;
        }
    }
}

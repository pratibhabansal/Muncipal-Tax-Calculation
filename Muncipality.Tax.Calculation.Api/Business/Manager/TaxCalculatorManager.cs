using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Muncipality.Tax.Calculation.Api.Business.Interface;
using Muncipality.Tax.Calculation.Api.Models;
using Muncipality.Tax.Calculation.Api.StrategyPattern;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Muncipality.Tax.Calculation.Api.Business.Manager
{
    public class TaxCalculatorManager : ITaxCalculator
    {
        private readonly ILogger<TaxCalculatorManager> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly ITaxCalculationStrategy _taxCalculationStrategy;

        public TaxCalculatorManager(ILogger<TaxCalculatorManager> logger, IMemoryCache memoryCache, ITaxCalculationStrategy taxCalculationStrategy)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _taxCalculationStrategy = taxCalculationStrategy;
        }

        public async Task<bool> CreateTax(string values)
        {
            try
            {
                var model = JsonSerializer.Deserialize<MuncipalTax>(values);
                _logger.LogInformation($"Create tax for muncipality:{model.MuncipalityName}");
                _memoryCache.Set(model.MuncipalityName, values); //here we can use entity framework or api call to store in DB
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in creating muncipality:{ex.Message}");
            }
            return false;
        }

        public async Task<double> CalculateTax(string muncipality, string date)
        {
            double taxrate = 0;
            try
            {
                var taxDetails = JsonSerializer.Deserialize<MuncipalTax>(_memoryCache.Get(muncipality)?.ToString());//Can be replaced with database
                if (!string.IsNullOrWhiteSpace(muncipality) && taxDetails != null && !string.IsNullOrWhiteSpace(date))
                {
                    _logger.LogInformation($"Calculating tax for {muncipality} on {date}");
                    DateTime inputDate = DateTime.ParseExact(date, "dd/MM/yyyy", null);

                    _taxCalculationStrategy.SetTaxCalculationStrategy(new DailyTaxCalculation());
                    taxrate = _taxCalculationStrategy.ProcessTax(inputDate, taxDetails);
                    if (taxrate == 0)
                    {
                        _taxCalculationStrategy.SetTaxCalculationStrategy(new WeeklyTaxCalculation());
                        taxrate = _taxCalculationStrategy.ProcessTax(inputDate, taxDetails);
                    }
                    if (taxrate == 0)
                    {
                        _taxCalculationStrategy.SetTaxCalculationStrategy(new MonthlyTaxCalculation());
                        taxrate = _taxCalculationStrategy.ProcessTax(inputDate, taxDetails);
                    }
                    if (taxrate == 0)
                    {
                        _taxCalculationStrategy.SetTaxCalculationStrategy(new YearlyTaxCalculation());
                        taxrate = _taxCalculationStrategy.ProcessTax(inputDate, taxDetails);
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in Fetching tax for muncipality:{muncipality} | date:{date}");
            }
            return taxrate;
        }
    }
}

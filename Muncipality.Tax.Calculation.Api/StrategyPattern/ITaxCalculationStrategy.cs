using Muncipality.Tax.Calculation.Api.Models;
using System;

namespace Muncipality.Tax.Calculation.Api.StrategyPattern
{
    public interface ITaxCalculationStrategy
    {
        public double ProcessTax(DateTime date, MuncipalTax tax);
        public void SetTaxCalculationStrategy(TaxStrategy taxStrategy);
    }
}

using Muncipality.Tax.Calculation.Api.Models;
using System;

namespace Muncipality.Tax.Calculation.Api.StrategyPattern
{
    public class TaxCalculationStrategy : ITaxCalculationStrategy
    {
        private TaxStrategy _taxStrategy;

        public double ProcessTax(DateTime date, MuncipalTax tax)
        {
            return _taxStrategy.ProcessTax(date, tax);
        }

        public void SetTaxCalculationStrategy(TaxStrategy taxStrategy)
        {
            _taxStrategy = taxStrategy;
        }
    }
}

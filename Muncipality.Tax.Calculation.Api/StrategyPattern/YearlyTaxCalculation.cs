using Muncipality.Tax.Calculation.Api.Models;
using System;

namespace Muncipality.Tax.Calculation.Api.StrategyPattern
{
    public class YearlyTaxCalculation : TaxStrategy
    {
        public override double ProcessTax(DateTime date, MuncipalTax tax)
        {
            double taxAmount = 0;
            if (tax?.YearlyTax != null && (date >= DateTime.ParseExact(tax?.YearlyTax?.FromDate, "dd/MM/yyyy", null) && date <= DateTime.ParseExact(tax?.YearlyTax?.ToDate, "dd/MM/yyyy", null)))
            {
                taxAmount = tax.YearlyTax.TaxAmount;
            }
            return taxAmount;
        }
    }
}

using Muncipality.Tax.Calculation.Api.Models;
using System;

namespace Muncipality.Tax.Calculation.Api.StrategyPattern
{
    public class MonthlyTaxCalculation : TaxStrategy
    {
        public override double ProcessTax(DateTime date, MuncipalTax tax)
        {
            double taxAmount = 0;
            if (tax?.MonthlyTax != null && (date >= DateTime.ParseExact(tax?.MonthlyTax?.FromDate, "dd/MM/yyyy", null) && date <= DateTime.ParseExact(tax?.MonthlyTax?.ToDate, "dd/MM/yyyy", null)))
            {
                taxAmount = tax.MonthlyTax.TaxAmount;
            }
            return taxAmount;
        }
    }
}

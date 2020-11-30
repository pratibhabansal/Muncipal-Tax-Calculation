using Muncipality.Tax.Calculation.Api.Models;
using System;

namespace Muncipality.Tax.Calculation.Api.StrategyPattern
{
    public class WeeklyTaxCalculation : TaxStrategy
    {
        public override double ProcessTax(DateTime date, MuncipalTax tax)
        {
            double taxAmount = 0;
            if (tax?.WeeklyTax != null && (date >= DateTime.ParseExact(tax?.WeeklyTax?.FromDate, "dd/MM/yyyy", null) && date <= DateTime.ParseExact(tax?.WeeklyTax?.ToDate, "dd/MM/yyyy", null)))
            {
                taxAmount = tax.WeeklyTax.TaxAmount;
            }
            return taxAmount;
        }
    }
}

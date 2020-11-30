using Muncipality.Tax.Calculation.Api.Models;
using System;
using System.Linq;

namespace Muncipality.Tax.Calculation.Api.StrategyPattern
{
    public class DailyTaxCalculation : TaxStrategy
    {
        public override double ProcessTax(DateTime date, MuncipalTax tax)
        {
            double taxAmount = 0;
            if (tax?.DailyTax != null)
            {
                var dateList = tax?.DailyTax?.Dates?.Split(',')?.ToList();
                if (dateList != null)
                {
                    foreach (var item in dateList)
                    {
                        if (date == DateTime.Parse(item))
                        {
                            taxAmount = tax.DailyTax.TaxAmount;
                            break;
                        }
                    }
                }
            }
            return taxAmount;
        }
    }
}

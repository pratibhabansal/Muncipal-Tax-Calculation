using Muncipality.Tax.Calculation.Api.Models;
using System;

namespace Muncipality.Tax.Calculation.Api.StrategyPattern
{
    public abstract class TaxStrategy
    {
        public abstract double ProcessTax(DateTime date, MuncipalTax tax);
    }
}

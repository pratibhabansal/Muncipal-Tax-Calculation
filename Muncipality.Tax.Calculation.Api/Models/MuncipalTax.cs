namespace Muncipality.Tax.Calculation.Api.Models
{
    using Newtonsoft.Json;

    public class MuncipalTax
    {
        [JsonProperty("MuncipalityName")]
        public string MuncipalityName { get; set; }

        [JsonProperty("YearlyTax")]
        public Tax YearlyTax { get; set; }

        [JsonProperty("MonthlyTax")]
        public Tax MonthlyTax { get; set; }

        [JsonProperty("WeeklyTax")]
        public Tax WeeklyTax { get; set; }

        [JsonProperty("DailyTax")]
        public DailyTax DailyTax { get; set; }
    }

    public class DailyTax
    {
        [JsonProperty("Dates")]
        public string Dates { get; set; }

        [JsonProperty("TaxAmount")]
        public double TaxAmount { get; set; }
    }
    public class Tax
    {
        [JsonProperty("FromDate")]
        public string FromDate { get; set; }

        [JsonProperty("ToDate")]
        public string ToDate { get; set; }

        [JsonProperty("TaxAmount")]
        public double TaxAmount { get; set; }
    }
}

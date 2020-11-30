using System.Threading.Tasks;

namespace Muncipality.Tax.Calculation.Api.Business.Interface
{
    public interface ITaxCalculator
    {
        Task<bool> CreateTax(string values);
        Task<double> CalculateTax(string muncipality, string date);
    }
}

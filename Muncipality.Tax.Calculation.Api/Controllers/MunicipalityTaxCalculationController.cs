using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Muncipality.Tax.Calculation.Api.Business.Interface;

namespace Muncipality.Tax.Calculation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalityTaxCalculationController : ControllerBase
    {
        private readonly ITaxCalculator _taxCalculator;
        private readonly ILogger<MunicipalityTaxCalculationController> _logger;

        public MunicipalityTaxCalculationController(ITaxCalculator taxCalculator, ILogger<MunicipalityTaxCalculationController> logger)
        {
            _taxCalculator = taxCalculator;
            _logger = logger;
        }

        [HttpGet("getTax")]
        public async Task<IActionResult> Get(string muncipalityName, string date)
        {
            _logger.LogInformation($"Get End point Called for {muncipalityName} | date:{date}");
            var result = await _taxCalculator.CalculateTax(muncipalityName, date);
            if (result == 0.0) return BadRequest("Not Able to match details");
            else return Ok(result);
        }

        [HttpPost("createTax")]
        public async Task<IActionResult> Post([FromBody] dynamic value)
        {
            _logger.LogInformation("Post End point Called to create tax");
            var result = await _taxCalculator.CreateTax(value?.ToString());
            if (result) return Ok("Success");
            else return BadRequest("Failed");
        }
    }
}
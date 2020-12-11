using LeasePlanCurrencyConverter.Domain;
using LeasePlanCurrencyConverter.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeasePlanCurrencyConverter.Dto;

namespace LeasePlanCurrencyConverter.Controllers
{

    [ApiController]
    public class CurrenciesController : Controller
    {

        private readonly ILogger<CurrenciesController> _logger;
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(ILogger<CurrenciesController> logger, ICurrencyService currencyService)
        {
            _logger = logger;
            _currencyService = currencyService;
        }

        [HttpGet("currencies")]
        [Produces(typeof(IEnumerable<Currency>))]
        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            return await _currencyService.GetCurrenciesAsync();
        }

        [HttpGet("currencies/{fromCode}/{toCode}/{amount}")]
        [Produces(typeof(decimal))]
        public async Task<double> GetConvertedValueAsync([FromRoute] CurrencyConverterDto dto)
        {
            var convertedAmount = await _currencyService.GetConvertedValueAsync(
                dto.FromCode,
                dto.ToCode,
                dto.Amount);

            return convertedAmount;
        }
    }
}

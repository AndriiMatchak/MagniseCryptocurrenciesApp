using MagniseCryptocurrenciesApp.Controllers.BaseControllers;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagniseCryptocurrenciesApp.Controllers.CryptoCurrencyControllers
{
    [Route("api/[controller]/[action]")]
    public class AssetRatesController : BaseController
    {
        private readonly IAssetRateService _assetRateService;

        public AssetRatesController(IAssetRateService assetRateService)
        {
            _assetRateService = assetRateService;
        }

        [HttpGet]
        public IActionResult GetAssetPriceInfo(string assetId)
        {
            return OkResult(_assetRateService.GetAssetPriceInfo(assetId));
        }

        [HttpGet]
        public IActionResult GetAssetsPriceInfo(List<string> assetsId)
        {
            return OkResult(_assetRateService.GetAssetsPriceInfo(assetsId));
        }
    }
}

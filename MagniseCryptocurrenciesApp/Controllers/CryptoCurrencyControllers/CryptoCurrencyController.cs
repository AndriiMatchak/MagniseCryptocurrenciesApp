using MagniseCryptocurrenciesApp.Controllers.BaseControllers;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MagniseCryptocurrenciesApp.Controllers.CryptoCurrencyControllers
{
    [Route("api/[controller]/[action]")]
    public class AssetsController : BaseController
    {
        private readonly IAssetsService _assetsService;

        public AssetsController(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        [HttpGet]
        public IActionResult GetAllAssets()
        {
            return OkResult(_assetsService.GetAllAssets());
        }

        [HttpGet]
        public IActionResult GetAssetPriceInfo(string assetId)
        {
            return OkResult(_assetsService.GetAssetPriceInfo(assetId));
        }

        [HttpGet]
        public IActionResult GetAssetsPriceInfo(List<string> assetsId)
        {
            return OkResult(_assetsService.GetAssetsPriceInfo(assetsId));
        }
    }
}

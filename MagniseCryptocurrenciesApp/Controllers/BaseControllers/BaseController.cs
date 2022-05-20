using MagniseCryptocurrenciesApp.Common.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace MagniseCryptocurrenciesApp.Controllers.BaseControllers
{
    [ApiController]
    [Produces("application/json")] 
    public class BaseController : ControllerBase
    {
        protected virtual OkObjectResult OkResult(object value)
        {
            return Ok(new ApiResponse(value));
        }

        protected OkObjectResult OkResult()
        {
            return Ok(new ApiResponse());
        }
    }
}

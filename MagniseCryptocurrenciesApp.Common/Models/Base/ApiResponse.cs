using MagniseCryptocurrenciesApp.Common.StaticResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagniseCryptocurrenciesApp.Common.Models.Base
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Message = ApiResponseMessages.Success;
        }

        public ApiResponse(dynamic data)
        {
            Message = ApiResponseMessages.Success;
            Data = data;
        }

        public string Message { get; set; }

        public object Errors { get; set; }

        public dynamic Data { get; set; }
    }
}

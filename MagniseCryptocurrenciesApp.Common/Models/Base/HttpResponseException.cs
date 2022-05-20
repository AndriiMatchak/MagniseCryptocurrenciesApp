using System;
using System.Collections.Generic;
using System.Text;

namespace MagniseCryptocurrenciesApp.Common.Models.Base
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(string message)
            : base(message)
        {
        }

        public HttpResponseException()
            : base()
        {
        }

        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}

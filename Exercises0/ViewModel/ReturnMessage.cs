using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Exercises0.ViewModel
{
    public class ReturnMessage
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public Object Result { get; set; }
        public string Message { get; set; }

        public ReturnMessage(HttpStatusCode HttpStatusCode, Object Result, string Message)
        {
            this.HttpStatusCode = HttpStatusCode;
            this.Result = Result;
            this.Message = Message;
        }
    }
}

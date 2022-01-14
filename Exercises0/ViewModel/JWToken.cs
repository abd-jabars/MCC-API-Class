using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Exercises0.ViewModel
{
    public class JWToken
    {
        public HttpStatusCode Status { get; set; }
        public string IdToken { get; set; }
        public string Message { get; set; }

        public JWToken(HttpStatusCode Status, string IdToken, string Message)
        {
            this.Status = Status;
            this.IdToken = IdToken;
            this.Message = Message;
        }
    }
}

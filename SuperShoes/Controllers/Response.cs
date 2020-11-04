using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShoes.Controllers
{
    public class Response
    {
        public bool Success { get; set; } = false;  // Failure by default 

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public object Component { get; set; }
    }
}

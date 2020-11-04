using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace SuperShoes.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected const int ResultBadRequest = 400;
        protected const int ResultRecordNotFound = 404;
        protected const int ResultDBError = 410;
        protected const int ResultServerError = 500;

        protected const int StatusIdNew = 1;
        protected const int StatusIdCancelled = 255;
    }
}

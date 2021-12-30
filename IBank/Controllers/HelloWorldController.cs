using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IBank.Controllers
{
    [ApiController]
    [Route("hello-world")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public String Get()
        {
            return "Hello world!";
        }
    }
}

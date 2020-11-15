using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [ApiController]
    [Route("api/me")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CreditController : ControllerBase
    {
        public CreditController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Get()
        {
            return "dummy";
        }
    }
}

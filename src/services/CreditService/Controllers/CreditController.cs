using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobService.Controllers
{
    [ApiController]
    [Route("api/me")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CreditController : ControllerBase
    {
        private readonly ILogger _logger;

        public CreditController(ILogger logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Get()
        {
            _logger.LogInformation("Credit GET");
            await Task.Delay(10);

            return "dummy";
        }
    }
}

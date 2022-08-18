using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TutorBuddyApi.Controllers
{
    [ApiController]
    [Route("heartbeat")]
    public class HealthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public HealthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Health Check for Cloud
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("health")]
        [HttpGet]
        public IActionResult Health()
        {
            return Ok(new { status = "UP" });
        }



        [HttpGet]
        [Route("get-parameter-store")]
        [Authorize]
        public IActionResult GetConfiguration()
        {
            var constr = _configuration.GetValue<string>("FluentEmail:SendGridPKey");
            var config = _configuration.GetValue<string>("ConnectionStrings:ConnectionStr"); 
            string[] str = { constr, config };
            return Ok(str);
        }




    }
}
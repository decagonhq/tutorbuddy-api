using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TutorBuddyApi.Controllers
{
    [ApiController]
    [Route("heartbeat")]
    public class HealthController : ControllerBase
    {
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
    }
}
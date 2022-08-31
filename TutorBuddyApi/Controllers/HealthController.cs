using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.Interface;

namespace TutorBuddyApi.Controllers
{
    [ApiController]
    [Route("heartbeat")]
    public class HealthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public HealthController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
             _unitOfWork = unitOfWork;
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
        
        public IActionResult GetConfiguration()
        {
            var constr = _configuration.GetValue<string>("AppSettings:Secret");
            var config = _configuration.GetValue<string>("JWT:ValidAudience"); 
            string[] str = { constr, config };
            return Ok(str);
        }




    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet] 
        public ActionResult Get()
        {
            return Ok("Autorizimi po funksionon!!");
        }
    }
}

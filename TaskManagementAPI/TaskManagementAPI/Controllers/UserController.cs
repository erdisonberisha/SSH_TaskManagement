using Microsoft.AspNetCore.Mvc;


namespace TaskManagementAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> AutoCompleteUsers(string username)
        {
            var users = await _userService.SearchUserNamesAsync(username);
            return user;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

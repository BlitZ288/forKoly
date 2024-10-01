using Auth.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("v1/home")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        class User
        {
            public string Name { get; set; }
        }

        [ServiceFilter<AuthorizationFilter>]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new User() { Name = "Fedor" });
        }
    }
}

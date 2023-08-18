using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/")]
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Welcome()
        {
            return Json(new { message = "Hello" });
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Consumes("application/json")] // Accepter JSON
    [Produces("application/json")] // Retourner JSON
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

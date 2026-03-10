using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;


namespace Entity.Service.API.Controllers
{
    [Route("api/entity")]
    [ApiController]
    [Authorize(Policy = "PasswordChanged")]
    [Consumes("application/json")] // Accepter JSON
    [Produces("application/json")] // Retourner JSON
    public class EntitySgiController : ControllerBase
    {

        /// <summary>
        /// [HttpPost]
        ///[Route("/new")]
        ///public void  CreateNewEntity()
        ///{

        ///}
        /// </summary>

    }
}

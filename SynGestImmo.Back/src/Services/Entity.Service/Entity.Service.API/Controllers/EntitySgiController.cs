using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;
using Entity.Service.Application.Dtos;
using Entity.Service.Application.Dtos.EntitySgi;

namespace Entity.Service.API.Controllers
{
    [Route("api/entity")]
    [ApiController]
    [Authorize(Policy = "PasswordChanged")]
    [Consumes("application/json")] // Accepter JSON
    [Produces("application/json")] // Retourner JSON
    public class EntitySgiController : ControllerBase
    {


        [HttpPost]
        [Route("/new")]
        public void CreateNewEntity()
        {

        }


    }
}

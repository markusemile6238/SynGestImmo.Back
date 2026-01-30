using Identity.Service.Application.DTOS.RoleDto;
using Identity.Service.Application.Features.RoleFeature.Queries;
using Identity.Service.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;

namespace Identity.Service.API.Controllers
{
    [Route("api/auth/admin/role")]
    [ApiController]
    [Consumes("application/json")] // Accepter JSON
    [Produces("application/json")] // Retourner JSON
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IMediator mediator, ILogger<RoleController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            _logger.LogInformation($"Search first role with Id {id}");
          

            var command = new GetRoleByIdQuery { Id = id };
            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
            
        }
    }
}

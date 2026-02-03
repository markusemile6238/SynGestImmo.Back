using Identity.Service.Application.Features.RoleFeature.Queries.GetAllRoles;
using Identity.Service.Application.Features.RoleFeature.Queries.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.API.Controllers
{
    [Route("api/auth/admin/role")]
    [ApiController]
    [Authorize(Policy = "PasswordChanged")]
    [Consumes("application/json")] // Accepter JSON
    [Produces("application/json")] // Retourner JSON
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IMediator mediator, ILogger<RoleController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            _logger.LogInformation("Search all roles");
            var command = new GetAllRolesQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
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

using Identity.Service.API.Validators.Auth;
using Identity.Service.Application.DTOS.Auth;
using Identity.Service.Application.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;

namespace Identity.Service.API.Controllers
{

    [Route("api/auth/change-password")]
    [ApiController]
    [Produces("application/json")]

    public class ContractsController : Controller
    {
        private readonly ILogger<ContractsController> _logger;
        private readonly IMediator _mediator;

        public ContractsController(ILogger<ContractsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RenewPassword([FromBody] RenewPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(e => e.Value.Errors.Count > 0)
                                             .ToDictionary(
                                                kvp => kvp.Key,
                                                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                             );
                return BadRequest(
                    CqsResult.Failure(
                        Error.Validation("Invalid model", errors)));
            }

            _logger.LogInformation("Renewing password for user with ID: {Email}", dto.Email);

            var command = new ChangePasswordCommand { 
                Email = dto.Email,
                OldPassword = dto.CurrentPassword,
                NewPassword = dto.NewPassword,
                ConfirmPassword = dto.ConfirmPassword
            };

            var result = await _mediator.Send(command);

            return Ok(result);

        }
    }
}

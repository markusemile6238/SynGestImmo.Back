using Identity.Service.API.Extensions;
using Identity.Service.API.Validators.Auth;
using Identity.Service.Application.DTOS.Auth;
using Identity.Service.Application.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RenewPassword([FromBody] RenewPasswordDto dto)
        {

            _logger.LogInformation($"----->{dto.OldPassword}");
            _logger.LogInformation($"----->{dto.NewPassword}");
            _logger.LogInformation($"----->{dto.ConfirmPassword}");

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

            _logger.LogInformation("Renewing password");

            var command = new ChangePasswordCommand { 
                OldPassword = dto.OldPassword,
                NewPassword = dto.NewPassword,
                ConfirmPassword = dto.ConfirmPassword
            };

            var result = await _mediator.Send(command);

            return result.ToActionResult();

        }
    }
}

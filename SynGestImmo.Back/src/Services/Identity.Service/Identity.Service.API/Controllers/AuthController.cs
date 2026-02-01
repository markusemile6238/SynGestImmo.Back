using Identity.Service.Application.DTOS.Auth;
using Identity.Service.Application.DTOS.Jwt;
using Identity.Service.Application.Features.Auth;
using Identity.Service.Application.Features.Jwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;

namespace Identity.Service.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")] // Retourner JSON
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            _logger.LogInformation("DTO reçu: {@dto}", dto);
            if (dto == null)
                return BadRequest(
                    CqsResult.Failure(
                        Error.Validation("Request body is missing or invalid JSON")
                    )
                );

            var result = await _mediator.Send(
                new LoginCommand { Email = dto.Email, Password = dto.Password }
            );

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody]RefreshTokenDto dto)
        {
            _logger.LogInformation("DTO reçu: {@dto}", dto);
            if (dto == null)
            return BadRequest(
                CqsResult.Failure(
                    Error.Validation("Request body is missing or invalid JSON")
                )
            );
            var result = await _mediator.Send(
                new RefreshTokenCommand { RefreshToken = dto.RefreshToken }
            );
            return Ok(result);
        }
    }
}

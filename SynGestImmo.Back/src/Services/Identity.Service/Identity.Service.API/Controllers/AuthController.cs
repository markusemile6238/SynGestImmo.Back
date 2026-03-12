using Identity.Service.Application.DTOS.Auth;
using Identity.Service.Application.DTOS.Jwt;
using Identity.Service.Application.Features.Auth;
using Identity.Service.Application.Features.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;



namespace Identity.Service.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")] // Retourner JSON
    public class AuthController(
        IMediator _mediator,
        ILogger<AuthController> _logger
        ) : ControllerBase
    {
        



        #region LOGIN

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            
            if (dto == null)
                return BadRequest(CqsResult.Failure(Error.Validation("Request body is missing or invalid JSON")));

            var result = await _mediator.Send(
                new LoginCommand { Email = dto.Email, Password = dto.Password }
            );

            return Ok(result);
        }

        #endregion

        #region REFRESH TOKEN

        [HttpPost("refresh")]
        [Authorize(Policy = "PasswordChanged")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
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
        #endregion

        #region LOGOUT
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto dto)
        {
            _logger.LogInformation($"Logout user");

            if (dto == null)
                return BadRequest(
                    CqsResult.Failure(
                        Error.Validation("Request body is missing or invalid JSON")
                    )
                );
            var result = await _mediator.Send(new LogoutCommand { RefreshToken = dto.RefreshToken });
            return Ok(result);


        }
        #endregion
    }
}

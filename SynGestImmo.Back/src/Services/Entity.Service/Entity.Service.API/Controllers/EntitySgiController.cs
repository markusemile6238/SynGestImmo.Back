using Entity.Service.Application.Dtos.EntitySgi;
using Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity;
using Entity.Service.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;

namespace Entity.Service.API.Controllers
{
    [Route("api/profil")]
    [ApiController]
    [Authorize(Policy = "PasswordChanged")]
    [Consumes("application/json")] // Accepter JSON
    [Produces("application/json")] // Retourner JSON
    public class EntitySgiController(
         IMediator _mediator,
         ILogger<EntitySgiController> _logger
        ) : ControllerBase
    {

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> CreateNewEntity([FromBody] CreateEntityDtos dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value!.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                return BadRequest(
                    CqsResult.Failure(
                        Error.Validation("Invalid request data", errors
                        )
                     )
                    );
            }

            _logger.LogInformation("Received request to create new entity with display name: {DisplayName}", dto.DisplayName);

            var command = new CreateEntityCommand
            {
                Id = dto.Id!.Value,
                EntityType = dto.EntityType!.Value,
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                Phone = dto.Phone,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                BirthDate = dto.BirthDate,
                NationalId = dto.NationalId,
                CreatedAt = dto.CreatedAt!.Value
            };
             _logger.LogInformation("Sending CreateEntityCommand for entity with display name: {DisplayName}", command.DisplayName);

            var result = await _mediator.Send(command);

            return Ok( result );

        }


    }
}

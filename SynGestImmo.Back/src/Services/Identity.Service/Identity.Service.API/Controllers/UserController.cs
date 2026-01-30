using Identity.Service.Application.DTOS.UserDto;
using Identity.Service.Application.Features.UserFeature.Commands.CreateUser;
using Identity.Service.Application.Features.UserFeature.Commands.DeleteUser;
using Identity.Service.Application.Features.UserFeature.Queries.GetUserByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;

namespace Identity.Service.API.Controllers
{

    [Route("api/auth/admin/user")] 
    [ApiController]
    [Consumes("application/json")] // Accepter JSON
    [Produces("application/json")] // Retourner JSON
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }



        [HttpPost]
        public async Task<CqsResult> CreateUserAsync([FromBody] CreateUserDto dto)
        {

            if (!ModelState.IsValid) {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e=>e.ErrorMessage).ToArray()
                    );
                return CqsResult.Failure(Error.Validation("Invalid request data",errors));                    
            }

            _logger.LogInformation("Creation of new user");

            var command = new CreateUserCommand
            {
                Email = dto.Email,
                Password = dto.Password,
                RoleId = dto.RoleId,
                SyndicPrefix = dto.SyndicPrefix
            };

            var result = await _mediator.Send(command);
           
            return result;        
        }

        [HttpGet]
        public async Task<CqsResult> GetUserByEmail([FromBody] GetUserByEmailDto dto)
        {
            if (!ModelState.IsValid)
                return CqsResult.Failure(Error.Validation("Email not valid. Please try with a correct email address"));

            var query = new GetUserByEmailQuery{ Email= dto.Email };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpDelete]
        public async Task<CqsResult> DeleteUserById([FromBody] DeleteUserDto dto)
        {
            if(!ModelState.IsValid)
                return CqsResult.Failure(Error.Validation("Id not valid. Please check the value and the type"));

            var command = new DeleteUserCommand { Id = dto.Id };
            var result = await _mediator.Send(command);
            return result;

        } 
    }
}

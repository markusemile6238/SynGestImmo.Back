using Identity.Service.Application.DTOS.UserDtos;
using Identity.Service.Application.Features.UserFeature.Commands.CreateUser;
using Identity.Service.Application.Features.UserFeature.Commands.DeleteUser;
using Identity.Service.Application.Features.UserFeature.Queries.GetUserByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tools.Result;
using Identity.Service.API.Extensions;
using Identity.Service.Application.Features.UserFeature.Queries.GetAllUser;
using Microsoft.AspNetCore.Authorization;
using Identity.Service.Application.Features.UserFeature.Commands.UpdateUser;
using Identity.Service.Application.Features.UserFeature.Queries.GetUserById;
using Identity.Service.Application.Features.UserFeature.Queries.GetUserByEntityId;

namespace Identity.Service.API.Controllers
{

    [Route("api/admin/user")] 
    [ApiController]
    [Authorize(Policy = "PasswordChanged")]
    [Consumes("application/json")] // Accepter JSON
    [Produces("application/json")] // Retourner JSON
    public class UserController(
        IMediator _mediator,
        ILogger<UserController> _logger
        ) : ControllerBase
    {

        #region GET ALL
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> GetAllAsync()
        {
            var query = new GetAllUserQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        } 
        #endregion

        #region CREATEUSER
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto dto)
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

            _logger.LogInformation("Creation of new user");

            var command = new CreateUserCommand
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                RoleId = dto.RoleId,
                SyndicPrefix = dto.SyndicPrefix
            };

            var result = await _mediator.Send(command);

            return result.ToActionResult();
        }
        #endregion

        #region GETUSERBYEMAIL
        [HttpGet]
        public async Task<ActionResult<CqsResult>> GetUserByEmail( GetUserByEmailDto dto)
        {
            if (!ModelState.IsValid)
                return CqsResult.Failure(Error.Validation("Email not valid. Please try with a correct email address"));

            var query = new GetUserByEmailQuery { Email = dto.Email };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        #endregion  

        #region GETUSERBYId
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<ActionResult<CqsResult>> GetUserById(string id)
        {
            if (!ModelState.IsValid)
                return CqsResult.Failure(Error.Validation("Id not valid."));

            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }   
        #endregion

        #region GETUSERBYEntityId
        [HttpGet]
        [Route("detail/e/{id}")]
        public async Task<ActionResult<CqsResult>> GetUserByEntityId(string id)
        {
            if (!ModelState.IsValid)
                return CqsResult.Failure(Error.Validation("Id not valid."));

            var query = new GetUserByEntityIdQuery { EntityId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }   
        #endregion

        #region DELETEUSER
        [HttpDelete]
        public async Task<ActionResult<CqsResult>> DeleteUserById([FromBody] DeleteUserDto dto)
        {
            if (!ModelState.IsValid)
                return CqsResult.Failure(Error.Validation("Id not valid. Please check the value and the type"));

            var command = new DeleteUserCommand { Id = dto.Id };
            var result = await _mediator.Send(command);
            return Ok(result);

        }
        #endregion




        #region UPDATEUSER
        [HttpPut]
        public async Task<ActionResult<CqsResult>> UpdateUser([FromBody] UpdateUserDto dto)
        {


            if (!ModelState.IsValid)
                return CqsResult.Failure(Error.Validation("Invalid Request"));
            var command = new UpdateUserCommand
            {
                Id = dto.Id,
                Username = dto.Username,
                Email = dto.Email,

                MainRoleId = dto.RoleId

            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion


    }
}


using Entity.Service.Application.Common;
using Entity.Service.Application.Dtos.Api;
using Entity.Service.Application.Features.IdentityFeature;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity
{
    public class CreateEntityHandler(
        IUowCreateEntity _uowCreateEntity,
        ILogger<CreateEntityHandler> _logger,
        ISqlExceptionTranslator _sqlExceptionTranslator,
        IEntityService _entityService,
        IIdentityApi identityApi
        ) : IRequestHandler<CreateEntityCommand, CqsResult>
    {

        public async  Task<CqsResult> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            #region VALIDATORS
                        
            if(request == null)
                return CqsResult.Failure(Error.Validation("Request cannot be null"));

            if(request.Id == Guid.Empty)
                return CqsResult.Failure(Error.Validation("Id is required"));

            if(!Enum.IsDefined(request.EntityType))
                return CqsResult.Failure(Error.Validation("Invalid Entity Type"));

            if (string.IsNullOrWhiteSpace(request.DisplayName))
                return CqsResult.Failure(Error.Validation("Display Name is required"));
            
            if (string.IsNullOrWhiteSpace(request.Email))
                return CqsResult.Failure(Error.Validation("Email is required"));
            
            if (string.IsNullOrWhiteSpace(request.Phone))
                return CqsResult.Failure(Error.Validation("Phone is required"));
            
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return CqsResult.Failure(Error.Validation("Firstname is required"));
            
            if (string.IsNullOrWhiteSpace(request.LastName))
                return CqsResult.Failure(Error.Validation("Lastname is required"));
         
            if (request.BirthDate == DateTime.MinValue)
                return CqsResult.Failure(Error.Validation("Birthday is required"));

            if (string.IsNullOrWhiteSpace(request.NationalId))
                return CqsResult.Failure(Error.Validation("National Id Name is required"));

            #endregion


            // on va chercher si L'id existe bien
            Console.WriteLine($"======>MAKE REQUEST TO IDENTITY SUR {request.Id}");

            _logger.LogInformation("Checking if user {UserId} exists in Identity service", request.Id);

            CqsResult<ApiIdentityGetUser> userExist = await identityApi.UserExistAsync(request.Id);

            if (!userExist.IsSuccess)
                return CqsResult.Failure(Error.Validation("User with the provided ID does not exist in Identity Service"));

            Console.WriteLine($"======>USER EXIST {userExist.Data?.Email}");

            try
            {

                var result = await _uowCreateEntity.ExecuteAsync(
                    (conn, tx) => _entityService.CreateNewEntity(request, conn, tx));

                if(result.IsSuccess)
                {
                    return CqsResult.Success("Entity created successfully");
                }
                else
                {
                    Console.WriteLine("HERE<<<<<<<<<<");
                    _logger.LogError("Entity creation failed: {Error}", result.Error?.Message);
                    return CqsResult.Failure(result.Error);
                }

            }
            catch(SqlException ex)
            {
                var error = _sqlExceptionTranslator.Translate(ex);
                _logger.LogError(ex, "SQL Exception occurred while creating entity");
                return CqsResult.Failure(error);
            }     
        }
    }
}

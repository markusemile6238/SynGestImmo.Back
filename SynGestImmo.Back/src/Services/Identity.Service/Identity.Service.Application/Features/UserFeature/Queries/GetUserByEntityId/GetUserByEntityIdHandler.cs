using Identity.Service.Application.Common;
using Identity.Service.Domain.Repositories.UserRepositories;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Domaine.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetUserByEntityId
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByEntityIdQuery, CqsResult<User?>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserRolesCommandRepository _userRoleCommandRepo;
        private readonly ISqlExceptionTranslator _sqlHandler;

        public GetUserByIdHandler(IUserQueryRepository userQueryRepository, ISqlExceptionTranslator sqlHandler,IUserRolesCommandRepository userRolesCommandRepository)
        {
            _userQueryRepository = userQueryRepository;

            _sqlHandler = sqlHandler;
            _userRoleCommandRepo = userRolesCommandRepository;
        }

        public async Task<CqsResult<User?>> Handle(GetUserByEntityIdQuery request, CancellationToken cancellationToken)
        {
            // validations
            if(request == null)
                return CqsResult<User?>.Failure(Error.Validation("Request is require"));


            if(request.EntityId == null)
                return CqsResult<User?>.Failure(Error.Validation("EntityId is require"));

            Guid entityId = new Guid();           
            
            try
            {
               entityId = Guid.Parse(request.EntityId);
            }
            catch
            {
                return CqsResult<User?>.Failure(Error.Validation("EntityId not valid"));
            }

            User? user;

            try
            {              
                user = await _userQueryRepository.GetUserByEntityIdAsync(entityId);

                if(user is null)
                    return CqsResult<User?>.Failure(Error.NotFound(request.EntityId));

                return CqsResult<User?>.Success(user);

            }catch(SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult<User?>.Failure(error);
            }

        }
    }
}

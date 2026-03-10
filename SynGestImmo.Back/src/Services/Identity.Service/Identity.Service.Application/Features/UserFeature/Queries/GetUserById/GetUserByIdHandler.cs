using Identity.Service.Application.Common;
using Identity.Service.Domain.Repositories.UserRepositories;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Domaine.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, CqsResult<User?>>
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

        public async Task<CqsResult<User?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // validations
            if(request == null)
                return CqsResult<User?>.Failure(Error.Validation("Request is require"));


            if(request.Id == null)
                return CqsResult<User?>.Failure(Error.Validation("Id is require"));

            Guid userId = new Guid();           
            
            try
            {
               userId = Guid.Parse(request.Id);
            }
            catch
            {
                return CqsResult<User?>.Failure(Error.Validation("Id not valid"));
            }

            User? user;

            try
            {              
                user = await _userQueryRepository.GetUserByIdAsync(userId);

                if(user is null)
                    return CqsResult<User?>.Failure(Error.NotFound(request.Id));

                return CqsResult<User?>.Success(user);

            }catch(SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult<User?>.Failure(error);
            }

        }
    }
}

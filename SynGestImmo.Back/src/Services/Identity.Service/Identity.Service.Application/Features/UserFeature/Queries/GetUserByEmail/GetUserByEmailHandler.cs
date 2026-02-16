using Identity.Service.Application.Common;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Domaine.Entities;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetUserByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, CqsResult<User?>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserRolesCommandRepository _userRoleCommandRepo;
        private readonly ILogger<GetUserByEmailHandler> _logger;
        private readonly ISqlExceptionTranslator _sqlHandler;

        public GetUserByEmailHandler(IUserQueryRepository userQueryRepository, ILogger<GetUserByEmailHandler> logger, ISqlExceptionTranslator sqlHandler,IUserRolesCommandRepository userRolesCommandRepository)
        {
            _userQueryRepository = userQueryRepository;
            _logger = logger;
            _sqlHandler = sqlHandler;
            _userRoleCommandRepo = userRolesCommandRepository;
        }

        public async Task<CqsResult<User?>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            // validations
            if(request == null)
                return CqsResult<User?>.Failure(Error.Validation("Request is require"));

            if(request.Email == null)
                return CqsResult<User?>.Failure(Error.Validation("Email is require"));

            User? user;

            try
            {
                

                user = await _userQueryRepository.GetUserByEmailAsync(request.Email);

                if(user is null)
                    return CqsResult<User?>.Failure(Error.NotFound(request.Email));

                return CqsResult<User?>.Success(user);

            }catch(SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult<User?>.Failure(error);
            }

        }
    }
}

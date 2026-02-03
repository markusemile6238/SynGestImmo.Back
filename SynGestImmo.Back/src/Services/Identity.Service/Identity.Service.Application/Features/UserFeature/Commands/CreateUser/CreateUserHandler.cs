using Identity.Service.Application.Common;
using Identity.Service.Application.Features.UserFeature.Services;
using Identity.Service.Domain.Entities;
using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Domaine.Entities;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CqsResult>
    {
        private readonly IUserCommandRepository _commandRepository;
        private readonly IRolesQueryRepository _rolesQueryRepository;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IUserReferenceService _userReferenceService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISqlExceptionTranslator _sqlHandler;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRolesCommandRepository _userRoleCommandRepo;

        public CreateUserHandler(
            IUserCommandRepository commandRepository,
            ILogger<CreateUserHandler> logger,
            IUserReferenceService userReferenceService,
            IPasswordHasher passwordHasher,
            IRolesQueryRepository rolesQueryRepository,
            IUserRolesCommandRepository userRolesCommandRepository,
            ISqlExceptionTranslator sqlHandler,
            IUnitOfWork unitOfWork

        )
        {
            _commandRepository = commandRepository;
            _logger = logger;
            _userReferenceService = userReferenceService;
            _passwordHasher = passwordHasher;
            _rolesQueryRepository = rolesQueryRepository;
            _sqlHandler = sqlHandler;
            _unitOfWork = unitOfWork;
            _userRoleCommandRepo = userRolesCommandRepository;
        }

        public async Task<CqsResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // validations
            if (string.IsNullOrEmpty(request.Email))
                return CqsResult.Failure(Error.Validation("Email is require"));

            if (string.IsNullOrEmpty(request.Password))
                return CqsResult.Failure(Error.Validation("Password is require"));

            if (request.RoleId <= 0)
                return CqsResult.Failure(Error.Validation("Role ID is required"));

            // check role existence
            Role? role;
            try
            {
                role = await _rolesQueryRepository.GetRoleByIdAsync(request.RoleId);
            }
            catch (SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult.Failure(error);
            }

            if(role == null)
                return CqsResult.Failure(Error.NotFound("Role does not exist"));

            try
            {

                User u = new User
                {
                    Email = request.Email,
                    PasswordHash = _passwordHasher.HashPassword(request.Password),
                    MainRoleId = role.Id,
                    EntityId = Guid.NewGuid(), // va servir de id dans owner/logger ou employee
                    UserRef = await _userReferenceService.GenerateAsync(role.Id), // reference de l'utilisateur dans la transcription de l'application
                    CreatedAt = DateTime.UtcNow
                };

                

                await _unitOfWork.ExecuteAsync(async (connection, transaction) =>
                {
                    var userId = await _commandRepository.CreateUserAsync(u, connection, transaction);
                    await _userRoleCommandRepo.AssignRoleToUserAsync(userId, u.MainRoleId, connection, transaction);
                });

                _logger.LogInformation("New User Create successfully");

                return CqsResult.Success("New user created successfully");
            
            }
            catch (SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult.Failure(error);
            }
            catch (DataException ex)
            {
                _logger.LogError(ex, "Data error while creating user {Email}", request.Email);
                return CqsResult.Failure(
                    Error.Database("Unable to create user")
                );
            }

        }
    }
}

using Identity.Service.Application.Common;
using Identity.Service.Application.Features.UserFeature.Commands.CreateUser;
using Identity.Service.Application.Features.UserFeature.Services;
using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Domain.Repositories.UserRepositories;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Domaine.Entities;
using System.Data;
using Tools.Result;

namespace Identity.Service.Infrastructure.Services
{
    public class UserCreatorService
        (
        IUserCommandRepository userCommandRepository,
        IUserRolesCommandRepository userRolesCommandRepository,
        IRolesQueryRepository rolesQueryRepository,
        IUserReferenceService userReferenceService, 
        IPasswordHasher passwordHasher
        ) : IUserCreator
    {
        private readonly IUserCommandRepository _userCommandRepository = userCommandRepository;
        private readonly IUserRolesCommandRepository _userRolesCommandRepository = userRolesCommandRepository;
        private readonly IRolesQueryRepository _rolesQueryRepository = rolesQueryRepository;
        private readonly IUserReferenceService _userReferenceService = userReferenceService;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;


        public async Task<CqsResult> CreateUserAsync(CreateUserCommand request, IDbConnection conn, IDbTransaction tx)
        {
           // check if role exist
           var role = await _rolesQueryRepository.GetRoleByIdAsync(request.RoleId, conn, tx);

            if(role is null)
                return CqsResult.Failure(new CqsError { Message = "Role not found", StatusCode = 404 });

            var user = new User
            {
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                UserRef = await _userReferenceService.GenerateAsync(request.RoleId),
                MainRoleId = request.RoleId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var userId = await _userCommandRepository.CreateUserAsync(user, conn, tx);

            await _userRolesCommandRepository.AssignRoleToUserAsync(userId, request.RoleId, conn, tx);

            return CqsResult.Success("User created successfully");
        }
    }
}

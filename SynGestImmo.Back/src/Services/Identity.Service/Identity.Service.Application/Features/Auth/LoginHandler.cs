using Identity.Service.Application.Common;
using Identity.Service.Application.DTOS.Auth;
using Identity.Service.Application.Features.Jwt;
using Identity.Service.Domain.Entities;
using Identity.Service.Domain.Repositories.UserRepositories;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Domaine.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using Tools.Result;

namespace Identity.Service.Application.Features.Auth
{
    public class LoginHandler : IRequestHandler<LoginCommand, CqsResult<LoginResponseDto>>
    {
        private readonly IUserQueryRepository _userRepo;
        private readonly IUserRolesQueryRepository _rolesRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenCommandsRepository _refreshTokenRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISqlExceptionTranslator _sqlHandler;

        public LoginHandler(IUserQueryRepository userRepo, IUserRolesQueryRepository rolesRepo, IPasswordHasher passwordHasher, ITokenService tokenService, IRefreshTokenCommandsRepository refreshTokenRepo, IUnitOfWork unitOfWork,
            ISqlExceptionTranslator sqlExceptionTranslator)
        {
            _userRepo = userRepo;
            _rolesRepo = rolesRepo;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _refreshTokenRepo = refreshTokenRepo;
            _unitOfWork = unitOfWork;
            _sqlHandler = sqlExceptionTranslator;
        }

        public async Task<CqsResult<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            #region VALIDATIONS

            if (request == null)
                return CqsResult<LoginResponseDto>.Failure(Error.Validation("Request is require"));

            if (string.IsNullOrWhiteSpace(request.Email))
                return CqsResult<LoginResponseDto>.Failure(Error.Validation("Email is require"));

            if (string.IsNullOrWhiteSpace(request.Password))
                return CqsResult<LoginResponseDto>.Failure(Error.Validation("Password is require"));

            #endregion

            bool loggedIn = false;
            string accessToken = string.Empty;
            string refreshToken = string.Empty;
            bool mustChangePassword = false;

            User? user;
            IEnumerable<Role> roles;

            try
            {
                await _unitOfWork.ExecuteAsync(async (conn, tx) =>
                {

                    user = await _userRepo.GetUserByEmailAsync(request.Email, conn, tx);

                    if (user == null || !user.IsActive)
                        return;

                    if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                    {
                        return;
                    }

                    roles = await _rolesRepo.GetRolesOfUserIdAsync(user.Id, conn, tx);

                    accessToken = _tokenService.GenerateAccessToken(user, roles.Select(r => r.Name));
                    refreshToken = _tokenService.GenerateRefreshToken();
                    var refreshHash = _tokenService.HashToken(refreshToken);

                    await _refreshTokenRepo.AddAsync(new RefreshToken
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        TokenHash = refreshHash,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(7),
                        IsRevoked = false
                    }, conn, tx);

                    mustChangePassword = user.MustChangePassword;
                    loggedIn = true;

                });

                if (!loggedIn)
                {
                    return CqsResult<LoginResponseDto>.Failure(Error.Validation("Credentials error"));
                }
                return CqsResult<LoginResponseDto>.Success(new LoginResponseDto(accessToken, refreshToken, mustChangePassword));
            }
            catch (SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult<LoginResponseDto>.Failure(error);
            }
        }
    }
}

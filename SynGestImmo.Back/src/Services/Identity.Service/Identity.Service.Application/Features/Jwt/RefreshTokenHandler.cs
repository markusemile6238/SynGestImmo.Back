using Identity.Service.Application.Common;
using Identity.Service.Application.DTOS.Auth;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Domaine.Entities;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;
using RError = Tools.Result.Error;


namespace Identity.Service.Application.Features.Jwt
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, CqsResult<LoginResponseDto>>
    {

        private readonly IRefreshTokenQueriesRepository _queryRepo;
        private readonly IRefreshTokenCommandsRepository _commandRepo;
        private readonly IUserQueryRepository _userRepo;
        private readonly IUserRolesQueryRepository _rolesRepo;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISqlExceptionTranslator _sqlHandler;

        public RefreshTokenHandler(
            IRefreshTokenQueriesRepository queryRepo,
            IRefreshTokenCommandsRepository commandRepo,
            IUserQueryRepository userRepo,
            IUserRolesQueryRepository rolesRepo,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            ISqlExceptionTranslator sqlHandler
            )
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _userRepo = userRepo;
            _rolesRepo = rolesRepo;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _sqlHandler = sqlHandler;
        }

        public async Task<CqsResult<LoginResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            #region VALIDATION

            if (request == null)
                return CqsResult<LoginResponseDto>.Failure(RError.Validation("request cannot be null"));

            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return CqsResult<LoginResponseDto>.Failure(RError.Validation("RefreshToken cannot be null or empty"));

            #endregion

            try
            {

                string newAccessToken = string.Empty;
                string newRefreshToken = string.Empty;
                bool refreshed = false;
                bool mustChangePassword = false;


                await _unitOfWork.ExecuteAsync(async (conn, tx) =>
                {
                    var hash = _tokenService.HashToken(request.RefreshToken);
                    var token = await _queryRepo.GetByTokenHashAsync(hash, conn, tx);

                    if (token == null || token.IsRevoked || token.ExpiresAt <= DateTime.UtcNow)
                        return;

                    User? user = await _userRepo.GetUserByIdAsync(token.UserId, conn, tx);

                    if (user == null || !user.IsActive)
                        return;

                    var roles = await _rolesRepo.GetRolesOfUserIdAsync(token.UserId, conn, tx);

                    newAccessToken = _tokenService.GenerateAccessToken(user, roles.Select(x => x.Name));

                    newRefreshToken = _tokenService.GenerateRefreshToken();
                    var newRefreshHash = _tokenService.HashToken(newRefreshToken);

                    await _commandRepo.RevokeAsync(token.Id, conn, tx);

                    await _commandRepo.AddAsync(new Domain.Entities.RefreshToken
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        TokenHash = newRefreshHash,
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(7),
                        IsRevoked = false
                    }, conn, tx);

                    mustChangePassword = user.MustChangePassword;
                    refreshed = true;
                });
                if (!refreshed)
                    return CqsResult<LoginResponseDto>.Failure(Error.Unauthorized("Invalid refresh token"));

                return CqsResult<LoginResponseDto>.Success(new LoginResponseDto(newAccessToken, newRefreshToken, mustChangePassword)
                );
            }
            catch (SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult<LoginResponseDto>.Failure(error);
            }






        }
    }
}

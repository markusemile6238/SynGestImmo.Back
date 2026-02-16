using Identity.Service.Application.Common;
using Identity.Service.Application.DTOS.Auth;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Tools.Result;
using RError = Tools.Result.Error;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
        private readonly ILogger<RefreshTokenHandler> _logger;

        public RefreshTokenHandler(IRefreshTokenQueriesRepository queryRepo, IRefreshTokenCommandsRepository commandRepo, IUserQueryRepository userRepo, IUserRolesQueryRepository rolesRepo, ITokenService tokenService, IUnitOfWork unitOfWork, ISqlExceptionTranslator sqlHandler, ILogger<RefreshTokenHandler> logger)
        {
            _queryRepo = queryRepo;
            _commandRepo = commandRepo;
            _userRepo = userRepo;
            _rolesRepo = rolesRepo;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _sqlHandler = sqlHandler;
            _logger = logger;
        }

        public async Task<CqsResult<LoginResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            #region VALIDATION

            if(request == null)
                return CqsResult<LoginResponseDto>.Failure(RError.Validation("request cannot be null"));

            if(string.IsNullOrWhiteSpace(request.RefreshToken))
                return CqsResult<LoginResponseDto>.Failure(RError.Validation("RefreshToken cannot be null or empty"));

            #endregion

            var hash = _tokenService.HashToken(request.RefreshToken);
            var token = await _queryRepo.GetByTokenHashAsync(hash);

            if(token == null || token.IsRevoked || token.ExpiresAt<DateTime.UtcNow)
                return CqsResult<LoginResponseDto>.Failure(RError.Validation("Invalid refresh token"));

            var user = await _userRepo.GetUserByIdAsync(token.UserId);

            if(user == null || !user.IsActive)
                return CqsResult<LoginResponseDto>.Failure(RError.Validation("Invalid user"));

            var roles = await _rolesRepo.GetRolesOfUserIdAsync(token.UserId);

            var newAccessToken = _tokenService.GenerateAccessToken(user, roles.Select(x => x.Name));

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newRefreshHash = _tokenService.HashToken(newRefreshToken);

            await _unitOfWork.ExecuteAsync(async (conn, tx) =>
            {
                await _commandRepo.RevokeAsync(token.Id, conn, tx);

                await _commandRepo.AddAsync(new Domain.Entities.RefreshToken
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    TokenHash = newRefreshHash,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false
                },conn,tx);
            });

            return CqsResult<LoginResponseDto>.Success(
                 new LoginResponseDto(newAccessToken, newRefreshToken,user.MustChangePassword)
                );

        }
    }
}

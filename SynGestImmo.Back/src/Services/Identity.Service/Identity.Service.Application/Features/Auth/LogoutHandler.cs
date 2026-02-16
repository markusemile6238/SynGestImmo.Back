using Identity.Service.Application.Common;
using Identity.Service.Application.DTOS.Auth;
using Identity.Service.Application.Features.Jwt;
using Identity.Service.Domain.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Identity.Service.Application.Features.Auth
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, CqsResult<bool>>
    {
        private readonly ILogger<LogoutHandler> _logger;
        private readonly IRefreshTokenCommandsRepository _commandsRepository;
        private readonly IRefreshTokenQueriesRepository _queriesRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISqlExceptionTranslator _sqlExceptionTranslator;

        public LogoutHandler(ILogger<LogoutHandler> logger, IRefreshTokenCommandsRepository commandsRepository, IRefreshTokenQueriesRepository queriesRepository, ITokenService tokenService, IUnitOfWork unitOfWork, ISqlExceptionTranslator sqlExceptionTranslator)
        {
            _logger = logger;
            _commandsRepository = commandsRepository;
            _queriesRepository = queriesRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _sqlExceptionTranslator = sqlExceptionTranslator;
        }

        public async Task<CqsResult<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            #region VALIDATIONS

            if (request == null)
            {
                _logger.LogWarning("Revoke token failed: request is null");
                return CqsResult<bool>.Failure(Error.Validation("Invalid request"));
            }

            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                _logger.LogWarning("Revoke token failed: refreshtoken is null");
                return CqsResult<bool>.Failure(Error.Validation("Invalid request"));
            }


            #endregion

            var hashed = _tokenService.HashToken(request.RefreshToken);
            RefreshToken? token;

            try
            {
                await _unitOfWork.ExecuteAsync(async (conn, tx) =>
                {
                    token = await _queriesRepository.GetByTokenHashAsync(hashed, conn, tx);

                    if (token == null || token.IsRevoked || token.ExpiresAt <= DateTime.UtcNow)
                        return;

                    await _commandsRepository.RevokeAsync(token.Id, conn, tx);
                });

                return CqsResult<bool>.Success(true);

            }
            catch (SqlException ex)
            {
                var error = _sqlExceptionTranslator.Translate(ex);
                return CqsResult<bool>.Failure(error);
            }
        }
    }
}

using Identity.Service.Application.Features.Jwt;
using Identity.Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenCommandsRepository, IRefreshTokenQueriesRepository
    {
        private readonly IDapperConnection _connection;

        public RefreshTokenRepository(IDapperConnection connection)
        {
            _connection = connection;
        }



        #region COMMANDS

        public async Task AddAsync(RefreshToken token, IDbConnection conn, IDbTransaction tx)
        {
            const string sql = @"
                INSERT INTO RefreshTokens (Id, UserId, TokenHash, ExpiresAt, IsRevoked, CreatedAt)
                VALUES (@Id, @UserId, @TokenHash, @ExpiresAt, 0, @CreatedAt)";
            await conn.ExecuteAsync(sql, token,tx);
        }


        public async Task RevokeAsync(Guid tokenId, IDbConnection conn, IDbTransaction tx)
        {
            const string sql = @"
                UPDATE RefreshTokens 
                SET IsRevoked=1,RevokedAt=SYSUTCDATETIME() 
                WHERE Id=@TokenId";
            await conn.ExecuteAsync(sql, new { tokenId },tx);
        }

        #endregion


        #region QUERIES

        public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash,IDbConnection conn, IDbTransaction tx)
        {
            Console.WriteLine($"=====>{tokenHash}");
            const string query = @"
                    SELECT
                        Id,
                        UserId,
                        TokenHash,
                        ExpiresAt,
                        IsRevoked,
                        CreatedAt,
                        RevokedAt
                    FROM RefreshTokens 
                    WHERE TokenHash = @TokenHash
                    AND IsRevoked = 0 
                    AND ExpiresAt > SYSUTCDATETIME()";
            return await conn.QueryFirstOrDefaultAsync<RefreshToken>(query, new { TokenHash= tokenHash }, tx);
        }

        #endregion


    }
}

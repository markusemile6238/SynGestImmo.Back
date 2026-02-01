using Identity.Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.Features.Jwt
{
    public interface IRefreshTokenCommandsRepository
    {
        Task AddAsync(RefreshToken token, IDbConnection conn, IDbTransaction tx);
        Task RevokeAsync(Guid tokenId,IDbConnection conn, IDbTransaction tx);
    }
}

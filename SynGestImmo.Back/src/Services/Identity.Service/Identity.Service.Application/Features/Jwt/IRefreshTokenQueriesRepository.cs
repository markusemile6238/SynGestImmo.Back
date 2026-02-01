using Identity.Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.Features.Jwt
{
    public interface IRefreshTokenQueriesRepository
    {
        Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);
    }
}

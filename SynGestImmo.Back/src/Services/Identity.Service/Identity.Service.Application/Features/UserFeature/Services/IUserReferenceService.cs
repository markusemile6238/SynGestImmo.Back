using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.Features.UserFeature.Services
{
    public interface IUserReferenceService
    {
        Task<string> GenerateAsync(int roleId, int? year = null);

    }
}

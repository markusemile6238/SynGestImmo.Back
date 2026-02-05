using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.Common
{
    public interface ICurrentUserService
    {
        string Email { get; }
        string UserId { get; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.Auth
{
    public class LogoutCommand : IRequest<CqsResult<bool>>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}

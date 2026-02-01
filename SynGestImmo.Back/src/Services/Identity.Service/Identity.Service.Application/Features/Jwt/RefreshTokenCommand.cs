using Identity.Service.Application.DTOS.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.Jwt
{
    public class RefreshTokenCommand : IRequest<CqsResult<LoginResponseDto>>
    {
        public string RefreshToken { get; set; }
    }
}

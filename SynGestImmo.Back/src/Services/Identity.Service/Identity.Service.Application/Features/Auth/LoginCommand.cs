using Identity.Service.Application.DTOS.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.Auth
{
    public class LoginCommand : IRequest<CqsResult<LoginResponseDto>>
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;


    }
}

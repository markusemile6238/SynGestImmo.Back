using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.Auth
{
    public class ChangePasswordCommand : IRequest<CqsResult<bool>>
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool PasswordIsConfirmed()
        {
            return NewPassword == ConfirmPassword;
        }

    }
}

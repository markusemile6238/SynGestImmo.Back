using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.DTOS.Auth
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool MustChangePassword { get; set; }

        public LoginResponseDto(string accessToken, string refreshToken, bool mustChangePassword)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            MustChangePassword = mustChangePassword;
        }
    }
}

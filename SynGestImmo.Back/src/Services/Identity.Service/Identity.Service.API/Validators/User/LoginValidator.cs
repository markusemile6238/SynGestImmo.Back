using FluentValidation;
using Identity.Service.Application.DTOS.Auth;

namespace Identity.Service.API.Validators.User
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator() { 

        RuleFor(x=>x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("8 characters minimum")
                .Matches(@"[A-Z]").WithMessage("Password must content  at least one Uppercase letter")
                .Matches(@"[@!#\/\?_-]").WithMessage("Password must content  at least one of this symbol (@!#\\/?_-)");
        }
    }
}

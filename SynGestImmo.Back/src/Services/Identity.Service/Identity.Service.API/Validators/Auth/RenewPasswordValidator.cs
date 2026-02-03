using FluentValidation;
using Identity.Service.Application.DTOS.Auth;

namespace Identity.Service.API.Validators.Auth
{
    public class RenewPasswordValidator : AbstractValidator<RenewPasswordDto>
    {
        public RenewPasswordValidator() 
        {

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.CurrentPassword)
               .NotEmpty().WithMessage("Current password is required");

            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage("Password is required")
               .MinimumLength(8).WithMessage("8 characters minimum")
               .Matches(@"[A-Z]").WithMessage("Password must content  at least one Uppercase letter")
               .Matches(@"[@!#\/\?_-]").WithMessage("Password must content  at least one of this symbol (@!#\\/?_-)");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("The new password and confirmation password do not match.");

        }
    
    }
}

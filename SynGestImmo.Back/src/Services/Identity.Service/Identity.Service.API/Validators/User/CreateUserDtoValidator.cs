using FluentValidation;
using Identity.Service.Application.DTOS.UserDtos;
using System.Data;

namespace Identity.Service.API.Validators.User
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("8 characters minimum")
                .Matches(@"[A-Z]").WithMessage("Password must content  at least one Uppercase letter")
                .Matches(@"[@!#\/\?_-]").WithMessage("Password must content  at least one of this symbol (@!#\\/?_-)");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required")
                .GreaterThan(0);

        }
    }
}

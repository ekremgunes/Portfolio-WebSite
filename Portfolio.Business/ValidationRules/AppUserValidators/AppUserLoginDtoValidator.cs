using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules.AppUserValidators
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(x=>x.Password).NotEmpty().WithMessage("Password can't be empty");
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Password can't be empty");
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("password too short");
        }
    }
}

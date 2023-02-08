using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules.SettingsValidators
{
    public class SettingsUpdateDtoValidator : AbstractValidator<SettingsUpdateDto>
    {
        public SettingsUpdateDtoValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("data id not found");
            

            RuleFor(x => x.aboutContent).NotEmpty().WithMessage("about content can't be empty");
            RuleFor(x => x.aboutHeader).NotEmpty().WithMessage("about header can't be empty");
            RuleFor(x => x.aboutImgPath).NotEmpty().WithMessage("about image can't be empty");

            RuleFor(x => x.sliderHeader).NotEmpty().WithMessage("slider header can't be empty");
            RuleFor(x => x.sliderServices).NotEmpty().WithMessage("slider services can't be empty");
            RuleFor(x => x.sliderImgPath).NotEmpty().WithMessage("slider image can't be empty");

            RuleFor(x => x.facebookUrl).NotEmpty().WithMessage("facebook url can't be empty");
            RuleFor(x => x.twitterUrl).NotEmpty().WithMessage("twitter url can't be empty");
            RuleFor(x => x.instagramUrl).NotEmpty().WithMessage("insatgram url can't be empty");

            RuleFor(x => x.slogan).NotEmpty().WithMessage("slogan area can't be empty");
            RuleFor(x => x.slogan).MinimumLength(5).WithMessage("slogan is too short! (5)");
            RuleFor(x => x.email).NotEmpty().WithMessage("email min length 5");
            RuleFor(x => x.email).MinimumLength(5).WithMessage("Email minimum length 5");
            RuleFor(x => x.email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Use a real email adress");
            RuleFor(x => x.logoImgPath).NotEmpty().WithMessage("Logo Image can't be empty");

        }
    }
}

using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules
{
    public class ContentDetailUpdateDtoValidator : AbstractValidator<ContentDetailUpdateDto>
    {
        public ContentDetailUpdateDtoValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("data id not found");
            RuleFor(x => x.contentDetailName).NotEmpty().WithMessage("Content detail name can't be empty");
            RuleFor(x => x.imagePath).NotEmpty().WithMessage("Content detail image  can't be empty");
            RuleFor(x => x.contentId).NotEmpty().WithMessage("Content can't be empty");
            RuleFor(x => x.rank).LessThan((short)101).WithMessage("Rank max value is 100");
            RuleFor(x => x.rank).GreaterThan((short)0).WithMessage("Rank min value is 1");
        }
    }
}

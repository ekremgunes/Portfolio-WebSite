using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules
{
    public class ContentUpdateDtoValidator : AbstractValidator<ContentUpdateDto>
    {
        public ContentUpdateDtoValidator()
        {
            RuleFor(x=>x.id).NotEmpty().WithMessage("data id not found");
            RuleFor(x => x.contentName).NotEmpty().WithMessage("Content name can't be empty");
            RuleFor(x => x.imagePath).NotEmpty().WithMessage("Content image  can't be empty");
            RuleFor(x => x.rank).LessThan((short)101).WithMessage("Rank max value is 100");
            RuleFor(x => x.rank).GreaterThan((short)-1).WithMessage("Rank min value is 0");
        }
    }
}

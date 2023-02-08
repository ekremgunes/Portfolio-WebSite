using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules
{
    public class ContentCreateDtoValidator : AbstractValidator<ContentCreateDto>
    {
        public ContentCreateDtoValidator()
        {
            RuleFor(x => x.contentName).NotEmpty().WithMessage("Content name can't be empty");
            RuleFor(x => x.imagePath).NotEmpty().WithMessage("Content image  can't be empty");
            RuleFor(x => x.rank).LessThan((short)101).WithMessage("Rank max value is 100");
            RuleFor(x => x.rank).GreaterThan((short)0).WithMessage("Rank min value is 1");
        }
    }
}   

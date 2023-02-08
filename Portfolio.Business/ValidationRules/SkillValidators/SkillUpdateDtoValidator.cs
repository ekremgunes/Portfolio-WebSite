using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules
{
    public class SkillUpdateDtoValidator : AbstractValidator<SkillUpdateDto>
    {
        public SkillUpdateDtoValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("data id not found");
            RuleFor(x => x.skillName).NotEmpty().WithMessage("Skill name can't be empty");
            RuleFor(x => x.rank).LessThan((short)101).WithMessage("Rank max value is 100");
            RuleFor(x => x.rank).GreaterThan((short)0).WithMessage("Rank min value is 1");
            RuleFor(x => x.skillPercent).NotEmpty().WithMessage("Skill Percent can't be empty");
            RuleFor(x => x.skillPercent).LessThan((short)101).WithMessage("Skill max value is 100");
            RuleFor(x => x.skillPercent).GreaterThan((short)0).WithMessage("Skill min value is 1");
        }
    }
}

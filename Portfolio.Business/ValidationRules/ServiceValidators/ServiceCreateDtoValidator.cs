using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules.ServiceValidators
{
    public class ServiceCreateDtoValidator : AbstractValidator<ServiceCreateDto>
    {
        public ServiceCreateDtoValidator()
        {
            RuleFor(x => x.serviceName).NotEmpty().WithMessage("Service name can't be empty");
            RuleFor(x => x.rank).LessThan((short)101).WithMessage("Rank max value is 100");
            RuleFor(x => x.rank).GreaterThan((short)0).WithMessage("Rank min value is 1");
        }
    }
}

using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules
{
    public class MessageCreatoDtoValidator : AbstractValidator<MessageCreateDto>
    {
        public MessageCreatoDtoValidator()
        {
            RuleFor(x => x.senderMail).NotEmpty().WithMessage("Email can't be empty");
            RuleFor(x => x.senderName).NotEmpty().WithMessage("Name  can't be empty");
            RuleFor(x => x.messageContent).NotEmpty().WithMessage("Message can't be empty");
            RuleFor(x => x.messageContent).MinimumLength(15).WithMessage("Message minimum length 15");
            RuleFor(x => x.messageContent).MaximumLength(350).WithMessage("Message maximum length 350");
            RuleFor(x => x.senderName).MinimumLength(5).WithMessage("Name minimum length 5");
            RuleFor(x => x.senderMail).MinimumLength(5).WithMessage("Email minimum length 5");
            RuleFor(x => x.senderMail)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Use a real email adress");
        }
    }
}

using FluentValidation;
using Portfolio.Dtos;

namespace Portfolio.Business.ValidationRules
{
    public class ImageCreateDtoValidator : AbstractValidator<ImageCreateDto>
    {
        public ImageCreateDtoValidator()
        {
            RuleFor(x=>x.imgPath).NotEmpty().WithMessage("Image can't be empty");
            RuleFor(x => x.contentDetailId).NotEmpty().WithMessage("Image must be have a Content Detail");
        }
    }
}

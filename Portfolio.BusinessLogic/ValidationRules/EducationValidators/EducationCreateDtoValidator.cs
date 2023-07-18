using FluentValidation;
using Portfolio.BusinessLogic.DTOs.EducationDTOs;

namespace Portfolio.BusinessLogic.ValidationRules.EducationValidators
{
    public class EducationCreateDtoValidator : AbstractValidator<EducationCreateDTO>
    {
        public EducationCreateDtoValidator()
        {
            RuleFor(x => x.LevelOfEducation)
                .NotEmpty().WithMessage("Level of Education required");
            RuleFor(x => x.Institution)
                .NotEmpty().WithMessage("Institution required");
            RuleFor(x => x.FieldOfStudy)
                .NotEmpty().WithMessage("Field of Study required");
            RuleFor(x => x.Session)
                .NotEmpty().WithMessage("Session required");
        }
    }
}

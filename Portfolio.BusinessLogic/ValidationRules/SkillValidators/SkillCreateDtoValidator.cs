using FluentValidation;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;

namespace Portfolio.BusinessLogic.ValidationRules.SkillValidators
{
	public class SkillCreateDtoValidator : AbstractValidator<SkillCreateDTO>
	{
		public SkillCreateDtoValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Skill name required");
		}
	}
}

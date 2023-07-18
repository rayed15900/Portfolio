using FluentValidation;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;

namespace Portfolio.BusinessLogic.ValidationRules.SkillValidators
{
	public class SkillUpdateDtoValidator : AbstractValidator<SkillUpdateDTO>
	{
		public SkillUpdateDtoValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Skill name required");
		}
	}
}

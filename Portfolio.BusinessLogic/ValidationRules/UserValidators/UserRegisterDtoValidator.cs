using FluentValidation;
using Portfolio.BusinessLogic.DTOs.UserDTO;

namespace Portfolio.BusinessLogic.ValidationRules.UserValidators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
			RuleFor(x => x.Email)
	            .NotEmpty().WithMessage("Email required")
	            .EmailAddress().WithMessage("Invalid email format");
			RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password required")
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long");
            RuleFor(x => x.ConfirmPassword)
				.NotEmpty().WithMessage("Confrim Password required")
				.Equal(x => x.Password).WithMessage("Passwords need to match");
        }
    }
}

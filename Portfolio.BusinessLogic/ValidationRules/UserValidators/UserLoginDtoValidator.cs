using FluentValidation;
using Portfolio.BusinessLogic.DTOs.UserDTO;

namespace Portfolio.BusinessLogic.ValidationRules.UserValidators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password required");
            RuleFor(x => x.Password)
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long");
        }
    }
}

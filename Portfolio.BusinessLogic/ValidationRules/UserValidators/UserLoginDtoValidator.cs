using FluentValidation;
using Portfolio.BusinessLogic.DTOs.UserDTO;

namespace Portfolio.BusinessLogic.ValidationRules.UserValidators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(x => x.Password).MinimumLength(4).WithMessage("Password too short");
        }
    }
}

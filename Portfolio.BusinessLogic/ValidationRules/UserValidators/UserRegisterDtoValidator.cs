using FluentValidation;
using Portfolio.BusinessLogic.DTOs.UserDTO;

namespace Portfolio.BusinessLogic.ValidationRules.UserValidators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(x => x.Password).MinimumLength(4).WithMessage("Password too short");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}

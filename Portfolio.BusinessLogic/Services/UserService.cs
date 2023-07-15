using FluentValidation;
using Portfolio.BusinessLogic.DTOs.UserDTO;
using Portfolio.BusinessLogic.Interfaces;

namespace Portfolio.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IValidator<UserLoginDTO> _loginValidator;
        private readonly IValidator<UserRegisterDTO> _registerValidator;

        public UserService(IValidator<UserLoginDTO> loginValidator, IValidator<UserRegisterDTO> registerValidator)
        {
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        public bool LoginisValid(UserLoginDTO dto) => _loginValidator.Validate(dto).IsValid;

        public bool RegisterisValid(UserRegisterDTO dto) => _registerValidator.Validate(dto).IsValid;
    }
}

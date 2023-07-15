using Portfolio.BusinessLogic.DTOs.UserDTO;

namespace Portfolio.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        bool LoginisValid(UserLoginDTO dto);
        bool RegisterisValid(UserRegisterDTO dto);
    }
}

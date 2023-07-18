using Portfolio.BusinessLogic.DTOs.EducationDTOs;
using Portfolio.BusinessLogic.Interfaces.Base;
using Portfolio.Models;

namespace Portfolio.BusinessLogic.Interfaces
{
    public interface IEducationService : IService<EducationCreateDTO, EducationUpdateDTO, EducationListDTO, Education>
    {
        Task<List<ListDTO>> GetAllEducationsByUserIdAsync<ListDTO>(string userId);
    }
}

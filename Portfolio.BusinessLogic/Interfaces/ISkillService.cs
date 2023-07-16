using Portfolio.BusinessLogic.DTOs.SkillDTOs;
using Portfolio.BusinessLogic.Interfaces.Base;
using Portfolio.Models;

namespace Portfolio.BusinessLogic.Interfaces
{
	public interface ISkillService : IService<SkillCreateDTO, SkillUpdateDTO, SkillListDTO, Skill>
	{
        Task<List<ListDTO>> GetAllSkillsByUserIdAsync<ListDTO>(string userId);
    }
}

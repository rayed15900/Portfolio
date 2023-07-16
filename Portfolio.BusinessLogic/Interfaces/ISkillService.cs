using Portfolio.BusinessLogic.DTOs.SkillDTOs;
using Portfolio.BusinessLogic.Interfaces.Base;
using Portfolio.Models;
using Portfolio.Utility;

namespace Portfolio.BusinessLogic.Interfaces
{
	public interface ISkillService : IService<SkillCreateDTO, SkillUpdateDTO, SkillListDTO, Skill>
	{
		// Task<IResponse<List<SkillListDTO>>> GetAllSkillAsync();
	}
}

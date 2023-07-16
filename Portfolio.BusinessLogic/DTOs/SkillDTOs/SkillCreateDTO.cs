using Portfolio.BusinessLogic.DTOs.Interfaces;

namespace Portfolio.BusinessLogic.DTOs.SkillDTOs
{
	public class SkillCreateDTO : IDTO
	{
		public string Name { get; set; }
		public string UserId { get; set; }
    }
}

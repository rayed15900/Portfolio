using Portfolio.BusinessLogic.DTOs.Interfaces;

namespace Portfolio.BusinessLogic.DTOs.SkillDTOs
{
	public class SkillUpdateDTO : IUpdateDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}

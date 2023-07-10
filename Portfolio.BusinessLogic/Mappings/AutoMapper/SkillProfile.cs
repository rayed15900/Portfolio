using AutoMapper;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;
using Portfolio.Models;

namespace Portfolio.BusinessLogic.Mappings.AutoMapper
{
	public class SkillProfile : Profile
	{
		public SkillProfile()
		{
			CreateMap<Skill, SkillCreateDTO>().ReverseMap();
			CreateMap<Skill, SkillUpdateDTO>().ReverseMap();
			CreateMap<Skill, SkillListDTO>().ReverseMap();
		}
	}
}

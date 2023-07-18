using AutoMapper;
using FluentValidation;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.BusinessLogic.Services.Base;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Models;

namespace Portfolio.BusinessLogic.Services
{
	public class SkillService : Service<SkillCreateDTO, SkillUpdateDTO, SkillListDTO, Skill>, ISkillService
	{
		private readonly IMapper _mapper;
		private readonly IUOW _uow;
		public SkillService(IUOW uow, IMapper mapper) : base(mapper, uow)
		{
			_uow = uow;
			_mapper = mapper;
		}

        public async Task<List<ListDTO>> GetAllSkillsByUserIdAsync<ListDTO>(string userId)
        {
            var allSkills = await _uow.GetRepository<Skill>().GetAllAsync();
            var skillsByUserId = allSkills.Where(skill => skill.UserId == userId).ToList();
            var dtoList = _mapper.Map<List<ListDTO>>(skillsByUserId);
            return dtoList;
        }
    }
}

using AutoMapper;
using FluentValidation;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.BusinessLogic.Services.Base;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Models;
using Portfolio.Utility;

namespace Portfolio.BusinessLogic.Services
{
	public class SkillService : Service<SkillCreateDTO, SkillUpdateDTO, SkillListDTO, Skill>, ISkillService
	{
		private readonly IMapper _mapper;
		private readonly IUOW _uow;
		public SkillService(IUOW uow, IMapper mapper, IValidator<SkillCreateDTO> createDtoValidator, IValidator<SkillUpdateDTO> updateDtoValidator) : base(mapper, uow, createDtoValidator, updateDtoValidator)
		{
			_uow = uow;
			_mapper = mapper;
		}

		public async Task<IResponse<List<SkillListDTO>>> GetAllSkillAsync()
		{
			var data = await _uow.GetRepository<Skill>().GetAllAsync();
			var skills = _mapper.Map<List<SkillListDTO>>(data);
			return new Response<List<SkillListDTO>>(ResponseType.Success, skills);
		}
	}
}

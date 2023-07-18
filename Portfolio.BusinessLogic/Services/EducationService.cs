using AutoMapper;
using FluentValidation;
using Portfolio.BusinessLogic.DTOs.EducationDTOs;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.BusinessLogic.Services.Base;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Models;

namespace Portfolio.BusinessLogic.Services
{
    public class EducationService : Service<EducationCreateDTO, EducationUpdateDTO, EducationListDTO, Education>, IEducationService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;
        public EducationService(IUOW uow, IMapper mapper) : base(mapper, uow)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ListDTO>> GetAllEducationsByUserIdAsync<ListDTO>(string userId)
        {
            var allEducations = await _uow.GetRepository<Education>().GetAllAsync();
            var EducationsByUserId = allEducations.Where(education => education.UserId == userId).ToList();
            var dtoList = _mapper.Map<List<ListDTO>>(EducationsByUserId);
            return dtoList;
        }
    }
}

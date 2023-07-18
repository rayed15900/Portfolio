using AutoMapper;
using Portfolio.BusinessLogic.DTOs.EducationDTOs;
using Portfolio.Models;

namespace Portfolio.BusinessLogic.Mappings.AutoMapper
{
    public class EducationProfile : Profile
    {
        public EducationProfile()
        {
            CreateMap<Education, EducationCreateDTO>().ReverseMap();
            CreateMap<Education, EducationListDTO>().ReverseMap();
            CreateMap<Education, EducationUpdateDTO>().ReverseMap();
        }
    }
}

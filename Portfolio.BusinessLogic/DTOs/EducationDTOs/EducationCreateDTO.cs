using Portfolio.BusinessLogic.DTOs.Interfaces;

namespace Portfolio.BusinessLogic.DTOs.EducationDTOs
{
    public class EducationCreateDTO : IDTO
    {
        public string LevelOfEducation { get; set; }
        public string Institution { get; set; }
        public string FieldOfStudy { get; set; }
        public string Session { get; set; }

        public string UserId { get; set; }
    }
}

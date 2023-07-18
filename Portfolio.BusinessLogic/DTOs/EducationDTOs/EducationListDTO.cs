using Portfolio.BusinessLogic.DTOs.Interfaces;

namespace Portfolio.BusinessLogic.DTOs.EducationDTOs
{
    public class EducationListDTO : IDTO
    {
        public int Id { get; set; }
        public string LevelOfEducation { get; set; }
        public string Institution { get; set; }
        public string FieldOfStudy { get; set; }
        public string Session { get; set; }
    }
}

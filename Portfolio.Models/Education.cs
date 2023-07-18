using Portfolio.Models.Base;

namespace Portfolio.Models
{
    public class Education : BaseModel
    {
        public string LevelOfEducation { get; set; }
        public string Institution { get; set; }
        public string FieldOfStudy { get; set; }
        public string Session { get; set; }

        public string UserId { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Models;

namespace Portfolio.DataAccess.Configurations
{
    public class EducationConfigurations : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LevelOfEducation)
                .IsRequired();
            builder.Property(x => x.Institution)
                .IsRequired();
            builder.Property(x => x.FieldOfStudy)
                .IsRequired();
            builder.Property(x => x.Session)
                .IsRequired();
        }
    }
}

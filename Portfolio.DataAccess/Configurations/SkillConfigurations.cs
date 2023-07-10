using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Models;

namespace Portfolio.DataAccess.Configurations
{
	public class SkillConfigurations : IEntityTypeConfiguration<Skill>
	{
		public void Configure(EntityTypeBuilder<Skill> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.ProgramingLanguages)
				.IsRequired();
			builder.Property(x => x.Databases)
				.IsRequired();
			builder.Property(x => x.FrameworksAndPlatforms)
				.IsRequired();
			builder.Property(x => x.Tools)
				.IsRequired();
			builder.Property(x => x.SoftwareArchitecturePatterns)
				.IsRequired();
		}
	}
}

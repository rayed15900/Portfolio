﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Models;

namespace Portfolio.DataAccess.Configurations
{
	public class SkillConfigurations : IEntityTypeConfiguration<Skill>
	{
		public void Configure(EntityTypeBuilder<Skill> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name)
				.IsRequired();
		}
	}
}

using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Configurations;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Context
{
	public class PortfolioContext : DbContext
	{
		public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new SkillConfigurations());
		}

		public DbSet<Skill> Skills { get; set; }
	}
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Configurations;
using Portfolio.Models;

namespace Portfolio.DataAccess.Context
{
	public class PortfolioContext : IdentityDbContext<User>
    {
		public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
		{
		}

        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new SkillConfigurations());

            modelBuilder.Entity<User>()
                .HasMany(u => u.Skills)
                .WithOne()
                .HasForeignKey(s => s.UserId)
                .IsRequired();
        }
	}
}

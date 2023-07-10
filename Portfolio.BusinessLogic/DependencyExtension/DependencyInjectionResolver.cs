using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.DataAccess.Context;
using Portfolio.DataAccess.UnitOfWork;

namespace Portfolio.BusinessLogic.DependencyExtension
{
	public static class DependencyInjectionResolver
	{
		public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<PortfolioContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("MsSql"));
			});

			//UnitOfWork
			services.AddScoped<IUOW, UOW>();
		}
	}
}

﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.BusinessLogic.DTOs.EducationDTOs;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;
using Portfolio.BusinessLogic.DTOs.UserDTO;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.BusinessLogic.Services;
using Portfolio.BusinessLogic.ValidationRules.EducationValidators;
using Portfolio.BusinessLogic.ValidationRules.SkillValidators;
using Portfolio.BusinessLogic.ValidationRules.UserValidators;
using Portfolio.DataAccess.Context;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Models;

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

			// UnitOfWork
			services.AddScoped<IUOW, UOW>();

            // Services
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IEducationService, EducationService>();

            // Validators
            services.AddTransient<IValidator<UserLoginDTO>, UserLoginDtoValidator>();
            services.AddTransient<IValidator<UserRegisterDTO>, UserRegisterDtoValidator>();

            services.AddTransient<IValidator<SkillCreateDTO>, SkillCreateDtoValidator>();
            services.AddTransient<IValidator<SkillUpdateDTO>, SkillUpdateDtoValidator>();

            services.AddTransient<IValidator<EducationCreateDTO>, EducationCreateDtoValidator>();
            services.AddTransient<IValidator<EducationUpdateDTO>, EducationUpdateDtoValidator>();

            #region Identity

            services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/Login";
				options.LogoutPath = "/Account/Logout";
				options.SlidingExpiration = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				options.Cookie = new CookieBuilder
				{
					Name = "AuthCookie",
					HttpOnly = true
				};
			});

			services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            });

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<PortfolioContext>().AddDefaultTokenProviders();

            #endregion
        }
    }
}

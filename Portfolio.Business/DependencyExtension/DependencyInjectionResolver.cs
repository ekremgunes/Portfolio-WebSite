using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Business.Interfaces;
using Portfolio.Business.Services;
using Portfolio.Business.ValidationRules;
using Portfolio.Business.ValidationRules.AppUserValidators;
using Portfolio.Business.ValidationRules.ServiceValidators;
using Portfolio.Business.ValidationRules.SettingsValidators;
using Portfolio.DataAccess.Context;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.DependencyExtension
{
    public static class DependencyInjectionResolver
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PortfolioContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("MsSql"));
            });

            //identity
            services.AddDbContext<AuthContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("MsSql"));
            });
            services.Configure<IdentityOptions>(options =>
            {
                // password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                
                // Lockout                
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".lisana.Security.Cookie"
                };
            });

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AuthContext>().AddDefaultTokenProviders();


            //uow
            services.AddScoped<IUow, Uow>();

            //services
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IContentDetailService, ContentDetailService>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IServicesService, ServicesService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IAppUserService, AppUserService>();

            //validators
            services.AddTransient<IValidator<ContentCreateDto>, ContentCreateDtoValidator>();
            services.AddTransient<IValidator<ContentUpdateDto>, ContentUpdateDtoValidator>();

            services.AddTransient<IValidator<ContentDetailCreateDto>, ContentDetailCreateDtoValidator>();
            services.AddTransient<IValidator<ContentDetailUpdateDto>, ContentDetailUpdateDtoValidator>();

            services.AddTransient<IValidator<ImageCreateDto>, ImageCreateDtoValidator>();
            
            services.AddTransient<IValidator<MessageCreateDto>, MessageCreatoDtoValidator>();
            
            services.AddTransient<IValidator<ServiceCreateDto>, ServiceCreateDtoValidator>();
            services.AddTransient<IValidator<ServiceUpdateDto>, ServiceUpdateDtoValidator>();
            
            services.AddTransient<IValidator<SettingsUpdateDto>, SettingsUpdateDtoValidator>();
            
            services.AddTransient<IValidator<SkillCreateDto>, SkillCreateDtoValidator>();
            services.AddTransient<IValidator<SkillUpdateDto>,SkillUpdateDtoValidator>();

            services.AddTransient<IValidator<AppUserLoginDto>,AppUserLoginDtoValidator>();
            services.AddTransient<IValidator<AppUserRegisterDto>,AppUserRegisterDtoValidator>();



        }
    }
}

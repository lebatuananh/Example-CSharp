using System;

namespace IdentityFrameworkExample
{
    using System.Text;

    using AutoMapper;

    using IdentityFrameworkExample.Entity;
    using IdentityFrameworkExample.Entity.EF;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection=new ServiceCollection();
       
            serviceCollection.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            serviceCollection.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                });
            serviceCollection.AddTransient<DbInitialize>();
            serviceCollection.AddAutoMapper();
            serviceCollection.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            //Register For DI
            serviceCollection.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            serviceCollection.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            serviceCollection.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
            //Add application services
            serviceCollection.AddSingleton(Mapper.Configuration);
            serviceCollection.AddScoped<IMapper>(
                sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
            

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}

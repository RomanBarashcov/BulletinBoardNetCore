using AppleUsed.BLL;
using AppleUsed.BLL.Interfaces;
using AppleUsed.BLL.Services;
using AppleUsed.DAL;
using AppleUsed.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AppleUsed.Web
{
    public static class ConfigureContainerExtensions
    {
        public static void AddDbContext(this IServiceCollection serviceCollection,
            string dataConnectionString = null, string authConnectionString = null)
        {
            serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(dataConnectionString ?? GetDataConnectionStringFromConfig()));

            serviceCollection.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }

        //public static void AddRepository(this IServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddScoped(typeof(IUnitOfWork), typeof(DataRepository<>));
        //}

        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmailSender, EmailSender>();
        }

        /// <summary>
        /// Adds rules to the <see cref="RazorViewEngineOptions"/> for dealing with Feature Folders
        /// </summary>
        /// <param name="serviceCollection">
        /// The <see cref="IServiceCollection"/> created in <see cref="Startup.ConfigureServices"/>
        /// </param>
        public static void AddFeatureFolders(this IServiceCollection serviceCollection)
        {
            serviceCollection.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });
        }
        
        private static string GetDataConnectionStringFromConfig()
        {
            return new DatabaseConfiguration().GetDataConnectionString();
        }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppleUsed.Repo;
using AppleUsed.Repo.Data;
using AppleUsed.Repo.Identity;
using AppleUsed.Service;
using AppleUsed.Service.Interfaces;

namespace AppleUsed.Web
{
    public static class ConfigureContainerExtensions
    {
        public static void AddDbContext(this IServiceCollection serviceCollection,
            string dataConnectionString = null, string authConnectionString = null)
        {
            serviceCollection.AddDbContext<DataContext>(options =>
                options.UseSqlite(dataConnectionString ?? GetDataConnectionStringFromConfig()));

            serviceCollection.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite(authConnectionString ?? GetAuthConnectionFromConfig()));

            serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
        }
        
        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(DataRepository<>));
        }

        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBookService, BookService>();

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

        private static string GetAuthConnectionFromConfig()
        {
            return new DatabaseConfiguration().GetAuthConnectionString();
        }
    }
}
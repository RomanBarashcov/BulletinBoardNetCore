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
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(GetDataConnectionStringFromConfig());
            var dbContext = new AppDbContext(optionsBuilder.Options);

            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            serviceCollection.AddTransient<IImageCompressorService, ImageCompressorService>();
            serviceCollection.AddTransient<IImageService>(
                s => new ImageService(new ImageCompressorService()));

            serviceCollection.AddTransient<IDataService>(
                s => new DataService(dbContext));

            serviceCollection.AddTransient<ISeedService>(
                s => new SeedService(dbContext));

            serviceCollection.AddTransient<ICityAreasService>(
                s => new CityAreasService(dbContext));

            serviceCollection.AddTransient<ICityService>(
                s => new CityService(dbContext));

            serviceCollection.AddTransient<IProductModelsService>(
                s => new ProductModelService(dbContext));

            serviceCollection.AddTransient<IAdService>(
                s => new AdService(dbContext,
                new DataService(dbContext), 
                new ImageService(new ImageCompressorService()), 
                new ConversationService(dbContext),
                new CityAreasService(dbContext),
                new CityService(dbContext),
                new ProductModelService(dbContext),
                new AdUpService(dbContext)));

            serviceCollection.AddTransient<IAdUpService>(
                s => new AdUpService(dbContext));

            serviceCollection.AddTransient<IAdViewsService>(
                s => new AdViewsService(dbContext));

            serviceCollection.AddTransient<IConversationService>(
                s => new ConversationService(new AppDbContext(optionsBuilder.Options)));

            serviceCollection.AddTransient<IPurchasesService>(
               s => new PurchasesService(new AppDbContext(optionsBuilder.Options),
                    new AdService(dbContext,
                    new DataService(dbContext),
                    new ImageService(new ImageCompressorService()),
                    new ConversationService(dbContext),
                    new CityAreasService(dbContext),
                    new CityService(dbContext),
                    new ProductModelService(dbContext),
                    new AdUpService(dbContext))));

            serviceCollection.AddTransient<IServiecActiveTimeService>(
                s => new ServiecActiveTimeService(dbContext));

            serviceCollection.AddTransient<IServicesService>(
                s => new ServicesService(dbContext, new ServiecActiveTimeService(dbContext)));
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
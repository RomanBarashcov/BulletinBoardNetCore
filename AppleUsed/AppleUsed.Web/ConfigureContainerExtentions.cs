using AppleUsed.BLL;
using AppleUsed.BLL.Interfaces;
using AppleUsed.BLL.Services;
using AppleUsed.DAL;
using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using AppleUsed.DAL.Repositories;
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

        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(GetDataConnectionStringFromConfig());
            var dbContext = new AppDbContext(optionsBuilder.Options);

            serviceCollection.AddTransient<IUnityOfWork>(
                r => new UnityOfWork(
                     new AdRepository(dbContext),
                     new AdPhotoRepository(dbContext),
                     new AdUpRepository(dbContext),
                     new AdViewsRepository(dbContext),
                     new CityAreasRepository(dbContext),
                     new CityRepository(dbContext),
                     new ProductTypeRepository(dbContext),
                     new ProductModelRepository(dbContext),
                     new ProductMemoriesRepository(dbContext),
                     new ProductColorsRepository(dbContext),
                     new ProductStatesRepository(dbContext),
                     new PurchaseRepository(dbContext),
                     new ServiceActiveTimeRepository(dbContext),
                     new ServiceRepository(dbContext),
                     new UserRepository(dbContext)));

            serviceCollection.AddTransient<IAdRepository>( 
                r => new AdRepository(dbContext));

            serviceCollection.AddTransient<IAdPhotoRepository>(
                r => new AdPhotoRepository(dbContext));

            serviceCollection.AddTransient<IAdUpRepository>( 
                r => new AdUpRepository(dbContext));

            serviceCollection.AddTransient<IAdViewsRepository>(
                r => new AdViewsRepository(dbContext));

            serviceCollection.AddTransient<ICityAreasRepository>(
                r => new CityAreasRepository(dbContext));

            serviceCollection.AddTransient<ICityRepository>(
                r => new CityRepository(dbContext));

            serviceCollection.AddTransient<IProductTypeRepository>(
                r => new ProductTypeRepository(dbContext));

            serviceCollection.AddTransient<IProductModelRepository>(
                r => new ProductModelRepository(dbContext));

            serviceCollection.AddTransient<IProductMemoriesRepository>(
                r => new ProductMemoriesRepository(dbContext));

            serviceCollection.AddTransient<IProductColorsRepository>(
                r => new ProductColorsRepository(dbContext));

            serviceCollection.AddTransient<IProductStatesRepository>(
                r => new ProductStatesRepository(dbContext));

            serviceCollection.AddTransient<IPurchaseRepository>(
                r => new PurchaseRepository(dbContext));

            serviceCollection.AddTransient<IServiceActiveTimeRepository>(
                r => new ServiceActiveTimeRepository(dbContext));

            serviceCollection.AddTransient<IServiceRepository>(
                r => new ServiceRepository(dbContext));

            serviceCollection.AddTransient<IUserRepository>( 
                r => new UserRepository(dbContext));

        }

        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(GetDataConnectionStringFromConfig());
            var dbContext = new AppDbContext(optionsBuilder.Options);
            var unitOfWork = new UnityOfWork(
                new AdRepository(dbContext), 
                new AdPhotoRepository(dbContext),
                new AdUpRepository(dbContext),
                new AdViewsRepository(dbContext),
                new CityAreasRepository(dbContext),
                new CityRepository(dbContext),
                new ProductTypeRepository(dbContext),
                new ProductModelRepository(dbContext),
                new ProductMemoriesRepository(dbContext),
                new ProductColorsRepository(dbContext),
                new ProductStatesRepository(dbContext),
                new PurchaseRepository(dbContext),
                new ServiceActiveTimeRepository(dbContext),
                new ServiceRepository(dbContext),
                new UserRepository(dbContext));


            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            serviceCollection.AddTransient<IImageCompressorService, ImageCompressorService>();
            serviceCollection.AddTransient<IImageService>(
                s => new ImageService(new ImageCompressorService()));

            serviceCollection.AddTransient<IDataTransformerService>(
                s => new DataTransformerService(unitOfWork));

            serviceCollection.AddTransient<ISeedService>(
                s => new SeedService(dbContext));

            serviceCollection.AddTransient<ICityAreasService>(
                s => new CityAreasService(unitOfWork));

            serviceCollection.AddTransient<ICityService>(
                s => new CityService(unitOfWork));

            serviceCollection.AddTransient<IProductModelsService>(
                s => new ProductModelService(unitOfWork));


            serviceCollection.AddTransient<IAdService>(
                s => new AdService(unitOfWork,
                     new ImageService(new ImageCompressorService()),
                     new DataTransformerService(unitOfWork),
                     new AdUpService(unitOfWork),
                     new ConversationService(dbContext)));

            serviceCollection.AddTransient<IAdUpService>(
                s => new AdUpService(unitOfWork));

            serviceCollection.AddTransient<IAdViewsService>(
                s => new AdViewsService(unitOfWork));

            serviceCollection.AddTransient<IConversationService>(
                s => new ConversationService(new AppDbContext(optionsBuilder.Options)));

            serviceCollection.AddTransient<IPurchasesService>(
               s => new PurchasesService(unitOfWork));

            serviceCollection.AddTransient<IServiceActiveTimeService>(
                s => new ServiceActiveTimeService(dbContext));

            serviceCollection.AddTransient<IServicesService>(
                s => new ServicesService(unitOfWork));

            serviceCollection.AddTransient<IUserService, UserService>();

            serviceCollection.AddTransient<IUserService>(
              s => new UserService(unitOfWork));

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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AppleUsed.Repo.Data;
using AppleUsed.Repo.Identity;

namespace AppleUsed.Repo
{
    /// <summary>
    /// This factory is provided so that the EF Core tools can build a full context
    /// without having to have access to where the DbContext is being created (i.e.
    /// in the UI layer).
    /// </summary>
    /// <remarks>
    /// Please see the following URL for more information:
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#using-idbcontextfactorytcontext
    /// </remarks>
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private static string DataConnectionString => new DatabaseConfiguration().GetDataConnectionString();

        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(DataConnectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
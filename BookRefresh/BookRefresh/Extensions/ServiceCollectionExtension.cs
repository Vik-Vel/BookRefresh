using BookRefresh.Data;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookRefresh.Extensions
{
    public static class ServiceCollectionExtension
    {
        //Allows us to add to the IServiceCollection in a more structured and manageable way, useful when setting up Dependency Injection (DI)
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }

        //This extension method adds and configures the database context in the application's DI container using the connection string from the configuration file.
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            //This extracts the database connection string from the config file. GetConnectionString looks for a string named "DefaultConnectionContext" in the configuration file.
            var connectionString = config.GetConnectionString("DefaultConnectionContext");

            //This adds the ApplicationDbContext to the service collection and configures it to use SQL Server with the connection string we just extracted. AddDbContext is a method that registers the database context in the DI container.
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            //This adds an exception page filter that is useful for developers. It helps diagnose database errors during development.
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;

        }

        //The AddApplicationIdentity method adds and configures Identity services in an ASP.NET Core application, including setting up account verification and using the Entity Framework for data storage. This makes the process of registration and user management easier and structured.
        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            //This part adds and configures the standard Identity system for users in ASP.NET Core.
            services.AddDefaultIdentity<IdentityUser>(options =>//This adds the standard identity system for IdentityUser, which is a class representing a user.
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();//This adds Entity Framework as a backend for storing Identity data. Uses ApplicationDbContext to access the database.

            return services;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StateManagments.Models.Data;

namespace StateManagments.Models;
public static class GlobalConfiguration
{
    public static IServiceCollection ModelsConfiguration(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<StateManagmentContext>(opt =>
        {
           
            opt
            .UseSqlServer(configuration.GetConnectionString("Local"));
        });


        return services;
    }

}

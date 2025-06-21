using DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContracts;
using eCommrce.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceSolution.ProductsService.DataAccessLayer;

public static class DependancyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {

        //TO DO: Add Data Access Layer services into the IoC container
        services.AddDbContext<ApplicationDbContext>(_ => _.UseMySQL(configuration.GetConnectionString("DefaultConnection")!)
        );

        services.AddScoped<IProductsRepository, ProductRepository>();
        return services;
    }

}

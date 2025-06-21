using eCommerce.BussinessLogicLayer.Mappers;
using eCommerce.BussinessLogicLayer.ServiceContracts;
using eCommerce.BussinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceSolution.ProductsService.BussinessLogicLayer;

public static class DependancyInjection
{
    public static IServiceCollection AddBussinessLogicLayer(this IServiceCollection services)
    {

        //TO DO: Add Data Access Layer services into the IoC container

        services.AddAutoMapper(typeof(ProductToProductMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        services.AddScoped<IProductsService, eCommerce.BussinessLogicLayer.Services.ProductServices>();

        return services;
    }

}

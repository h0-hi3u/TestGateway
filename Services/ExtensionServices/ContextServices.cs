using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using Model.EntityModels;
using Services.DataBaseServices;

namespace Services.ExtensionServices;

public static class ContextServices
{
    public static IServiceCollection ConfigDatabaseServices(this IServiceCollection services)
    {
        var config = services.BuildServiceProvider().GetService<IConfiguration>();

        services.AddDbContext<TestGatewayContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("TestGatewayConn"), sqlOptions => sqlOptions.CommandTimeout(20));
        });
        services.AddScoped<TestGatewayContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddDataService(this IServiceCollection services)
    {
        
        services.AddScoped<IProductServices, ProductServices>();
        services.AddScoped<ISupplierServices, SupplierServices>();
        services.AddScoped<ICategoryServices, CategoryServices>();
        return services;
    }
        
}

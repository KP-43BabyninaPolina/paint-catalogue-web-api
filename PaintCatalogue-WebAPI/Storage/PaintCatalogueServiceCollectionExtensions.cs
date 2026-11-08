using PaintCatalogue_WebAPI.Storage;
using PaintCatalogue_WebAPI.Storage.Contracts;
using PaintCatalogue_WebAPI.Storage.InMemory;

namespace Microsoft.Extensions.DependencyInjection;

public static class PaintCatalogueServiceCollectionExtensions
{

    public  static IServiceCollection AddInMemoryStorage(this IServiceCollection services) => services
        .AddSingleton<IBrandRepository, BrandStorage>()
        .AddSingleton<IPaintRepository, PaintStorage>();
    
    
    public static IServiceCollection AddRepositories(this IServiceCollection services) => services
        .AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
        .AddScoped<IPaintRepository, PaintRepository>()
        .AddScoped<IBrandRepository, BrandRepository>();
}
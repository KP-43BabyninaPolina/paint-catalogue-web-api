using PaintCatalogue_WebAPI.Storage;

namespace Microsoft.Extensions.DependencyInjection;

public static class StorageServiceCollectionExtensions
{
    public static IServiceCollection AddCatalogueStorages(this IServiceCollection services)
    {
        services.AddSingleton<IBrandStorage, BrandStorage>();
        services.AddSingleton<IPaintStorage, PaintStorage>();
        
        return services;
    }
}
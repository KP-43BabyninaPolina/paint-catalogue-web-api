using PaintCatalogue_WebAPI.DataModel.Entities;
using PaintCatalogue_WebAPI.Storage.Contracts;

namespace PaintCatalogue_WebAPI.Storage.InMemory;

public class BrandStorage : IBrandRepository
{
    private readonly List<Brand> _brands = [];

    public BrandStorage()
    {
        _brands.Add(new Brand
        {
            Id = 1,
            Name = "Rosa",
            Country = "Ukraine",
            Description = "High-quality paints for studying."
        });
    }

    public IQueryable<Brand> GetAll()
    {
        return _brands.AsQueryable();
    }

    public Task<Brand?> FindAsync(int key)
    {
        var brand = _brands.FirstOrDefault(x => x.Id == key);
        return Task.FromResult(brand);
    }

    public Task AddAsync(Brand entity)
    {
        entity.Id = _brands.Count > 0 ? _brands.Max(x => x.Id) + 1 : 1;
        _brands.Add(entity);
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Brand entity)
    {
        var existingBrand = _brands.FirstOrDefault(x => x.Id == entity.Id);

        if (existingBrand != null)
        {
            existingBrand.Name = entity.Name;
            existingBrand.Country = entity.Country;
            existingBrand.Description = entity.Description;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Brand entity)
    {
        _brands.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAllAsync()
    {
        _brands.Clear();
        return Task.CompletedTask;
    }
}
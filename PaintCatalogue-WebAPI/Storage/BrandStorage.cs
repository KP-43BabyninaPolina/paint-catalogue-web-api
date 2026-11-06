using PaintCatalogue_WebAPI.DataModel.Entities;

namespace PaintCatalogue_WebAPI.Storage;

public class BrandStorage : IBrandStorage
{
    private readonly List<Brand> _brands = [];

    public BrandStorage()
    {
        _brands.Add(new Brand
        {
            Id = 1,
            Name = "Rosa",
            Country = "Ukraine",
            Description = "High-quality paints for studying"
        });
        _brands.Add(new Brand
        {
            Id = 2,
            Name = "Van Gogh",
            Country = "Netherlands",
            Description = "Professional art materials"
        });
    }

    public IEnumerable<Brand> GetAll()
    {
        return _brands.OrderBy(x => x.Id);
    }

    public Brand? Get(int id)
    {
        return _brands.FirstOrDefault(x => x.Id == id);
    }

    public Brand Add(Brand brand)
    {
        brand.Id = _brands.Count > 0 ? _brands.Max(x => x.Id) + 1 : 1;
        _brands.Add(brand);

        return brand;
    }

    public Brand? Update(int id, Brand updatedBrand)
    {
        var existingBrand = Get(id);

        if (existingBrand == null) return null;

        existingBrand.Name = updatedBrand.Name;
        existingBrand.Country = updatedBrand.Country;
        existingBrand.Description = updatedBrand.Description;

        return existingBrand;
    }

    public void DeleteAll()
    {
        _brands.Clear();
    }

    public void Delete(int id)
    {
        _brands.RemoveAll(x => x.Id == id);
    }
}
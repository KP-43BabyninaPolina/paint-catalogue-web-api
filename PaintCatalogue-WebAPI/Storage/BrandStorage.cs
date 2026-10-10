using PaintCatalogue_WebAPI.Models;

namespace PaintCatalogue_WebAPI.Storage;

public class BrandStorage
{
    private static readonly List<Brand> Brands = [];

    static BrandStorage()
    {
        Brands.Add(new Brand
        {
            Id = 1,
            Name = "Rosa",
            Country = "Ukraine",
            Description = "High-quality paints for studying"
        });
        Brands.Add(new Brand
        {
            Id = 2,
            Name = "Van Gogh",
            Country = "Netherlands",
            Description = "Professional art materials"
        });
    }

    public IEnumerable<Brand> GetAll()
    {
        return Brands.OrderBy(x => x.Id);
    }

    public Brand? Get(int id)
    {
        return Brands.FirstOrDefault(x => x.Id == id);
    }

    public Brand Add(Brand brand)
    {
        brand.Id = Brands.Count > 0 ? Brands.Max(x => x.Id) + 1 : 1;
        Brands.Add(brand);

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
        Brands.Clear();
    }

    public void Delete(int id)
    {
        Brands.RemoveAll(x => x.Id == id);
    }
}
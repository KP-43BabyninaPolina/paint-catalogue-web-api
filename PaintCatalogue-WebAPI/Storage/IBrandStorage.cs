using PaintCatalogue_WebAPI.Models;

namespace PaintCatalogue_WebAPI.Storage;

public interface IBrandStorage
{
    IEnumerable<Brand> GetAll();
    Brand? Get(int id);
    Brand Add(Brand brand);
    Brand? Update(int id, Brand updatedBrand);
    void Delete(int id);
    void DeleteAll();
}
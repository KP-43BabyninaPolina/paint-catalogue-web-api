using PaintCatalogue_WebAPI.DataModel.Entities;

namespace PaintCatalogue_WebAPI.Storage;

public interface IPaintStorage
{
    IEnumerable<Paint> GetAll();
    Paint? Get(int id);
    IEnumerable<Paint> GetByBrandId(int brandId);
    Paint Add(Paint paint);
    Paint? Update(int id, Paint updatedPaint);
    void Delete(int id);
    void DeleteAll();
}
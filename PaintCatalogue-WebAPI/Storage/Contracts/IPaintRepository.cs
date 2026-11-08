using PaintCatalogue_WebAPI.DataModel.Entities;

namespace PaintCatalogue_WebAPI.Storage.Contracts;

public interface IPaintRepository : IGenericRepository<Paint, int>
{
    Task<IEnumerable<Paint>> GetByBrandIdAsync(int brandId);
}
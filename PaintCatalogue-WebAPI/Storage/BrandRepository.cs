using PaintCatalogue_WebAPI.DataModel;
using PaintCatalogue_WebAPI.DataModel.Entities;
using PaintCatalogue_WebAPI.Storage.Contracts;

namespace PaintCatalogue_WebAPI.Storage;

internal class BrandRepository : GenericRepository<Brand, int>, IBrandRepository
{
    public BrandRepository(DataModelContext context) : base(context)
    {
    }
}
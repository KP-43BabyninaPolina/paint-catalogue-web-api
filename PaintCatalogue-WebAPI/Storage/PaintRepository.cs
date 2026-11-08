using Microsoft.EntityFrameworkCore;
using PaintCatalogue_WebAPI.DataModel;
using PaintCatalogue_WebAPI.DataModel.Entities;
using PaintCatalogue_WebAPI.Storage.Contracts;

namespace PaintCatalogue_WebAPI.Storage;

internal class PaintRepository : GenericRepository<Paint, int>, IPaintRepository
{
    public PaintRepository(DataModelContext context) : base(context)
    {
    }

    // Прибрати після переходу на _mapper.ProjectTo в PaintController,
    // тимчасово створено для використання синхронного ToList()
    // та сумісності з in-memory сховищем List<Paint>
    public override IQueryable<Paint> GetAll()
    {
        return _context.Set<Paint>().Include(p => p.Brand);
    }

    public override Task<Paint?> FindAsync(int key)
    {
        return _context.Set<Paint>()
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == key);
    }
    
    public async Task<IEnumerable<Paint>> GetByBrandIdAsync(int brandId)
    {
        return await _context.Set<Paint>()
            .Where(p => p.BrandId == brandId)
            .OrderBy(p => p.Id)
            .ToListAsync();
    }
}
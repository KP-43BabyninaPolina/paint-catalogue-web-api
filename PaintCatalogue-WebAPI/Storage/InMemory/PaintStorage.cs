using PaintCatalogue_WebAPI.DataModel.Entities;
using PaintCatalogue_WebAPI.DataModel.Entities.Enums;
using PaintCatalogue_WebAPI.Storage.Contracts;

namespace PaintCatalogue_WebAPI.Storage.InMemory;

public class PaintStorage : IPaintRepository
{
    private readonly List<Paint> _paints = [];

    public PaintStorage()
    {
        _paints.Add(new Paint
        {
            Id = 1,
            ColorCode = "715",
            ColorName = "Ultramarine",
            Type = PaintType.Watercolor,
            Price = 153.0m,
            Volume = 10,
            Lightfastness = 3,
            Transparency = Transparency.SemiTransparent,
            BrandId = 1,
            Brand = new Brand {            
                Id = 1,
                Name = "Template"}
        });
    }

    public IQueryable<Paint> GetAll()
    {
        return _paints.AsQueryable();
    }

    public Task<Paint?> FindAsync(int key)
    {
        var paint = _paints.FirstOrDefault(x => x.Id == key);
        return Task.FromResult(paint);
    }

    public Task<IEnumerable<Paint>> GetByBrandIdAsync(int brandId)
    {
        var paints = _paints.Where(x => x.BrandId == brandId).OrderBy(x => x.Id).AsEnumerable();
        return Task.FromResult(paints);
    }

    public Task AddAsync(Paint entity)
    {
        entity.Id = _paints.Count > 0 ? _paints.Max(x => x.Id) + 1 : 1;
        _paints.Add(entity);
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Paint entity)
    {
        var existingPaint = _paints.FirstOrDefault(x => x.Id == entity.Id);

        if (existingPaint != null)
        {
            existingPaint.ColorName = entity.ColorName;
            existingPaint.Type = entity.Type;
            existingPaint.Price = entity.Price;
            existingPaint.Volume = entity.Volume;
            existingPaint.Lightfastness = entity.Lightfastness;
            existingPaint.Transparency = entity.Transparency;
            existingPaint.ColorCode = entity.ColorCode;
            existingPaint.BrandId = entity.BrandId;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Paint entity)
    {
        _paints.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAllAsync()
    {
        _paints.Clear();
        return Task.CompletedTask;
    }
}
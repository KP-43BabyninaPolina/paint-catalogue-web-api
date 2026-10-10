using PaintCatalogue_WebAPI.Models;
using PaintCatalogue_WebAPI.Models.Enums;

namespace PaintCatalogue_WebAPI.Storage;

public class PaintStorage
{
    private static readonly List<Paint> Paints = [];

    static PaintStorage()
    {
        Paints.Add(new Paint
        {
            Id = 1,
            FullTitle = "Rosa Gallery Watercolor, Ultramarine, 10ml tube",
            ColorCode = "715",
            ColorName = "Ultramarine",
            Type = PaintType.Watercolor,
            Price = 153.0m,
            Volume = 10,
            Lightfastness = 3,
            Transparency = Transparency.SemiTransparent,
            BrandId = 1
        });
        Paints.Add(new Paint
        {
            Id = 2,
            FullTitle = "Van Gogh Watercolor, Cobalt Blue (Ultramarine), 10ml tube",
            ColorCode = "512",
            ColorName = "Cobalt Blue (Ultramarine)",
            Type = PaintType.Watercolor,
            Price = 182.0m,
            Volume = 10,
            Lightfastness = 2,
            Transparency = Transparency.Transparent,
            BrandId = 2
        });
    }

    public IEnumerable<Paint> GetAll()
    {
        return Paints.OrderBy(x => x.Id);
    }

    public Paint? Get(int id)
    {
        return Paints.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Paint> GetByBrandId(int brandId)
    {
        return Paints.Where(x => x.BrandId == brandId).OrderBy(x => x.Id);
    }

    public Paint Add(Paint paint)
    {
        paint.Id = Paints.Count > 0 ? Paints.Max(x => x.Id) + 1 : 1;

        Paints.Add(paint);

        return paint;
    }

    public Paint? Update(int id, Paint updatedPaint)
    {
        var existingPaint = Get(id);

        if (existingPaint == null) return null;

        existingPaint.FullTitle = updatedPaint.FullTitle;
        existingPaint.ColorName = updatedPaint.ColorName;
        existingPaint.Type = updatedPaint.Type;
        existingPaint.Price = updatedPaint.Price;
        existingPaint.Volume = updatedPaint.Volume;
        existingPaint.Lightfastness = updatedPaint.Lightfastness;
        existingPaint.Transparency = updatedPaint.Transparency;
        existingPaint.ColorCode = updatedPaint.ColorCode;
        existingPaint.BrandId = updatedPaint.BrandId;

        return existingPaint;
    }

    public void DeleteAll()
    {
        Paints.Clear();
    }

    public void Delete(int id)
    {
        Paints.RemoveAll(x => x.Id == id);
    }
}
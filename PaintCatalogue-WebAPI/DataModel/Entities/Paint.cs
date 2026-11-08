using PaintCatalogue_WebAPI.DataModel.Entities.Enums;

namespace PaintCatalogue_WebAPI.DataModel.Entities;

public class Paint
{
    public int Id { get; set; }
    
    public string? ColorCode { get; set; }

    public string? ColorName { get; set; }
    
    public PaintType Type { get; set; }
    
    public decimal Price { get; set; }

    public int Volume { get; set; }

    public int Lightfastness { get; set; }
    
    public Transparency Transparency { get; set; }

    public int BrandId { get; set; }

    public Brand Brand { get; set; } = null!;
}
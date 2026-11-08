using PaintCatalogue_WebAPI.DataModel.Entities.Enums;

namespace PaintCatalogue_WebAPI.Dtos.Paint;

public class PaintCreateDto
{
    /// <summary>
    /// Unique per Brand color identifier consisting of 3 digits. 
    /// </summary>
    public string ColorCode { get; set; } = null!;
    
    public string ColorName { get; set; } = null!;
    
    /// <summary>
    /// Type indicator for marking Paint as Watercolor, Gouache, Acrylic or Oil.
    /// </summary>
    public PaintType Type { get; set; }
    
    /// <summary>
    /// Price in UAH (Ukrainian Hryvnia).
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Volume of the product in milliliters.
    /// </summary>
    public int Volume { get; set; }
    
    /// <summary>
    /// Lightfastness level with ascending value from 1 to 5.
    /// </summary>
    public int Lightfastness { get; set; }
    
    /// <summary>
    /// Transparency indicator for marking Paint as Transparent, SemiTransparent or Opaque.
    /// </summary>
    public Transparency Transparency { get; set; }
    
    /// <summary>
    /// Identifier of the paint producer Brand.
    /// </summary>
    public int BrandId { get; set; }
}
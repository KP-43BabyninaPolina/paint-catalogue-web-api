using PaintCatalogue_WebAPI.DataModel.Entities.Enums;
using PaintCatalogue_WebAPI.Dtos.Brand;

namespace PaintCatalogue_WebAPI.Dtos.Paint;

public class PaintOutputDto
{
    public int Id { get; set; }
    
    /// <summary>
    /// Main information about the paint producer Brand. 
    /// </summary>
    public BrandShortOutputDto Brand { get; set; } = null!;
    
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
    /// Auto-composed full commercial title of the paint product.
    /// </summary>
    public string FullTitle { get; set; } = null!;
}
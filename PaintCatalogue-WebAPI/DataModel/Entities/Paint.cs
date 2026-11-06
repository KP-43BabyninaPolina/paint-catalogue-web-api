using PaintCatalogue_WebAPI.DataModel.Entities.Enums;

namespace PaintCatalogue_WebAPI.DataModel.Entities;

public class Paint
{
    public int Id { get; set; }

    /// <summary>
    /// Full commercial title of the Paint product. 
    /// </summary>
    public string? FullTitle { get; set; }

    /// <summary>
    /// Unique per Brand color identifier consisting of 3 digits. 
    /// </summary>
    public string? ColorCode { get; set; }

    public string? ColorName { get; set; }
    
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

    public int BrandId { get; set; }
}
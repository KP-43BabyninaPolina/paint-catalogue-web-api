namespace PaintCatalogue_WebAPI.Dtos.Brand;

public class BrandCreateDto
{
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Country where the brand is originated.
    /// </summary>
    public string Country { get; set; } = null!;
    
    /// <summary>
    /// Short textual characteristics about target consumers and purpose.
    /// </summary>
    public string? Description { get; set; }
}
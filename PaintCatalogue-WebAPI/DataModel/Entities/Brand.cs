namespace PaintCatalogue_WebAPI.DataModel.Entities;

public class Brand
{
    public int Id { get; set; }

    public string? Name { get; set; }

    /// <summary>
    /// Country where Brand is originated.
    /// </summary>
    public string? Country { get; set; }
    
    public string? Description { get; set; }
}
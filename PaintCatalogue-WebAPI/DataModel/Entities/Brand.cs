namespace PaintCatalogue_WebAPI.DataModel.Entities;

public class Brand
{
    public int Id { get; set; }

    public string? Name { get; set; }
    
    public string? Country { get; set; }
    
    public string? Description { get; set; }
    
    public List<Paint> Paints { get; set; } = [];
}
using System.Text.Json.Serialization;

namespace PaintCatalogue_WebAPI.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaintType
{
    Watercolor,
    Gouache,
    Acrylic,
    Oil
}
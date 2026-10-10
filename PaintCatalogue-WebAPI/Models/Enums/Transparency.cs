using System.Text.Json.Serialization;

namespace PaintCatalogue_WebAPI.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Transparency
{
    Opaque,
    SemiTransparent,
    Transparent
}
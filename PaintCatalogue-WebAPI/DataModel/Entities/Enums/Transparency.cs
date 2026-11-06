using System.Text.Json.Serialization;

namespace PaintCatalogue_WebAPI.DataModel.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Transparency
{
    Opaque,
    SemiTransparent,
    Transparent
}
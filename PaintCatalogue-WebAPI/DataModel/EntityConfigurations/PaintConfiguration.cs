using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintCatalogue_WebAPI.DataModel.Entities;
using PaintCatalogue_WebAPI.DataModel.Entities.Enums;

namespace PaintCatalogue_WebAPI.DataModel.EntityConfigurations;

public class PaintConfiguration : IEntityTypeConfiguration<Paint>
{
    public void Configure(EntityTypeBuilder<Paint> builder)
    {
        builder.ToTable("Paints");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        

        builder.Property(p => p.ColorName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.ColorCode)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(p => p.Type)
            .IsRequired();

        builder.Property(p => p.Transparency)
            .IsRequired();

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Volume)
            .IsRequired();

        builder.Property(p => p.Lightfastness)
            .IsRequired();
        
        builder.HasOne(p => p.Brand)        
            .WithMany(b => b.Paints)        
            .HasForeignKey(p => p.BrandId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasData(new Paint
        {
            Id = 1,
            ColorCode = "715",
            ColorName = "Ultramarine",
            Type = PaintType.Watercolor,
            Price = 153.0m,
            Volume = 10,
            Lightfastness = 3,
            Transparency = Transparency.SemiTransparent,
            BrandId = 1
        });
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintCatalogue_WebAPI.DataModel.Entities;

namespace PaintCatalogue_WebAPI.DataModel.EntityConfigurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.Country)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(b => b.Description)
            .HasMaxLength(500);
        
        builder.HasData(new Brand
        {
            Id = 1,
            Name = "Rosa",
            Country = "Ukraine",
            Description = "High-quality paints for studying"
        });
    }
}
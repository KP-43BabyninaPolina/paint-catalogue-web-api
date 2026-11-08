using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PaintCatalogue_WebAPI.DataModel.Entities;

namespace PaintCatalogue_WebAPI.DataModel;

public class DataModelContext : DbContext
{
    public DbSet<Paint> Paints { get; set; }
    public DbSet<Brand> Brands { get; set;}

    public DataModelContext(DbContextOptions<DataModelContext> options) : base(options)
    {

    } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}
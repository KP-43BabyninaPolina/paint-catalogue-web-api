using System.Reflection;
using PaintCatalogue_WebAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using PaintCatalogue_WebAPI.DataModel;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace PaintCatalogue_WebAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        // Щоб працювати з Sqlite базою даних:
        builder.Services.AddDbContext<DataModelContext>(contextOptions =>
            contextOptions.UseSqlite("Data Source=paint_catalogue.db"));
        builder.Services.AddRepositories();
        
        // Щоб працювати з in-memory колекціями (для демонстрації DI):
        // builder.Services.AddInMemoryStorage();
        
        builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
        
        builder.Services.AddValidatorsFromAssemblyContaining<PaintValidator>();
        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Paint Catalogue API",
                    Description = "An ASP.NET Core RESTful API for cataloging art paints, brands, " +
                                  "and their specific properties."
                };

                return Task.CompletedTask;
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "v1"); });
        }

        app.MapControllers();

        app.Run();
    }
}
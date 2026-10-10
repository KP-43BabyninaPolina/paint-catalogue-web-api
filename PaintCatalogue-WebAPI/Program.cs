using PaintCatalogue_WebAPI.Validators;
using FluentValidation;
using Microsoft.OpenApi;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace PaintCatalogue_WebAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

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
using FluentValidation;
using PaintCatalogue_WebAPI.Models;

namespace PaintCatalogue_WebAPI.Validators;

public class BrandValidator : AbstractValidator<Brand>
{
    public BrandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(x => x.Country)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
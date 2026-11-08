using FluentValidation;
using PaintCatalogue_WebAPI.Dtos.Brand;

namespace PaintCatalogue_WebAPI.Validators;

public class BrandValidator : AbstractValidator<BrandCreateDto>
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
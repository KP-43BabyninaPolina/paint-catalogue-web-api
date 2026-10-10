using FluentValidation;
using PaintCatalogue_WebAPI.Models;

namespace PaintCatalogue_WebAPI.Validators;

public class PaintValidator : AbstractValidator<Paint>
{
    public PaintValidator()
    {
        RuleFor(x => x.ColorCode)
            .NotEmpty()
            .Matches(@"^\d{3}$")
            .WithMessage("Color code must consist of exactly 3 digits (e.g., '007', '125').");

        RuleFor(x => x.FullTitle)
            .NotEmpty()
            .Length(2, 100);

        RuleFor(x => x.ColorName)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Unknown paint type specified.");

        RuleFor(x => x.Transparency)
            .IsInEnum().WithMessage("Unknown transparency type specified.");

        RuleFor(x => x.Price).GreaterThan(0);

        RuleFor(x => x.Volume).GreaterThan(0);

        RuleFor(x => x.Lightfastness).InclusiveBetween(1, 5);

        RuleFor(x => x.BrandId).GreaterThanOrEqualTo(1);
    }
}
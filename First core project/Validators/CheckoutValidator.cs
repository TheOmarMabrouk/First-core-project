using First_core_project.DTOs;
using FluentValidation;

public class CheckoutValidator : AbstractValidator<CheckoutDto>
{
    public CheckoutValidator()
    {
        RuleFor(x => x.ShippingAddress)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(x => x.Notes)
            .MaximumLength(500);
    }
}
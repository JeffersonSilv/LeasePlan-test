using FluentValidation;

namespace LeasePlanCurrencyConverter.Dto
{
    public class CurrencyConverterDtoValidator : AbstractValidator<CurrencyConverterDto>
    {
        public CurrencyConverterDtoValidator()
        {
            RuleFor(x => x.FromCode).NotEmpty();
            RuleFor(x => x.FromCode).NotNull();
            RuleFor(x => x.FromCode).Length(3);
            RuleFor(x => x.FromCode).Matches(@"^[a-zA-Z-']*$");

            RuleFor(x => x.ToCode).NotEmpty();
            RuleFor(x => x.ToCode).NotNull();
            RuleFor(x => x.ToCode).Length(3);
            RuleFor(x => x.ToCode).Matches(@"^[a-zA-Z-']*$");


            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Amount).NotNull();
            RuleFor(x => x.Amount)
                .Must(x => x >= 0)
                .WithMessage("Amount cannot be negative");
        }

    }
}
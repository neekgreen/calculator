namespace WebApi.Features.Calculations
{
    using FluentValidation;

    public class InputModelValidator : AbstractValidator<InputModel>
    {
        public InputModelValidator()
        {
            RuleFor(t=> t.Expression).NotNull();
        }
    }
}
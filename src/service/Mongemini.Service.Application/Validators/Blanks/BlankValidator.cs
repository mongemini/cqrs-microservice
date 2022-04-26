using FluentValidation;
using Mongemini.Service.Application.ViewModels.Blanks;

namespace Mongemini.Service.Application.Validators.Blanks
{
    public class BlankValidator : AbstractValidator<BlankViewModel>
    {
        public BlankValidator()
        {
            RuleFor(m => m.Id)
            .NotEmpty()
            .MinimumLength(1);
        }
    }
}

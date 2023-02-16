using FluentValidation;
using Organic.Areas.Admin.ViewModels.User;

namespace Organic.Areas.Admin.Validators.Admin.User.Add
{
    public class UpdateViewModelValidator : AbstractValidator<AddViewModel>
    {
        public UpdateViewModelValidator()
        {
            RuleFor(avm => avm.FirstName)
                .NotEmpty().WithMessage("Firstname can't be empty")
                .MinimumLength(3).WithMessage("Minimum length should be 3")
                .MaximumLength(25).WithMessage("Maximum length should be 25");

            RuleFor(avm => avm.LastName)
                .NotEmpty().WithMessage("Lastname can't be empty")
                .MinimumLength(5).WithMessage("Minimum length should be 5")
                .MaximumLength(35).WithMessage("Maximum length should be 35");

            RuleFor(avm => avm.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .EmailAddress();
        }
    }
}

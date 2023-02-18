using FluentValidation;
using Organic.Areas.Admin.ViewModels.Product.Category;

namespace Organic.Validators.Admin.Category
{
    public class UpdateViewModelValidator : AbstractValidator<UpdateViewModel>
    {
        public UpdateViewModelValidator()
        {
            RuleFor(avm => avm.Name)
                .NotEmpty().WithMessage("Category name can't be empty")
                .MaximumLength(25).WithMessage("Maximum length should be 20");

            RuleFor(avm => avm.IconClass)
                .MinimumLength(5).WithMessage("Icon class is not correct");
        }
    }
}

using FluentValidation;
using Organic.Areas.Client.ViewModels.Authentication;

namespace Organic.Validators.Client.User
{
    public class NewPasswordViewModelValidator : AbstractValidator<NewPasswordViewModel>
    {
        public NewPasswordViewModelValidator()
        {
            RuleFor(avm => avm.Password)
                        .NotEmpty().WithMessage("Şifrə daxil edilməlidir")
                        .MinimumLength(3).WithMessage("Şifrənin uzunluğu 8-den çox olmalıdır.")
                        .MaximumLength(28).WithMessage("Şifrənin uzunluğu 15-den az olmalıdır.")
                        .Matches(@"[a-z]").WithMessage("Ən azı bir kiçik hərf olmalıdır")
                        .Matches(@"[A-Z]").WithMessage("Ən azı bir böyük hərf olmalıdır")
                        .Matches(@"\d").WithMessage("Ən azı bir rəqəm olmalıdır")
                        .Matches(@"[^\da-zA-Z]").WithMessage("Ən azı bir xüsusi simvol olmalıdır");
        }
    }
}

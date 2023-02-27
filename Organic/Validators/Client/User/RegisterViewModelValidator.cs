using FluentValidation;
using Organic.Areas.Client.ViewModels.Authentication;

namespace Organic.Validators.Admin.User
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(avm => avm.FirstName)
                .NotEmpty().WithMessage("Adınızı qeyd edin.")
                .MinimumLength(3).MaximumLength(18).WithMessage("Adın uzunluğu 3 və ya 18 arasında olmalıdır.");


            RuleFor(avm => avm.LastName)
                .NotEmpty().WithMessage("Soyadınızı qeyd edin.")
                .MinimumLength(3).MaximumLength(28).WithMessage("Soyadın uzunluğu 3 və ya 28 arasında olmalıdır.");

            RuleFor(avm => avm.Email)
                .NotEmpty().WithMessage("Email bölməsini doldurun.")
                .EmailAddress().WithMessage("Emaili düzgün daxil edin.");

            RuleFor(avm => avm.Password)
                        .NotEmpty().WithMessage("Şifrə daxil edilməlidir")
                        .MinimumLength(8).MaximumLength(15).WithMessage("Şifrənin uzunluğu 8 və ya 15 arasında olmalıdır")
                        .Matches(@"[a-z]").WithMessage("Ən azı bir kiçik hərf olmalıdır")
                        .Matches(@"[A-Z]").WithMessage("Ən azı bir böyük hərf olmalıdır")
                        .Matches(@"\d").WithMessage("Ən azı bir rəqəm olmalıdır")
                        .Matches(@"[^\da-zA-Z]").WithMessage("Ən azı bir xüsusi simvol olmalıdır");
        }
    }
}

using FluentValidation;
using Organic.Areas.Client.ViewModels.Authentication;

namespace Organic.Validators.Client.User
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(avm => avm.FirstName)
                .NotEmpty().WithMessage("Adınızı qeyd edin.")
                .MinimumLength(3).WithMessage("Adın uzunluğu 3-dən çox olmalıdır.")
                .MaximumLength(28).WithMessage("Adın uzunluğu 28-dən az olmalıdır.");


            RuleFor(avm => avm.LastName)
                .NotEmpty().WithMessage("Soyadınızı qeyd edin.")
                .MinimumLength(3).WithMessage("Soyadın uzunluğu 3-dən çox olmalıdır.")
                .MaximumLength(28).WithMessage("Soyadın uzunluğu 28-dən az olmalıdır.");

            RuleFor(avm => avm.Email)
                .NotEmpty().WithMessage("Email bölməsini doldurun.")
                .EmailAddress().WithMessage("Emaili düzgün daxil edin.");

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

﻿using FluentValidation;
using Organic.Areas.Admin.ViewModels.Product.Tag;

namespace Organic.Validators.Admin.Tag
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Name)
                .NotEmpty().WithMessage("Tag name can't be empty")
                .MaximumLength(25).WithMessage("Maximum length should be 20");
        }
    }
}

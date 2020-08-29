using FluentValidation;
using System;

namespace NISApi.DTO.Request.SystemTables
{
    public class CreateNationalityRequest
    {
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

    }

    public class CreateNationalityRequestValidator : AbstractValidator<CreateNationalityRequest>
    {
        public CreateNationalityRequestValidator()
        {
            RuleFor(o => o.Code).NotEmpty().MaximumLength(10).WithMessage("Please enter a descriptive code");
            RuleFor(o => o.ShortDescription).NotEmpty().MaximumLength(50).WithMessage("Please enter a short description");
            RuleFor(o => o.LongDescription).NotEmpty().MaximumLength(90).WithMessage("Please enter a long description");
        }
    }

}
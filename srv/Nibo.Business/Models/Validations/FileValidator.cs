using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Nibo.Business.Models.Validations
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(100)
                .WithMessage("File size is larger than allowed");

            RuleFor(x => x.ContentType).NotNull()
                .WithMessage("File type is invalid");

            RuleFor(x => x.FileName).NotNull()
                .WithMessage("File Name is invalid");
        }
    }
}

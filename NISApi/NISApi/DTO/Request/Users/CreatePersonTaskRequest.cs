using FluentValidation;
using System;

namespace NISApi.DTO.Request.Users
{
    public class CreatePersonTaskRequest
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public string Summary { get; set; }
        public string TaskType { get; set; }
        public string Priority { get; set; }
        public string ReferenceEntity { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTimeOffset? ReferenceDate { get; set; }
        public DateTimeOffset? DateToBeCompleted { get; set; }
        public string Colour { get; set; }
        public string User { get; set; }
        public string UserID { get; set; }

    }

    public class CreatePersonTaskRequestValidator : AbstractValidator<CreatePersonTaskRequest>
    {
        public CreatePersonTaskRequestValidator()
        {
            RuleFor(o => o.Title).NotEmpty().MaximumLength(50).WithMessage("Please enter a title");
            RuleFor(o => o.Status).NotEmpty().MaximumLength(50).WithMessage("Please enter a status");
            RuleFor(o => o.Priority).NotEmpty().MaximumLength(50).WithMessage("Please enter a priority");
            RuleFor(o => o.TaskType).NotEmpty().MaximumLength(50).WithMessage("Please enter a type describing the task");
        }
    }

}
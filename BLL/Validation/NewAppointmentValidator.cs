using BLL.Models;
using FluentValidation;

namespace BLL.Validation
{
    public class NewAppointmentValidator : AbstractValidator<NewAppointmentModel>
    {
        public NewAppointmentValidator()
        {
            RuleFor(x => x.PatientName)
                .NotEmpty().WithMessage("Patient name cannot be empty.")
                .MaximumLength(100).WithMessage("Maximmum length for name is 100 characters.");

            RuleFor(x => x.PatientPhone)
                .NotEmpty().WithMessage("Phone number cannot be empty.")
                .MinimumLength(10).WithMessage("Minimum length for phone number is 10 characters.")
                .MaximumLength(13).WithMessage("Maximmum length for phone number is 13 characters.");

            RuleFor(x => x.FavorId)
                .NotNull().WithMessage("FavorId cannot be empty.");

            RuleFor(x => x.DoctorId)
               .NotNull().WithMessage("DoctorId cannot be empty.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date cannot be empty.")
                .GreaterThan(p => DateTime.Now).WithMessage("Invalid Date. It cannot be less then now.");
        }
    }
}

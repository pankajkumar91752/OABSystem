using Microsoft.VisualBasic;
using OABSystem.Data;
using OABSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace OABSystem.Util
{
    internal class ValidationHelpers : ValidationAttribute

    {
        private const double DefaultAppointmentMinutes = 30;

        public OABSystemContext Context { get; }

        public ValidationHelpers(OABSystemContext context)
        {
            Context = context;
        }
        public ValidationResult ValidateAppointmentAvailbilty(Appointment appointment)
        {
            var r = Context.HealthcareProfessional.FirstOrDefault(e => e.Id == appointment.HealthcareProfessional.Id)?.Appointments.Any(a => a.AppointmentDateTime < appointment.AppointmentDateTime.Date
            && a.AppointmentDateTime.AddMinutes(DefaultAppointmentMinutes) > appointment.AppointmentDateTime.Date);
            return r == true ? new ValidationResult($"Schduled appointment time is not avalible {appointment.AppointmentDateTime}") : ValidationResult.Success;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || value is not DateTime) return new ValidationResult("Not Valid date");
            return ValidAppointmentDate((DateTime)value);


        }

        public static ValidationResult ValidAppointmentDate(DateTime date) => DateTime.Now > date ?
            new ValidationResult("Cannot book appointment in past") : ValidationResult.Success;


    }
}
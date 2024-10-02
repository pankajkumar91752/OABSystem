using Newtonsoft.Json.Linq;
using OABSystem.Data;
using System.ComponentModel.DataAnnotations;
namespace OABSystem.Models
{
    public class Appointment : IValidatableObject
    {
        //[Required(ErrorMessage = "Appointment ID is required")]
        [Key]
        public int AppointmentId { get; set; }

        //[Required(ErrorMessage = "Healthcare professional is required")]
        // public string HealthcareProfessionalId { get; set; }

        [Required(ErrorMessage = "Appointment date and time are required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(Appointment), method: nameof(ValidAppointmentDate))]
        public DateTime AppointmentDateTime { get; set; }

        [Required(ErrorMessage = "Patient name is required")]
        [StringLength(50, ErrorMessage = "Patient name cannot exceed 50 characters")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Patient contact is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Patient contact must be a 10-digit number")]
        public string PatientContact { get; set; }

        [Required(ErrorMessage = "Reason for appointment is required")]
        [StringLength(255, ErrorMessage = "Reason for appointment cannot exceed 255 characters")]
        public string ReasonForAppointment { get; set; }

        public HealthcareProfessional? HealthcareProfessional { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => ValidateAppointmentDateTime();

        public IEnumerable<ValidationResult> ValidateAppointmentDateTime() => new List<ValidationResult>
            {
                ValidAppointmentDate(AppointmentDateTime),
                ValidateAppointmentAvailbilty()
            };

        private ValidationResult ValidateAppointmentAvailbilty()
        {
            var r = HealthcareProfessional.Appointments.Any(a => a.AppointmentDateTime < AppointmentDateTime
            && a.AppointmentDateTime.AddMinutes(DefaultAppointmentMinutes) > AppointmentDateTime);
            return r == true ? new ValidationResult($"Schduled appointment time is not avalible at {AppointmentDateTime} with {this.HealthcareProfessional.Name}") : ValidationResult.Success;
        }

        private const double DefaultAppointmentMinutes = 30;
        public static ValidationResult ValidAppointmentDate(DateTime date) => DateTime.Today.AddDays(1) > date ?
            new ValidationResult("Cannot book appointment in past") : ValidationResult.Success;
    }
}
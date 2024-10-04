using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Models;

[Table("Appointment")]
[Index("HealthcareProfessionalId", Name = "IX_Appointment_HealthcareProfessionalId1")]
public partial class Appointment:IValidatableObject
{
    [Key]
    public int AppointmentId { get; set; }
    
    public DateTime AppointmentDateTime { get; set; }

    [Required]
    [StringLength(50)]
    public string PatientName { get; set; }

    [Required]
    public string PatientContact { get; set; }

    [Required]
    [StringLength(255)]
    public string ReasonForAppointment { get; set; }

    public int HealthcareProfessionalId { get; set; }

    [StringLength(50)]
    public string? UserName { get; set; }

    [ForeignKey("HealthcareProfessionalId")]
    [InverseProperty("Appointments")]
    public virtual HealthcareProfessional HealthcareProfessional { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(AppointmentDateTime < DateTime.Today.AddDays(1))
            yield return new ValidationResult("Appointment Date can not before tomorrow", new[] { nameof(AppointmentDateTime) });

      
    }
}

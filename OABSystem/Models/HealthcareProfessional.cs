using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace OABSystem.Models
{
    public class HealthcareProfessional
    {
        [Key]
        public int Id { get; set; } // Primary key, consider using a GUID
       
        [Required(ErrorMessage = "HealthcareProfessional name is required")]
        [StringLength(50, ErrorMessage = "HealthcareProfessional name cannot exceed 50 characters")]
        public string Name { get; set; }
        [AllowNull]
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Models;

[Table("HealthcareProfessional")]
public partial class HealthcareProfessional
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [InverseProperty("HealthcareProfessional")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

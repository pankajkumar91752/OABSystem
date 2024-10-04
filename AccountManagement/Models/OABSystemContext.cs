using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Models;

public partial class OABSystemContext : DbContext
{
    public OABSystemContext()
    {
    }

    public OABSystemContext(DbContextOptions<OABSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<HealthcareProfessional> HealthcareProfessionals { get; set; }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HealthcareProfessional>().HasData(
             
              new HealthcareProfessional
              {
                  Id = 21,
                  Name = "Dr. Kumar"

              },
              new HealthcareProfessional
              {
                  Id = 22,
                  Name = "Dr. N",

              }
              );

       modelBuilder.Entity<Appointment>().HasData(
               new Appointment
               {
                   AppointmentId =1,
                   AppointmentDateTime = new DateTime(2024, 10, 5, 14, 30, 0),
                   PatientName = "John Doe",
                   PatientContact = "1234567890",
                   ReasonForAppointment = "Annual check-up"
               }
           );
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasOne(d => d.HealthcareProfessional).WithMany(p => p.Appointments).HasConstraintName("FK_Appointment_HealthcareProfessional_HealthcareProfessionalId1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

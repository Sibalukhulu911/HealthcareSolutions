using HealthcareSolutions.API.Models;
using Microsoft.EntityFrameworkCore;
using HealthcareSolutions.API.Data;

namespace HealthcareSolutions.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Practitioner> Practitioners => Set<Practitioner>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<VisitNote> VisitNotes => Set<VisitNote>();
    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<MedicationRequest> MedicationRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureUser(modelBuilder);
        ConfigurePatient(modelBuilder);
        ConfigurePractitioner(modelBuilder);
        ConfigureAppointment(modelBuilder);
        ConfigureMedicalRecord(modelBuilder);
        ConfigureVisitNote(modelBuilder);
        ConfigureMedication(modelBuilder);
        ConfigurePrescription(modelBuilder);

        modelBuilder.Entity<MedicationRequest>()
            .HasOne(r => r.Practitioner)
            .WithMany()
            .HasForeignKey(r => r.PractitionerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicationRequest>()
            .HasOne(r => r.ApprovedMedication)
            .WithMany()
            .HasForeignKey(r => r.ApprovedMedicationId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);

            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(u => u.Username)
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .IsRequired();

            entity.Property(u => u.Role)
                .IsRequired();

            entity.Property(u => u.IsActive)
                .IsRequired();

            entity.Property(u => u.CreatedAt)
                .IsRequired();
        });
    }

    private static void ConfigurePatient(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(p => p.PatientId);

            entity.Property(p => p.PatientFirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.PatientLastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(p => p.Email)
                .HasMaxLength(150);

            entity.Property(p => p.Address)
                .HasMaxLength(300);

            entity.Property(p => p.CreatedAt)
                .IsRequired();

            entity.Property(p => p.UpdatedAt)
                .IsRequired();
        });
    }

    private static void ConfigurePractitioner(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Practitioner>(entity =>
        {
            entity.HasKey(p => p.PractitionerId);

            entity.Property(p => p.PractitionerFirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.PractitionerLastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Specialty)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.OfficeNumber)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasOne(p => p.User)
                .WithOne(u => u.Practitioner)
                .HasForeignKey<Practitioner>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureAppointment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(a => a.AppointmentId);

            entity.Property(a => a.Status)
                .IsRequired();

            entity.Property(a => a.Reason)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(a => a.ScheduledAt)
                .IsRequired();

            entity.Property(a => a.CreatedAt)
                .IsRequired();

            entity.Property(a => a.UpdatedAt)
                .IsRequired();

            entity.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Practitioner)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PractitionerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureMedicalRecord(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(m => m.MedicalRecordId);

            entity.HasIndex(m => m.PatientId)
                .IsUnique();

            entity.HasOne(m => m.Patient)
                .WithOne(p => p.MedicalRecord)
                .HasForeignKey<MedicalRecord>(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(m => m.CreatedAt)
                .IsRequired();

            entity.Property(m => m.UpdatedAt)
                .IsRequired();
        });
    }

    private static void ConfigureVisitNote(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VisitNote>(entity =>
        {
            entity.HasKey(v => v.VisitNoteId);

            entity.Property(v => v.Notes)
                .IsRequired();

            entity.Property(v => v.Diagnosis)
                .HasMaxLength(1000);

            entity.Property(v => v.Treatment)
                .HasMaxLength(1000);

            entity.Property(v => v.CreatedAt)
                .IsRequired();

            entity.HasOne(v => v.MedicalRecord)
                .WithMany(m => m.VisitNotes)
                .HasForeignKey(v => v.MedicalRecordId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.Practitioner)
                .WithMany(p => p.VisitNotes)
                .HasForeignKey(v => v.PractitionerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.Appointment)
                .WithOne(a => a.VisitNote)
                .HasForeignKey<VisitNote>(v => v.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureMedication(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(m => m.MedicationId);

            entity.Property(m => m.MedicationName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(m => m.Description)
                .HasMaxLength(500);

            entity.Property(m => m.Dosage)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(m => m.IsAvailable)
                .IsRequired();
        });
    }

    private static void ConfigurePrescription(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(p => p.PrescriptionId);

            entity.Property(p => p.Dosage)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Frequency)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Duration)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Instructions)
                .HasMaxLength(1000);

            entity.Property(p => p.Status)
                .IsRequired();

            entity.Property(p => p.CreatedAt)
                .IsRequired();

            entity.HasOne(p => p.MedicalRecord)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(p => p.MedicalRecordId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Practitioner)
                .WithMany(pr => pr.Prescriptions)
                .HasForeignKey(p => p.PractitionerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Appointment)
                .WithMany(a => a.Prescriptions)
                .HasForeignKey(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Medication)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(p => p.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

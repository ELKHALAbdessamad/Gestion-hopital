using HospitalManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medecin> Medecins { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<DossierMedical> DossiersMedicaux { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Patient
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Prenom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DateNaissance).IsRequired();
                entity.Property(e => e.Sexe).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Telephone).IsRequired();
                entity.Property(e => e.Adresse).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.NumeroSecuriteSociale).HasMaxLength(20);

                entity.HasIndex(e => e.NumeroSecuriteSociale)
                      .IsUnique()
                      .HasFilter("[NumeroSecuriteSociale] IS NOT NULL");
            });

            // Medecin
            modelBuilder.Entity<Medecin>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nom)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Prenom)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Specialite)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.NumeroLicence)
                      .HasMaxLength(50);

                entity.Property(e => e.DateEmbauche)
                      .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.NumeroLicence)
                      .IsUnique()
                      .HasFilter("[NumeroLicence] IS NOT NULL");

                entity.Property(e => e.ServiceId)
                      .IsRequired();

                entity.HasOne(e => e.Service)
                      .WithMany(s => s.Medecins)
                      .HasForeignKey(e => e.ServiceId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.RendezVous)
                      .WithOne(r => r.Medecin)
                      .HasForeignKey(r => r.MedecinId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.DossiersMedicaux)
                      .WithOne(d => d.Medecin)
                      .HasForeignKey(d => d.MedecinId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Service
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
            });

            // RendezVous
            modelBuilder.Entity<RendezVous>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.RendezVous)
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Medecin)
                    .WithMany(m => m.RendezVous)
                    .HasForeignKey(e => e.MedecinId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.MedecinId, e.DateHeure });
            });

            // DossierMedical
            modelBuilder.Entity<DossierMedical>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.DossiersMedicaux)
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Medecin)
                    .WithMany(m => m.DossiersMedicaux)
                    .HasForeignKey(e => e.MedecinId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ApplicationUser
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(e => e.Patient)
                    .WithMany()
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(e => e.Medecin)
                    .WithMany()
                    .HasForeignKey(e => e.MedecinId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });
        }
    }
}
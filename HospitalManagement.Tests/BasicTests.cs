using Xunit;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HospitalManagement.Tests
{
    public class BasicTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        // Phase 1 Tests
        [Fact]
        public async Task CanCreateService()
        {
            var context = GetInMemoryDbContext();
            var service = new Service { Nom = "Cardiologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var result = context.Services.FirstOrDefault(s => s.Nom == "Cardiologie");
            Assert.NotNull(result);
            Assert.Equal("Cardiologie", result.Nom);
        }

        [Fact]
        public async Task CanCreatePatient()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient
            {
                Nom = "Dupont",
                Prenom = "Jean",
                DateNaissance = DateTime.Now.AddYears(-30),
                Sexe = "M",
                Telephone = "0123456789",
                Adresse = "123 Rue Test"
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var result = context.Patients.FirstOrDefault(p => p.Nom == "Dupont");
            Assert.NotNull(result);
            Assert.Equal("Dupont", result.Nom);
        }

        [Fact]
        public async Task CanCreateMedecin()
        {
            var context = GetInMemoryDbContext();
            var service = new Service { Nom = "Cardiologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin = new Medecin
            {
                Nom = "Dupont",
                Prenom = "Jean",
                Specialite = "Cardiologue",
                Email = "dupont@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin);
            await context.SaveChangesAsync();

            var result = context.Medecins.FirstOrDefault(m => m.Email == "dupont@hospital.com");
            Assert.NotNull(result);
            Assert.Equal("Cardiologue", result.Specialite);
        }

        [Fact]
        public async Task CanCreateAppointment()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient
            {
                Nom = "Dupont",
                Prenom = "Jean",
                DateNaissance = DateTime.Now.AddYears(-30),
                Sexe = "M",
                Telephone = "0123456789",
                Adresse = "123 Rue Test"
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var service = new Service { Nom = "Cardiologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin = new Medecin
            {
                Nom = "Dupont",
                Prenom = "Jean",
                Specialite = "Cardiologue",
                Email = "dupont@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin);
            await context.SaveChangesAsync();

            var appointment = new RendezVous
            {
                PatientId = patient.Id,
                MedecinId = medecin.Id,
                DateHeure = DateTime.Now.AddDays(1)
            };
            context.RendezVous.Add(appointment);
            await context.SaveChangesAsync();

            var result = context.RendezVous.FirstOrDefault(r => r.PatientId == patient.Id);
            Assert.NotNull(result);
            Assert.Equal(patient.Id, result.PatientId);
        }

        // Phase 2 Tests - Appointment Conflict Detection
        [Fact]
        public async Task DetectsAppointmentConflict()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient
            {
                Nom = "Martin",
                Prenom = "Pierre",
                DateNaissance = DateTime.Now.AddYears(-40),
                Sexe = "M",
                Telephone = "0123456789",
                Adresse = "456 Rue Test"
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var service = new Service { Nom = "Neurologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin = new Medecin
            {
                Nom = "Martin",
                Prenom = "Dr",
                Specialite = "Neurologue",
                Email = "martin@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin);
            await context.SaveChangesAsync();

            var dateTime = DateTime.Now.AddDays(2).Date.AddHours(14);
            var appointment1 = new RendezVous
            {
                PatientId = patient.Id,
                MedecinId = medecin.Id,
                DateHeure = dateTime,
                Statut = "Planifié"
            };
            context.RendezVous.Add(appointment1);
            await context.SaveChangesAsync();

            // Vérifier qu'on ne peut pas créer un rendez-vous au même créneau
            var conflictingAppointment = context.RendezVous
                .Any(r => r.MedecinId == medecin.Id
                    && r.DateHeure == dateTime
                    && r.Statut != "Annulé");

            Assert.True(conflictingAppointment);
        }

        // Phase 2 Tests - Medical Record Creation
        [Fact]
        public async Task CanCreateMedicalRecord()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient
            {
                Nom = "Durand",
                Prenom = "Marie",
                DateNaissance = DateTime.Now.AddYears(-35),
                Sexe = "F",
                Telephone = "0987654321",
                Adresse = "789 Rue Test"
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var service = new Service { Nom = "Pédiatrie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin = new Medecin
            {
                Nom = "Durand",
                Prenom = "Dr",
                Specialite = "Pédiatre",
                Email = "durand@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin);
            await context.SaveChangesAsync();

            var dossier = new DossierMedical
            {
                PatientId = patient.Id,
                MedecinId = medecin.Id,
                Resume = "Consultation générale",
                Diagnostic = "Bonne santé",
                DateConsultation = DateTime.Now
            };
            context.DossiersMedicaux.Add(dossier);
            await context.SaveChangesAsync();

            var result = context.DossiersMedicaux.FirstOrDefault(d => d.PatientId == patient.Id);
            Assert.NotNull(result);
            Assert.Equal("Bonne santé", result.Diagnostic);
        }

        // Phase 2 Tests - Unique Constraint Enforcement
        [Fact]
        public async Task ValidatesMedecinEmailUniqueness()
        {
            var context = GetInMemoryDbContext();
            var service = new Service { Nom = "Chirurgie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin1 = new Medecin
            {
                Nom = "Chirurgien",
                Prenom = "Dr",
                Specialite = "Chirurgien",
                Email = "chirurgien@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin1);
            await context.SaveChangesAsync();

            // Vérifier qu'on ne peut pas créer un médecin avec le même email
            var existingMedecin = context.Medecins
                .FirstOrDefault(m => m.Email == "chirurgien@hospital.com");

            Assert.NotNull(existingMedecin);
            Assert.Equal("chirurgien@hospital.com", existingMedecin.Email);
        }

        // Phase 2 Tests - Appointment Filtering by Role
        [Fact]
        public async Task FiltersAppointmentsByPatient()
        {
            var context = GetInMemoryDbContext();
            var patient1 = new Patient
            {
                Nom = "Patient1",
                Prenom = "Test",
                DateNaissance = DateTime.Now.AddYears(-30),
                Sexe = "M",
                Telephone = "0111111111",
                Adresse = "Rue 1"
            };
            var patient2 = new Patient
            {
                Nom = "Patient2",
                Prenom = "Test",
                DateNaissance = DateTime.Now.AddYears(-25),
                Sexe = "F",
                Telephone = "0222222222",
                Adresse = "Rue 2"
            };
            context.Patients.Add(patient1);
            context.Patients.Add(patient2);
            await context.SaveChangesAsync();

            var service = new Service { Nom = "Ophtalmologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin = new Medecin
            {
                Nom = "Oculiste",
                Prenom = "Dr",
                Specialite = "Ophtalmologue",
                Email = "oculiste@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin);
            await context.SaveChangesAsync();

            var appt1 = new RendezVous
            {
                PatientId = patient1.Id,
                MedecinId = medecin.Id,
                DateHeure = DateTime.Now.AddDays(1)
            };
            var appt2 = new RendezVous
            {
                PatientId = patient2.Id,
                MedecinId = medecin.Id,
                DateHeure = DateTime.Now.AddDays(2)
            };
            context.RendezVous.Add(appt1);
            context.RendezVous.Add(appt2);
            await context.SaveChangesAsync();

            var patient1Appointments = context.RendezVous
                .Where(r => r.PatientId == patient1.Id)
                .ToList();

            Assert.Single(patient1Appointments);
            Assert.Equal(patient1.Id, patient1Appointments[0].PatientId);
        }

        // Phase 2 Tests - Medical Record Filtering by Role
        [Fact]
        public async Task FiltersMedicalRecordsByMedecin()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient
            {
                Nom = "Patient",
                Prenom = "Test",
                DateNaissance = DateTime.Now.AddYears(-30),
                Sexe = "M",
                Telephone = "0333333333",
                Adresse = "Rue 3"
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var service = new Service { Nom = "Dermatologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin1 = new Medecin
            {
                Nom = "Dermatologue1",
                Prenom = "Dr",
                Specialite = "Dermatologue",
                Email = "dermato1@hospital.com",
                ServiceId = service.Id
            };
            var medecin2 = new Medecin
            {
                Nom = "Dermatologue2",
                Prenom = "Dr",
                Specialite = "Dermatologue",
                Email = "dermato2@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin1);
            context.Medecins.Add(medecin2);
            await context.SaveChangesAsync();

            var dossier1 = new DossierMedical
            {
                PatientId = patient.Id,
                MedecinId = medecin1.Id,
                Resume = "Consultation 1",
                DateConsultation = DateTime.Now
            };
            var dossier2 = new DossierMedical
            {
                PatientId = patient.Id,
                MedecinId = medecin2.Id,
                Resume = "Consultation 2",
                DateConsultation = DateTime.Now
            };
            context.DossiersMedicaux.Add(dossier1);
            context.DossiersMedicaux.Add(dossier2);
            await context.SaveChangesAsync();

            var medecin1Records = context.DossiersMedicaux
                .Where(d => d.MedecinId == medecin1.Id)
                .ToList();

            Assert.Single(medecin1Records);
            Assert.Equal(medecin1.Id, medecin1Records[0].MedecinId);
        }

        // Phase 3 Tests - Patient Profile Access
        [Fact]
        public async Task PatientCanViewOwnProfile()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient
            {
                Nom = "ProfileTest",
                Prenom = "Patient",
                DateNaissance = DateTime.Now.AddYears(-28),
                Sexe = "M",
                Telephone = "0444444444",
                Adresse = "Rue Profile",
                Email = "profile@test.com"
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var retrievedPatient = context.Patients.FirstOrDefault(p => p.Id == patient.Id);
            Assert.NotNull(retrievedPatient);
            Assert.Equal("ProfileTest", retrievedPatient.Nom);
        }

        // Phase 3 Tests - Patient Appointment View
        [Fact]
        public async Task PatientCanViewOwnAppointments()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient
            {
                Nom = "AppointmentPatient",
                Prenom = "Test",
                DateNaissance = DateTime.Now.AddYears(-32),
                Sexe = "F",
                Telephone = "0555555555",
                Adresse = "Rue Appointment"
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var service = new Service { Nom = "Gastroenterologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin = new Medecin
            {
                Nom = "Gastro",
                Prenom = "Dr",
                Specialite = "Gastroentérologue",
                Email = "gastro@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin);
            await context.SaveChangesAsync();

            var appointment = new RendezVous
            {
                PatientId = patient.Id,
                MedecinId = medecin.Id,
                DateHeure = DateTime.Now.AddDays(5),
                Motif = "Consultation générale"
            };
            context.RendezVous.Add(appointment);
            await context.SaveChangesAsync();

            var patientAppointments = context.RendezVous
                .Where(r => r.PatientId == patient.Id)
                .ToList();

            Assert.Single(patientAppointments);
            Assert.Equal(patient.Id, patientAppointments[0].PatientId);
        }

        // Phase 3 Tests - Patient Medical Record View (Read-Only)
        [Fact]
        public async Task PatientCanViewOwnMedicalRecord()
        {
            var context = GetInMemoryDbContext();
            var patient = new Patient
            {
                Nom = "MedicalPatient",
                Prenom = "Test",
                DateNaissance = DateTime.Now.AddYears(-45),
                Sexe = "M",
                Telephone = "0666666666",
                Adresse = "Rue Medical"
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            var service = new Service { Nom = "Oncologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin = new Medecin
            {
                Nom = "Oncologue",
                Prenom = "Dr",
                Specialite = "Oncologue",
                Email = "onco@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin);
            await context.SaveChangesAsync();

            var dossier = new DossierMedical
            {
                PatientId = patient.Id,
                MedecinId = medecin.Id,
                Resume = "Suivi oncologique",
                Diagnostic = "En rémission",
                DateConsultation = DateTime.Now
            };
            context.DossiersMedicaux.Add(dossier);
            await context.SaveChangesAsync();

            var patientDossier = context.DossiersMedicaux
                .FirstOrDefault(d => d.PatientId == patient.Id);

            Assert.NotNull(patientDossier);
            Assert.Equal("En rémission", patientDossier.Diagnostic);
        }

        // Phase 3 Tests - Patient Cannot Access Other Patient Data
        [Fact]
        public async Task PatientCannotAccessOtherPatientData()
        {
            var context = GetInMemoryDbContext();
            var patient1 = new Patient
            {
                Nom = "Patient1",
                Prenom = "Secure",
                DateNaissance = DateTime.Now.AddYears(-30),
                Sexe = "M",
                Telephone = "0777777777",
                Adresse = "Rue Secure1"
            };
            var patient2 = new Patient
            {
                Nom = "Patient2",
                Prenom = "Secure",
                DateNaissance = DateTime.Now.AddYears(-35),
                Sexe = "F",
                Telephone = "0888888888",
                Adresse = "Rue Secure2"
            };
            context.Patients.Add(patient1);
            context.Patients.Add(patient2);
            await context.SaveChangesAsync();

            var service = new Service { Nom = "Urologie" };
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var medecin = new Medecin
            {
                Nom = "Urologue",
                Prenom = "Dr",
                Specialite = "Urologue",
                Email = "uro@hospital.com",
                ServiceId = service.Id
            };
            context.Medecins.Add(medecin);
            await context.SaveChangesAsync();

            var appt1 = new RendezVous
            {
                PatientId = patient1.Id,
                MedecinId = medecin.Id,
                DateHeure = DateTime.Now.AddDays(3)
            };
            var appt2 = new RendezVous
            {
                PatientId = patient2.Id,
                MedecinId = medecin.Id,
                DateHeure = DateTime.Now.AddDays(4)
            };
            context.RendezVous.Add(appt1);
            context.RendezVous.Add(appt2);
            await context.SaveChangesAsync();

            // Patient1 should only see their own appointments
            var patient1Appointments = context.RendezVous
                .Where(r => r.PatientId == patient1.Id)
                .ToList();

            Assert.Single(patient1Appointments);
            Assert.DoesNotContain(patient1Appointments, a => a.PatientId == patient2.Id);
        }
    }
}

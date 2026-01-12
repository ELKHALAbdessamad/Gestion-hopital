using HospitalManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            Console.WriteLine("🔄 Chargement des données de test...");

            // Vérifier si les données existent déjà
            bool hasData = context.Services.Any() || context.Patients.Any() || context.Medecins.Any();
            
            if (hasData)
            {
                Console.WriteLine("⚠️ Suppression des anciennes données...");
                
                // Supprimer dans l'ordre pour respecter les contraintes de clés étrangères
                context.DossiersMedicaux.RemoveRange(context.DossiersMedicaux);
                context.RendezVous.RemoveRange(context.RendezVous);
                context.Medecins.RemoveRange(context.Medecins);
                context.Patients.RemoveRange(context.Patients);
                context.Services.RemoveRange(context.Services);
                
                await context.SaveChangesAsync();
                Console.WriteLine("✅ Anciennes données supprimées.");
            }

            // ===== SERVICES =====
            var services = new List<Service>
            {
                new Service
                {
                    Nom = "Cardiologie",
                    Description = "Spécialité médicale dédiée aux maladies du cœur et du système cardiovasculaire",
                    CapaciteAccueil = 50,
                    Emplacement = "Bâtiment A, Étage 2",
                    Telephone = "+212 5 29 11 11 11"
                },
                new Service
                {
                    Nom = "Pédiatrie",
                    Description = "Soins médicaux spécialisés pour les enfants et les nourrissons",
                    CapaciteAccueil = 40,
                    Emplacement = "Bâtiment B, Étage 1",
                    Telephone = "+212 5 29 11 11 12"
                },
                new Service
                {
                    Nom = "Urgences",
                    Description = "Service d'urgence disponible 24h/24, 7j/7 pour les cas critiques",
                    CapaciteAccueil = 100,
                    Emplacement = "Rez-de-chaussée, Entrée principale",
                    Telephone = "+212 5 29 11 11 13"
                },
                new Service
                {
                    Nom = "Chirurgie Générale",
                    Description = "Interventions chirurgicales générales et spécialisées",
                    CapaciteAccueil = 30,
                    Emplacement = "Bâtiment C, Étage 3",
                    Telephone = "+212 5 29 11 11 14"
                },
                new Service
                {
                    Nom = "Orthopédie",
                    Description = "Traitement des maladies et blessures des os et articulations",
                    CapaciteAccueil = 35,
                    Emplacement = "Bâtiment D, Étage 2",
                    Telephone = "+212 5 29 11 11 15"
                },
                new Service
                {
                    Nom = "Dermatologie",
                    Description = "Spécialité médicale des maladies de la peau",
                    CapaciteAccueil = 25,
                    Emplacement = "Bâtiment A, Étage 1",
                    Telephone = "+212 5 29 11 11 16"
                }
            };

            context.Services.AddRange(services);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ {services.Count} services créés.");

            // ===== PATIENTS =====
            var patients = new List<Patient>
            {
                new Patient
                {
                    Nom = "Benani",
                    Prenom = "Ahmed",
                    DateNaissance = new DateTime(1985, 5, 15),
                    Sexe = "M",
                    Telephone = "+212 6 12 34 56 78",
                    Email = "ahmed.benani@email.com",
                    Adresse = "123 Rue de la Paix, Casablanca",
                    NumeroSecuriteSociale = "1234567890",
                    DateInscription = DateTime.Now.AddMonths(-6)
                },
                new Patient
                {
                    Nom = "Alaoui",
                    Prenom = "Fatima",
                    DateNaissance = new DateTime(1990, 8, 22),
                    Sexe = "F",
                    Telephone = "+212 6 23 45 67 89",
                    Email = "fatima.alaoui@email.com",
                    Adresse = "456 Avenue Mohammed V, Rabat",
                    NumeroSecuriteSociale = "0987654321",
                    DateInscription = DateTime.Now.AddMonths(-4)
                },
                new Patient
                {
                    Nom = "Bouazza",
                    Prenom = "Mohamed",
                    DateNaissance = new DateTime(1988, 3, 10),
                    Sexe = "M",
                    Telephone = "+212 6 34 56 78 90",
                    Email = "mohamed.bouazza@email.com",
                    Adresse = "789 Boulevard Zerktouni, Casablanca",
                    NumeroSecuriteSociale = "1122334455",
                    DateInscription = DateTime.Now.AddMonths(-3)
                },
                new Patient
                {
                    Nom = "Chaoui",
                    Prenom = "Leila",
                    DateNaissance = new DateTime(1992, 11, 28),
                    Sexe = "F",
                    Telephone = "+212 6 45 67 89 01",
                    Email = "leila.chaoui@email.com",
                    Adresse = "321 Rue Tarik Ibn Ziad, Fes",
                    NumeroSecuriteSociale = "5566778899",
                    DateInscription = DateTime.Now.AddMonths(-2)
                },
                new Patient
                {
                    Nom = "Darif",
                    Prenom = "Hassan",
                    DateNaissance = new DateTime(1980, 7, 5),
                    Sexe = "M",
                    Telephone = "+212 6 56 78 90 12",
                    Email = "hassan.darif@email.com",
                    Adresse = "654 Avenue Hassan II, Marrakech",
                    NumeroSecuriteSociale = "9988776655",
                    DateInscription = DateTime.Now.AddMonths(-1)
                }
            };

            context.Patients.AddRange(patients);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ {patients.Count} patients créés.");

            // ===== MEDECINS =====
            var medecins = new List<Medecin>
            {
                new Medecin
                {
                    Nom = "Bennani",
                    Prenom = "Dr. Karim",
                    Specialite = "Cardiologie",
                    Email = "karim.bennani@hospital.com",
                    NumeroLicence = "MED001",
                    DateEmbauche = DateTime.Now.AddYears(-5),
                    ServiceId = services[0].Id
                },
                new Medecin
                {
                    Nom = "Idrissi",
                    Prenom = "Dr. Nadia",
                    Specialite = "Pédiatrie",
                    Email = "nadia.idrissi@hospital.com",
                    NumeroLicence = "MED002",
                    DateEmbauche = DateTime.Now.AddYears(-4),
                    ServiceId = services[1].Id
                },
                new Medecin
                {
                    Nom = "Fassi",
                    Prenom = "Dr. Rachid",
                    Specialite = "Chirurgie Générale",
                    Email = "rachid.fassi@hospital.com",
                    NumeroLicence = "MED003",
                    DateEmbauche = DateTime.Now.AddYears(-6),
                    ServiceId = services[3].Id
                },
                new Medecin
                {
                    Nom = "Tazi",
                    Prenom = "Dr. Amina",
                    Specialite = "Orthopédie",
                    Email = "amina.tazi@hospital.com",
                    NumeroLicence = "MED004",
                    DateEmbauche = DateTime.Now.AddYears(-3),
                    ServiceId = services[4].Id
                },
                new Medecin
                {
                    Nom = "Rami",
                    Prenom = "Dr. Jamal",
                    Specialite = "Dermatologie",
                    Email = "jamal.rami@hospital.com",
                    NumeroLicence = "MED005",
                    DateEmbauche = DateTime.Now.AddYears(-2),
                    ServiceId = services[5].Id
                },
                new Medecin
                {
                    Nom = "Kabbaj",
                    Prenom = "Dr. Samir",
                    Specialite = "Urgences",
                    Email = "samir.kabbaj@hospital.com",
                    NumeroLicence = "MED006",
                    DateEmbauche = DateTime.Now.AddYears(-7),
                    ServiceId = services[2].Id
                }
            };

            context.Medecins.AddRange(medecins);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ {medecins.Count} médecins créés.");

            // ===== RENDEZ-VOUS =====
            var rendezVous = new List<RendezVous>
            {
                new RendezVous
                {
                    PatientId = patients[0].Id,
                    MedecinId = medecins[0].Id,
                    DateHeure = DateTime.Now.AddDays(3).AddHours(10),
                    Motif = "Consultation cardiaque",
                    Statut = "Confirmé",
                    DateCreation = DateTime.Now
                },
                new RendezVous
                {
                    PatientId = patients[1].Id,
                    MedecinId = medecins[1].Id,
                    DateHeure = DateTime.Now.AddDays(5).AddHours(14),
                    Motif = "Visite pédiatrique",
                    Statut = "Confirmé",
                    DateCreation = DateTime.Now
                },
                new RendezVous
                {
                    PatientId = patients[2].Id,
                    MedecinId = medecins[2].Id,
                    DateHeure = DateTime.Now.AddDays(7).AddHours(9),
                    Motif = "Consultation pré-opératoire",
                    Statut = "Confirmé",
                    DateCreation = DateTime.Now
                },
                new RendezVous
                {
                    PatientId = patients[3].Id,
                    MedecinId = medecins[3].Id,
                    DateHeure = DateTime.Now.AddDays(2).AddHours(15),
                    Motif = "Consultation orthopédique",
                    Statut = "Confirmé",
                    DateCreation = DateTime.Now
                },
                new RendezVous
                {
                    PatientId = patients[4].Id,
                    MedecinId = medecins[4].Id,
                    DateHeure = DateTime.Now.AddDays(4).AddHours(11),
                    Motif = "Consultation dermatologique",
                    Statut = "Confirmé",
                    DateCreation = DateTime.Now
                },
                new RendezVous
                {
                    PatientId = patients[0].Id,
                    MedecinId = medecins[1].Id,
                    DateHeure = DateTime.Now.AddDays(1).AddHours(16),
                    Motif = "Suivi médical",
                    Statut = "En attente",
                    DateCreation = DateTime.Now
                }
            };

            context.RendezVous.AddRange(rendezVous);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ {rendezVous.Count} rendez-vous créés.");

            // ===== DOSSIERS MEDICAUX =====
            var dossiers = new List<DossierMedical>
            {
                new DossierMedical
                {
                    PatientId = patients[0].Id,
                    MedecinId = medecins[0].Id,
                    Resume = "Consultation cardiaque - Hypertension artérielle",
                    DateConsultation = DateTime.Now.AddMonths(-2),
                    Diagnostic = "Hypertension artérielle",
                    Traitement = "Antihypertenseur quotidien",
                    Observations = "Patient stable, suivi régulier recommandé"
                },
                new DossierMedical
                {
                    PatientId = patients[1].Id,
                    MedecinId = medecins[1].Id,
                    Resume = "Consultation pédiatrique - Otite moyenne",
                    DateConsultation = DateTime.Now.AddMonths(-1),
                    Diagnostic = "Otite moyenne",
                    Traitement = "Antibiotiques et anti-inflammatoires",
                    Observations = "Amélioration notable après 5 jours de traitement"
                },
                new DossierMedical
                {
                    PatientId = patients[2].Id,
                    MedecinId = medecins[2].Id,
                    Resume = "Consultation chirurgicale - Hernie discale",
                    DateConsultation = DateTime.Now.AddMonths(-3),
                    Diagnostic = "Hernie discale",
                    Traitement = "Intervention chirurgicale programmée",
                    Observations = "Préparation pré-opératoire en cours"
                },
                new DossierMedical
                {
                    PatientId = patients[3].Id,
                    MedecinId = medecins[3].Id,
                    Resume = "Consultation orthopédique - Fracture du poignet",
                    DateConsultation = DateTime.Now.AddMonths(-1),
                    Diagnostic = "Fracture du poignet",
                    Traitement = "Immobilisation et physiothérapie",
                    Observations = "Consolidation progressive, suivi hebdomadaire"
                },
                new DossierMedical
                {
                    PatientId = patients[4].Id,
                    MedecinId = medecins[4].Id,
                    Resume = "Consultation dermatologique - Dermatite allergique",
                    DateConsultation = DateTime.Now.AddMonths(-2),
                    Diagnostic = "Dermatite allergique",
                    Traitement = "Crème corticoïde et antihistaminiques",
                    Observations = "Amélioration après identification de l'allergène"
                },
                new DossierMedical
                {
                    PatientId = patients[0].Id,
                    MedecinId = medecins[0].Id,
                    Resume = "Suivi cardiaque régulier - Contrôle tension",
                    DateConsultation = DateTime.Now.AddMonths(-1),
                    Diagnostic = "Suivi cardiaque",
                    Traitement = "Continuation du traitement actuel",
                    Observations = "Résultats ECG normaux, tension artérielle contrôlée"
                }
            };

            context.DossiersMedicaux.AddRange(dossiers);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ {dossiers.Count} dossiers médicaux créés.");
            Console.WriteLine("🎉 Toutes les données de test ont été chargées avec succès !");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Medecin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(100)]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "La spécialité est obligatoire")]
        [StringLength(100)]
        public string Specialite { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        public string? NumeroLicence { get; set; }

        // ✅ ServiceId devient OBLIGATOIRE (non-nullable)
        [Required(ErrorMessage = "Le service est obligatoire")]
        public int ServiceId { get; set; }

        public DateTime DateEmbauche { get; set; } = DateTime.Now;

        // Relations (sans virtual)
        public Service? Service { get; set; }
        public ICollection<RendezVous> RendezVous { get; set; } = new List<RendezVous>();
        public ICollection<DossierMedical> DossiersMedicaux { get; set; } = new List<DossierMedical>();
    }
}
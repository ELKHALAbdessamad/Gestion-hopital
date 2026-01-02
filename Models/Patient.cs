using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(100)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "La date de naissance est obligatoire")]
        public DateTime DateNaissance { get; set; }

        [Required(ErrorMessage = "Le sexe est obligatoire")]
        [StringLength(20)]
        public string Sexe { get; set; }

        [Required(ErrorMessage = "Le téléphone est obligatoire")]
        [Phone(ErrorMessage = "Numéro de téléphone invalide")]
        public string Telephone { get; set; }

        [EmailAddress(ErrorMessage = "Email invalide")]
        public string? Email { get; set; }  // Optionnel avec ?

        [Required(ErrorMessage = "L'adresse est obligatoire")]
        [StringLength(200)]
        public string Adresse { get; set; }

        [StringLength(20)]
        public string? NumeroSecuriteSociale { get; set; }  // Optionnel avec ?

        public DateTime DateInscription { get; set; }

        // Relations
        public virtual ICollection<RendezVous>? RendezVous { get; set; }
        public virtual ICollection<DossierMedical>? DossiersMedicaux { get; set; }
    }
}
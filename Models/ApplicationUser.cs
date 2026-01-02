using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Prenom { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telephone { get; set; }

        [Required]
        public string Role { get; set; } = "Patient"; // Admin, Medecin, Receptionniste, Patient

        public DateTime DateInscription { get; set; } = DateTime.Now;

        // Relations optionnelles
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int? MedecinId { get; set; }
        public Medecin? Medecin { get; set; }
    }
}
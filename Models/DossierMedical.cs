using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class DossierMedical
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le patient est obligatoire")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Le médecin est obligatoire")]
        public int MedecinId { get; set; }

        [Required(ErrorMessage = "Le résumé est obligatoire")]
        [StringLength(1000)]
        public string Resume { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public DateTime DateConsultation { get; set; }

        [StringLength(500)]
        public string Diagnostic { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Traitement { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Observations { get; set; } = string.Empty;

        // Relations - ✅ RETIRER 'virtual'
        public Patient? Patient { get; set; }
        public Medecin? Medecin { get; set; }
    }
}
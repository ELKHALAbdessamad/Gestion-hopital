using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class RendezVous
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le patient est obligatoire")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Le médecin est obligatoire")]
        public int MedecinId { get; set; }

        [Required(ErrorMessage = "La date et l'heure sont obligatoires")]
        public DateTime DateHeure { get; set; }

        [Required(ErrorMessage = "Le statut est obligatoire")]
        [StringLength(50)]
        public string Statut { get; set; } = "Planifié";

        [StringLength(500)]
        public string? Motif { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public int Duree { get; set; } = 30;

        public DateTime DateCreation { get; set; } = DateTime.Now;

        // Relations - ✅ RETIRER 'virtual'
        public Patient? Patient { get; set; }
        public Medecin? Medecin { get; set; }
    }
}
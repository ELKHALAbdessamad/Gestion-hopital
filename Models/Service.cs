using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = null!;

        [StringLength(200)]
        public string? Description { get; set; }

        [Range(1, 1000)]
        public int CapaciteAccueil { get; set; } = 50;

        [StringLength(200)]
        public string? Emplacement { get; set; }

        [StringLength(20)]
        public string? Telephone { get; set; }

        public virtual ICollection<Medecin> Medecins { get; set; } = new List<Medecin>();
    }
}

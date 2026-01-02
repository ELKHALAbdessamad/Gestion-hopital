using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }
    }
}

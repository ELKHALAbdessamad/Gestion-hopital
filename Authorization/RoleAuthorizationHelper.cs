using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Authorization
{
    public static class RoleAuthorizationHelper
    {
        public const string ADMIN = "Admin";
        public const string MEDECIN = "Medecin";
        public const string PATIENT = "Patient";
        public const string RECEPTIONNISTE = "Receptionniste";

        /// <summary>
        /// Vérifie si l'utilisateur a accès à une page spécifique
        /// </summary>
        public static bool CanAccessPage(string userRole, string pageName)
        {
            var permissions = GetPagePermissions();
            
            if (!permissions.ContainsKey(pageName))
                return false;

            return permissions[pageName].Contains(userRole);
        }

        /// <summary>
        /// Retourne les permissions par page
        /// </summary>
        private static Dictionary<string, List<string>> GetPagePermissions()
        {
            return new Dictionary<string, List<string>>
            {
                // Pages accessibles par tous les rôles
                { "Profile", new List<string> { ADMIN, MEDECIN, PATIENT, RECEPTIONNISTE } },
                { "EditProfile", new List<string> { ADMIN, MEDECIN, PATIENT, RECEPTIONNISTE } },
                { "RendezVous", new List<string> { ADMIN, MEDECIN, PATIENT, RECEPTIONNISTE } },

                // Pages Patients - Accessibles par Admin et Receptionniste
                { "Patients", new List<string> { ADMIN, RECEPTIONNISTE } },
                { "PatientsCreate", new List<string> { ADMIN, RECEPTIONNISTE } },
                { "PatientsEdit", new List<string> { ADMIN, RECEPTIONNISTE } },
                { "PatientsDelete", new List<string> { ADMIN, RECEPTIONNISTE } },
                { "PatientsDetails", new List<string> { ADMIN, RECEPTIONNISTE } },

                // Pages Médecins - Accessibles par Admin et Médecins
                { "Medecins", new List<string> { ADMIN, MEDECIN } },
                { "MedecinsCreate", new List<string> { ADMIN } },
                { "MedecinsEdit", new List<string> { ADMIN } },
                { "MedecinsDelete", new List<string> { ADMIN } },
                { "MedecinsDetails", new List<string> { ADMIN, MEDECIN } },

                // Pages Services - Accessibles par Admin et Receptionniste
                { "Services", new List<string> { ADMIN, RECEPTIONNISTE } },
                { "ServicesCreate", new List<string> { ADMIN } },
                { "ServicesEdit", new List<string> { ADMIN } },
                { "ServicesDelete", new List<string> { ADMIN } },
                { "ServicesDetails", new List<string> { ADMIN, RECEPTIONNISTE } },

                // Pages Dossiers Médicaux - Accessibles par Admin et Médecins
                { "DossiersMedicaux", new List<string> { ADMIN, MEDECIN } },
                { "DossiersMedicauxCreate", new List<string> { ADMIN, MEDECIN } },
                { "DossiersMedicauxEdit", new List<string> { ADMIN, MEDECIN } },
                { "DossiersMedicauxDelete", new List<string> { ADMIN } },
                { "DossiersMedicauxDetails", new List<string> { ADMIN, MEDECIN } },
            };
        }
    }
}

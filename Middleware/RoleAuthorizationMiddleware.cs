using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Middleware
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        // Définir les routes protégées par rôle
        private static readonly Dictionary<string, List<string>> ProtectedRoutes = new()
        {
            // Patients - Accessible par Admin et Receptionniste
            { "/Patients", new List<string> { "Admin", "Receptionniste" } },
            { "/Patients/Create", new List<string> { "Admin", "Receptionniste" } },
            { "/Patients/Edit", new List<string> { "Admin", "Receptionniste" } },
            { "/Patients/Delete", new List<string> { "Admin", "Receptionniste" } },
            { "/Patients/Details", new List<string> { "Admin", "Receptionniste" } },

            // Médecins - Accessible par Admin
            { "/Medecins", new List<string> { "Admin" } },
            { "/Medecins/Create", new List<string> { "Admin" } },
            { "/Medecins/Edit", new List<string> { "Admin" } },
            { "/Medecins/Delete", new List<string> { "Admin" } },

            // Services - Accessible par Admin et Receptionniste
            { "/Services", new List<string> { "Admin", "Receptionniste" } },
            { "/Services/Create", new List<string> { "Admin" } },
            { "/Services/Edit", new List<string> { "Admin" } },
            { "/Services/Delete", new List<string> { "Admin" } },

            // Dossiers Médicaux - Accessible par Admin et Médecins
            { "/DossiersMedicaux", new List<string> { "Admin", "Medecin" } },
            { "/DossiersMedicaux/Create", new List<string> { "Admin", "Medecin" } },
            { "/DossiersMedicaux/Edit", new List<string> { "Admin", "Medecin" } },
            { "/DossiersMedicaux/Delete", new List<string> { "Admin" } },
        };

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? "";
            var userRole = context.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value ?? "";

            // Vérifier si la route est protégée
            var protectedRoute = ProtectedRoutes.FirstOrDefault(r => path.StartsWith(r.Key.ToLower()));

            if (!string.IsNullOrEmpty(protectedRoute.Key))
            {
                // Vérifier si l'utilisateur est authentifié
                if (!context.User.Identity?.IsAuthenticated ?? true)
                {
                    context.Response.Redirect("/Account/Login");
                    return;
                }

                // Vérifier si l'utilisateur a le rôle requis
                if (!protectedRoute.Value.Contains(userRole))
                {
                    context.Response.Redirect("/Home/Index");
                    return;
                }
            }

            await _next(context);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Page d'accueil - Redirige vers Dashboard si authentifié, sinon Welcome
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Rediriger vers le dashboard pour les utilisateurs authentifiés
                return RedirectToAction("Dashboard");
            }
            // Afficher la page Welcome pour les utilisateurs non authentifiés
            return RedirectToAction("Welcome");
        }

        // Page d'accueil publique
        [AllowAnonymous]
        public IActionResult Welcome()
        {
            return View();
        }

        // Page des spécialités et services
        [AllowAnonymous]
        public async Task<IActionResult> Specialties()
        {
            var services = await _context.Services
                .Include(s => s.Medecins)
                .ToListAsync();
            return View(services);
        }

        // Dashboard protégé
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var viewModel = new DashboardViewModel();

            try
            {
                viewModel.TotalPatients = await _context.Patients.CountAsync();
                viewModel.TotalMedecins = await _context.Medecins.CountAsync();
                viewModel.TotalServices = await _context.Services.CountAsync();

                var today = DateTime.Today;
                viewModel.RendezvousAujourdhui = await _context.RendezVous
                    .Where(r => r.DateHeure.Date == today)
                    .CountAsync();

                var weekAgo = today.AddDays(-6);
                var rdvParJour = await _context.RendezVous
                    .Where(r => r.DateHeure.Date >= weekAgo && r.DateHeure.Date <= today)
                    .GroupBy(r => r.DateHeure.Date)
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToListAsync();

                for (int i = 0; i < 7; i++)
                {
                    var date = weekAgo.AddDays(i);
                    viewModel.RendezvousParJour[i] = rdvParJour
                        .FirstOrDefault(r => r.Date == date)?.Count ?? 0;
                }

                var servicesStats = await _context.Services
                    .Include(s => s.Medecins)
                    .ThenInclude(m => m.RendezVous)
                    .Select(s => new
                    {
                        Nom = s.Nom,
                        Count = s.Medecins.SelectMany(m => m.RendezVous).Count()
                    })
                    .Where(s => s.Count > 0)
                    .OrderByDescending(s => s.Count)
                    .Take(5)
                    .ToListAsync();

                viewModel.ServicesLabels = servicesStats.Select(s => s.Nom).ToArray();
                viewModel.ServicesData = servicesStats.Select(s => s.Count).ToArray();

                viewModel.RecentActivities = await GetRecentActivities();

                var weekStart = today.AddDays(-(int)today.DayOfWeek);
                viewModel.RendezvousSemaine = await _context.RendezVous
                    .Where(r => r.DateHeure.Date >= weekStart)
                    .CountAsync();

                var monthStart = new DateTime(today.Year, today.Month, 1);
                viewModel.RendezvousMois = await _context.RendezVous
                    .Where(r => r.DateHeure.Date >= monthStart)
                    .CountAsync();

                viewModel.PatientsNouveau = await _context.Patients
                    .Where(p => p.Id > 0) // Ajustez selon votre logique
                    .CountAsync();
            }
            catch (Exception ex)
            {
                // Log l'erreur si nécessaire
                Console.WriteLine($"Erreur lors du chargement du dashboard: {ex.Message}");

                // Retourner un ViewModel vide en cas d'erreur
                viewModel = new DashboardViewModel
                {
                    TotalPatients = 0,
                    TotalMedecins = 0,
                    TotalServices = 0,
                    RendezvousAujourdhui = 0
                };
            }

            return View(viewModel);
        }

        private async Task<List<ActivityItem>> GetRecentActivities()
        {
            var activities = new List<ActivityItem>();

            try
            {
                // Derniers patients ajoutés
                var recentPatients = await _context.Patients
                    .OrderByDescending(p => p.Id)
                    .Take(2)
                    .ToListAsync();

                foreach (var patient in recentPatients)
                {
                    activities.Add(new ActivityItem
                    {
                        Type = "new",
                        Icon = "fas fa-user-plus",
                        Title = "Nouveau Patient Enregistré",
                        Description = $"{patient.Nom} {patient.Prenom} a été ajouté au système",
                        TimeAgo = GetTimeAgo(DateTime.Now) // Ajustez avec la vraie date de création si disponible
                    });
                }

                // Derniers rendez-vous
                var recentRdv = await _context.RendezVous
                    .Include(r => r.Medecin)
                    .OrderByDescending(r => r.Id)
                    .Take(2)
                    .ToListAsync();

                foreach (var rdv in recentRdv)
                {
                    activities.Add(new ActivityItem
                    {
                        Type = "update",
                        Icon = "fas fa-calendar-check",
                        Title = "Rendez-vous Programmé",
                        Description = $"Dr. {rdv.Medecin?.Nom} - {rdv.DateHeure:dd/MM/yyyy HH:mm}",
                        TimeAgo = GetTimeAgo(rdv.DateHeure)
                    });
                }

                // Vérifier les rendez-vous en attente (aujourd'hui)
                var pendingRdv = await _context.RendezVous
                    .Where(r => r.DateHeure.Date == DateTime.Today && r.DateHeure > DateTime.Now)
                    .CountAsync();

                if (pendingRdv > 0)
                {
                    activities.Add(new ActivityItem
                    {
                        Type = "alert",
                        Icon = "fas fa-exclamation-circle",
                        Title = "Rendez-vous à venir",
                        Description = $"{pendingRdv} rendez-vous programmés aujourd'hui",
                        TimeAgo = "Maintenant"
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des activités: {ex.Message}");
            }

            // Si pas d'activités, ajouter une activité par défaut
            if (!activities.Any())
            {
                activities.Add(new ActivityItem
                {
                    Type = "new",
                    Icon = "fas fa-info-circle",
                    Title = "Système Actif",
                    Description = "Le système de gestion hospitalière est opérationnel",
                    TimeAgo = "Maintenant"
                });
            }

            return activities.OrderByDescending(a => a.Timestamp).Take(5).ToList();
        }

        private string GetTimeAgo(DateTime date)
        {
            var timeSpan = DateTime.Now - date;

            if (timeSpan.TotalMinutes < 1)
                return "À l'instant";
            if (timeSpan.TotalMinutes < 60)
                return $"Il y a {(int)timeSpan.TotalMinutes} min";
            if (timeSpan.TotalHours < 24)
                return $"Il y a {(int)timeSpan.TotalHours}h";
            if (timeSpan.TotalDays < 7)
                return $"Il y a {(int)timeSpan.TotalDays}j";

            return date.ToString("dd/MM/yyyy");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
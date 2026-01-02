using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace HospitalManagement.Controllers
{
    [Authorize]
    public class RendezVousController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RendezVousController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: RendezVous
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return RedirectToAction("Login", "Account");

                IQueryable<RendezVous> query = _context.RendezVous
                    .Include(r => r.Patient)
                    .Include(r => r.Medecin);

                // Filtrer selon le rôle
                if (user.Role == "Patient" && user.PatientId.HasValue)
                {
                    query = query.Where(r => r.PatientId == user.PatientId.Value);
                }
                else if (user.Role == "Medecin" && user.MedecinId.HasValue)
                {
                    query = query.Where(r => r.MedecinId == user.MedecinId.Value);
                }
                // Admin et Receptionniste voient tous les rendez-vous

                var rendezVous = await query.OrderBy(r => r.DateHeure).ToListAsync();
                return View(rendezVous);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erreur lors du chargement des rendez-vous: {ex.Message}";
                return View(new List<RendezVous>());
            }
        }

        // GET: RendezVous/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID du rendez-vous non spécifié.";
                return NotFound();
            }

            try
            {
                var rendezVous = await _context.RendezVous
                    .Include(r => r.Patient)
                    .Include(r => r.Medecin)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (rendezVous == null)
                {
                    TempData["Error"] = "Rendez-vous introuvable.";
                    return NotFound();
                }

                return View(rendezVous);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans Details: {ex.Message}");
                TempData["Error"] = $"Erreur lors du chargement des détails: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: RendezVous/Create
        [Authorize(Roles = "Admin,Receptionniste")]
        public IActionResult Create()
        {
            try
            {
                var patients = _context.Patients.ToList();
                var medecins = _context.Medecins.ToList();

                ViewBag.Patients = patients;
                ViewBag.Medecins = medecins;

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans Create GET: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                TempData["Error"] = $"Erreur lors du chargement du formulaire: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RendezVous/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Create(RendezVous rendezVous)
        {
            string dateStr = Request.Form["DateRendezVous"];
            string heureStr = Request.Form["HeureRendezVous"];

            try
            {
                if (!string.IsNullOrEmpty(dateStr) && !string.IsNullOrEmpty(heureStr))
                {
                    try
                    {
                        // Combiner la date (YYYY-MM-DD) et l'heure (HH:mm)
                        string dateHeureStr = $"{dateStr} {heureStr}";
                        rendezVous.DateHeure = DateTime.ParseExact(dateHeureStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        ModelState.Remove("DateHeure");
                    }
                    catch (FormatException)
                    {
                        ModelState.AddModelError("DateHeure", "Format de date invalide. Utilisez le format AAAA-MM-JJ et HH:mm (ex: 2024-03-15 14:30)");
                    }
                }
                else
                {
                    ModelState.AddModelError("DateHeure", "La date et l'heure sont requises.");
                }

                // Supprimer les erreurs de validation pour les propriétés de navigation
                ModelState.Remove("Patient");
                ModelState.Remove("Medecin");

                if (ModelState.IsValid)
                {
                    // Vérifier si le médecin est disponible
                    var conflit = await _context.RendezVous
                        .AnyAsync(r => r.MedecinId == rendezVous.MedecinId
                            && r.DateHeure == rendezVous.DateHeure
                            && r.Statut != "Annulé");

                    if (conflit)
                    {
                        ModelState.AddModelError("DateHeure", "Ce créneau horaire n'est pas disponible pour ce médecin.");
                    }
                    else
                    {
                        rendezVous.DateCreation = DateTime.Now;
                        _context.Add(rendezVous);
                        await _context.SaveChangesAsync();

                        Console.WriteLine($"Rendez-vous créé avec succès - ID: {rendezVous.Id}");
                        TempData["Success"] = "Rendez-vous créé avec succès !";
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Erreur de validation: {error.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans Create POST: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                ModelState.AddModelError("", $"Erreur lors de l'enregistrement : {ex.Message}");
            }

            // Conserver la date et l'heure saisies pour l'affichage
            ViewBag.DateRendezVousSaisie = dateStr;
            ViewBag.HeureRendezVousSaisie = heureStr;

            // Recharger les listes
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Medecins = _context.Medecins.ToList();

            return View(rendezVous);
        }

        // GET: RendezVous/Edit/5
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID du rendez-vous non spécifié.";
                return NotFound();
            }

            try
            {
                var rendezVous = await _context.RendezVous.FindAsync(id);
                if (rendezVous == null)
                {
                    TempData["Error"] = "Rendez-vous introuvable.";
                    return NotFound();
                }

                ViewBag.DateRendezVousSaisie = rendezVous.DateHeure.ToString("yyyy-MM-dd");
                ViewBag.HeureRendezVousSaisie = rendezVous.DateHeure.ToString("HH:mm");
                ViewBag.Patients = _context.Patients.ToList();
                ViewBag.Medecins = _context.Medecins.ToList();

                return View(rendezVous);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans Edit GET: {ex.Message}");
                TempData["Error"] = $"Erreur lors du chargement: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RendezVous/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Edit(int id, RendezVous rendezVous)
        {
            if (id != rendezVous.Id)
            {
                TempData["Error"] = "ID du rendez-vous incorrect.";
                return NotFound();
            }

            string dateStr = Request.Form["DateRendezVous"];
            string heureStr = Request.Form["HeureRendezVous"];

            try
            {
                if (!string.IsNullOrEmpty(dateStr) && !string.IsNullOrEmpty(heureStr))
                {
                    try
                    {
                        // Combiner la date (YYYY-MM-DD) et l'heure (HH:mm)
                        string dateHeureStr = $"{dateStr} {heureStr}";
                        rendezVous.DateHeure = DateTime.ParseExact(dateHeureStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        ModelState.Remove("DateHeure");
                    }
                    catch (FormatException)
                    {
                        ModelState.AddModelError("DateHeure", "Format de date invalide. Utilisez le format AAAA-MM-JJ et HH:mm");
                    }
                }

                // Supprimer les erreurs de validation pour les propriétés de navigation
                ModelState.Remove("Patient");
                ModelState.Remove("Medecin");

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(rendezVous);
                        await _context.SaveChangesAsync();

                        Console.WriteLine($"Rendez-vous modifié - ID: {rendezVous.Id}");
                        TempData["Success"] = "Rendez-vous modifié avec succès !";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_context.RendezVous.Any(e => e.Id == rendezVous.Id))
                        {
                            TempData["Error"] = "Rendez-vous introuvable.";
                            return NotFound();
                        }
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans Edit POST: {ex.Message}");
                ModelState.AddModelError("", $"Erreur lors de la modification : {ex.Message}");
            }

            ViewBag.DateRendezVousSaisie = dateStr;
            ViewBag.HeureRendezVousSaisie = heureStr;
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Medecins = _context.Medecins.ToList();

            return View(rendezVous);
        }

        // GET: RendezVous/Delete/5
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID du rendez-vous non spécifié.";
                return NotFound();
            }

            try
            {
                var rendezVous = await _context.RendezVous
                    .Include(r => r.Patient)
                    .Include(r => r.Medecin)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (rendezVous == null)
                {
                    TempData["Error"] = "Rendez-vous introuvable.";
                    return NotFound();
                }

                return View(rendezVous);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans Delete GET: {ex.Message}");
                TempData["Error"] = $"Erreur lors du chargement: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RendezVous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var rendezVous = await _context.RendezVous.FindAsync(id);

                if (rendezVous == null)
                {
                    TempData["Error"] = "Rendez-vous introuvable.";
                    return RedirectToAction(nameof(Index));
                }

                _context.RendezVous.Remove(rendezVous);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Rendez-vous supprimé - ID: {id}");
                TempData["Success"] = "Rendez-vous supprimé avec succès !";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans Delete POST: {ex.Message}");
                TempData["Error"] = $"Erreur lors de la suppression : {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Rendez-vous du jour
        public async Task<IActionResult> Today()
        {
            try
            {
                var today = DateTime.Today;
                var rendezVous = await _context.RendezVous
                    .Include(r => r.Patient)
                    .Include(r => r.Medecin)
                    .Where(r => r.DateHeure.Date == today)
                    .OrderBy(r => r.DateHeure)
                    .ToListAsync();

                Console.WriteLine($"Rendez-vous du jour: {rendezVous.Count}");
                ViewData["Title"] = "Rendez-vous du jour";
                return View("Index", rendezVous);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans Today: {ex.Message}");
                TempData["Error"] = $"Erreur lors du chargement: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Rendez-vous par patient
        public async Task<IActionResult> ByPatient(int patientId)
        {
            try
            {
                var patient = await _context.Patients.FindAsync(patientId);
                if (patient == null)
                {
                    TempData["Error"] = "Patient introuvable.";
                    return NotFound();
                }

                var rendezVous = await _context.RendezVous
                    .Include(r => r.Patient)
                    .Include(r => r.Medecin)
                    .Where(r => r.PatientId == patientId)
                    .OrderByDescending(r => r.DateHeure)
                    .ToListAsync();

                ViewData["PatientName"] = $"{patient.Nom} {patient.Prenom}";
                return View("Index", rendezVous);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans ByPatient: {ex.Message}");
                TempData["Error"] = $"Erreur lors du chargement: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Rendez-vous par médecin
        public async Task<IActionResult> ByMedecin(int medecinId)
        {
            try
            {
                var medecin = await _context.Medecins.FindAsync(medecinId);
                if (medecin == null)
                {
                    TempData["Error"] = "Médecin introuvable.";
                    return NotFound();
                }

                var rendezVous = await _context.RendezVous
                    .Include(r => r.Patient)
                    .Include(r => r.Medecin)
                    .Where(r => r.MedecinId == medecinId)
                    .OrderBy(r => r.DateHeure)
                    .ToListAsync();

                ViewData["MedecinName"] = $"Dr. {medecin.Nom} {medecin.Prenom}";
                return View("Index", rendezVous);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERREUR dans ByMedecin: {ex.Message}");
                TempData["Error"] = $"Erreur lors du chargement: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
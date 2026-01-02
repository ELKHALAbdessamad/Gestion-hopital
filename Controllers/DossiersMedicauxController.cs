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

namespace HospitalManagement.Controllers
{
    [Authorize]
    public class DossiersMedicauxController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DossiersMedicauxController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DossiersMedicaux
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            IQueryable<DossierMedical> query = _context.DossiersMedicaux
                .Include(d => d.Patient)
                .Include(d => d.Medecin);

            // Filtrer selon le rôle
            if (user.Role == "Patient" && user.PatientId.HasValue)
            {
                // Patient voit uniquement son dossier
                query = query.Where(d => d.PatientId == user.PatientId.Value);
            }
            else if (user.Role == "Medecin" && user.MedecinId.HasValue)
            {
                // Médecin voit les dossiers de ses patients
                query = query.Where(d => d.MedecinId == user.MedecinId.Value);
            }
            // Admin et Receptionniste voient tous les dossiers

            var dossiers = await query.ToListAsync();
            return View(dossiers);
        }

        // GET: DossiersMedicaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var dossier = await _context.DossiersMedicaux
                .Include(d => d.Patient)
                .Include(d => d.Medecin)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dossier == null) return NotFound();

            // Vérifier l'accès selon le rôle
            if (user.Role == "Patient" && user.PatientId.HasValue)
            {
                if (dossier.PatientId != user.PatientId.Value)
                    return Forbid();
            }
            else if (user.Role == "Medecin" && user.MedecinId.HasValue)
            {
                if (dossier.MedecinId != user.MedecinId.Value)
                    return Forbid();
            }

            return View(dossier);
        }

        // GET: DossiersMedicaux/Create
        [Authorize(Roles = "Admin,Medecin,Receptionniste")]
        public IActionResult Create()
        {
            // ✅ Charger les patients avec format "Nom Prénom"
            ViewBag.PatientId = new SelectList(
                _context.Patients.Select(p => new {
                    p.Id,
                    NomComplet = p.Nom + " " + p.Prenom
                }),
                "Id",
                "NomComplet"
            );

            // ✅ Charger les médecins avec format "Nom Prénom (Spécialité)"
            ViewBag.MedecinId = new SelectList(
                _context.Medecins.Select(m => new {
                    m.Id,
                    NomComplet = m.Nom + " " + m.Prenom + " (" + m.Specialite + ")"
                }),
                "Id",
                "NomComplet"
            );

            return View();
        }

        // POST: DossiersMedicaux/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DossierMedical dossier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dossier.DateCreation = DateTime.Now;
                    _context.Add(dossier);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Dossier médical créé avec succès !";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                        Console.WriteLine($"Erreur de validation: {error.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erreur lors de l'enregistrement : {ex.Message}");
                Console.WriteLine($"Exception: {ex.Message}");
            }

            // Recharger les listes en cas d'erreur
            ViewBag.PatientId = new SelectList(
                _context.Patients.Select(p => new { p.Id, NomComplet = p.Nom + " " + p.Prenom }),
                "Id", "NomComplet", dossier.PatientId
            );
            ViewBag.MedecinId = new SelectList(
                _context.Medecins.Select(m => new { m.Id, NomComplet = m.Nom + " " + m.Prenom + " (" + m.Specialite + ")" }),
                "Id", "NomComplet", dossier.MedecinId
            );

            return View(dossier);
        }

        // GET: DossiersMedicaux/Edit/5
        [Authorize(Roles = "Admin,Medecin,Receptionniste")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var dossier = await _context.DossiersMedicaux.FindAsync(id);
            if (dossier == null) return NotFound();

            ViewBag.PatientId = new SelectList(
                _context.Patients.Select(p => new { p.Id, NomComplet = p.Nom + " " + p.Prenom }),
                "Id", "NomComplet", dossier.PatientId
            );
            ViewBag.MedecinId = new SelectList(
                _context.Medecins.Select(m => new { m.Id, NomComplet = m.Nom + " " + m.Prenom + " (" + m.Specialite + ")" }),
                "Id", "NomComplet", dossier.MedecinId
            );

            return View(dossier);
        }

        // POST: DossiersMedicaux/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Medecin,Receptionniste")]
        public async Task<IActionResult> Edit(int id, DossierMedical dossier)
        {
            if (id != dossier.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dossier);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Dossier médical modifié avec succès !";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DossiersMedicaux.Any(e => e.Id == dossier.Id))
                        return NotFound();
                    throw;
                }
            }

            ViewBag.PatientId = new SelectList(
                _context.Patients.Select(p => new { p.Id, NomComplet = p.Nom + " " + p.Prenom }),
                "Id", "NomComplet", dossier.PatientId
            );
            ViewBag.MedecinId = new SelectList(
                _context.Medecins.Select(m => new { m.Id, NomComplet = m.Nom + " " + m.Prenom + " (" + m.Specialite + ")" }),
                "Id", "NomComplet", dossier.MedecinId
            );

            return View(dossier);
        }

        // GET: DossiersMedicaux/Delete/5
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID du dossier non spécifié.";
                return NotFound();
            }

            var dossier = await _context.DossiersMedicaux
                .Include(d => d.Patient)
                .Include(d => d.Medecin)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dossier == null)
            {
                TempData["Error"] = "Dossier médical introuvable.";
                return NotFound();
            }

            return View(dossier);
        }

        // POST: DossiersMedicaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var dossier = await _context.DossiersMedicaux.FindAsync(id);

                if (dossier == null)
                {
                    TempData["Error"] = "Dossier médical introuvable.";
                    return RedirectToAction(nameof(Index));
                }

                _context.DossiersMedicaux.Remove(dossier);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Le dossier médical a été supprimé avec succès.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erreur lors de la suppression : {ex.Message}";
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
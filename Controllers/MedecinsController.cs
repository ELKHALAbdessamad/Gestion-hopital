using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace HospitalManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MedecinsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedecinsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medecins
        public async Task<IActionResult> Index()
        {
            var medecins = await _context.Medecins.Include(m => m.Service).ToListAsync();
            return View(medecins);
        }

        // GET: Medecins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var medecin = await _context.Medecins
                .Include(m => m.Service)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medecin == null) return NotFound();

            return View(medecin);
        }

        // GET: Medecins/Create
        public IActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(_context.Services, "Id", "Nom");
            return View();
        }

        // POST: Medecins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medecin medecin)
        {
            try
            {
                if (medecin.ServiceId == 0)
                {
                    var defaultService = await _context.Services
                        .FirstOrDefaultAsync(s => s.Nom == "Non assigné");
                    
                    if (defaultService != null)
                    {
                        medecin.ServiceId = defaultService.Id;
                        ModelState.Remove("ServiceId");
                    }
                    else
                    {
                        ModelState.AddModelError("ServiceId", "Veuillez sélectionner un service");
                    }
                }

                if (ModelState.IsValid)
                {
                    medecin.DateEmbauche = DateTime.Now;
                    _context.Add(medecin);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Médecin créé avec succès !";
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

            ViewBag.ServiceId = new SelectList(_context.Services, "Id", "Nom", medecin.ServiceId);
            return View(medecin);
        }

        // GET: Medecins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var medecin = await _context.Medecins.FindAsync(id);
            if (medecin == null) return NotFound();

            ViewBag.ServiceId = new SelectList(_context.Services, "Id", "Nom", medecin.ServiceId);
            return View(medecin);
        }

        // POST: Medecins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Medecin medecin)
        {
            if (id != medecin.Id) return NotFound();

            if (medecin.ServiceId == 0)
            {
                var defaultService = await _context.Services
                    .FirstOrDefaultAsync(s => s.Nom == "Non assigné");
                
                if (defaultService != null)
                {
                    medecin.ServiceId = defaultService.Id;
                    ModelState.Remove("ServiceId");
                }
                else
                {
                    ModelState.AddModelError("ServiceId", "Veuillez sélectionner un service");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medecin);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Médecin modifié avec succès !";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Medecins.Any(e => e.Id == medecin.Id))
                        return NotFound();
                    throw;
                }
            }

            ViewBag.ServiceId = new SelectList(_context.Services, "Id", "Nom", medecin.ServiceId);
            return View(medecin);
        }

        // GET: Medecins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID du médecin non spécifié.";
                return NotFound();
            }

            var medecin = await _context.Medecins
                .Include(m => m.Service)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medecin == null)
            {
                TempData["Error"] = "Médecin introuvable.";
                return NotFound();
            }

            return View(medecin);
        }

        // POST: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var medecin = await _context.Medecins.FindAsync(id);
                
                if (medecin == null)
                {
                    TempData["Error"] = "Médecin introuvable.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Medecins.Remove(medecin);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = $"Le médecin {medecin.Nom} {medecin.Prenom} a été supprimé avec succès.";
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
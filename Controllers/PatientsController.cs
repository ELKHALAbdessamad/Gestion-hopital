using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace HospitalManagement.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            
            // Seuls Admin et Réceptionniste peuvent voir la liste complète
            if (userRole != "Admin" && userRole != "Receptionniste")
            {
                return Forbid();
            }

            var patients = await _context.Patients.ToListAsync();
            return View(patients);
        }

        // GET: Patients/Create
        [Authorize(Roles = "Admin,Receptionniste")]
        public IActionResult Create()
        {
            return View(new Patient());
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.DateInscription = DateTime.Now;

                _context.Add(patient);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Patient créé avec succès !";
                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Edit(int id, Patient patient)
        {
            if (id != patient.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Patient modifié avec succès !";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Patients.Any(e => e.Id == patient.Id))
                        return NotFound();
                    throw;
                }
            }

            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Receptionniste")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Patient supprimé avec succès !";
            }
            else
            {
                TempData["Error"] = "Patient introuvable.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
                return NotFound();

            return View(patient);
        }
    }
}

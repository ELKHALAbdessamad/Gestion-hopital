using HospitalManagement.Data;
using HospitalManagement.Models;
using HospitalManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HospitalManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    
                    // S'assurer que l'utilisateur a le bon rôle dans Identity
                    var roles = await _userManager.GetRolesAsync(user);
                    if (!roles.Contains(user.Role))
                    {
                        await _userManager.AddToRoleAsync(user, user.Role);
                    }
                    
                    TempData["Success"] = $"Bienvenue {user?.Prenom} {user?.Nom} !";

                    // Redirection selon le rôle
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    // Utiliser les rôles Identity, pas le champ Role
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles.Contains("Admin"))
                        return RedirectToAction("Index", "Home");
                    else if (userRoles.Contains("Medecin"))
                        return RedirectToAction("Index", "RendezVous");
                    else if (userRoles.Contains("Receptionniste"))
                        return RedirectToAction("Index", "Home");
                    else if (userRoles.Contains("Patient"))
                        return RedirectToAction("Index", "RendezVous");
                    else
                        return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
            }

            return View(model);
        }

        // GET: Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    Telephone = model.Telephone,
                    Role = model.Role,
                    DateInscription = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Ajouter le rôle à l'utilisateur dans Identity
                    var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
                    if (!roleResult.Succeeded)
                    {
                        foreach (var error in roleResult.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                        return View(model);
                    }
                    
                    if (model.Role == "Patient")
                    {
                        var patient = new Patient
                        {
                            Nom = model.Nom,
                            Prenom = model.Prenom,
                            Email = model.Email,
                            Telephone = model.Telephone,
                            DateNaissance = DateTime.Now.AddYears(-30),
                            Sexe = "Non spécifié",
                            Adresse = "À renseigner"
                        };

                        _context.Patients.Add(patient);
                        await _context.SaveChangesAsync();

                        user.PatientId = patient.Id;
                        await _userManager.UpdateAsync(user);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["Success"] = "Inscription réussie ! Bienvenue.";

                    // Redirection selon le rôle après inscription
                    return model.Role switch
                    {
                        "Admin" => RedirectToAction("Index", "Home"),
                        "Medecin" => RedirectToAction("Index", "RendezVous"),
                        "Receptionniste" => RedirectToAction("Index", "Home"),
                        "Patient" => RedirectToAction("Index", "RendezVous"),
                        _ => RedirectToAction("Index", "Home")
                    };
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Info"] = "Vous êtes déconnecté.";
            return RedirectToAction("Login", "Account");
        }

        // GET: Account/Profile
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            return View(user);
        }

        // GET: Account/EditProfile
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var model = new EditProfileViewModel
            {
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Telephone = user.Telephone
            };

            return View(model);
        }

        // POST: Account/EditProfile
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            user.Nom = model.Nom;
            user.Prenom = model.Prenom;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Telephone = model.Telephone;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "Profil mis à jour avec succès !";
                return RedirectToAction("Profile");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
    }
}

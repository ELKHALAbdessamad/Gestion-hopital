using HospitalManagement.Data;
using HospitalManagement.Models;
using HospitalManagement.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ==============================
// DB Context
// ==============================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==============================
// Identity
// ==============================
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ==============================
// Cookie d’authentification
// ==============================
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Home/Index";
});

// ==============================
// MVC
// ==============================
builder.Services.AddControllersWithViews();

// ==============================
// Localisation FR
// ==============================
var supportedCultures = new List<CultureInfo> { new CultureInfo("fr-FR") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("fr-FR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// ==============================
// Utilisation localisation
// ==============================
var localizationOptions = app.Services.GetRequiredService<
    Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

// ==============================
// Pipeline HTTP
// ==============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Middleware d'autorisation par rôle - DÉSACTIVÉ
// app.UseMiddleware<RoleAuthorizationMiddleware>();

// ==============================
// APPLIQUER LES MIGRATIONS ET SEED DATA
// ==============================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // ✅ Crée les rôles si pas existants
    string[] roles = { "Admin", "Medecin", "Receptionniste", "Patient" };
    foreach (var role in roles)
    {

        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // ✅ Crée un admin si inexistant
    string adminEmail = "admin@hospital.com";
    var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
    if (existingAdmin == null)
    {
        var adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            Nom = "Admin",
            Prenom = "Super",
            Role = "Admin"
        };
        var result = await userManager.CreateAsync(adminUser, "Admin123!");
        if (result.Succeeded)
        {
            var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!roleResult.Succeeded)
            {
                Console.WriteLine($"Erreur lors de l'ajout du rôle Admin: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            Console.WriteLine($"Erreur lors de la création de l'admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
    else
    {
        // S'assurer que l'admin existant a le rôle Admin dans Identity
        var isAdmin = await userManager.IsInRoleAsync(existingAdmin, "Admin");
        if (!isAdmin)
        {
            var roleResult = await userManager.AddToRoleAsync(existingAdmin, "Admin");
            if (!roleResult.Succeeded)
            {
                Console.WriteLine($"Erreur lors de l'ajout du rôle Admin à l'utilisateur existant: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
        }
    }

    // ✅ Créer les comptes de test
    // Patient
    string patientEmail = "patient@hospital.com";
    var existingPatient = await userManager.FindByEmailAsync(patientEmail);
    if (existingPatient == null)
    {
        var patientUser = new ApplicationUser
        {
            UserName = patientEmail,
            Email = patientEmail,
            Nom = "Dupont",
            Prenom = "Jean",
            Telephone = "0123456789",
            Role = "Patient"
        };
        var result = await userManager.CreateAsync(patientUser, "Patient123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(patientUser, "Patient");
            // Créer le patient associé
            var patient = new Patient
            {
                Nom = "Dupont",
                Prenom = "Jean",
                Email = patientEmail,
                Telephone = "0123456789",
                DateNaissance = DateTime.Now.AddYears(-30),
                Sexe = "M",
                Adresse = "123 Rue de la Paix",
                DateInscription = DateTime.Now
            };
            db.Patients.Add(patient);
            await db.SaveChangesAsync();
            patientUser.PatientId = patient.Id;
            await userManager.UpdateAsync(patientUser);
        }
    }

    // Réceptionniste
    string receptionnisteEmail = "receptionniste@hospital.com";
    var existingReceptionniste = await userManager.FindByEmailAsync(receptionnisteEmail);
    if (existingReceptionniste == null)
    {
        var receptionnisteUser = new ApplicationUser
        {
            UserName = receptionnisteEmail,
            Email = receptionnisteEmail,
            Nom = "Martin",
            Prenom = "Marie",
            Telephone = "0987654321",
            Role = "Receptionniste"
        };
        var result = await userManager.CreateAsync(receptionnisteUser, "Receptionniste123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(receptionnisteUser, "Receptionniste");
        }
    }

    // Médecin
    string medecinEmail = "medecin@hospital.com";
    var existingMedecin = await userManager.FindByEmailAsync(medecinEmail);
    if (existingMedecin == null)
    {
        var medecinUser = new ApplicationUser
        {
            UserName = medecinEmail,
            Email = medecinEmail,
            Nom = "Bernard",
            Prenom = "Dr",
            Telephone = "0555555555",
            Role = "Medecin"
        };
        var result = await userManager.CreateAsync(medecinUser, "Medecin123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(medecinUser, "Medecin");
            // Créer le médecin associé (assigner à un service)
            var service = await db.Services.FirstOrDefaultAsync();
            if (service == null)
            {
                service = new Service { Nom = "Cardiologie" };
                db.Services.Add(service);
                await db.SaveChangesAsync();
            }
            var medecin = new Medecin
            {
                Nom = "Bernard",
                Prenom = "Dr",
                Specialite = "Cardiologue",
                Email = medecinEmail,
                ServiceId = service.Id
            };
            db.Medecins.Add(medecin);
            await db.SaveChangesAsync();
            medecinUser.MedecinId = medecin.Id;
            await userManager.UpdateAsync(medecinUser);
        }
    }

    // ✅ Seed les données de test
    await SeedData.InitializeAsync(db, userManager);
}

// ==============================
// Routes
// ==============================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Welcome}/{id?}");

app.Run();

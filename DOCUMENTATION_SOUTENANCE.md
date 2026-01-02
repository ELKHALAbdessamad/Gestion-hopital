# 📋 DOCUMENTATION PROJET - HOSPITAL MANAGEMENT SYSTEM

## 🎯 PRÉSENTATION DU PROJET

**Nom**: Hospital Management System  
**Type**: Application Web ASP.NET Core MVC  
**Framework**: .NET 6.0  
**Base de données**: SQL Server avec Entity Framework Core  
**Authentification**: ASP.NET Core Identity

### Objectif
Système de gestion hospitalière permettant la gestion des patients, médecins, rendez-vous et dossiers médicaux avec un système d'authentification par rôles.

---

## 🏗️ ARCHITECTURE DU PROJET

### Structure des dossiers
```
HospitalManagement/
├── Controllers/          # Contrôleurs MVC (logique métier)
├── Models/              # Modèles de données (entités)
├── Views/               # Vues Razor (interface utilisateur)
├── Data/                # Contexte de base de données
├── ViewModels/          # Modèles pour les vues
├── Middleware/          # Middleware personnalisé
├── Authorization/       # Helpers d'autorisation
├── Migrations/          # Migrations Entity Framework
└── wwwroot/            # Fichiers statiques (CSS, JS, images)
```

---

## 👥 SYSTÈME DE RÔLES

### 4 Rôles principaux

1. **Admin** 
   - Accès complet à toutes les fonctionnalités
   - Gestion des médecins et services
   - Compte test: `admin@hospital.com` / `Admin123!`

2. **Réceptionniste**
   - Gestion des patients
   - Création/modification des rendez-vous
   - Compte test: `receptionniste@hospital.com` / `Receptionniste123!`

3. **Médecin**
   - Consultation de ses rendez-vous
   - Gestion des dossiers médicaux de ses patients
   - Compte test: `medecin@hospital.com` / `Medecin123!`

4. **Patient**
   - Consultation de ses propres rendez-vous
   - Consultation de ses dossiers médicaux
   - Compte test: `patient@hospital.com` / `Patient123!`

---

## 📊 MODÈLES DE DONNÉES

### 1. Patient
```csharp
- Id (int)
- Nom, Prenom (string)
- DateNaissance (DateTime)
- Sexe (string)
- Telephone, Email (string)
- Adresse (string)
- NumeroSecuriteSociale (string, optionnel)
- DateInscription (DateTime)
```

### 2. Medecin
```csharp
- Id (int)
- Nom, Prenom (string)
- Specialite (string)
- Email (string)
- NumeroLicence (string, optionnel)
- ServiceId (int, obligatoire)
- DateEmbauche (DateTime)
```

### 3. RendezVous
```csharp
- Id (int)
- PatientId, MedecinId (int)
- DateHeure (DateTime)
- Statut (string: Planifié, Confirmé, Annulé, Terminé)
- Motif (string, optionnel)
- Notes (string, optionnel)
- Duree (int, en minutes)
- DateCreation (DateTime)
```

### 4. DossierMedical
```csharp
- Id (int)
- PatientId, MedecinId (int)
- Resume (string)
- DateConsultation (DateTime)
- Diagnostic (string)
- Traitement (string)
- Observations (string)
- DateCreation (DateTime)
```

### 5. Service
```csharp
- Id (int)
- Nom (string)
- Description (string, optionnel)
- CapaciteAccueil (int)
- Emplacement (string, optionnel)
- Telephone (string, optionnel)
```

### 6. ApplicationUser (Identity)
```csharp
- Hérite de IdentityUser
- Nom, Prenom (string)
- Telephone (string)
- Role (string)
- PatientId, MedecinId (int?, optionnels)
- DateInscription (DateTime)
```

---

## 🔐 FONCTIONNALITÉS PAR MODULE

### Module Authentification (AccountController)
- **Login**: Connexion avec email/mot de passe
- **Register**: Inscription avec sélection de rôle
- **Logout**: Déconnexion
- **Profile**: Consultation du profil
- **EditProfile**: Modification du profil

### Module Patients (PatientsController)
- **Index**: Liste des patients (Admin, Réceptionniste)
- **Create**: Création d'un patient
- **Edit**: Modification d'un patient
- **Delete**: Suppression d'un patient
- **Details**: Détails d'un patient

### Module Médecins (MedecinsController)
- **Index**: Liste des médecins (Admin uniquement)
- **Create**: Création d'un médecin avec assignation à un service
- **Edit**: Modification d'un médecin
- **Delete**: Suppression d'un médecin
- **Details**: Détails d'un médecin

### Module Rendez-vous (RendezVousController)
- **Index**: Liste des rendez-vous (filtrée par rôle)
- **Create**: Création d'un rendez-vous (Admin, Réceptionniste)
- **Edit**: Modification d'un rendez-vous
- **Delete**: Suppression d'un rendez-vous
- **Details**: Détails d'un rendez-vous
- **Today**: Rendez-vous du jour
- **ByPatient**: Rendez-vous par patient
- **ByMedecin**: Rendez-vous par médecin

### Module Dossiers Médicaux (DossiersMedicauxController)
- **Index**: Liste des dossiers (filtrée par rôle)
- **Create**: Création d'un dossier (Médecin)
- **Edit**: Modification d'un dossier
- **Delete**: Suppression d'un dossier
- **Details**: Détails d'un dossier

### Module Services (ServicesController)
- **Index**: Liste des services (Admin)
- **Create**: Création d'un service
- **Edit**: Modification d'un service
- **Delete**: Suppression d'un service

---

## 🔧 CONFIGURATION TECHNIQUE

### Packages NuGet utilisés
```xml
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (6.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (6.0.25)
- Microsoft.EntityFrameworkCore.Tools (6.0.25)
- Microsoft.EntityFrameworkCore.Design (6.0.25)
```

### Chaîne de connexion (appsettings.json)
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=HospitalDB;..."
}
```

### Configuration Identity (Program.cs)
- Mot de passe: minimum 6 caractères, majuscule, minuscule, chiffre
- Cookie d'authentification avec redirection vers /Account/Login
- Localisation en français (fr-FR)

---

## 🚀 DÉMARRAGE DU PROJET

### 1. Prérequis
- .NET 6.0 SDK installé
- SQL Server (LocalDB ou instance complète)
- Visual Studio 2022 ou VS Code

### 2. Configuration
```bash
# Restaurer les packages
dotnet restore

# Mettre à jour la base de données
dotnet ef database update

# Lancer l'application
dotnet run
```

### 3. Accès à l'application
- URL: `https://localhost:5001` ou `http://localhost:5000`
- Page d'accueil: `/Home/Welcome`
- Page de connexion: `/Account/Login`

### 4. Comptes de test créés automatiquement
Au démarrage, le système crée automatiquement:
- 1 Admin
- 1 Réceptionniste
- 1 Médecin (avec service Cardiologie)
- 1 Patient

### 5. Données de test (SeedData)
L'application charge automatiquement des données de démonstration :
- **6 services** : Cardiologie, Pédiatrie, Urgences, Chirurgie, Orthopédie, Dermatologie
- **6 médecins** : Assignés aux différents services
- **5 patients** : Avec informations complètes
- **6 rendez-vous** : Programmés dans les prochains jours
- **6 dossiers médicaux** : Avec diagnostics et traitements

> 💡 **Pour recharger les données** : Consultez le fichier `REINITIALISER_BASE_DONNEES.md`

---

## 📝 POINTS CLÉS POUR LA SOUTENANCE

### Forces du projet
✅ Architecture MVC bien structurée  
✅ Système d'authentification robuste avec Identity  
✅ Gestion des rôles et autorisations  
✅ Relations entre entités bien définies  
✅ Validation des données côté serveur  
✅ Interface utilisateur en français  
✅ Seed data automatique au démarrage  
✅ Gestion des erreurs avec TempData  

### Fonctionnalités principales
1. **Gestion multi-rôles** avec permissions spécifiques
2. **CRUD complet** pour toutes les entités
3. **Relations complexes** entre Patient, Médecin, RendezVous, DossierMedical
4. **Filtrage intelligent** des données selon le rôle connecté
5. **Validation des conflits** (ex: disponibilité médecin)

### Technologies démontrées
- ASP.NET Core MVC 6.0
- Entity Framework Core (Code First)
- ASP.NET Core Identity
- Razor Views
- SQL Server
- Migrations automatiques
- Dependency Injection
- Middleware personnalisé

---

## 🔄 FLUX D'UTILISATION TYPIQUE

### Scénario 1: Prise de rendez-vous
1. Réceptionniste se connecte
2. Crée un nouveau patient (si nécessaire)
3. Crée un rendez-vous en sélectionnant patient et médecin
4. Système vérifie la disponibilité du médecin
5. Rendez-vous créé et visible pour le patient et le médecin

### Scénario 2: Consultation médicale
1. Médecin se connecte
2. Consulte ses rendez-vous du jour
3. Après consultation, crée un dossier médical
4. Renseigne diagnostic, traitement, observations
5. Dossier accessible au patient et aux administrateurs

### Scénario 3: Gestion administrative
1. Admin se connecte
2. Gère les services hospitaliers
3. Crée/modifie des médecins et les assigne aux services
4. Supervise l'ensemble des rendez-vous et dossiers

---

## 📈 ÉVOLUTIONS POSSIBLES

- Système de notifications par email
- Calendrier interactif pour les rendez-vous
- Statistiques et tableaux de bord
- Export PDF des dossiers médicaux
- Gestion des prescriptions médicales
- Historique des modifications
- API REST pour application mobile
- Système de paiement intégré

---

## 🐛 RÉSOLUTION DE PROBLÈMES

### Erreur de connexion à la base de données
- Vérifier la chaîne de connexion dans `appsettings.json`
- S'assurer que SQL Server est démarré

### Erreur de migration
```bash
dotnet ef migrations add NomMigration
dotnet ef database update
```

### Problème d'authentification
- Vérifier que les rôles sont créés au démarrage
- Consulter les logs dans la console

---

## 📞 INFORMATIONS COMPLÉMENTAIRES

**Date de création**: Décembre 2024  
**Version**: 1.0  
**Langage**: C# (.NET 6.0)  
**Pattern**: MVC (Model-View-Controller)  
**ORM**: Entity Framework Core  

---

*Document préparé pour la soutenance du projet Hospital Management System*

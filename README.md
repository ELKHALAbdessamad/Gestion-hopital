# 🏥 Système de Gestion Hospitalière

[![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4)](https://docs.microsoft.com/aspnet/core)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%206.0-512BD4)](https://docs.microsoft.com/ef/core)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

> Application web complète de gestion hospitalière développée avec ASP.NET Core MVC 6.0

## 📋 À Propos du Projet

**Hospital Management System** est une solution web moderne et sécurisée conçue pour digitaliser et optimiser la gestion quotidienne d'un établissement hospitalier. Le système centralise toutes les opérations administratives et médicales dans une plateforme unique, intuitive et accessible.

### 🎯 Objectifs

- ✅ Digitaliser la gestion des dossiers patients
- ✅ Automatiser la planification des rendez-vous
- ✅ Centraliser les informations médicales
- ✅ Améliorer la communication entre services
- ✅ Sécuriser l'accès aux données sensibles
- ✅ Optimiser les processus administratifs


### ✨ Fonctionnalités Principales

#### 🔐 Gestion Multi-Rôles
- **Administrateur** : Accès complet, gestion des médecins et services
- **Réceptionniste** : Gestion des patients et rendez-vous
- **Médecin** : Consultation des rendez-vous et gestion des dossiers médicaux
- **Patient** : Consultation de ses rendez-vous et dossiers médicaux

#### 👥 Gestion des Patients
- Création et modification des fiches patients
- Historique médical complet
- Informations détaillées (coordonnées, sécurité sociale, etc.)
- Recherche et filtrage avancés

#### 👨‍⚕️ Gestion des Médecins
- Profils des médecins avec spécialités
- Assignation aux services hospitaliers
- Gestion des licences médicales
- Suivi des rendez-vous

#### 📅 Système de Rendez-vous
- Planification intelligente avec vérification de disponibilité
- Gestion des statuts (Planifié, Confirmé, Annulé, Terminé)
- Filtrage par patient, médecin ou date
- Détection automatique des conflits d'horaires

#### 📋 Dossiers Médicaux Électroniques
- Création de dossiers par les médecins
- Diagnostics, traitements et observations
- Historique complet des consultations
- Accès sécurisé selon les rôles

#### 🏥 Gestion des Services
- Organisation par départements (Cardiologie, Pédiatrie, etc.)
- Capacité d'accueil et emplacements
- Assignation des médecins
- Informations de contact

#### 🎨 Interface Moderne
- Design responsive avec Bootstrap 5
- Navigation intuitive
- Messages de confirmation/erreur
- Tableaux de bord avec statistiques

## 🚀 Technologies Utilisées

### Backend
- **Framework** : ASP.NET Core 6.0 MVC
- **Langage** : C# 10
- **ORM** : Entity Framework Core 6.0
- **Authentification** : ASP.NET Core Identity
- **Base de données** : SQL Server

### Frontend
- **Template Engine** : Razor Views
- **CSS Framework** : Bootstrap 5
- **JavaScript** : jQuery
- **Icons** : Font Awesome

### Outils
- **IDE** : Visual Studio 2022 / VS Code
- **Contrôle de version** : Git & GitHub
- **Base de données** : SQL Server Management Studio
- **Package Manager** : NuGet


## 📊 Architecture du Projet

### Pattern MVC (Model-View-Controller)

```
┌─────────────────────────────────────────────────────────┐
│                    UTILISATEUR                          │
└────────────────────┬────────────────────────────────────┘
                     │ Requête HTTP
                     ▼
┌─────────────────────────────────────────────────────────┐
│                  CONTROLLER                             │
│  - Traite les requêtes                                  │
│  - Exécute la logique métier                            │
│  - Coordonne Model et View                              │
└────────────────────┬────────────────────────────────────┘
                     │
        ┌────────────┴────────────┐
        ▼                         ▼
┌──────────────┐          ┌──────────────┐
│    MODEL     │          │     VIEW     │
│  - Entités   │          │  - Razor     │
│  - Données   │          │  - HTML/CSS  │
│  - Logique   │          │  - Interface │
└──────┬───────┘          └──────────────┘
       │
       ▼
┌──────────────┐
│  DATABASE    │
│  SQL Server  │
└──────────────┘
```

### Structure des Dossiers

```
HospitalManagement/
├── Controllers/              # Contrôleurs MVC
│   ├── AccountController.cs
│   ├── PatientsController.cs
│   ├── MedecinsController.cs
│   ├── RendezVousController.cs
│   ├── DossiersMedicauxController.cs
│   └── ServicesController.cs
│
├── Models/                   # Entités de données
│   ├── Patient.cs
│   ├── Medecin.cs
│   ├── RendezVous.cs
│   ├── DossierMedical.cs
│   ├── Service.cs
│   └── ApplicationUser.cs
│
├── Views/                    # Vues Razor
│   ├── Account/
│   ├── Patients/
│   ├── Medecins/
│   ├── RendezVous/
│   ├── DossiersMedicaux/
│   ├── Services/
│   └── Shared/
│
├── Data/                     # Accès aux données
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs
│
├── ViewModels/               # Modèles pour les vues
├── Migrations/               # Migrations EF Core
├── Authorization/            # Gestion des autorisations
├── Middleware/               # Middleware personnalisé
└── wwwroot/                  # Fichiers statiques
```

## 🔐 Système de Rôles

### 1. **Administrateur**
- Accès complet à toutes les fonctionnalités
- Gestion des services et médecins
- Supervision de toutes les opérations

### 2. **Réceptionniste**
- Gestion des patients
- Création et modification des rendez-vous
- Consultation des informations

### 3. **Médecin**
- Consultation de ses rendez-vous
- Gestion des dossiers médicaux de ses patients
- Mise à jour des diagnostics et traitements

### 4. **Patient**
- Consultation de ses rendez-vous
- Accès à ses dossiers médicaux
- Gestion de son profil


## 📦 Installation et Configuration

### Prérequis

Avant de commencer, assurez-vous d'avoir installé :

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) ou supérieur
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express ou version complète)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Installation Étape par Étape

#### 1. Cloner le Dépôt

```bash
git clone https://github.com/ELKHALAbdessamad/Gestion-hopital.git
cd Gestion-hopital
```

#### 2. Restaurer les Packages NuGet

```bash
dotnet restore
```

#### 3. Configurer la Base de Données

Modifiez le fichier `appsettings.json` avec votre chaîne de connexion SQL Server :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HospitalDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

**Options de connexion :**

- **LocalDB** (développement) :
  ```
  Server=(localdb)\\mssqllocaldb;Database=HospitalDB;Trusted_Connection=True;
  ```

- **SQL Server Express** :
  ```
  Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;
  ```

- **SQL Server avec authentification** :
  ```
  Server=localhost;Database=HospitalDB;User Id=sa;Password=VotreMotDePasse;
  ```

#### 4. Appliquer les Migrations

```bash
dotnet ef database update
```

Cette commande va :
- Créer la base de données `HospitalDB`
- Créer toutes les tables nécessaires
- Initialiser les données de test

#### 5. Lancer l'Application

```bash
dotnet run
```

L'application sera accessible sur :
- **HTTPS** : `https://localhost:5001`
- **HTTP** : `http://localhost:5000`

### 🔑 Comptes de Test

Au premier démarrage, l'application crée automatiquement des comptes de test :

| Rôle | Email | Mot de passe | Accès |
|------|-------|--------------|-------|
| **Admin** | admin@hospital.com | Admin123! | Accès complet |
| **Réceptionniste** | receptionniste@hospital.com | Receptionniste123! | Patients, Rendez-vous |
| **Médecin** | medecin@hospital.com | Medecin123! | Rendez-vous, Dossiers |
| **Patient** | patient@hospital.com | Patient123! | Ses rendez-vous et dossiers |

### 📊 Données de Démonstration

L'application charge automatiquement :
- ✅ **6 services** : Cardiologie, Pédiatrie, Urgences, Chirurgie, Orthopédie, Dermatologie
- ✅ **6 médecins** : Un par service avec spécialités
- ✅ **5 patients** : Avec informations complètes
- ✅ **6 rendez-vous** : Programmés dans les prochains jours
- ✅ **6 dossiers médicaux** : Avec diagnostics et traitements

## 🛠️ Commandes Utiles

### Entity Framework

```bash
# Créer une nouvelle migration
dotnet ef migrations add NomDeLaMigration

# Appliquer les migrations
dotnet ef database update

# Supprimer la dernière migration
dotnet ef migrations remove

# Réinitialiser la base de données
dotnet ef database drop --force
dotnet ef database update
```

### Build et Run

```bash
# Compiler le projet
dotnet build

# Lancer en mode développement
dotnet run

# Lancer en mode production
dotnet run --configuration Release

# Publier l'application
dotnet publish -c Release -o ./publish

# Nettoyer les fichiers de build
dotnet clean
```

### Tests

```bash
# Exécuter les tests
dotnet test

# Exécuter les tests avec couverture
dotnet test /p:CollectCoverage=true
```

## 🎯 Fonctionnalités Détaillées

### Gestion des Patients
- Création, modification, suppression de patients
- Recherche et filtrage
- Historique complet des consultations
- Informations détaillées (coordonnées, sécurité sociale, etc.)

### Gestion des Rendez-vous
- Planification avec vérification de disponibilité
- Gestion des statuts (Planifié, Confirmé, Annulé, Terminé)
- Vue par médecin, patient ou date
- Notifications et rappels

### Dossiers Médicaux
- Création de dossiers par les médecins
- Historique médical complet
- Diagnostics, traitements et observations
- Accès sécurisé selon les rôles

### Gestion des Services
- Organisation par départements
- Capacité d'accueil
- Assignation des médecins
- Informations de contact

## 🛠️ Commandes Utiles

### Migrations
```bash
# Créer une nouvelle migration
dotnet ef migrations add NomDeLaMigration

# Appliquer les migrations
dotnet ef database update

# Supprimer la dernière migration
dotnet ef migrations remove

# Réinitialiser la base de données
dotnet ef database drop --force
dotnet ef database update
```

### Build et Run
```bash
# Compiler le projet
dotnet build

# Lancer en mode développement
dotnet run

# Lancer en mode production
dotnet run --configuration Release

# Publier l'application
dotnet publish -c Release -o ./publish
```

## 📸 Captures d'écran

### Page d'accueil
Interface d'accueil avec navigation intuitive et design moderne.

### Tableau de bord Admin
Vue d'ensemble des statistiques et gestion complète du système.

### Gestion des rendez-vous
Interface de planification avec calendrier et disponibilités.

### Dossiers médicaux
Consultation et gestion des dossiers patients.

## 🔒 Sécurité

- ✅ Authentification par cookies sécurisés
- ✅ Autorisation basée sur les rôles
- ✅ Protection CSRF avec tokens anti-forgery
- ✅ Validation des données côté serveur et client
- ✅ Hashage des mots de passe avec Identity
- ✅ Protection contre les injections SQL (EF Core)

## 📈 Évolutions Futures

- [ ] Système de notifications par email
- [ ] Calendrier interactif pour les rendez-vous
- [ ] Tableaux de bord avec statistiques
- [ ] Export PDF des dossiers médicaux
- [ ] Gestion des prescriptions médicales
- [ ] API REST pour application mobile
- [ ] Système de paiement intégré
- [ ] Chat en temps réel médecin-patient

## 🤝 Contribution

Les contributions sont les bienvenues ! N'hésitez pas à :

1. Fork le projet
2. Créer une branche (`git checkout -b feature/AmazingFeature`)
3. Commit vos changements (`git commit -m 'Add some AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrir une Pull Request

## 📝 License

Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.

## 👨‍💻 Auteur

**ELKHAL Abdessamad**

- GitHub: [@ELKHALAbdessamad](https://github.com/ELKHALAbdessamad)
- Email: [Elkhalabdessamad000@gmail.com]

## 🙏 Remerciements

- ASP.NET Core Team pour le framework
- Bootstrap Team pour le framework CSS
- Font Awesome pour les icônes
- La communauté open source

## 📞 Support

Pour toute question ou problème :
- Ouvrir une [issue](https://github.com/ELKHALAbdessamad/Gestion-d-h-pital/issues)
- Consulter la [documentation](DOCUMENTATION_SOUTENANCE.md)

---

⭐ **Si ce projet vous a été utile, n'oubliez pas de lui donner une étoile !** ⭐

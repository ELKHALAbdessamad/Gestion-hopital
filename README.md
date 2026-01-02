# 🏥 Système de Gestion Hospitalière

[![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4)](https://docs.microsoft.com/aspnet/core)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%206.0-512BD4)](https://docs.microsoft.com/ef/core)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## 📋 Description

Application web complète de gestion hospitalière développée avec **ASP.NET Core MVC 6.0**. Ce système permet la gestion efficace des patients, médecins, services, rendez-vous et dossiers médicaux avec un système d'authentification et d'autorisation par rôles.

### ✨ Fonctionnalités Principales

- 🔐 **Authentification sécurisée** avec ASP.NET Core Identity
- 👥 **Gestion multi-rôles** (Admin, Médecin, Réceptionniste, Patient)
- 🏥 **Gestion des services** hospitaliers
- 👨‍⚕️ **Gestion des médecins** et leurs spécialités
- 🧑‍🤝‍🧑 **Gestion des patients** avec informations complètes
- 📅 **Système de rendez-vous** avec vérification de disponibilité
- 📋 **Dossiers médicaux** électroniques
- 🎨 **Interface responsive** avec Bootstrap 5
- 🌍 **Localisation française** complète

## 🚀 Technologies Utilisées

### Backend
- **Framework** : ASP.NET Core 6.0 MVC
- **ORM** : Entity Framework Core 6.0
- **Base de données** : SQL Server
- **Authentification** : ASP.NET Core Identity
- **Langage** : C# 10

### Frontend
- **Template Engine** : Razor Views
- **CSS Framework** : Bootstrap 5
- **Icons** : Font Awesome
- **JavaScript** : Vanilla JS

## 📊 Architecture

```
HospitalManagement/
├── Controllers/          # Contrôleurs MVC (logique métier)
│   ├── AccountController.cs
│   ├── PatientsController.cs
│   ├── MedecinsController.cs
│   ├── RendezVousController.cs
│   ├── DossiersMedicauxController.cs
│   └── ServicesController.cs
├── Models/              # Modèles de données (entités)
│   ├── Patient.cs
│   ├── Medecin.cs
│   ├── RendezVous.cs
│   ├── DossierMedical.cs
│   └── Service.cs
├── Views/               # Vues Razor (interface utilisateur)
├── Data/                # Contexte de base de données
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs
├── ViewModels/          # Modèles pour les vues
├── Migrations/          # Migrations Entity Framework
└── wwwroot/            # Fichiers statiques (CSS, JS, images)
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

## 📦 Installation

### Prérequis

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB ou instance complète)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### Étapes d'installation

1. **Cloner le dépôt**
```bash
git clone https://github.com/ELKHALAbdessamad/Gestion-d-h-pital.git
cd Gestion-d-h-pital
```

2. **Restaurer les packages NuGet**
```bash
dotnet restore
```

3. **Configurer la chaîne de connexion**

Modifiez `appsettings.json` avec votre chaîne de connexion SQL Server :
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HospitalDB;Trusted_Connection=True;"
  }
}
```

4. **Appliquer les migrations**
```bash
dotnet ef database update
```

5. **Lancer l'application**
```bash
dotnet run
```

6. **Accéder à l'application**

Ouvrez votre navigateur et allez sur : `https://localhost:5001`

## 🔑 Comptes de Test

L'application crée automatiquement des comptes de test au premier démarrage :

| Rôle | Email | Mot de passe |
|------|-------|--------------|
| Admin | admin@hospital.com | Admin123! |
| Réceptionniste | receptionniste@hospital.com | Receptionniste123! |
| Médecin | medecin@hospital.com | Medecin123! |
| Patient | patient@hospital.com | Patient123! |

## 📊 Données de Démonstration

Au premier lancement, l'application charge automatiquement :

- ✅ **6 services** : Cardiologie, Pédiatrie, Urgences, Chirurgie, Orthopédie, Dermatologie
- ✅ **6 médecins** : Un par service avec spécialités
- ✅ **5 patients** : Avec informations complètes
- ✅ **6 rendez-vous** : Programmés dans les prochains jours
- ✅ **6 dossiers médicaux** : Avec diagnostics et traitements

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
- Email: [Votre email]

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

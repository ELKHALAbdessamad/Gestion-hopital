# 🏥 HOSPITAL MANAGEMENT SYSTEM - GUIDE RAPIDE SOUTENANCE

## 🚀 DÉMARRAGE RAPIDE

### Option 1 : Script automatique (Recommandé)

**Sur macOS/Linux :**
```bash
./LANCER_APPLICATION.sh
```

**Sur Windows :**
```bash
LANCER_APPLICATION.bat
```

### Option 2 : Commandes manuelles

```bash
# 1. Réinitialiser la base de données (si nécessaire)
dotnet ef database drop --force
dotnet ef database update

# 2. Lancer l'application
dotnet run
```

L'application sera accessible sur : **https://localhost:5001**

---

## 🔐 COMPTES DE TEST

| Rôle | Email | Mot de passe |
|------|-------|--------------|
| **Admin** | admin@hospital.com | Admin123! |
| **Réceptionniste** | receptionniste@hospital.com | Receptionniste123! |
| **Médecin** | medecin@hospital.com | Medecin123! |
| **Patient** | patient@hospital.com | Patient123! |

---

## 📚 DOCUMENTS DISPONIBLES

1. **DOCUMENTATION_SOUTENANCE.md** - Documentation complète du projet
2. **DONNEES_TEST.md** - Liste détaillée de toutes les données de test
3. **REINITIALISER_BASE_DONNEES.md** - Guide pour réinitialiser la base de données
4. **README_SOUTENANCE.md** - Ce fichier (guide rapide)

---

## 📊 DONNÉES CHARGÉES AUTOMATIQUEMENT

Au premier démarrage, l'application charge automatiquement :

- ✅ **6 services** (Cardiologie, Pédiatrie, Urgences, etc.)
- ✅ **6 médecins** (1 par service)
- ✅ **5 patients** (avec informations complètes)
- ✅ **6 rendez-vous** (programmés dans les prochains jours)
- ✅ **6 dossiers médicaux** (avec diagnostics et traitements)

---

## 🎯 DÉMONSTRATION RAPIDE

### 1. Connexion Admin (Accès complet)
```
Email: admin@hospital.com
Mot de passe: Admin123!
```

**Ce que vous pouvez montrer :**
- Gestion des services (/Services)
- Gestion des médecins (/Medecins)
- Gestion des patients (/Patients)
- Tous les rendez-vous (/RendezVous)
- Tous les dossiers médicaux (/DossiersMedicaux)

### 2. Connexion Médecin (Vue médecin)
```
Email: medecin@hospital.com
Mot de passe: Medecin123!
```

**Ce que vous pouvez montrer :**
- Voir uniquement ses rendez-vous
- Créer un dossier médical
- Consulter les dossiers de ses patients

### 3. Connexion Patient (Vue patient)
```
Email: patient@hospital.com
Mot de passe: Patient123!
```

**Ce que vous pouvez montrer :**
- Voir uniquement ses rendez-vous
- Consulter ses dossiers médicaux
- Modifier son profil

### 4. Connexion Réceptionniste (Gestion administrative)
```
Email: receptionniste@hospital.com
Mot de passe: Receptionniste123!
```

**Ce que vous pouvez montrer :**
- Créer un nouveau patient
- Créer un rendez-vous
- Modifier/Supprimer un rendez-vous

---

## 🔍 POINTS CLÉS À PRÉSENTER

### 1. Architecture MVC
- **Models** : Entités (Patient, Medecin, RendezVous, etc.)
- **Views** : Interface utilisateur Razor
- **Controllers** : Logique métier

### 2. Sécurité
- Authentification avec ASP.NET Core Identity
- Autorisation par rôles (Admin, Médecin, Réceptionniste, Patient)
- Filtrage des données selon le rôle connecté

### 3. Base de données
- Entity Framework Core (Code First)
- Relations entre entités (One-to-Many, Many-to-One)
- Migrations automatiques
- Seed data au démarrage

### 4. Fonctionnalités
- CRUD complet pour toutes les entités
- Validation des données
- Gestion des conflits (disponibilité médecin)
- Messages de confirmation/erreur (TempData)

---

## 📋 CHECKLIST AVANT LA SOUTENANCE

- [ ] Base de données réinitialisée avec données de test
- [ ] Application lancée et accessible
- [ ] Testé la connexion avec les 4 rôles
- [ ] Vérifié que les données s'affichent correctement
- [ ] Préparé les scénarios de démonstration
- [ ] Documentation imprimée ou accessible

---

## 🛠️ RÉSOLUTION DE PROBLÈMES

### Problème : Les données ne s'affichent pas
**Solution :** Réinitialisez la base de données
```bash
dotnet ef database drop --force
dotnet ef database update
dotnet run
```

### Problème : Erreur de connexion à la base de données
**Solution :** Vérifiez la chaîne de connexion dans `appsettings.json`

### Problème : Erreur lors du lancement
**Solution :** Vérifiez que .NET 6.0 SDK est installé
```bash
dotnet --version
```

---

## 📞 STRUCTURE DU PROJET

```
HospitalManagement/
├── Controllers/          # Logique métier (7 contrôleurs)
├── Models/              # Entités (6 modèles)
├── Views/               # Interface utilisateur
├── Data/                # Contexte DB + SeedData
├── ViewModels/          # Modèles pour les vues
├── Migrations/          # Migrations EF Core
└── wwwroot/            # Fichiers statiques
```

---

## 🎓 TECHNOLOGIES UTILISÉES

- **Framework** : ASP.NET Core 6.0 MVC
- **ORM** : Entity Framework Core 6.0
- **Base de données** : SQL Server
- **Authentification** : ASP.NET Core Identity
- **Frontend** : Razor Views, Bootstrap 5
- **Langage** : C# 10

---

## 📈 STATISTIQUES DU PROJET

- **Lignes de code** : ~3000+
- **Contrôleurs** : 7
- **Modèles** : 6
- **Vues** : 30+
- **Migrations** : 5
- **Rôles** : 4
- **Fonctionnalités CRUD** : 5 modules complets

---

## ✨ POINTS FORTS À MENTIONNER

1. **Architecture propre** : Séparation claire des responsabilités (MVC)
2. **Sécurité robuste** : Authentification et autorisation par rôles
3. **Relations complexes** : Gestion des relations entre entités
4. **Validation complète** : Validation côté serveur et client
5. **Interface intuitive** : Design responsive avec Bootstrap
6. **Données de test** : Chargement automatique pour démonstration
7. **Gestion des erreurs** : Messages clairs pour l'utilisateur
8. **Code First** : Migrations automatiques de la base de données

---

## 🎬 ORDRE DE PRÉSENTATION SUGGÉRÉ

1. **Introduction** (2 min)
   - Présentation du projet
   - Objectifs et contexte

2. **Architecture technique** (3 min)
   - Structure MVC
   - Technologies utilisées
   - Base de données

3. **Démonstration** (10 min)
   - Connexion Admin → Vue d'ensemble
   - Connexion Médecin → Gestion des rendez-vous
   - Connexion Patient → Vue patient
   - Création d'un rendez-vous (Réceptionniste)

4. **Code** (3 min)
   - Montrer un contrôleur
   - Montrer un modèle avec relations
   - Montrer le système d'autorisation

5. **Conclusion** (2 min)
   - Points forts
   - Évolutions possibles
   - Questions

---

**Bonne chance pour votre soutenance ! 🎓**

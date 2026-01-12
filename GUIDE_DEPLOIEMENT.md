# 🚀 Guide de Déploiement - Système de Gestion Hospitalière

## 📋 Table des Matières
1. [Options de Déploiement](#options-de-déploiement)
2. [Déploiement sur Azure (Recommandé)](#déploiement-sur-azure)
3. [Déploiement sur un Serveur Local/VPS](#déploiement-sur-serveur-local)
4. [Configuration de la Base de Données](#configuration-base-de-données)
5. [Vérification et Tests](#vérification-et-tests)

---

## 🎯 Options de Déploiement

### Option 1: Azure App Service (Recommandé pour débutants)
- ✅ Facile à configurer
- ✅ Gratuit pour commencer (plan gratuit disponible)
- ✅ Base de données SQL Azure incluse
- ✅ HTTPS automatique
- ✅ Mise à l'échelle automatique
- 💰 Coût: ~18€/mois après période gratuite

### Option 2: Railway.app (GRATUIT - Recommandé!)
- ✅ 100% GRATUIT pour commencer (500h/mois)
- ✅ Supporte .NET nativement
- ✅ Base de données PostgreSQL gratuite
- ✅ Déploiement depuis GitHub automatique
- ✅ HTTPS automatique
- ✅ Très simple à utiliser
- 💰 Coût: GRATUIT jusqu'à 500h/mois

### Option 3: Render.com (GRATUIT)
- ✅ Plan gratuit disponible
- ✅ Supporte .NET
- ✅ Base de données PostgreSQL gratuite
- ✅ Déploiement depuis GitHub
- ✅ HTTPS automatique
- ⚠️ Se met en veille après 15 min d'inactivité
- 💰 Coût: GRATUIT (avec limitations)

### Option 4: Serveur Local/VPS
- ⚠️ Plus technique
- ⚠️ Nécessite configuration manuelle
- ✅ Contrôle total
- ✅ Pas de coûts cloud (si serveur local)
- 💰 Coût: 5-12€/mois pour VPS

### ❌ Vercel / Netlify
- ❌ Ne supportent PAS .NET/ASP.NET Core
- ✅ Uniquement pour: Next.js, React, Node.js, sites statiques

---

## 🚂 Déploiement sur Railway.app (GRATUIT - LE PLUS SIMPLE!)

Railway est **100% gratuit** pour commencer et supporte .NET nativement. C'est l'option la plus simple pour les débutants!

### Étape 1: Créer un compte Railway

1. Allez sur [railway.app](https://railway.app)
2. Cliquez sur "Start a New Project"
3. Connectez-vous avec GitHub (recommandé)

### Étape 2: Préparer le projet pour Railway

Créez un fichier `railway.toml` à la racine du projet:

```toml
[build]
builder = "NIXPACKS"

[deploy]
startCommand = "dotnet HospitalManagement.dll"
restartPolicyType = "ON_FAILURE"
restartPolicyMaxRetries = 10
```

Créez un fichier `nixpacks.toml` à la racine:

```toml
[phases.setup]
nixPkgs = ["dotnet-sdk_6"]

[phases.build]
cmds = ["dotnet publish -c Release -o out"]

[start]
cmd = "cd out && dotnet HospitalManagement.dll"
```

### Étape 3: Modifier la configuration pour PostgreSQL

Railway offre PostgreSQL gratuit (pas SQL Server). Modifiez votre projet:

1. **Installez le package PostgreSQL:**

```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 6.0.25
```

2. **Modifiez `Program.cs`:**

Remplacez:
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

Par:
```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (connectionString.Contains("postgres"))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseSqlServer(connectionString);
    }
});
```

3. **Ajoutez le using en haut de `Program.cs`:**

```csharp
using Npgsql.EntityFrameworkCore.PostgreSQL;
```

### Étape 4: Créer le projet sur Railway

1. **Sur Railway, cliquez sur "New Project"**
2. **Sélectionnez "Deploy from GitHub repo"**
3. **Autorisez Railway à accéder à votre GitHub**
4. **Sélectionnez le dépôt `Gestion-hopital`**
5. **Railway détectera automatiquement que c'est un projet .NET**

### Étape 5: Ajouter une base de données PostgreSQL

1. **Dans votre projet Railway, cliquez sur "+ New"**
2. **Sélectionnez "Database" → "Add PostgreSQL"**
3. **Railway créera automatiquement la base de données**
4. **La variable `DATABASE_URL` sera automatiquement ajoutée**

### Étape 6: Configurer les variables d'environnement

1. **Cliquez sur votre service (application)**
2. **Allez dans l'onglet "Variables"**
3. **Ajoutez ces variables:**

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:$PORT
ConnectionStrings__DefaultConnection=${{Postgres.DATABASE_URL}}
```

### Étape 7: Déployer

1. **Railway déploiera automatiquement votre application**
2. **Attendez quelques minutes (première fois ~5-10 min)**
3. **Cliquez sur "Settings" → "Generate Domain"**
4. **Vous obtiendrez une URL comme:** `https://votre-app.up.railway.app`

### Étape 8: Appliquer les migrations

Railway appliquera automatiquement les migrations au démarrage grâce à votre `Program.cs`.

**C'est tout! Votre application est en ligne! 🎉**

---

## 🎨 Déploiement sur Render.com (GRATUIT Alternative)

Render est une autre excellente option gratuite.

### Étape 1: Créer un compte

1. Allez sur [render.com](https://render.com)
2. Inscrivez-vous avec GitHub

### Étape 2: Créer un Web Service

1. **Cliquez sur "New +" → "Web Service"**
2. **Connectez votre dépôt GitHub `Gestion-hopital`**
3. **Configurez:**
   - **Name:** `hospital-management`
   - **Environment:** `Docker` ou `.NET`
   - **Build Command:** `dotnet publish -c Release -o out`
   - **Start Command:** `cd out && dotnet HospitalManagement.dll`
   - **Plan:** Free

### Étape 3: Ajouter une base de données PostgreSQL

1. **Cliquez sur "New +" → "PostgreSQL"**
2. **Name:** `hospital-db`
3. **Plan:** Free
4. **Cliquez sur "Create Database"**

### Étape 4: Lier la base de données

1. **Retournez dans votre Web Service**
2. **Allez dans "Environment"**
3. **Ajoutez:**

```
ASPNETCORE_ENVIRONMENT=Production
DATABASE_URL=[Copiez l'URL de votre base PostgreSQL]
ConnectionStrings__DefaultConnection=$DATABASE_URL
```

### Étape 5: Déployer

Render déploiera automatiquement. Votre URL sera: `https://hospital-management.onrender.com`

⚠️ **Note:** Le plan gratuit de Render se met en veille après 15 minutes d'inactivité. Le premier chargement après veille prend ~30 secondes.

---

## 🌐 Déploiement sur Azure (Payant mais Professionnel)

### Étape 1: Créer un compte Azure
1. Allez sur [portal.azure.com](https://portal.azure.com)
2. Cliquez sur "Créer un compte gratuit"
3. Suivez les instructions (carte bancaire requise mais pas de frais pour le plan gratuit)
4. Vous obtenez 200$ de crédit gratuit pour 30 jours

### Étape 2: Créer une Base de Données SQL Azure

1. **Dans le portail Azure, cliquez sur "Créer une ressource"**
2. **Recherchez "SQL Database" et cliquez sur "Créer"**
3. **Remplissez les informations:**
   - **Abonnement:** Sélectionnez votre abonnement
   - **Groupe de ressources:** Créez-en un nouveau (ex: "HospitalManagement-RG")
   - **Nom de la base de données:** `HospitalDB`
   - **Serveur:** Cliquez sur "Créer nouveau"
     - Nom du serveur: `hospital-server-[votrenom]` (doit être unique)
     - Connexion administrateur: `adminuser`
     - Mot de passe: Créez un mot de passe fort (ex: `Hospital@2026!`)
     - Emplacement: Choisissez le plus proche (ex: "France Central")
   - **Calcul + stockage:** Cliquez sur "Configurer la base de données"
     - Sélectionnez "Basic" (le moins cher, ~5€/mois)
     - Ou "Serverless" pour payer uniquement à l'utilisation

4. **Configuration du pare-feu:**
   - Cochez "Autoriser les services Azure à accéder au serveur"
   - Ajoutez votre IP actuelle

5. **Cliquez sur "Vérifier + créer" puis "Créer"**

6. **Récupérez la chaîne de connexion:**
   - Une fois créée, allez dans votre base de données
   - Cliquez sur "Chaînes de connexion" dans le menu de gauche
   - Copiez la chaîne ADO.NET
   - Elle ressemble à:
   ```
   Server=tcp:hospital-server-xxx.database.windows.net,1433;Initial Catalog=HospitalDB;Persist Security Info=False;User ID=adminuser;Password={votre_mot_de_passe};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
   ```

### Étape 3: Mettre à jour la chaîne de connexion localement

1. **Ouvrez le fichier `appsettings.json`**
2. **Remplacez la chaîne de connexion par celle d'Azure:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:hospital-server-xxx.database.windows.net,1433;Initial Catalog=HospitalDB;Persist Security Info=False;User ID=adminuser;Password=Hospital@2026!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Étape 4: Appliquer les migrations à Azure

```bash
# Dans votre terminal, exécutez:
dotnet ef database update
```

Cela créera toutes les tables dans votre base de données Azure.

### Étape 5: Créer l'App Service (Application Web)

1. **Dans le portail Azure, cliquez sur "Créer une ressource"**
2. **Recherchez "Web App" et cliquez sur "Créer"**
3. **Remplissez les informations:**
   - **Groupe de ressources:** Utilisez le même que la base de données
   - **Nom:** `hospital-management-[votrenom]` (sera votre URL: hospital-management-xxx.azurewebsites.net)
   - **Publier:** Code
   - **Pile d'exécution:** .NET 6 (LTS)
   - **Système d'exploitation:** Linux (moins cher) ou Windows
   - **Région:** Même que votre base de données
   - **Plan tarifaire:** F1 (Gratuit) pour commencer

4. **Cliquez sur "Vérifier + créer" puis "Créer"**

### Étape 6: Configurer la chaîne de connexion dans App Service

1. **Allez dans votre App Service créée**
2. **Dans le menu de gauche, cliquez sur "Configuration"**
3. **Sous "Chaînes de connexion", cliquez sur "+ Nouvelle chaîne de connexion"**
   - **Nom:** `DefaultConnection`
   - **Valeur:** Collez votre chaîne de connexion Azure SQL
   - **Type:** SQLAzure
4. **Cliquez sur "OK" puis "Enregistrer"**

### Étape 7: Déployer l'application

#### Méthode A: Déploiement depuis Visual Studio (Plus facile)

1. **Ouvrez votre projet dans Visual Studio**
2. **Clic droit sur le projet → "Publier"**
3. **Sélectionnez "Azure" → "Suivant"**
4. **Sélectionnez "Azure App Service (Linux)" ou "Azure App Service (Windows)"**
5. **Connectez-vous à votre compte Azure**
6. **Sélectionnez votre App Service créée**
7. **Cliquez sur "Terminer" puis "Publier"**

#### Méthode B: Déploiement depuis GitHub Actions (Automatique)

1. **Dans le portail Azure, allez dans votre App Service**
2. **Cliquez sur "Centre de déploiement" dans le menu de gauche**
3. **Sélectionnez "GitHub" comme source**
4. **Autorisez Azure à accéder à votre GitHub**
5. **Sélectionnez:**
   - Organisation: Votre compte GitHub
   - Dépôt: `Gestion-hopital`
   - Branche: `main`
6. **Cliquez sur "Enregistrer"**

Azure créera automatiquement un workflow GitHub Actions qui déploiera votre application à chaque push!

#### Méthode C: Déploiement depuis la ligne de commande

```bash
# 1. Installez Azure CLI si pas déjà fait
# macOS:
brew install azure-cli

# 2. Connectez-vous à Azure
az login

# 3. Créez un package de déploiement
dotnet publish -c Release -o ./publish

# 4. Créez un fichier zip
cd publish
zip -r ../deploy.zip .
cd ..

# 5. Déployez vers Azure
az webapp deployment source config-zip \
  --resource-group HospitalManagement-RG \
  --name hospital-management-[votrenom] \
  --src deploy.zip
```

### Étape 8: Vérifier le déploiement

1. **Attendez quelques minutes que le déploiement se termine**
2. **Allez sur votre URL:** `https://hospital-management-[votrenom].azurewebsites.net`
3. **Vous devriez voir votre application fonctionner!**

---

## 💻 Déploiement sur Serveur Local/VPS

### Prérequis
- Un serveur avec Ubuntu 20.04+ ou Windows Server
- Accès SSH (pour Linux) ou RDP (pour Windows)
- Nom de domaine (optionnel)

### Sur Ubuntu/Linux

#### 1. Installer .NET 6 Runtime

```bash
# Mettre à jour le système
sudo apt update
sudo apt upgrade -y

# Installer .NET 6
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt update
sudo apt install -y aspnetcore-runtime-6.0
```

#### 2. Installer SQL Server ou utiliser une base de données

```bash
# Option A: Installer SQL Server sur Linux
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2019.list)"
sudo apt update
sudo apt install -y mssql-server
sudo /opt/mssql/bin/mssql-conf setup

# Option B: Utiliser PostgreSQL (nécessite modification du code)
sudo apt install -y postgresql postgresql-contrib
```

#### 3. Préparer l'application

```bash
# Sur votre machine locale, créez le package
dotnet publish -c Release -o ./publish

# Transférez vers le serveur
scp -r ./publish user@votre-serveur:/var/www/hospital-management
```

#### 4. Configurer Nginx comme reverse proxy

```bash
# Installer Nginx
sudo apt install -y nginx

# Créer la configuration
sudo nano /etc/nginx/sites-available/hospital-management
```

Ajoutez cette configuration:

```nginx
server {
    listen 80;
    server_name votre-domaine.com;  # ou votre IP

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

```bash
# Activer le site
sudo ln -s /etc/nginx/sites-available/hospital-management /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl restart nginx
```

#### 5. Créer un service systemd

```bash
sudo nano /etc/systemd/system/hospital-management.service
```

Ajoutez:

```ini
[Unit]
Description=Hospital Management System
After=network.target

[Service]
WorkingDirectory=/var/www/hospital-management
ExecStart=/usr/bin/dotnet /var/www/hospital-management/HospitalManagement.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=hospital-management
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

```bash
# Démarrer le service
sudo systemctl enable hospital-management
sudo systemctl start hospital-management
sudo systemctl status hospital-management
```

#### 6. Configurer HTTPS avec Let's Encrypt (Optionnel mais recommandé)

```bash
# Installer Certbot
sudo apt install -y certbot python3-certbot-nginx

# Obtenir un certificat SSL
sudo certbot --nginx -d votre-domaine.com

# Le renouvellement automatique est configuré automatiquement
```

---

## 🗄️ Configuration de la Base de Données

### Chaînes de connexion selon l'environnement

#### Azure SQL Database
```json
"Server=tcp:votre-serveur.database.windows.net,1433;Initial Catalog=HospitalDB;User ID=admin;Password=VotreMotDePasse;Encrypt=True;"
```

#### SQL Server Local
```json
"Server=localhost;Database=HospitalDB;User Id=sa;Password=VotreMotDePasse;TrustServerCertificate=True;"
```

#### SQL Server avec Windows Authentication
```json
"Server=localhost;Database=HospitalDB;Integrated Security=True;TrustServerCertificate=True;"
```

### Appliquer les migrations

```bash
# Depuis votre machine de développement
dotnet ef database update

# Ou depuis le serveur
cd /var/www/hospital-management
dotnet ef database update --project HospitalManagement.csproj
```

---

## ✅ Vérification et Tests

### 1. Vérifier que l'application fonctionne

```bash
# Tester localement
curl http://localhost:5000

# Tester via le domaine
curl http://votre-domaine.com
```

### 2. Vérifier les logs

#### Sur Azure
- Allez dans App Service → "Log stream" dans le menu de gauche

#### Sur Linux
```bash
# Logs de l'application
sudo journalctl -u hospital-management -f

# Logs Nginx
sudo tail -f /var/log/nginx/error.log
sudo tail -f /var/log/nginx/access.log
```

### 3. Tester les fonctionnalités

1. **Page d'accueil:** `https://votre-url.com`
2. **Connexion Admin:** 
   - Email: `admin@hospital.com`
   - Mot de passe: `Admin123!`
3. **Vérifier les données de test:**
   - Patients
   - Médecins
   - Services
   - Rendez-vous
   - Dossiers médicaux

---

## 🔧 Dépannage

### Problème: L'application ne démarre pas

```bash
# Vérifier les logs
sudo journalctl -u hospital-management -n 50

# Vérifier que .NET est installé
dotnet --version

# Vérifier les permissions
sudo chown -R www-data:www-data /var/www/hospital-management
```

### Problème: Erreur de connexion à la base de données

1. Vérifiez la chaîne de connexion dans `appsettings.json`
2. Vérifiez que le serveur SQL est accessible
3. Vérifiez les règles de pare-feu

### Problème: Erreur 502 Bad Gateway

```bash
# Vérifier que l'application tourne
sudo systemctl status hospital-management

# Redémarrer l'application
sudo systemctl restart hospital-management

# Vérifier Nginx
sudo nginx -t
sudo systemctl restart nginx
```

---

## 📊 Coûts Estimés

### Railway.app ⭐ RECOMMANDÉ
- **Plan Gratuit:** 0€/mois (500 heures/mois)
- **Plan Hobby:** 5$/mois (usage illimité)
- **Base de données PostgreSQL:** Incluse gratuitement
- **Total:** **GRATUIT** pour commencer!

### Render.com
- **Plan Gratuit:** 0€/mois
- **Base de données PostgreSQL:** 0€/mois (gratuit)
- **Limitation:** Se met en veille après 15 min
- **Total:** **GRATUIT**

### Azure
- **Plan Gratuit (F1):** 0€/mois (limité à 60 min/jour)
- **Plan Basic (B1):** ~13€/mois
- **Base de données Basic:** ~5€/mois
- **Total minimum:** ~18€/mois

### VPS (DigitalOcean, Linode, etc.)
- **Droplet 1GB RAM:** ~5-6€/mois
- **Droplet 2GB RAM:** ~12€/mois (recommandé)

---

## 🏆 Quelle option choisir?

### Pour un débutant (VOUS):
1. **Railway.app** ⭐⭐⭐⭐⭐ - Le plus simple, gratuit, parfait pour apprendre
2. **Render.com** ⭐⭐⭐⭐ - Gratuit mais se met en veille
3. **Azure** ⭐⭐⭐ - Professionnel mais payant

### Pour un projet professionnel:
1. **Azure** - Le plus robuste et scalable
2. **Railway.app (Plan Hobby)** - Bon rapport qualité/prix
3. **VPS** - Si vous avez les compétences techniques

---

## 🎓 Ressources Supplémentaires

- [Documentation Azure App Service](https://docs.microsoft.com/azure/app-service/)
- [Documentation .NET Deployment](https://docs.microsoft.com/aspnet/core/host-and-deploy/)
- [Tutoriel Nginx](https://www.nginx.com/resources/wiki/start/)
- [Let's Encrypt](https://letsencrypt.org/)

---

## 📞 Support

Si vous rencontrez des problèmes:
1. Vérifiez les logs d'erreur
2. Consultez la section Dépannage ci-dessus
3. Recherchez l'erreur sur Google/Stack Overflow
4. Créez une issue sur GitHub

---

**Bon déploiement! 🚀**

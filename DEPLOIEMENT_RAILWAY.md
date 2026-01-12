# 🚂 Déploiement sur Railway.app - Guide Rapide

## ✅ Votre projet est maintenant prêt pour Railway !

### 📦 Ce qui a été configuré :
- ✅ `railway.toml` - Configuration Railway
- ✅ `nixpacks.toml` - Configuration de build
- ✅ Support PostgreSQL ajouté
- ✅ Détection automatique de la base de données

---

## 🚀 Étapes de Déploiement (10 minutes)

### 1️⃣ Créer un compte Railway

1. Allez sur **[railway.app](https://railway.app)**
2. Cliquez sur **"Login"**
3. Connectez-vous avec **GitHub** (recommandé)
4. Autorisez Railway à accéder à vos dépôts

### 2️⃣ Pousser le code sur GitHub

```bash
# Dans votre terminal, exécutez:
git add .
git commit -m "Configuration Railway: support PostgreSQL"
git push origin main
```

### 3️⃣ Créer un nouveau projet sur Railway

1. Sur Railway, cliquez sur **"New Project"**
2. Sélectionnez **"Deploy from GitHub repo"**
3. Choisissez le dépôt **`Gestion-hopital`**
4. Railway commencera automatiquement le déploiement

### 4️⃣ Ajouter une base de données PostgreSQL

1. Dans votre projet Railway, cliquez sur **"+ New"**
2. Sélectionnez **"Database"**
3. Choisissez **"Add PostgreSQL"**
4. Railway créera automatiquement la base de données
5. La variable `DATABASE_URL` sera automatiquement liée

### 5️⃣ Configurer les variables d'environnement

1. Cliquez sur votre **service web** (l'application)
2. Allez dans l'onglet **"Variables"**
3. Ajoutez ces variables:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:$PORT
```

**Note:** La variable `DATABASE_URL` est automatiquement ajoutée par Railway quand vous créez la base PostgreSQL.

### 6️⃣ Redéployer (si nécessaire)

1. Dans votre service, allez dans **"Deployments"**
2. Cliquez sur **"Redeploy"** si le premier déploiement a échoué
3. Attendez 3-5 minutes

### 7️⃣ Générer un domaine public

1. Dans votre service, allez dans **"Settings"**
2. Cliquez sur **"Generate Domain"**
3. Vous obtiendrez une URL comme: `https://gestion-hopital-production.up.railway.app`

### 8️⃣ Tester votre application

1. Ouvrez l'URL générée dans votre navigateur
2. Vous devriez voir votre application !
3. Les données de test seront automatiquement chargées

---

## 🔐 Comptes de Test

Une fois déployé, vous pouvez vous connecter avec:

### Administrateur
- **Email:** `admin@hospital.com`
- **Mot de passe:** `Admin123!`

### Médecin
- **Email:** `medecin@hospital.com`
- **Mot de passe:** `Medecin123!`

### Réceptionniste
- **Email:** `receptionniste@hospital.com`
- **Mot de passe:** `Receptionniste123!`

### Patient
- **Email:** `patient@hospital.com`
- **Mot de passe:** `Patient123!`

---

## 📊 Données de Test Incluses

Votre application sera déployée avec:
- ✅ 6 Services médicaux
- ✅ 5 Patients
- ✅ 6 Médecins
- ✅ 6 Rendez-vous
- ✅ 6 Dossiers médicaux

---

## 🔧 Dépannage

### Problème: Le déploiement échoue

**Solution:**
1. Vérifiez les logs dans Railway (onglet "Deployments")
2. Assurez-vous que la base PostgreSQL est bien créée
3. Vérifiez que `DATABASE_URL` est bien définie
4. Redéployez en cliquant sur "Redeploy"

### Problème: Erreur de connexion à la base de données

**Solution:**
1. Allez dans votre service → "Variables"
2. Vérifiez que `DATABASE_URL` existe
3. Si elle n'existe pas, liez manuellement la base:
   - Cliquez sur "+ New Variable"
   - Sélectionnez "Add Reference"
   - Choisissez votre base PostgreSQL → `DATABASE_URL`

### Problème: L'application ne démarre pas

**Solution:**
1. Vérifiez les logs dans "Deployments"
2. Assurez-vous que `ASPNETCORE_URLS=http://0.0.0.0:$PORT` est défini
3. Vérifiez que le port est bien configuré

### Problème: Les migrations ne s'appliquent pas

**Solution:**
Les migrations s'appliquent automatiquement au démarrage grâce à `Program.cs`.
Si elles ne s'appliquent pas:
1. Vérifiez les logs
2. Assurez-vous que la connexion à la base fonctionne
3. Redéployez l'application

---

## 💰 Coûts

### Plan Gratuit (Hobby)
- **500 heures d'exécution par mois** (environ 20 jours)
- **Base de données PostgreSQL gratuite** (512 MB)
- **Parfait pour:** Développement, tests, projets personnels

### Plan Hobby ($5/mois)
- **Exécution illimitée**
- **Base de données PostgreSQL** (1 GB)
- **Parfait pour:** Projets en production

---

## 📈 Prochaines Étapes

Une fois déployé:

1. **Testez toutes les fonctionnalités:**
   - Connexion avec différents rôles
   - Création de patients
   - Gestion des rendez-vous
   - Dossiers médicaux

2. **Partagez votre URL:**
   - Avec votre équipe
   - Pour votre soutenance
   - Dans votre CV/Portfolio

3. **Configurez un domaine personnalisé (optionnel):**
   - Dans Railway → Settings → Custom Domain
   - Ajoutez votre propre domaine (ex: `hospital.votredomaine.com`)

4. **Surveillez l'utilisation:**
   - Railway → Metrics
   - Vérifiez les heures d'exécution restantes

---

## 🎉 Félicitations !

Votre application de gestion hospitalière est maintenant en ligne et accessible depuis n'importe où dans le monde !

**URL de votre application:** `https://[votre-projet].up.railway.app`

---

## 📞 Support

- **Documentation Railway:** [docs.railway.app](https://docs.railway.app)
- **Discord Railway:** [discord.gg/railway](https://discord.gg/railway)
- **GitHub Issues:** Créez une issue sur votre dépôt

---

**Bon déploiement! 🚀**

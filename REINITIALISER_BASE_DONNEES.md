# 🔄 GUIDE DE RÉINITIALISATION DE LA BASE DE DONNÉES

## Option 1 : Réinitialisation complète (RECOMMANDÉ)

Cette méthode supprime et recrée la base de données avec toutes les données de test.

### Étapes :

1. **Ouvrir un terminal dans le dossier du projet**

2. **Supprimer la base de données existante :**
```bash
dotnet ef database drop --force
```

3. **Recréer la base de données avec les migrations :**
```bash
dotnet ef database update
```

4. **Lancer l'application :**
```bash
dotnet run
```

Au démarrage, l'application va automatiquement créer :
- ✅ 4 rôles (Admin, Médecin, Réceptionniste, Patient)
- ✅ 4 comptes utilisateurs de test
- ✅ 6 services hospitaliers
- ✅ 5 patients
- ✅ 6 médecins
- ✅ 6 rendez-vous
- ✅ 6 dossiers médicaux

---

## Option 2 : Vérification des données existantes

Si vous voulez juste vérifier si les données sont déjà chargées :

1. **Lancer l'application :**
```bash
dotnet run
```

2. **Regarder la console** - Vous verrez des messages comme :
   - `✅ Les données de test existent déjà.` (si déjà chargées)
   - `🔄 Chargement des données de test...` (si en cours de chargement)
   - `✅ 6 services créés.`
   - `✅ 5 patients créés.`
   - etc.

---

## 📊 DONNÉES DE TEST CRÉÉES

### 🏥 Services (6)
1. **Cardiologie** - Bâtiment A, Étage 2
2. **Pédiatrie** - Bâtiment B, Étage 1
3. **Urgences** - Rez-de-chaussée
4. **Chirurgie Générale** - Bâtiment C, Étage 3
5. **Orthopédie** - Bâtiment D, Étage 2
6. **Dermatologie** - Bâtiment A, Étage 1

### 👨‍⚕️ Médecins (6)
1. **Dr. Karim Bennani** - Cardiologie (MED001)
2. **Dr. Nadia Idrissi** - Pédiatrie (MED002)
3. **Dr. Rachid Fassi** - Chirurgie Générale (MED003)
4. **Dr. Amina Tazi** - Orthopédie (MED004)
5. **Dr. Jamal Rami** - Dermatologie (MED005)
6. **Dr. Samir Kabbaj** - Urgences (MED006)

### 🧑‍🤝‍🧑 Patients (5)
1. **Ahmed Benani** - ahmed.benani@email.com
2. **Fatima Alaoui** - fatima.alaoui@email.com
3. **Mohamed Bouazza** - mohamed.bouazza@email.com
4. **Leila Chaoui** - leila.chaoui@email.com
5. **Hassan Darif** - hassan.darif@email.com

### 📅 Rendez-vous (6)
- Plusieurs rendez-vous programmés dans les prochains jours
- Différents statuts : Confirmé, En attente

### 📋 Dossiers Médicaux (6)
- Hypertension artérielle
- Otite moyenne
- Hernie discale
- Fracture du poignet
- Dermatite allergique
- Suivi cardiaque

---

## 🔐 COMPTES DE TEST

Ces comptes sont créés automatiquement au démarrage :

| Rôle | Email | Mot de passe |
|------|-------|--------------|
| Admin | admin@hospital.com | Admin123! |
| Réceptionniste | receptionniste@hospital.com | Receptionniste123! |
| Médecin | medecin@hospital.com | Medecin123! |
| Patient | patient@hospital.com | Patient123! |

---

## ⚠️ RÉSOLUTION DE PROBLÈMES

### Erreur : "Cannot drop database because it is currently in use"
**Solution :** Fermez toutes les connexions à la base de données (arrêtez l'application, fermez SQL Server Management Studio, etc.)

### Les données ne s'affichent pas
**Solution :** 
1. Vérifiez la console au démarrage pour voir les messages de chargement
2. Connectez-vous avec un compte Admin pour voir toutes les données
3. Vérifiez que vous êtes sur la bonne page (ex: /Patients, /Medecins, etc.)

### Erreur de migration
**Solution :**
```bash
# Supprimer toutes les migrations
rm -rf Migrations/

# Créer une nouvelle migration
dotnet ef migrations add InitialCreate

# Appliquer la migration
dotnet ef database update
```

---

## 🎯 VÉRIFICATION APRÈS CHARGEMENT

Pour vérifier que tout fonctionne :

1. **Connectez-vous avec le compte Admin** : admin@hospital.com / Admin123!

2. **Vérifiez chaque module :**
   - `/Services` → Devrait afficher 6 services
   - `/Medecins` → Devrait afficher 6 médecins
   - `/Patients` → Devrait afficher 5 patients
   - `/RendezVous` → Devrait afficher 6 rendez-vous
   - `/DossiersMedicaux` → Devrait afficher 6 dossiers

3. **Testez les autres rôles :**
   - Déconnectez-vous
   - Connectez-vous avec medecin@hospital.com
   - Vérifiez que vous voyez uniquement vos rendez-vous

---

## 📝 NOTES IMPORTANTES

- Les données de test sont chargées **UNE SEULE FOIS** au premier démarrage
- Si vous relancez l'application, les données existantes ne seront pas dupliquées
- Pour recharger les données, vous devez supprimer la base de données (Option 1)
- Les dates des rendez-vous sont calculées dynamiquement (dans les prochains jours)

---

*Guide créé pour faciliter la démonstration du projet Hospital Management System*

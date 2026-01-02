# 📊 DONNÉES DE TEST - HOSPITAL MANAGEMENT SYSTEM

Ce document liste toutes les données de test chargées automatiquement dans l'application.

---

## 🔐 COMPTES UTILISATEURS

| Rôle | Email | Mot de passe | Accès |
|------|-------|--------------|-------|
| **Admin** | admin@hospital.com | Admin123! | Accès complet à tout |
| **Réceptionniste** | receptionniste@hospital.com | Receptionniste123! | Patients, Rendez-vous |
| **Médecin** | medecin@hospital.com | Medecin123! | Ses rendez-vous, Dossiers médicaux |
| **Patient** | patient@hospital.com | Patient123! | Ses rendez-vous, Ses dossiers |

---

## 🏥 SERVICES HOSPITALIERS (6)

| # | Nom | Description | Capacité | Emplacement | Téléphone |
|---|-----|-------------|----------|-------------|-----------|
| 1 | **Cardiologie** | Maladies du cœur et système cardiovasculaire | 50 | Bâtiment A, Étage 2 | +212 5 29 11 11 11 |
| 2 | **Pédiatrie** | Soins médicaux pour enfants et nourrissons | 40 | Bâtiment B, Étage 1 | +212 5 29 11 11 12 |
| 3 | **Urgences** | Service d'urgence 24h/24, 7j/7 | 100 | Rez-de-chaussée | +212 5 29 11 11 13 |
| 4 | **Chirurgie Générale** | Interventions chirurgicales | 30 | Bâtiment C, Étage 3 | +212 5 29 11 11 14 |
| 5 | **Orthopédie** | Maladies et blessures des os | 35 | Bâtiment D, Étage 2 | +212 5 29 11 11 15 |
| 6 | **Dermatologie** | Maladies de la peau | 25 | Bâtiment A, Étage 1 | +212 5 29 11 11 16 |

---

## 👨‍⚕️ MÉDECINS (6)

| # | Nom | Spécialité | Email | Licence | Service | Embauche |
|---|-----|------------|-------|---------|---------|----------|
| 1 | **Dr. Karim Bennani** | Cardiologie | karim.bennani@hospital.com | MED001 | Cardiologie | Il y a 5 ans |
| 2 | **Dr. Nadia Idrissi** | Pédiatrie | nadia.idrissi@hospital.com | MED002 | Pédiatrie | Il y a 4 ans |
| 3 | **Dr. Rachid Fassi** | Chirurgie Générale | rachid.fassi@hospital.com | MED003 | Chirurgie | Il y a 6 ans |
| 4 | **Dr. Amina Tazi** | Orthopédie | amina.tazi@hospital.com | MED004 | Orthopédie | Il y a 3 ans |
| 5 | **Dr. Jamal Rami** | Dermatologie | jamal.rami@hospital.com | MED005 | Dermatologie | Il y a 2 ans |
| 6 | **Dr. Samir Kabbaj** | Urgences | samir.kabbaj@hospital.com | MED006 | Urgences | Il y a 7 ans |

---

## 🧑‍🤝‍🧑 PATIENTS (5)

| # | Nom | Date naissance | Sexe | Téléphone | Email | Adresse | N° Sécu | Inscription |
|---|-----|----------------|------|-----------|-------|---------|---------|-------------|
| 1 | **Ahmed Benani** | 15/05/1985 | M | +212 6 12 34 56 78 | ahmed.benani@email.com | 123 Rue de la Paix, Casablanca | 1234567890 | Il y a 6 mois |
| 2 | **Fatima Alaoui** | 22/08/1990 | F | +212 6 23 45 67 89 | fatima.alaoui@email.com | 456 Avenue Mohammed V, Rabat | 0987654321 | Il y a 4 mois |
| 3 | **Mohamed Bouazza** | 10/03/1988 | M | +212 6 34 56 78 90 | mohamed.bouazza@email.com | 789 Boulevard Zerktouni, Casablanca | 1122334455 | Il y a 3 mois |
| 4 | **Leila Chaoui** | 28/11/1992 | F | +212 6 45 67 89 01 | leila.chaoui@email.com | 321 Rue Tarik Ibn Ziad, Fes | 5566778899 | Il y a 2 mois |
| 5 | **Hassan Darif** | 05/07/1980 | M | +212 6 56 78 90 12 | hassan.darif@email.com | 654 Avenue Hassan II, Marrakech | 9988776655 | Il y a 1 mois |

---

## 📅 RENDEZ-VOUS (6)

| # | Patient | Médecin | Date/Heure | Motif | Statut | Durée |
|---|---------|---------|------------|-------|--------|-------|
| 1 | Ahmed Benani | Dr. Karim Bennani | Dans 3 jours à 10h00 | Consultation cardiaque | ✅ Confirmé | 30 min |
| 2 | Fatima Alaoui | Dr. Nadia Idrissi | Dans 5 jours à 14h00 | Visite pédiatrique | ✅ Confirmé | 30 min |
| 3 | Mohamed Bouazza | Dr. Rachid Fassi | Dans 7 jours à 09h00 | Consultation pré-opératoire | ✅ Confirmé | 30 min |
| 4 | Leila Chaoui | Dr. Amina Tazi | Dans 2 jours à 15h00 | Consultation orthopédique | ✅ Confirmé | 30 min |
| 5 | Hassan Darif | Dr. Jamal Rami | Dans 4 jours à 11h00 | Consultation dermatologique | ✅ Confirmé | 30 min |
| 6 | Ahmed Benani | Dr. Nadia Idrissi | Demain à 16h00 | Suivi médical | ⏳ En attente | 30 min |

---

## 📋 DOSSIERS MÉDICAUX (6)

### 1. Ahmed Benani - Hypertension artérielle
- **Médecin** : Dr. Karim Bennani (Cardiologie)
- **Date consultation** : Il y a 2 mois
- **Résumé** : Consultation cardiaque - Hypertension artérielle
- **Diagnostic** : Hypertension artérielle
- **Traitement** : Antihypertenseur quotidien
- **Observations** : Patient stable, suivi régulier recommandé

### 2. Fatima Alaoui - Otite moyenne
- **Médecin** : Dr. Nadia Idrissi (Pédiatrie)
- **Date consultation** : Il y a 1 mois
- **Résumé** : Consultation pédiatrique - Otite moyenne
- **Diagnostic** : Otite moyenne
- **Traitement** : Antibiotiques et anti-inflammatoires
- **Observations** : Amélioration notable après 5 jours de traitement

### 3. Mohamed Bouazza - Hernie discale
- **Médecin** : Dr. Rachid Fassi (Chirurgie Générale)
- **Date consultation** : Il y a 3 mois
- **Résumé** : Consultation chirurgicale - Hernie discale
- **Diagnostic** : Hernie discale
- **Traitement** : Intervention chirurgicale programmée
- **Observations** : Préparation pré-opératoire en cours

### 4. Leila Chaoui - Fracture du poignet
- **Médecin** : Dr. Amina Tazi (Orthopédie)
- **Date consultation** : Il y a 1 mois
- **Résumé** : Consultation orthopédique - Fracture du poignet
- **Diagnostic** : Fracture du poignet
- **Traitement** : Immobilisation et physiothérapie
- **Observations** : Consolidation progressive, suivi hebdomadaire

### 5. Hassan Darif - Dermatite allergique
- **Médecin** : Dr. Jamal Rami (Dermatologie)
- **Date consultation** : Il y a 2 mois
- **Résumé** : Consultation dermatologique - Dermatite allergique
- **Diagnostic** : Dermatite allergique
- **Traitement** : Crème corticoïde et antihistaminiques
- **Observations** : Amélioration après identification de l'allergène

### 6. Ahmed Benani - Suivi cardiaque
- **Médecin** : Dr. Karim Bennani (Cardiologie)
- **Date consultation** : Il y a 1 mois
- **Résumé** : Suivi cardiaque régulier - Contrôle tension
- **Diagnostic** : Suivi cardiaque
- **Traitement** : Continuation du traitement actuel
- **Observations** : Résultats ECG normaux, tension artérielle contrôlée

---

## 🎯 SCÉNARIOS DE DÉMONSTRATION

### Scénario 1 : Connexion Admin
1. Se connecter avec `admin@hospital.com` / `Admin123!`
2. Voir tous les services (6)
3. Voir tous les médecins (6)
4. Voir tous les patients (5)
5. Voir tous les rendez-vous (6)
6. Voir tous les dossiers médicaux (6)

### Scénario 2 : Connexion Médecin
1. Se connecter avec `medecin@hospital.com` / `Medecin123!`
2. Voir uniquement ses propres rendez-vous
3. Créer un dossier médical pour un patient
4. Consulter les dossiers de ses patients

### Scénario 3 : Connexion Patient
1. Se connecter avec `patient@hospital.com` / `Patient123!`
2. Voir uniquement ses propres rendez-vous
3. Consulter ses propres dossiers médicaux
4. Voir son profil

### Scénario 4 : Connexion Réceptionniste
1. Se connecter avec `receptionniste@hospital.com` / `Receptionniste123!`
2. Créer un nouveau patient
3. Créer un rendez-vous pour un patient
4. Modifier un rendez-vous existant

---

## 📊 STATISTIQUES DES DONNÉES

- **Total utilisateurs** : 4 (1 Admin, 1 Réceptionniste, 1 Médecin, 1 Patient)
- **Total services** : 6
- **Total médecins** : 6 (1 par service)
- **Total patients** : 5
- **Total rendez-vous** : 6 (5 confirmés, 1 en attente)
- **Total dossiers médicaux** : 6
- **Capacité totale d'accueil** : 280 lits

---

## 🔄 RELATIONS ENTRE LES DONNÉES

### Relations Patient → Rendez-vous
- Ahmed Benani : 2 rendez-vous
- Fatima Alaoui : 1 rendez-vous
- Mohamed Bouazza : 1 rendez-vous
- Leila Chaoui : 1 rendez-vous
- Hassan Darif : 1 rendez-vous

### Relations Patient → Dossiers médicaux
- Ahmed Benani : 2 dossiers
- Fatima Alaoui : 1 dossier
- Mohamed Bouazza : 1 dossier
- Leila Chaoui : 1 dossier
- Hassan Darif : 1 dossier

### Relations Médecin → Rendez-vous
- Dr. Karim Bennani : 1 rendez-vous
- Dr. Nadia Idrissi : 2 rendez-vous
- Dr. Rachid Fassi : 1 rendez-vous
- Dr. Amina Tazi : 1 rendez-vous
- Dr. Jamal Rami : 1 rendez-vous
- Dr. Samir Kabbaj : 0 rendez-vous

### Relations Service → Médecins
- Cardiologie : 1 médecin
- Pédiatrie : 1 médecin
- Urgences : 1 médecin
- Chirurgie Générale : 1 médecin
- Orthopédie : 1 médecin
- Dermatologie : 1 médecin

---

*Document créé pour faciliter la démonstration et la soutenance du projet*

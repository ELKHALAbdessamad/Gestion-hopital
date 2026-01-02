# Requirements Document - Système de Gestion Hospitalière avec Contrôle d'Accès par Rôle

## Introduction

Le Système de Gestion Hospitalière est une application web ASP.NET Core permettant de gérer les patients, médecins, services, rendez-vous et dossiers médicaux. Le système doit implémenter un contrôle d'accès granulaire basé sur quatre rôles distincts (Administrateur, Médecin, Réceptionniste, Patient) avec des permissions spécifiques pour chaque rôle. Chaque acteur doit accéder uniquement aux fonctionnalités et données autorisées selon son rôle.

## Glossary

- **Administrateur (Admin)**: Utilisateur avec accès complet au système pour gérer les services, médecins, patients et consulter les statistiques
- **Médecin**: Utilisateur autorisé à gérer ses rendez-vous, consulter et créer des dossiers médicaux pour ses patients
- **Réceptionniste**: Utilisateur autorisé à gérer les patients, créer et modifier les rendez-vous, consulter les dossiers médicaux
- **Patient**: Utilisateur autorisé à consulter ses propres rendez-vous et son dossier médical
- **Service**: Unité organisationnelle de l'hôpital (ex: Cardiologie, Pédiatrie) regroupant des médecins
- **Rendez-vous**: Consultation planifiée entre un patient et un médecin à une date et heure spécifiques
- **Dossier Médical**: Historique médical complet d'un patient incluant diagnostic, traitement et observations
- **Authentification**: Processus de vérification de l'identité d'un utilisateur via email et mot de passe
- **Autorisation**: Processus de vérification des permissions d'un utilisateur pour accéder à une ressource
- **CRUD**: Create (Créer), Read (Lire), Update (Modifier), Delete (Supprimer)

---

## Requirements

### Requirement 1: Authentification et Gestion des Utilisateurs

**User Story:** En tant qu'utilisateur, je veux me connecter au système avec mes identifiants, afin d'accéder aux fonctionnalités autorisées selon mon rôle.

#### Acceptance Criteria

1. WHEN un utilisateur accède à la page de connexion THEN THE system SHALL afficher un formulaire avec champs email et mot de passe
2. WHEN un utilisateur saisit des identifiants valides THEN THE system SHALL authentifier l'utilisateur et le rediriger vers le tableau de bord approprié à son rôle
3. WHEN un utilisateur saisit des identifiants invalides THEN THE system SHALL afficher un message d'erreur et maintenir l'utilisateur sur la page de connexion
4. WHEN un utilisateur clique sur "Inscription" THEN THE system SHALL afficher un formulaire d'inscription avec sélection du rôle (Admin, Médecin, Réceptionniste, Patient)
5. WHEN un utilisateur complète l'inscription avec des données valides THEN THE system SHALL créer le compte et authentifier automatiquement l'utilisateur

---

### Requirement 2: Gestion des Services (Administrateur)

**User Story:** En tant qu'administrateur, je veux gérer les services de l'hôpital, afin d'organiser les médecins par spécialité.

#### Acceptance Criteria

1. WHEN un administrateur accède à la section Services THEN THE system SHALL afficher la liste complète de tous les services avec nom, description et capacité
2. WHEN un administrateur clique sur "Créer Service" THEN THE system SHALL afficher un formulaire pour saisir nom, description, capacité d'accueil et emplacement
3. WHEN un administrateur soumet un nouveau service avec données valides THEN THE system SHALL créer le service et afficher un message de succès
4. WHEN un administrateur clique sur "Modifier" pour un service THEN THE system SHALL afficher le formulaire pré-rempli avec les données actuelles
5. WHEN un administrateur soumet les modifications THEN THE system SHALL mettre à jour le service et afficher un message de succès
6. WHEN un administrateur clique sur "Supprimer" pour un service THEN THE system SHALL vérifier qu'aucun médecin n'est assigné et supprimer le service
7. IF un administrateur tente de supprimer un service contenant des médecins THEN THE system SHALL afficher un message d'erreur et empêcher la suppression

---

### Requirement 3: Gestion des Médecins (Administrateur)

**User Story:** En tant qu'administrateur, je veux gérer les médecins de l'hôpital, afin de maintenir un registre à jour des professionnels de santé.

#### Acceptance Criteria

1. WHEN un administrateur accède à la section Médecins THEN THE system SHALL afficher la liste complète de tous les médecins avec nom, prénom, spécialité, email et service
2. WHEN un administrateur clique sur "Créer Médecin" THEN THE system SHALL afficher un formulaire avec champs nom, prénom, spécialité, email, numéro de licence et sélection du service
3. WHEN un administrateur soumet un nouveau médecin avec données valides THEN THE system SHALL créer le médecin et l'assigner au service sélectionné
4. WHEN un administrateur clique sur "Modifier" pour un médecin THEN THE system SHALL afficher le formulaire pré-rempli avec les données actuelles
5. WHEN un administrateur soumet les modifications THEN THE system SHALL mettre à jour le médecin et afficher un message de succès
6. WHEN un administrateur clique sur "Supprimer" pour un médecin THEN THE system SHALL supprimer le médecin et tous ses rendez-vous associés
7. WHEN un administrateur consulte les détails d'un médecin THEN THE system SHALL afficher le service auquel il est assigné et la liste de ses rendez-vous

---

### Requirement 4: Gestion des Patients (Réceptionniste et Administrateur)

**User Story:** En tant que réceptionniste, je veux gérer les patients de l'hôpital, afin de maintenir un registre à jour des informations personnelles.

#### Acceptance Criteria

1. WHEN un réceptionniste accède à la section Patients THEN THE system SHALL afficher la liste complète de tous les patients avec nom, prénom, date de naissance, téléphone et adresse
2. WHEN un réceptionniste clique sur "Créer Patient" THEN THE system SHALL afficher un formulaire avec champs nom, prénom, date de naissance, sexe, téléphone, email, adresse et numéro de sécurité sociale
3. WHEN un réceptionniste soumet un nouveau patient avec données valides THEN THE system SHALL créer le patient et afficher un message de succès
4. WHEN un réceptionniste clique sur "Modifier" pour un patient THEN THE system SHALL afficher le formulaire pré-rempli avec les données actuelles
5. WHEN un réceptionniste soumet les modifications THEN THE system SHALL mettre à jour le patient et afficher un message de succès
6. WHEN un réceptionniste clique sur "Supprimer" pour un patient THEN THE system SHALL supprimer le patient et tous ses rendez-vous et dossiers médicaux associés
7. WHEN un réceptionniste consulte les détails d'un patient THEN THE system SHALL afficher l'historique complet des rendez-vous et dossiers médicaux

---

### Requirement 5: Gestion des Rendez-vous (Réceptionniste, Médecin et Patient)

**User Story:** En tant que réceptionniste, je veux planifier et gérer les rendez-vous, afin d'organiser les consultations entre patients et médecins.

#### Acceptance Criteria

1. WHEN un réceptionniste accède à la section Rendez-vous THEN THE system SHALL afficher la liste complète de tous les rendez-vous avec patient, médecin, date, heure et statut
2. WHEN un réceptionniste clique sur "Créer Rendez-vous" THEN THE system SHALL afficher un formulaire avec sélection du patient, médecin, date, heure, motif et durée
3. WHEN un réceptionniste soumet un nouveau rendez-vous avec données valides THEN THE system SHALL vérifier la disponibilité du médecin et créer le rendez-vous
4. IF un réceptionniste tente de créer un rendez-vous à un créneau occupé THEN THE system SHALL afficher un message d'erreur et empêcher la création
5. WHEN un réceptionniste clique sur "Modifier" pour un rendez-vous THEN THE system SHALL afficher le formulaire pré-rempli avec les données actuelles
6. WHEN un réceptionniste soumet les modifications THEN THE system SHALL mettre à jour le rendez-vous et afficher un message de succès
7. WHEN un réceptionniste clique sur "Supprimer" pour un rendez-vous THEN THE system SHALL supprimer le rendez-vous et afficher un message de succès
8. WHEN un médecin accède à la section Rendez-vous THEN THE system SHALL afficher uniquement ses rendez-vous filtrés par sa personne
9. WHEN un patient accède à la section Rendez-vous THEN THE system SHALL afficher uniquement ses rendez-vous filtrés par son compte
10. WHEN un utilisateur consulte les rendez-vous du jour THEN THE system SHALL afficher uniquement les rendez-vous prévus pour la date actuelle

---

### Requirement 6: Gestion des Dossiers Médicaux (Médecin et Réceptionniste)

**User Story:** En tant que médecin, je veux consulter et créer des dossiers médicaux pour mes patients, afin de documenter les consultations et traitements.

#### Acceptance Criteria

1. WHEN un médecin accède à la section Dossiers Médicaux THEN THE system SHALL afficher la liste des dossiers pour ses patients uniquement
2. WHEN un réceptionniste accède à la section Dossiers Médicaux THEN THE system SHALL afficher la liste complète de tous les dossiers médicaux
3. WHEN un médecin clique sur "Créer Dossier" THEN THE system SHALL afficher un formulaire avec sélection du patient, date de consultation, diagnostic, traitement et observations
4. WHEN un médecin soumet un nouveau dossier avec données valides THEN THE system SHALL créer le dossier médical et afficher un message de succès
5. WHEN un médecin clique sur "Modifier" pour un dossier THEN THE system SHALL afficher le formulaire pré-rempli avec les données actuelles
6. WHEN un médecin soumet les modifications THEN THE system SHALL mettre à jour le dossier et afficher un message de succès
7. WHEN un médecin clique sur "Supprimer" pour un dossier THEN THE system SHALL supprimer le dossier et afficher un message de succès
8. WHEN un patient accède à son dossier médical THEN THE system SHALL afficher uniquement son propre dossier médical en lecture seule

---

### Requirement 7: Contrôle d'Accès Basé sur les Rôles

**User Story:** En tant qu'administrateur système, je veux que chaque utilisateur accède uniquement aux fonctionnalités autorisées selon son rôle, afin de sécuriser les données sensibles.

#### Acceptance Criteria

1. WHEN un utilisateur non authentifié tente d'accéder à une page protégée THEN THE system SHALL rediriger vers la page de connexion
2. WHEN un patient tente d'accéder à la section Gestion des Médecins THEN THE system SHALL afficher une page d'accès refusé
3. WHEN un réceptionniste tente d'accéder à la section Dashboard Administrateur THEN THE system SHALL afficher une page d'accès refusé
4. WHEN un médecin tente d'accéder à la section Gestion des Services THEN THE system SHALL afficher une page d'accès refusé
5. WHEN un administrateur accède à n'importe quelle section THEN THE system SHALL autoriser l'accès à toutes les fonctionnalités
6. WHEN un utilisateur tente de modifier une ressource via URL directe sans permission THEN THE system SHALL retourner une erreur 403 Forbidden

---

### Requirement 8: Dashboard et Statistiques (Administrateur)

**User Story:** En tant qu'administrateur, je veux consulter un tableau de bord avec statistiques, afin de monitorer l'activité de l'hôpital.

#### Acceptance Criteria

1. WHEN un administrateur accède au Dashboard THEN THE system SHALL afficher le nombre total de patients, médecins, services et rendez-vous
2. WHEN un administrateur consulte le Dashboard THEN THE system SHALL afficher le nombre de rendez-vous prévus pour aujourd'hui
3. WHEN un administrateur consulte le Dashboard THEN THE system SHALL afficher le nombre de rendez-vous par service
4. WHEN un administrateur consulte le Dashboard THEN THE system SHALL afficher le nombre de rendez-vous par médecin
5. WHEN un administrateur consulte le Dashboard THEN THE system SHALL afficher le taux d'occupation des services

---

### Requirement 9: Profil Utilisateur et Édition

**User Story:** En tant qu'utilisateur, je veux consulter et modifier mon profil, afin de maintenir mes informations à jour.

#### Acceptance Criteria

1. WHEN un utilisateur clique sur "Mon Profil" THEN THE system SHALL afficher ses informations personnelles (nom, prénom, email, téléphone, rôle)
2. WHEN un utilisateur clique sur "Modifier Profil" THEN THE system SHALL afficher un formulaire pré-rempli avec ses données actuelles
3. WHEN un utilisateur soumet les modifications THEN THE system SHALL mettre à jour le profil et afficher un message de succès
4. WHEN un utilisateur clique sur "Déconnexion" THEN THE system SHALL terminer la session et rediriger vers la page de connexion

---

### Requirement 10: Gestion des Erreurs et Validation

**User Story:** En tant qu'utilisateur, je veux recevoir des messages d'erreur clairs, afin de comprendre les problèmes et corriger mes actions.

#### Acceptance Criteria

1. WHEN un utilisateur soumet un formulaire avec données invalides THEN THE system SHALL afficher les messages d'erreur de validation pour chaque champ
2. WHEN un utilisateur tente une action non autorisée THEN THE system SHALL afficher un message d'erreur explicite
3. WHEN une erreur serveur se produit THEN THE system SHALL afficher une page d'erreur générique sans détails techniques
4. WHEN un utilisateur tente de créer une ressource avec un identifiant unique déjà existant THEN THE system SHALL afficher un message d'erreur et empêcher la création
5. WHEN un utilisateur tente de supprimer une ressource liée à d'autres données THEN THE system SHALL afficher un message d'erreur explicite et empêcher la suppression


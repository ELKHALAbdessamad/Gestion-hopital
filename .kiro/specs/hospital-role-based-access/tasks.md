# Implementation Plan - Système de Gestion Hospitalière avec Contrôle d'Accès par Rôle

## Overview

Ce plan d'implémentation divise le travail en trois phases principales :
1. **Phase 1 (Développeur 1)**: Gestion Administrative (Services, Médecins, Dashboard)
2. **Phase 2 (Développeur 2)**: Gestion Médicale (Dossiers, Rendez-vous, Authentification)
3. **Phase 3 (Développeur 3)**: Interface Patient (Profil, Rendez-vous Patient, Dossier Patient)

Chaque phase inclut l'implémentation des fonctionnalités, les tests unitaires et les tests de propriété.

---

## Phase 1: Gestion Administrative (Développeur 1)

- [x] 1. Implémenter le contrôle d'accès par rôle pour les Services
  - Ajouter l'attribut `[Authorize(Roles="Admin")]` au ServicesController
  - Vérifier que seuls les administrateurs peuvent accéder à la gestion des services
  - _Requirements: 2.1, 7.2, 7.3, 7.4, 7.5_

- [x] 1.1 Écrire les tests unitaires pour le contrôle d'accès des Services
  - Tester que les administrateurs peuvent accéder aux services
  - Tester que les non-administrateurs sont refusés
  - _Requirements: 7.2, 7.3, 7.4_

- [x] 1.2 Écrire le test de propriété pour le contrôle d'accès des Services
  - **Property 8: Role-Based Access Control Enforcement**
  - **Validates: Requirements 7.2, 7.3, 7.4, 7.5, 7.6**

- [x] 2. Implémenter la validation de suppression de Service
  - Vérifier qu'un service ne peut être supprimé que s'il ne contient pas de médecins
  - Afficher un message d'erreur approprié si la suppression est impossible
  - _Requirements: 2.6, 2.7_

- [x] 2.1 Écrire les tests unitaires pour la suppression de Service
  - Tester la suppression d'un service sans médecins
  - Tester la prévention de suppression d'un service avec médecins
  - _Requirements: 2.6, 2.7_

- [x] 2.2 Écrire le test de propriété pour la suppression de Service
  - **Property 3: Service Deletion Requires Empty Medecin List**
  - **Validates: Requirements 2.6, 2.7**

- [ ] 3. Implémenter le contrôle d'accès par rôle pour les Médecins
  - Ajouter l'attribut `[Authorize(Roles="Admin")]` au MedecinsController
  - Vérifier que seuls les administrateurs peuvent accéder à la gestion des médecins
  - _Requirements: 3.1, 7.2, 7.3, 7.4, 7.5_

- [ ] 3.1 Écrire les tests unitaires pour le contrôle d'accès des Médecins
  - Tester que les administrateurs peuvent accéder aux médecins
  - Tester que les non-administrateurs sont refusés
  - _Requirements: 7.2, 7.3, 7.4_

- [ ] 3.2 Écrire le test de propriété pour l'assignation de Médecin à Service
  - **Property 4: Medecin Assignment to Service**
  - **Validates: Requirements 3.3**

- [ ] 4. Implémenter la validation de création de Médecin
  - Vérifier qu'un médecin doit être assigné à un service
  - Afficher un message d'erreur si aucun service n'est sélectionné
  - _Requirements: 3.3_

- [ ] 4.1 Écrire les tests unitaires pour la création de Médecin
  - Tester la création d'un médecin avec un service valide
  - Tester la prévention de création d'un médecin sans service
  - _Requirements: 3.3_

- [ ] 5. Implémenter la cascade de suppression pour les Médecins
  - Vérifier que la suppression d'un médecin supprime tous ses rendez-vous
  - Afficher un message de succès après suppression
  - _Requirements: 3.6_

- [ ] 5.1 Écrire les tests unitaires pour la suppression de Médecin
  - Tester la suppression d'un médecin sans rendez-vous
  - Tester la suppression d'un médecin avec rendez-vous (cascade)
  - _Requirements: 3.6_

- [ ] 6. Créer le Dashboard avec statistiques
  - Créer une vue Dashboard.cshtml
  - Implémenter le HomeController pour afficher les statistiques
  - Afficher le nombre total de patients, médecins, services et rendez-vous
  - _Requirements: 8.1, 8.2, 8.3, 8.4, 8.5_

- [ ] 6.1 Écrire les tests unitaires pour le Dashboard
  - Tester que les statistiques sont correctes
  - Tester que seuls les administrateurs peuvent accéder au dashboard
  - _Requirements: 8.1, 8.2, 8.3, 8.4, 8.5_

- [ ] 6.2 Écrire le test de propriété pour l'exactitude des statistiques du Dashboard
  - **Property 12: Dashboard Statistics Accuracy**
  - **Validates: Requirements 8.1, 8.2, 8.3, 8.4, 8.5**

- [ ] 7. Checkpoint - Vérifier que tous les tests passent
  - Ensure all tests pass, ask the user if questions arise.

---

## Phase 2: Gestion Médicale (Développeur 2)

- [x] 8. Implémenter le contrôle d'accès par rôle pour l'Authentification
  - Vérifier que les utilisateurs non authentifiés sont redirigés vers la page de connexion
  - Implémenter la redirection appropriée selon le rôle après connexion
  - _Requirements: 1.2, 7.1_

- [x] 8.1 Écrire les tests unitaires pour l'authentification
  - Tester la connexion avec des identifiants valides
  - Tester la connexion avec des identifiants invalides
  - Tester la redirection selon le rôle
  - _Requirements: 1.2, 1.3_

- [x] 8.2 Écrire le test de propriété pour l'authentification réussie
  - **Property 1: Authentication Success Implies User Session**
  - **Validates: Requirements 1.2**

- [x] 8.3 Écrire le test de propriété pour les identifiants invalides
  - **Property 2: Invalid Credentials Prevent Authentication**
  - **Validates: Requirements 1.3**

- [x] 9. Implémenter l'inscription avec création de Patient
  - Créer un formulaire d'inscription avec sélection du rôle
  - Créer automatiquement un Patient si le rôle est "Patient"
  - Authentifier automatiquement l'utilisateur après inscription
  - _Requirements: 1.4, 1.5_

- [x] 9.1 Écrire les tests unitaires pour l'inscription
  - Tester l'inscription avec des données valides
  - Tester la création automatique du Patient
  - Tester l'authentification automatique
  - _Requirements: 1.4, 1.5_

- [x] 9.2 Écrire le test de propriété pour l'inscription
  - **Property 1: Authentication Success Implies User Session**
  - **Validates: Requirements 1.5**

- [x] 10. Implémenter le contrôle d'accès par rôle pour les Rendez-vous
  - Ajouter l'attribut `[Authorize(Roles="Admin,Medecin,Receptionniste,Patient")]` au RendezVousController
  - Implémenter le filtrage des rendez-vous selon le rôle
  - _Requirements: 5.1, 5.8, 5.9, 7.1_

- [x] 10.1 Écrire les tests unitaires pour le contrôle d'accès des Rendez-vous
  - Tester que les utilisateurs authentifiés peuvent accéder aux rendez-vous
  - Tester que les utilisateurs non authentifiés sont redirigés
  - _Requirements: 5.1, 7.1_

- [x] 10.2 Écrire le test de propriété pour l'isolation des données des Rendez-vous
  - **Property 6: Patient Data Isolation**
  - **Validates: Requirements 5.9, 8.8**

- [x] 10.3 Écrire le test de propriété pour l'isolation des données du Médecin
  - **Property 7: Medecin Data Isolation**
  - **Validates: Requirements 5.8, 6.1**

- [x] 11. Implémenter la détection des conflits de Rendez-vous
  - Vérifier qu'un médecin ne peut pas avoir deux rendez-vous au même moment
  - Afficher un message d'erreur si un conflit est détecté
  - _Requirements: 5.3, 5.4_

- [x] 11.1 Écrire les tests unitaires pour la détection des conflits
  - Tester la création d'un rendez-vous sans conflit
  - Tester la prévention de création d'un rendez-vous avec conflit
  - _Requirements: 5.3, 5.4_

- [x] 11.2 Écrire le test de propriété pour la détection des conflits
  - **Property 5: Appointment Conflict Detection**
  - **Validates: Requirements 5.3, 5.4**

- [x] 12. Implémenter le filtrage des Rendez-vous par rôle
  - Afficher tous les rendez-vous pour les administrateurs et réceptionnistes
  - Afficher uniquement les rendez-vous du médecin pour les médecins
  - Afficher uniquement les rendez-vous du patient pour les patients
  - _Requirements: 5.1, 5.8, 5.9_

- [x] 12.1 Écrire les tests unitaires pour le filtrage des Rendez-vous
  - Tester le filtrage pour chaque rôle
  - Tester que les données isolées ne sont pas visibles
  - _Requirements: 5.8, 5.9_

- [x] 13. Implémenter le contrôle d'accès par rôle pour les Dossiers Médicaux
  - Ajouter l'attribut `[Authorize(Roles="Admin,Medecin,Receptionniste,Patient")]` au DossiersMedicauxController
  - Implémenter le filtrage des dossiers selon le rôle
  - _Requirements: 6.1, 6.2, 6.8, 7.1_

- [x] 13.1 Écrire les tests unitaires pour le contrôle d'accès des Dossiers
  - Tester que les utilisateurs authentifiés peuvent accéder aux dossiers
  - Tester que les utilisateurs non authentifiés sont redirigés
  - _Requirements: 6.1, 6.2, 7.1_

- [x] 14. Implémenter le filtrage des Dossiers Médicaux par rôle
  - Afficher tous les dossiers pour les administrateurs et réceptionnistes
  - Afficher uniquement les dossiers des patients du médecin pour les médecins
  - Afficher uniquement le dossier du patient pour les patients (lecture seule)
  - _Requirements: 6.1, 6.2, 6.8_

- [x] 14.1 Écrire les tests unitaires pour le filtrage des Dossiers
  - Tester le filtrage pour chaque rôle
  - Tester que les données isolées ne sont pas visibles
  - Tester l'accès en lecture seule pour les patients
  - _Requirements: 6.1, 6.2, 6.8_

- [x] 15. Implémenter la validation des contraintes d'intégrité
  - Vérifier les contraintes uniques (email, numéro de licence, numéro de sécurité sociale)
  - Afficher des messages d'erreur appropriés
  - _Requirements: 10.4_

- [x] 15.1 Écrire les tests unitaires pour les contraintes uniques
  - Tester la création avec des valeurs uniques valides
  - Tester la prévention de création avec des valeurs uniques dupliquées
  - _Requirements: 10.4_

- [x] 15.2 Écrire le test de propriété pour l'application des contraintes uniques
  - **Property 10: Unique Constraint Enforcement**
  - **Validates: Requirements 10.4**

- [x] 16. Implémenter la gestion des erreurs de validation
  - Afficher les messages d'erreur de validation pour chaque champ
  - Afficher les messages d'erreur explicites pour les actions non autorisées
  - _Requirements: 10.1, 10.2_

- [x] 16.1 Écrire les tests unitaires pour la gestion des erreurs
  - Tester les messages d'erreur de validation
  - Tester les messages d'erreur d'autorisation
  - _Requirements: 10.1, 10.2_

- [x] 17. Checkpoint - Vérifier que tous les tests passent
  - Ensure all tests pass, ask the user if questions arise.

---

## Phase 3: Interface Patient (Développeur 3)

- [x] 18. Implémenter le contrôle d'accès par rôle pour le Profil Utilisateur
  - Ajouter l'attribut `[Authorize]` au AccountController pour les actions de profil
  - Vérifier que chaque utilisateur ne peut voir que son propre profil
  - _Requirements: 9.1, 9.2, 9.3, 9.4, 7.1_

- [x] 18.1 Écrire les tests unitaires pour le Profil Utilisateur
  - Tester l'accès au profil pour les utilisateurs authentifiés
  - Tester la redirection pour les utilisateurs non authentifiés
  - Tester que chaque utilisateur ne voit que son propre profil
  - _Requirements: 9.1, 9.2, 9.3, 9.4_

- [x] 19. Implémenter la modification du Profil Utilisateur
  - Créer un formulaire de modification du profil
  - Mettre à jour les informations de l'utilisateur
  - Afficher un message de succès après modification
  - _Requirements: 9.2, 9.3_

- [x] 19.1 Écrire les tests unitaires pour la modification du Profil
  - Tester la modification avec des données valides
  - Tester la modification avec des données invalides
  - _Requirements: 9.2, 9.3_

- [x] 19.2 Écrire le test de propriété pour la modification du Profil
  - **Property 9: Unauthenticated Access Redirection**
  - **Validates: Requirements 7.1**

- [x] 20. Implémenter la déconnexion
  - Créer une action Logout dans AccountController
  - Terminer la session utilisateur
  - Rediriger vers la page de connexion
  - _Requirements: 9.4_

- [x] 20.1 Écrire les tests unitaires pour la déconnexion
  - Tester que la session est terminée après déconnexion
  - Tester la redirection vers la page de connexion
  - _Requirements: 9.4_

- [x] 21. Implémenter la vue des Rendez-vous du Patient
  - Créer une vue pour afficher les rendez-vous du patient
  - Afficher uniquement les rendez-vous du patient authentifié
  - Afficher les détails du médecin et de la date/heure
  - _Requirements: 5.9, 5.10_

- [x] 21.1 Écrire les tests unitaires pour la vue des Rendez-vous du Patient
  - Tester que le patient voit uniquement ses rendez-vous
  - Tester que les rendez-vous d'autres patients ne sont pas visibles
  - _Requirements: 5.9_

- [x] 22. Implémenter la vue du Dossier Médical du Patient
  - Créer une vue pour afficher le dossier médical du patient
  - Afficher uniquement le dossier du patient authentifié
  - Afficher en mode lecture seule
  - _Requirements: 6.8_

- [x] 22.1 Écrire les tests unitaires pour la vue du Dossier du Patient
  - Tester que le patient voit uniquement son dossier
  - Tester que les dossiers d'autres patients ne sont pas visibles
  - Tester que le dossier est en lecture seule
  - _Requirements: 6.8_

- [x] 22.2 Écrire le test de propriété pour l'isolation des données du Patient
  - **Property 6: Patient Data Isolation**
  - **Validates: Requirements 5.9, 8.8**

- [x] 23. Implémenter la gestion des erreurs d'accès refusé
  - Créer une page d'erreur 403 Forbidden
  - Afficher un message explicite quand l'accès est refusé
  - _Requirements: 7.2, 7.3, 7.4, 7.6_

- [x] 23.1 Écrire les tests unitaires pour les erreurs d'accès refusé
  - Tester que les utilisateurs non autorisés reçoivent une erreur 403
  - Tester que le message d'erreur est explicite
  - _Requirements: 7.2, 7.3, 7.4, 7.6_

- [x] 23.2 Écrire le test de propriété pour l'application du contrôle d'accès
  - **Property 8: Role-Based Access Control Enforcement**
  - **Validates: Requirements 7.2, 7.3, 7.4, 7.5, 7.6**

- [x] 24. Implémenter la gestion des erreurs de suppression avec dépendances
  - Vérifier les dépendances avant suppression
  - Afficher un message d'erreur explicite si la suppression est impossible
  - _Requirements: 10.5_

- [x] 24.1 Écrire les tests unitaires pour la gestion des erreurs de suppression
  - Tester la suppression d'une ressource sans dépendances
  - Tester la prévention de suppression d'une ressource avec dépendances
  - _Requirements: 10.5_

- [x] 24.2 Écrire le test de propriété pour l'intégrité référentielle
  - **Property 11: Referential Integrity on Deletion**
  - **Validates: Requirements 10.5**

- [x] 25. Checkpoint - Vérifier que tous les tests passent
  - Ensure all tests pass, ask the user if questions arise.

---

## Summary

**Total Tasks**: 25 main tasks with 40+ sub-tasks
**Estimated Duration**: 3-4 weeks for 3 developers
**Testing Coverage**: 
- Unit tests for all CRUD operations
- Property-based tests for all correctness properties
- Authorization tests for all roles
- Data isolation tests for all entities

**Deliverables**:
- Fully functional hospital management system
- Role-based access control for all features
- Comprehensive test suite with 100+ tests
- Dashboard with statistics
- Patient portal with appointment and medical record views


# Design Document - Système de Gestion Hospitalière avec Contrôle d'Accès par Rôle

## Overview

Le système de gestion hospitalière est une application web ASP.NET Core MVC qui implémente un contrôle d'accès granulaire basé sur quatre rôles distincts. L'architecture suit le pattern MVC avec une séparation claire entre les couches de présentation (Views), logique métier (Controllers) et accès aux données (DbContext). Le système utilise ASP.NET Identity pour l'authentification et les rôles pour l'autorisation.

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     Presentation Layer                       │
│  (Views - Razor Pages avec contrôle d'accès par rôle)       │
└────────────────────┬────────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────────┐
│                   Controller Layer                           │
│  (AccountController, PatientsController, etc.)              │
│  - Authentification                                          │
│  - Autorisation par rôle [Authorize(Roles="...")]          │
│  - Filtrage des données selon le rôle                       │
└────────────────────┬────────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────────┐
│                   Data Access Layer                          │
│  (ApplicationDbContext - Entity Framework Core)             │
│  - Patients, Médecins, Services, RendezVous, Dossiers      │
│  - Relations et contraintes d'intégrité                     │
└────────────────────┬────────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────────┐
│                   Database Layer                             │
│  (SQL Server)                                                │
│  - Tables avec clés étrangères et index                     │
└─────────────────────────────────────────────────────────────┘
```

## Components and Interfaces

### 1. Authentication Component
- **AccountController**: Gère login, register, logout, profil
- **ApplicationUser**: Modèle utilisateur avec rôle et relations optionnelles
- **LoginViewModel, RegisterViewModel**: Modèles de vue pour authentification

### 2. Authorization Component
- **[Authorize]**: Attribut pour protéger les actions
- **[Authorize(Roles="Admin,Medecin")]**: Attribut pour contrôle d'accès par rôle
- **RoleAuthorizationMiddleware**: Middleware optionnel pour autorisation globale

### 3. Service Management Component
- **ServicesController**: CRUD pour les services
- **Service Model**: Entité représentant un service hospitalier
- **Validations**: Vérification que les services ne contiennent pas de médecins avant suppression

### 4. Doctor Management Component
- **MedecinsController**: CRUD pour les médecins
- **Medecin Model**: Entité représentant un médecin avec relation au service
- **Validations**: Vérification de l'unicité du numéro de licence

### 5. Patient Management Component
- **PatientsController**: CRUD pour les patients
- **Patient Model**: Entité représentant un patient
- **Validations**: Vérification de l'unicité du numéro de sécurité sociale

### 6. Appointment Management Component
- **RendezVousController**: CRUD pour les rendez-vous avec filtrage par rôle
- **RendezVous Model**: Entité représentant un rendez-vous
- **Validations**: Vérification de la disponibilité du médecin, détection des conflits

### 7. Medical Record Component
- **DossiersMedicauxController**: CRUD pour les dossiers médicaux
- **DossierMedical Model**: Entité représentant un dossier médical
- **Filtrage**: Affichage des dossiers selon le rôle (médecin voit ses patients, réceptionniste voit tous)

### 8. Dashboard Component
- **HomeController**: Affiche le dashboard avec statistiques
- **DashboardViewModel**: Modèle contenant les statistiques agrégées
- **Statistiques**: Nombre de patients, médecins, services, rendez-vous, taux d'occupation

## Data Models

### ApplicationUser
```
- Id (string, PK)
- UserName (string, unique)
- Email (string, unique)
- Nom (string, required)
- Prenom (string, required)
- Telephone (string, optional)
- Role (string, required) - Admin, Medecin, Receptionniste, Patient
- DateInscription (DateTime)
- PatientId (int, FK, optional)
- MedecinId (int, FK, optional)
```

### Patient
```
- Id (int, PK)
- Nom (string, required)
- Prenom (string, required)
- DateNaissance (DateTime, required)
- Sexe (string, required)
- Telephone (string, required)
- Email (string, optional)
- Adresse (string, required)
- NumeroSecuriteSociale (string, optional, unique)
- DateInscription (DateTime)
- RendezVous (ICollection<RendezVous>)
- DossiersMedicaux (ICollection<DossierMedical>)
```

### Medecin
```
- Id (int, PK)
- Nom (string, required)
- Prenom (string, required)
- Specialite (string, required)
- Email (string, required, unique)
- NumeroLicence (string, optional, unique)
- ServiceId (int, FK, required)
- DateEmbauche (DateTime)
- Service (Service, FK)
- RendezVous (ICollection<RendezVous>)
- DossiersMedicaux (ICollection<DossierMedical>)
```

### Service
```
- Id (int, PK)
- Nom (string, required, unique)
- Description (string, optional)
- CapaciteAccueil (int, default=50)
- Emplacement (string, optional)
- Telephone (string, optional)
- Medecins (ICollection<Medecin>)
```

### RendezVous
```
- Id (int, PK)
- PatientId (int, FK, required)
- MedecinId (int, FK, required)
- DateHeure (DateTime, required)
- Statut (string, default="Planifié")
- Motif (string, optional)
- Notes (string, optional)
- Duree (int, default=30)
- DateCreation (DateTime)
- Patient (Patient, FK)
- Medecin (Medecin, FK)
```

### DossierMedical
```
- Id (int, PK)
- PatientId (int, FK, required)
- MedecinId (int, FK, required)
- Resume (string, required)
- DateCreation (DateTime)
- DateConsultation (DateTime)
- Diagnostic (string, optional)
- Traitement (string, optional)
- Observations (string, optional)
- Patient (Patient, FK)
- Medecin (Medecin, FK)
```

## Correctness Properties

A property is a characteristic or behavior that should hold true across all valid executions of a system—essentially, a formal statement about what the system should do. Properties serve as the bridge between human-readable specifications and machine-verifiable correctness guarantees.

### Property 1: Authentication Success Implies User Session
*For any* valid email and password combination, after successful authentication, the system SHALL maintain an active user session that persists across subsequent requests.

**Validates: Requirements 1.2**

### Property 2: Invalid Credentials Prevent Authentication
*For any* invalid email or password combination, the system SHALL reject the authentication attempt and maintain the user in an unauthenticated state.

**Validates: Requirements 1.3**

### Property 3: Service Deletion Requires Empty Medecin List
*For any* service, the system SHALL prevent deletion if the service contains one or more médecins, and SHALL allow deletion only when the médecin list is empty.

**Validates: Requirements 2.6, 2.7**

### Property 4: Medecin Assignment to Service
*For any* médecin creation or modification, the system SHALL require a valid service assignment and SHALL prevent creation without a service.

**Validates: Requirements 3.3**

### Property 5: Appointment Conflict Detection
*For any* médecin and datetime combination, the system SHALL prevent creation of overlapping appointments for the same médecin at the same time.

**Validates: Requirements 5.3, 5.4**

### Property 6: Patient Data Isolation
*For any* patient user, the system SHALL display only their own rendez-vous and dossiers médicaux, and SHALL prevent access to other patients' data.

**Validates: Requirements 5.9, 8.8**

### Property 7: Medecin Data Isolation
*For any* médecin user, the system SHALL display only their own rendez-vous and dossiers médicaux for their patients, and SHALL prevent access to other médecins' data.

**Validates: Requirements 5.8, 6.1**

### Property 8: Role-Based Access Control Enforcement
*For any* user with role R and resource requiring role R', the system SHALL grant access if R equals R' or R is Admin, and SHALL deny access otherwise.

**Validates: Requirements 7.2, 7.3, 7.4, 7.5, 7.6**

### Property 9: Unauthenticated Access Redirection
*For any* unauthenticated user attempting to access a protected resource, the system SHALL redirect to the login page.

**Validates: Requirements 7.1**

### Property 10: Unique Constraint Enforcement
*For any* field marked as unique (email, numéro de licence, numéro de sécurité sociale), the system SHALL prevent creation of duplicate values and display an error message.

**Validates: Requirements 10.4**

### Property 11: Referential Integrity on Deletion
*For any* resource with dependent data (patient with rendez-vous, médecin with dossiers), the system SHALL cascade delete dependent records or prevent deletion if constraints exist.

**Validates: Requirements 10.5**

### Property 12: Dashboard Statistics Accuracy
*For any* dashboard view, the system SHALL display statistics that match the actual count of records in the database (total patients, médecins, services, rendez-vous).

**Validates: Requirements 8.1, 8.2, 8.3, 8.4, 8.5**

## Error Handling

### Authentication Errors
- Invalid credentials: Afficher "Email ou mot de passe incorrect"
- Account locked: Afficher "Compte verrouillé, contactez l'administrateur"
- User not found: Afficher "Utilisateur introuvable"

### Authorization Errors
- Access denied: Afficher page 403 Forbidden
- Insufficient permissions: Afficher "Vous n'avez pas les permissions pour accéder à cette ressource"

### Data Validation Errors
- Required field missing: Afficher "Le champ [nom] est obligatoire"
- Invalid format: Afficher "Format invalide pour [nom]"
- Unique constraint violation: Afficher "[nom] est déjà utilisé"

### Business Logic Errors
- Appointment conflict: Afficher "Ce créneau horaire n'est pas disponible"
- Service deletion with médecins: Afficher "Impossible de supprimer ce service car il contient des médecins"
- Patient deletion with rendez-vous: Afficher "Impossible de supprimer ce patient car il a des rendez-vous"

### System Errors
- Database connection error: Afficher "Erreur de connexion à la base de données"
- Unexpected error: Afficher "Une erreur inattendue s'est produite"

## Testing Strategy

### Unit Testing Approach
Unit tests verify specific examples, edge cases, and error conditions:
- Test authentication with valid/invalid credentials
- Test CRUD operations for each entity
- Test validation rules (required fields, unique constraints)
- Test authorization checks for each role
- Test business logic (appointment conflicts, service deletion constraints)

### Property-Based Testing Approach
Property-based tests verify universal properties that should hold across all inputs using **xUnit with Xunit.Combinatorial** or **Bogus** for data generation:
- Generate random users with different roles and verify access control
- Generate random appointments and verify conflict detection
- Generate random patients and verify data isolation
- Generate random services and verify deletion constraints
- Generate random statistics and verify accuracy

### Test Configuration
- Minimum 100 iterations per property-based test
- Use in-memory database for fast test execution
- Mock external dependencies (email, SMS)
- Each property-based test tagged with: `**Feature: hospital-role-based-access, Property {number}: {property_text}**`

### Test Coverage Goals
- Authentication: 100% coverage
- Authorization: 100% coverage
- CRUD operations: 95% coverage
- Business logic: 100% coverage
- Error handling: 90% coverage


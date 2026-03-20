
namespace Entity.Service.Domain.Enum
{
    public enum JobTitleEnum
    {
        // --- Management & Administration (Direction) ---
        GeneralManager = 1,          // Directeur Général
        OperationsManager = 2,       // Responsable des opérations
        AdministrativeAssistant = 3, // Assistant(e) administratif(ve)

        // --- Property Management (Gestion de copropriété) ---
        PropertyManager = 4,         // Gestionnaire de copropriété / Syndic
        JuniorPropertyManager = 5,   // Gestionnaire junior
        PortfolioManager = 6,        // Gestionnaire de portefeuille immobilier
        AssistantPropertyManager = 7,// Assistant(e) de copropriété

        // --- Financial & Accounting (Comptabilité) ---
        PropertyAccountant = 8,      // Comptable copropriété
        AccountsPayableClerk = 9,    // Chargé des comptes fournisseurs
        AccountsReceivableClerk = 10,// Chargé du recouvrement (charges)
        FinancialController = 11,    // Contrôleur financier

        // --- Technical & Maintenance (Technique) ---
        TechnicalManager = 12,       // Responsable technique
        MaintenanceCoordinator = 13, // Coordinateur de maintenance
        BuildingInspector = 14,      // Inspecteur technique d'immeuble
        OnSiteCaretaker = 15,        // Gardien / Concierge (employé par le syndic)

        // --- Legal & Compliance (Juridique) ---
        LegalCounsel = 16,           // Juriste immobilier
        ComplianceOfficer = 17,       // Responsable conformité / règlements

            // --- Administrative & Support (Secrétariat et Accueil) ---
        Receptionist = 18,           // Réceptionniste / Standardiste
        OfficeManager = 19,          // Responsable d'office (coordination interne)
        ExecutiveSecretary = 20,     // Secrétaire de direction
        AdministrativeClerk = 21,    // Commis administratif (saisie de données)
        MailroomCoordinator = 22,    // Gestionnaire de courrier (crucial pour les AG)
        Archivist = 23,              // Archiviste (gestion des dossiers d'immeubles)

        // --- Human Resources (Si le syndic est une grande structure) ---
        HRManager = 24,              // Responsable RH (pour gérer les gardiens d'immeubles)
        PayrollSpecialist = 25,      // Gestionnaire de paie

        // --- Communication & IT ---
        CommunicationOfficer = 26,   // Chargé de communication (portail copropriété)
        ITSupportTechnician = 27,    // Technicien informatique

        // --- Sales & Development (Développement du portefeuille) ---
        BusinessDevelopmentManager = 28, // Chargé de développement (recherche de nouvelles copropriétés)
    }
}

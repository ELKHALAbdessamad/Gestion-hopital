using System;
using System.Collections.Generic;

namespace HospitalManagement.ViewModels
{
    public class DashboardViewModel
    {
        // Statistiques principales
        public int TotalPatients { get; set; }
        public int TotalMedecins { get; set; }
        public int RendezvousAujourdhui { get; set; }
        public int TotalServices { get; set; }

        // Données pour graphiques
        public int[] RendezvousParJour { get; set; } = new int[7];
        public string[] ServicesLabels { get; set; } = Array.Empty<string>();
        public int[] ServicesData { get; set; } = Array.Empty<int>();

        // Activités récentes
        public List<ActivityItem> RecentActivities { get; set; } = new List<ActivityItem>();

        // Statistiques supplémentaires
        public int RendezvousSemaine { get; set; }
        public int RendezvousMois { get; set; }
        public int PatientsNouveau { get; set; }
        public decimal TauxOccupation { get; set; }
    }

    public class ActivityItem
    {
        public string Type { get; set; } = "new"; // new, update, alert
        public string Icon { get; set; } = "fas fa-info-circle";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = "À l'instant";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
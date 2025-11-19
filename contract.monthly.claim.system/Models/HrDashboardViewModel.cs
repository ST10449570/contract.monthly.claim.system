using System.Collections.Generic;

namespace contract.monthly.claim.system.Models
{
    public class HrDashboardViewModel
    {
        // summary counts shown on HR dashboard
        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }

        // optional overall total
        public int TotalClaims { get; set; }

        // optional lists for detail panels (can be null if not used)
        public ICollection<MonthlyClaim>? PendingClaims { get; set; }
        public ICollection<MonthlyClaim>? RecentClaims { get; set; }
    }
}
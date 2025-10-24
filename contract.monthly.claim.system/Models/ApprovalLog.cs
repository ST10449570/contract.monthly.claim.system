using System;

namespace contract.monthly.claim.system.Models
{
    public class ApprovalLog
    {
        public int Id { get; set; }

        public int MonthlyClaimId { get; set; }
        public MonthlyClaim? MonthlyClaim { get; set; }

        public string Action { get; set; } = string.Empty; // "Approved" or "Rejected"
        public string Actor { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

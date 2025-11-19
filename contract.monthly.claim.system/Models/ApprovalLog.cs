// contract.monthly.claim.system.Models.ApprovalLog (ApprovalLog.cs)
namespace contract.monthly.claim.system.Models
{
    public class ApprovalLog
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }

        // e.g. "Approved", "Rejected", "Submitted"
        public string? Action { get; set; }

        // Correct property name: PerformedBy (not PerferfomedBy)
        public string? PerformedBy { get; set; }

        // When action was made
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /* optional FKs if you need:
        public int? MonthlyClaimId { get; set; }
        public MonthlyClaim? Claim { get; set; }
        */
    }
}
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace contract.monthly.claim.system.Models
{
    public enum ClaimStatus { Pending, Approved, Rejected }
   
    public class MonthlyClaim
    {
        public int Id { get; set; }

        [Required]
        public int LecturerId { get; set; }
        public Lecturer? Lecturer { get; set; }

        [Range(0, 10000)]
        public decimal Hours { get; set; }

        [Range(0, 100000)]
        public decimal HourlyRate { get; set; }

        public decimal Total => Hours * HourlyRate;

        public string? Notes { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Document>? Documents { get; set; }
        public ICollection<ApprovalLog>? ApprovalLogs { get; set; }
    }
}

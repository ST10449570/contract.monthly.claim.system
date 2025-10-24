namespace contract.monthly.claim.system.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public MonthlyClaim? Claim { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public int MonthlyClaimId { get; internal set; }
    }
}

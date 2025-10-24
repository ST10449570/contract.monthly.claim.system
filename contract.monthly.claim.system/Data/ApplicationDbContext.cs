using Microsoft.EntityFrameworkCore;
using contract.monthly.claim.system.Models;

namespace contract.monthly.claim.system.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Make sure these use MonthlyClaim (NOT System.Security.Claims.Claim)
        public DbSet<Lecturer> Lecturers { get; set; } = null!;
        public DbSet<MonthlyClaim> MonthlyClaims { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<ApprovalLog> ApprovalLogs { get; set; } = null!;
    }
}

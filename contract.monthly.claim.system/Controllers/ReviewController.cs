using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using contract.monthly.claim.system.Data;
using contract.monthly.claim.system.Models;
using System.Linq;
using System.Threading.Tasks;

namespace contract.monthly.claim.system.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReviewController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /Review/HrDashboard
        public async Task<IActionResult> HrDashboard()
        {
            var pendingCount = await _db.MonthlyClaims.CountAsync(c => c.Status == ClaimStatus.Pending);
            var approvedCount = await _db.MonthlyClaims.CountAsync(c => c.Status == ClaimStatus.Approved);
            var rejectedCount = await _db.MonthlyClaims.CountAsync(c => c.Status == ClaimStatus.Rejected);
            var total = await _db.MonthlyClaims.CountAsync();

            // optional: get the most recent pending claims to show on the dashboard
            var pendingClaims = await _db.MonthlyClaims
                .Where(c => c.Status == ClaimStatus.Pending)
                .OrderByDescending(c => c.SubmittedAt)
                .Take(10)
                .ToListAsync();

            var vm = new HrDashboardViewModel
            {
                PendingCount = pendingCount,
                ApprovedCount = approvedCount,
                RejectedCount = rejectedCount,
                TotalClaims = total,
                PendingClaims = pendingClaims
            };

            return View(vm);
        }

        // Example Pending action (async + ordering)
        public async Task<IActionResult> Pending()
        {
            var claims = await _db.MonthlyClaims
                .Where(c => c.Status == ClaimStatus.Pending)
                .OrderByDescending(c => c.SubmittedAt)   // ensures newest first
                .ToListAsync();

            return View(claims);
        }

        // ... other existing actions (Approve, Reject, Details) ...
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using contract.monthly.claim.system.Data;
using contract.monthly.claim.system.Models;
using Microsoft.Extensions.Logging;

namespace contract.monthly.claim.system.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(ApplicationDbContext db, ILogger<ReviewController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET: Review/Index
        public async Task<IActionResult> Index()
        {
            var pending = await _db.MonthlyClaims
                .Where(c => c.Status == ClaimStatus.Pending)
                .Include(c => c.Lecturer)
                .Include(c => c.Documents)
                .OrderByDescending(c => c.SubmittedAt)
                .ToListAsync();

            return View(pending);
        }

        // POST: Review/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var claim = await _db.MonthlyClaims.FindAsync(id);
            if (claim == null) return NotFound();

            if (claim.Status != ClaimStatus.Pending)
            {
                TempData["Error"] = "Only pending claims can be approved.";
                return RedirectToAction(nameof(Index));
            }

            claim.Status = ClaimStatus.Approved;
            _db.ApprovalLogs.Add(new ApprovalLog
            {
                MonthlyClaimId = claim.Id,
                Action = "Approved",
                Actor = "Coordinator (temp)", // replace with user identity in future
                Timestamp = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            TempData["Success"] = $"Claim {claim.Id} approved.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Review/Reject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string? reason)
        {
            var claim = await _db.MonthlyClaims.FindAsync(id);
            if (claim == null) return NotFound();

            if (claim.Status != ClaimStatus.Pending)
            {
                TempData["Error"] = "Only pending claims can be rejected.";
                return RedirectToAction(nameof(Index));
            }

            claim.Status = ClaimStatus.Rejected;
            _db.ApprovalLogs.Add(new ApprovalLog
            {
                MonthlyClaimId = claim.Id,
                Action = "Rejected" + (string.IsNullOrWhiteSpace(reason) ? "" : $": {reason}"),
                Actor = "Coordinator (temp)",
                Timestamp = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            TempData["Success"] = $"Claim {claim.Id} rejected.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Review/Details/5 (optional)
        public async Task<IActionResult> Details(int id)
        {
            var claim = await _db.MonthlyClaims
                .Include(c => c.Lecturer)
                .Include(c => c.Documents)
                .Include(c => c.ApprovalLogs)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (claim == null) return NotFound();
            return View(claim);
        }
    }
}

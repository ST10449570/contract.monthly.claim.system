using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using contract.monthly.claim.system.Data;
using contract.monthly.claim.system.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace contract.monthly.claim.system.Controllers
{
    public class MonthlyClaimsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<MonthlyClaimsController> _logger;

        // Allowed file extensions & max size (5 MB)
        private readonly string[] _allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
        private const long MaxFileBytes = 5_000_000;

        public MonthlyClaimsController(ApplicationDbContext db, IWebHostEnvironment env, ILogger<MonthlyClaimsController> logger)
        {
            _db = db;
            _env = env;
            _logger = logger;
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hours,HourlyRate,Notes")] MonthlyClaim inputClaim, IFormFile? upload)
        {
            try
            {
                // Basic server-side validation
                if (inputClaim.Hours < 0) ModelState.AddModelError(nameof(inputClaim.Hours), "Hours must be 0 or more.");
                if (inputClaim.HourlyRate < 0) ModelState.AddModelError(nameof(inputClaim.HourlyRate), "Hourly rate must be 0 or more.");

                // Validate upload if present
                if (upload != null)
                {
                    var ext = Path.GetExtension(upload.FileName).ToLowerInvariant();
                    if (!_allowedExtensions.Contains(ext))
                        ModelState.AddModelError("File", "Invalid file type. Allowed: .pdf, .docx, .xlsx");
                    if (upload.Length > MaxFileBytes)
                        ModelState.AddModelError("File", "File too large. Max 5 MB.");
                }

                if (!ModelState.IsValid)
                    return View(inputClaim);

                // Recalculate and create a new claim entity (avoid shadowing the parameter)
                var newClaim = new MonthlyClaim
                {
                    LecturerId = 1, // TEMP: replace with current user id when auth is added
                    Hours = inputClaim.Hours,
                    HourlyRate = inputClaim.HourlyRate,
                    Notes = inputClaim.Notes,
                    Status = ClaimStatus.Pending,
                    SubmittedAt = DateTime.UtcNow
                };

                _db.MonthlyClaims.Add(newClaim);
                await _db.SaveChangesAsync();

                // Handle file upload (if present)
                if (upload != null)
                {
                    var ext = Path.GetExtension(upload.FileName).ToLowerInvariant();
                    var storedName = $"{Guid.NewGuid()}{ext}";
                    var uploadsRoot = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsRoot)) Directory.CreateDirectory(uploadsRoot);
                    var filePath = Path.Combine(uploadsRoot, storedName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await upload.CopyToAsync(fs);
                    }

                    var docRecord = new Document
                    {
                        MonthlyClaimId = newClaim.Id,
                        OriginalFileName = Path.GetFileName(upload.FileName),
                        StoredFileName = storedName,
                        Size = upload.Length,
                        UploadedAt = DateTime.UtcNow
                    };

                    _db.Documents.Add(docRecord);
                    await _db.SaveChangesAsync();
                }

                TempData["Success"] = "Claim submitted successfully.";
                return RedirectToAction(nameof(Details), new { id = newClaim.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating claim");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                return View(inputClaim);
            }
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var claim = await _db.MonthlyClaims
                .Include(c => c.Documents)
                .Include(c => c.ApprovalLogs)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (claim == null) return NotFound();
            return View(claim);
        }

        // GET: Claims/MyClaims
        public async Task<IActionResult> MyClaims()
        {
            // TEMP: use lecturer id 1 for testing until auth is implemented
            int lecturerId = 1;

            var list = await _db.MonthlyClaims
                .Where(c => c.LecturerId == lecturerId)
                .OrderByDescending(c => c.SubmittedAt)
                .Include(c => c.Documents)
                .ToListAsync();

            return View(list);
        }
    }
}

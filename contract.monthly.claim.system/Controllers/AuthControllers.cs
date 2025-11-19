using Microsoft.AspNetCore.Mvc;

namespace contract.monthly.claim.system.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
           
            if (username == "lecturer" && password == "123")
            {
                TempData["Role"] = "Lecturer";
                return RedirectToAction("Create", "MonthlyClaims");
            }
            if (username == "coordinator" && password == "123")
            {
                TempData["Role"] = "Coordinator";
                return RedirectToAction("Pending", "Review");
            }
            if (username == "manager" && password == "123")
            {
                TempData["Role"] = "Manager";
                return RedirectToAction("Pending", "Review");
            }

            ViewBag.Error = "Invalid login";
            return View();
        }
    }
}
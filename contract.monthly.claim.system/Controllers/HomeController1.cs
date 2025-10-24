using Microsoft.AspNetCore.Mvc;

namespace contract.monthly.claim.system.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

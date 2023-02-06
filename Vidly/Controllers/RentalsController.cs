using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        public IActionResult New()
        {
            return View();
        }
    }
}

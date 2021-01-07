using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShipsInSpace.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace ShipsInSpace.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Check if user is pirate
            if (User.HasClaim(c => c.Type == "License")) 
                return RedirectToAction("StepOne", "CreateShip");

            // Redirect to manager page
            return RedirectToAction("Plate", "Registration");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

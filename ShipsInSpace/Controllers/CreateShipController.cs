using Microsoft.AspNetCore.Mvc;

namespace ShipsInSpace.Controllers
{
    public class CreateShipController : Controller
    {
        public IActionResult StepOne()
        {
            return View("CreateShip");
        }

        public IActionResult StepTwo()
        {
            return View("CreateShip");
        }

        public IActionResult Confirmation()
        {
            return View("CreateShip");
        }
    }
}

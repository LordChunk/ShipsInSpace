using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

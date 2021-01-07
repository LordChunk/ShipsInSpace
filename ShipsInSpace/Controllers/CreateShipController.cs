using GalacticSpaceTransitAuthority;
using Microsoft.AspNetCore.Mvc;
using ShipsInSpace.Models;

namespace ShipsInSpace.Controllers
{
    public class CreateShipController : Controller
    {
        private ISpaceTransitAuthority spaceTransitAuthority;

        public CreateShipController(ISpaceTransitAuthority mySpaceTransitAuthority)
        {
            spaceTransitAuthority = mySpaceTransitAuthority;
        }

        public IActionResult Step1(SelectionOfShipModel model = null)
        {
            if (model == null)
            {
                model = new SelectionOfShipModel(spaceTransitAuthority);
            }

            return View("CreateShip", model);
        }

        [HttpPost]
        public IActionResult Step1Confirm(SelectionOfShipModel model)
        {
            if (!ValidateChoicesStep1(model))
            {
                RedirectToAction("Step1", model);
            }
            return RedirectToAction("Step3", model);
        }
        private bool ValidateChoicesStep1(SelectionOfShipModel model)
        {
            return true;
        }

        public IActionResult Step3(SelectionOfShipModel model)
        {
            return View("CreateShip");
        }

        [HttpPost]
        public IActionResult Step2Confirm(SelectionOfShipModel model)
        {
            if (!ValidateChoicesStep2(model))
            {
                RedirectToAction("Step3", model);
            }
            return RedirectToAction("Step3", model);
        }
        private bool ValidateChoicesStep2(SelectionOfShipModel model)
        {
            return true;
        }

        public IActionResult Step3()
        {
            return View("CreateShip");
        }
    }
}

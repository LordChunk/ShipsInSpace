using System.Collections.Generic;
using GalacticSpaceTransitAuthority;
using Microsoft.AspNetCore.Mvc;
using ShipsInSpace.Models;

namespace ShipsInSpace.Controllers
{
    [Route("{controller}")]
    public class CreateShipController : Controller
    {
        private ISpaceTransitAuthority spaceTransitAuthority;

        public CreateShipController(ISpaceTransitAuthority mySpaceTransitAuthority)
        {
            spaceTransitAuthority = mySpaceTransitAuthority;
        }

        [Route("1")]
        public IActionResult Step1(SelectionOfShipViewModel model)
        {
            return View("CreateShip", model);
        }

        [HttpPost]
        [Route("1")]
        public IActionResult Step1Confirm(SelectionOfShipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Step1", model);
            }

            model.SelectedShip.Wings = new List<WingViewModel>();

            for (int i = 0; i < model.NumberOfWings; i++)
            {
                model.SelectedShip.Wings.Add(new WingViewModel());
            }

            spaceTransitAuthority.CheckActualHullCapacity(model.SelectedShip.Hull);

            return RedirectToAction("Step2", model);
        }
        private bool ValidateChoicesStep1(SelectionOfShipViewModel model)
        {
            return true;
        }

        [Route("2")]
        public IActionResult Step2(SelectionOfShipViewModel model)
        {
            return View("CreateShip", model);
        }

        [HttpPost]
        [Route("2")]
        public IActionResult Step2Confirm(SelectionOfShipViewModel model)
        {
            if (!ValidateChoicesStep2(model))
            {
                RedirectToAction("Step2", model);
            }

            return RedirectToAction("Step3", model);
        }
        private bool ValidateChoicesStep2(SelectionOfShipViewModel model)
        {
            return true;
        }


        [Route("3")]
        public IActionResult Step3()
        {
            return View("CreateShip");
        }
    }
}

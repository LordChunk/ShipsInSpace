using System.Collections.Generic;
using GalacticSpaceTransitAuthority;
using Microsoft.AspNetCore.Mvc;
using ShipsInSpace.Models;

namespace ShipsInSpace.Controllers
{
    public class CreateShipController : Controller
    {
        private readonly ISpaceTransitAuthority _spaceTransitAuthority;

        public CreateShipController(ISpaceTransitAuthority mySpaceTransitAuthority)
        {
            _spaceTransitAuthority = mySpaceTransitAuthority;
        }

        public IActionResult Index() => RedirectToAction("HullAndEngine");

        public IActionResult HullAndEngine(SelectionOfShipModel model)
        {
            return View("CreateShip", model);
        }

        [HttpPost]
        public IActionResult HullAndEngineConfirm(SelectionOfShipModel model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("HullAndEngine", model);
            }

            model.SelectedShip.Wings = new List<Wing>();

            for (int i = 0; i < model.NumberOfWings; i++)
            {
                model.SelectedShip.Wings.Add(new Wing());
            }

            _spaceTransitAuthority.CheckActualHullCapacity(GetHullFromViewModel(model.SelectedShip.Hull));
            return RedirectToAction("Weapons", model);
        }

        private Hull GetHullFromViewModel(HullViewModel hullView)
        {
            foreach (var hull in _spaceTransitAuthority.GetHulls())
            {
                if (hullView.Id == hull.Id)
                {
                    return hull;
                }
            }

            return null;
        }

        private bool ValidateChoicesStep1(SelectionOfShipViewModel model)
        {
            return View("")
        }



        public IActionResult Weapons(SelectionOfShipModel model)
        {
            return View("CreateShip", model);
        }

        [HttpPost]
        public IActionResult WeaponsConfirm(SelectionOfShipModel model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Weapons", model);
            }

            return RedirectToAction("Step3", model);
        }
        private bool ValidateChoicesStep2(SelectionOfShipModel model)
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

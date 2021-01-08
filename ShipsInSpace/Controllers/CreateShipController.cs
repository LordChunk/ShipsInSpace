using System.Collections.Generic;
using GalacticSpaceTransitAuthority;
using Microsoft.AspNetCore.Mvc;
using ShipsInSpace.Models;

namespace ShipsInSpace.Controllers
{
    public class CreateShipController : Controller
    {
        private ISpaceTransitAuthority _spaceTransitAuthority;

        public CreateShipController(ISpaceTransitAuthority mySpaceTransitAuthority)
        {
            _spaceTransitAuthority = mySpaceTransitAuthority;
        }

        public IActionResult Index() => RedirectToAction("HullAndEngine");

        public IActionResult HullAndEngine(HullAndEngineModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult HullAndEngineConfirm(HullAndEngineModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("HullAndEngine", model);

            model.Ship.Wings = new List<WingViewModel>();

            for (var i = 0; i < model.NumberOfWings; i++)
            {
                model.Ship.Wings.Add(new WingViewModel());
            }

            return RedirectToAction("Wings", model.Ship);
        }

        public IActionResult Wings(ShipViewModel model)
        {
            return View(model);
        }

        public IActionResult WingsConfirm(ShipViewModel model)
        {
            return RedirectToAction(!ModelState.IsValid ? "Wings" : "Weapons", model);
        }



        public IActionResult Weapons(ShipViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult WeaponsConfirm(ShipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Weapons", model);
            }

            return RedirectToAction("Step3", model);
        }



        //[Route("3")]
        //public IActionResult Step3()
        //{
        //    return View("CreateShip");
        //}

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
    }
}

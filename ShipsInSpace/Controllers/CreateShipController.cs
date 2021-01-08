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

        public IActionResult HullAndEngine(SelectionOfShipViewModel model)
        {
            return View("CreateShip", model);
        }

        [HttpPost]
        public IActionResult HullAndEngineConfirm(SelectionOfShipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("HullAndEngine", model);
            }

            model.SelectedShip.Wings = new List<WingViewModel>();

            for (int i = 0; i < model.NumberOfWings; i++)
            {
                model.SelectedShip.Wings.Add(new WingViewModel());
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
        
        public IActionResult Weapons(SelectionOfShipViewModel model)
        {
            return View("CreateShip", model);
        }

        [HttpPost]
        public IActionResult WeaponsConfirm(SelectionOfShipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Weapons", model);
            }

            return RedirectToAction("Step3", model);
        }

        [Route("3")]
        public IActionResult Step3()
        {
            return View("CreateShip");
        }
    }
}

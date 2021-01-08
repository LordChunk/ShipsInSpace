using System.Collections.Generic;
using System.Linq;
using GalacticSpaceTransitAuthority;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShipsInSpace.Models;
using ShipsInSpace.Models.ViewModels;

namespace ShipsInSpace.Controllers
{
    public class CreateShipController : Controller
    {
<<<<<<< HEAD
        private ISpaceTransitAuthority _spaceTransitAuthority;
        private readonly UserManager<User> _userManager;
=======
        private readonly ISpaceTransitAuthority _spaceTransitAuthority;
>>>>>>> dc405a8bd553747d7001c95cb4af763124a76431

        public CreateShipController(ISpaceTransitAuthority mySpaceTransitAuthority, UserManager<User> userManager)
        {
            _spaceTransitAuthority = mySpaceTransitAuthority;
            _userManager = userManager;
        }

        public IActionResult Index() { 
            // TODO: Fetch license plate

            return View("HullAndEngine", new HullAndEngineModel() { Ship = new ShipViewModel()});
        }

        [HttpPost]
        public IActionResult Wings(HullAndEngineModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            model.Ship.Wings = new List<WingViewModel>();

            for (var i = 0; i < model.NumberOfWings; i++)
            {
                model.Ship.Wings.Add(new WingViewModel());
            }

            return View(model.Ship);
        }

        [HttpPost]
        public IActionResult Weapons(ShipViewModel model)
        {
            if (!ModelState.IsValid) return View("Wings", model);

            // Instantiate viewmodel hardpoints based on size of wing
            //foreach (var wing in model.Wings)
            //{
            //    wing.Hardpoint = new List<WeaponViewModel>();

            //    // Fetch hardpoints from service
            //    var numberOfHardpoints = _spaceTransitAuthority.GetWings().First(w => w.Id == wing.Id).NumberOfHardpoints;
            //    for (var i = 0; i < numberOfHardpoints; i++)
            //    {
            //        wing.Hardpoint.Add(new WeaponViewModel());
            //    }
            //}

            return View(model);
        }


        [HttpPost]
        public IActionResult Confirm(ShipViewModel model)
        {
            if (!ModelState.IsValid) return View("Weapons", model);
            
            var finalShip = FillModelFromIds(model);
            
            return View(finalShip);
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

        private Ship FillModelFromIds(ShipViewModel model)
        {
            var ship = new Ship()
            {
                Engine = _spaceTransitAuthority.GetEngines().FirstOrDefault(e => e.Id == model.Engine.Id),
                Hull =  _spaceTransitAuthority.GetHulls().FirstOrDefault(h => h.Id == model.Hull.Id),
                Name = "Test",
                // Select wings from spaceship authority that overlap with the selected ids
                Wings = _spaceTransitAuthority.GetWings()
                    .Where(wing => model.Wings.Select(wing1 => wing1.Id)
                    .Contains(wing.Id))
                    .ToList(),
            };

            // Add weapons to wings
            var i = 0;
            foreach (var wing in model.Wings)
            {
                ship.Wings[i].Hardpoint = _spaceTransitAuthority.GetWeapons().Where(w => wing.HardpointIds.Contains(w.Id)).ToList();
                i++;
            }

            return ship;
        }
    }
}

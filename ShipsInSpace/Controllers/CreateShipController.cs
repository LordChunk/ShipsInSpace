using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GalacticSpaceTransitAuthority;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipsInSpace.Models;
using ShipsInSpace.Models.ViewModels;

namespace ShipsInSpace.Controllers
{
    [Authorize(Policy = "License")]
    public class CreateShipController : Controller
    {
        private readonly ISpaceTransitAuthority _spaceTransitAuthority;
        private readonly Mapper _mapper;

        public CreateShipController(ISpaceTransitAuthority spaceTransitAuthority, Mapper mapper)
        {
            _spaceTransitAuthority = spaceTransitAuthority;
            _mapper = mapper;
        }

        public IActionResult Index() { 
            // TODO: Fetch license plate

            return View("HullAndEngine", new HullAndEngineModel() { Ship = new ShipViewModel{ Id = 1 }});
        }

        [HttpPost]
        public IActionResult ConfirmHullAndEngine(HullAndEngineModel model)
        {
            model.Ship = _mapper.Map<ShipViewModel>(FillModelFromIds(model.Ship));

            var hull = GetHullFromViewModel(model.Ship.Hull);
            // Calculate ship take off mass allowance
            model.Ship.Hull.ActualTakeOffMass = _spaceTransitAuthority.CheckActualHullCapacity(hull);

            ModelState.Clear();

            if (!TryValidateModel(model.Ship)) return View("HullAndEngine", model);

            // Fill wings list according to selected number of wings
            model.Ship.Wings = new List<WingViewModel>();
            for (var i = 0; i < model.NumberOfWings; i++)
                model.Ship.Wings.Add(new WingViewModel());

            return View("Wings", model.Ship);
        }

        [HttpPost]
        public IActionResult ConfirmWings(ShipViewModel model)
        {
            if (!ModelState.IsValid) return View("Wings", model);

            return View("Weapons", model);
        }


        [HttpPost]
        public IActionResult OverviewShip(ShipViewModel model)
        {
            if (!ModelState.IsValid) return View("Weapons", model);
            
            var finalShip = FillModelFromIds(model);
            
            return View(finalShip);
        }

        [HttpPost]
        public IActionResult SubmitShip(ShipViewModel model)
        {
            var finalShip = FillModelFromIds(model);

            return Json(finalShip);
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
            };

            if (model.Wings != null)
            {
                ship.Wings = _spaceTransitAuthority.GetWings()
                .Where(wing => model.Wings.Select(wing1 => wing1.Id)
                    .Contains(wing.Id))
                    .ToList();

                // Add weapons to wings
                var i = 0;
                foreach (var wing in model.Wings)
                {
                    ship.Wings[i].Hardpoint = _spaceTransitAuthority.GetWeapons().Where(w => wing.HardpointIds.Contains(w.Id)).ToList();
                    i++;
                }
            }

            return ship;
        }
    }
}

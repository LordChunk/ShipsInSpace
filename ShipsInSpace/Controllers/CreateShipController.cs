using Microsoft.AspNetCore.Mvc;
using ShipsInSpace.Models;

namespace ShipsInSpace.Controllers
{
    [Route("{controller}")]
    public class CreateShipController : Controller
    {
        [Route("1")]
        public IActionResult Step1(SelectionOfShipModel model = null)
        {
            if (model == null)
            {
                model = new SelectionOfShipModel();
            }
            return View("CreateShip", model);
        }

        [HttpPost]
        [Route("1")]
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

        [Route("2")]
        public IActionResult Step3(SelectionOfShipModel model)
        {
            return View("CreateShip");
        }

        [HttpPost]
        [Route("2")]
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


        [Route("3")]
        public IActionResult Step3()
        {
            return View("CreateShip");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ShipsInSpace.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace ShipsInSpace.Controllers
{
    [Authorize(Policy = "Manager")]
    public class RegistrationController : Controller
    {
        public IActionResult RegisterPlate()
        {
            return View("CreateRegistration");
        }

        [HttpPost]
        public IActionResult RegisterPlate(RegistrationViewModel model)
        {
            if (!ModelState.IsValid) return View("CreateRegistration");

            model.SecretCode = GenerateSecretCode(model.Plate);

            return View("RegistrationComplete",model);
        }

        private string GenerateSecretCode(string plate)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in GetHash(plate))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        private static IEnumerable<byte> GetHash(string inputString)
        {
            using HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}

using System.ComponentModel.DataAnnotations;
using ShipsInSpace.Models.Enums;

namespace ShipsInSpace.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Plate")]
        [DataType(DataType.Text)]
        public string Plate { get; set; }

        [Required]
        [Display(Name = "Pilot's license")]
        public PilotLicense PilotLicense { get; set; }

        public string SecretCode { get; set; }
    }
}

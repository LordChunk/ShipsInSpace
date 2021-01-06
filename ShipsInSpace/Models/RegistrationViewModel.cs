using System.ComponentModel.DataAnnotations;

namespace ShipsInSpace.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Plate")]
        [DataType(DataType.Text)]
        public string Plate { get; set; }

        [Required]
        [Display(Name = "pilot's license")]
        [MaxLength(1)]
        [MinLength(1)]
        public char PilotLicense { get; set; }

        public string SecretCode { get; set; }
    }
}

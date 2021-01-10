using System.ComponentModel.DataAnnotations;

namespace ShipsInSpace.Models.ViewModels
{
    public class HullAndEngineModel
    {
        public ShipViewModel Ship { get; set; }

        [Display(Name = "Number of wings")]
        [Range(1, 10)]
        public int NumberOfWings { get; set; }
    }
}
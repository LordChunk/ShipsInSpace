using System.ComponentModel.DataAnnotations;

namespace ShipsInSpace.Models.ViewModels
{
    public class HullAndEngineModel
    {
        public ShipViewModel Ship { get; set; }
        [Display(Name = "Number of wings")]
        public int NumberOfWings { get; set; }
    }
}
using System.ComponentModel;

namespace Course2.Models.DTOs
{
    public class UpLoadVideo 
    {

        public int PositionSelectedId { get; set; }

        public Video? Video { get; set; }

        [DisplayName(displayName: "Munkakör")]
        public IEnumerable<Position>? Positions { get; set; }

    }
}

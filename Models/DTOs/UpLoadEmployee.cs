using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Course2.Models.DTOs
{
    public class UpLoadEmployee
    {
        public int PositionSelectedId { get; set; }

        public Employee Employee { get; set; }

        [DisplayName(displayName: "Munkakör")]
        public IEnumerable<Position> Positions { get; set; }


    }
}

using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Course2.Models
{
    public class Position : BaseObj
    {
        [Required(ErrorMessage = "Üres mező")]
        [DisplayName(displayName: "Munkakör")]
        public string? PositionName { get; set; }
    }
}

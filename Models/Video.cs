using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Course2.Models
{
    public class Video : BaseObj
    {
        [Required(ErrorMessage = "Üres mező")]
        [DisplayName(displayName: "Videó neve")]
        public string VideoName { get; set; }

        [Required(ErrorMessage = "Üres mező")]
        [DisplayName(displayName: "Videó URL címe")]
        public string VideoURL { get; set; }

        [Required(ErrorMessage = "Üres mező")]
        [DisplayName(displayName: "Pozíció")]
        public int? PositionId { get; set; }

    }
}

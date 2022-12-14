using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Course2.Models
{
    public class Employee : BaseObj
    {
        [Required(ErrorMessage = "Üres mező")]
        [DisplayName(displayName: "Név")]
        public string? EmployeeName { get; set; }

        [EmailAddress(ErrorMessage = "Nem megfelelő e-mail (pl.: email@email.hu)")]
        [Required(ErrorMessage = "Üres mező")]
        [DisplayName(displayName: "E-mail")]
        public string? EmployeeEmail { get; set; }

        public int? PositionId { get; set; }
        //  public virtual List<Video> Videos { get; set; }

        public void checkVideo()
        {
            string clickVideo = "click videó";
        }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Course2.Models
{
    public class LogWatchVideo : BaseObj
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int VideoId { get; set; }
        [Required]
        public DateTime dateTime { get; set; }
    }
}

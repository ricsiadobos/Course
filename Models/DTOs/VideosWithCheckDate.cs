using System.ComponentModel;

namespace Course2.Models.DTOs
{
    public class VideosWithWatchDate 
    {
        
        public int VideoId { get; set; }
        public string? VideoName { get; set; }  
        public string? VideoURL { get; set; }
        public int? PositionId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? WatchDate { get; set; }

    }
}

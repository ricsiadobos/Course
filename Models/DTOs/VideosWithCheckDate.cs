using System.ComponentModel;

namespace Course2.Models.DTOs
{
    public class VideosWithWatchDate 
    {
        
        public int VideoId { get; set; }
        public string? VideoName { get; set; }  
        public string? VideoURL { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? WatchDateFirst { get; set; }

        public DateTime? WatchDateLast { get; set; }

    }
}

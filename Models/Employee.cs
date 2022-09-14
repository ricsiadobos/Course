namespace Course2.Models
{
    public class Employee : BaseObj
    {
        public string EmloyeeName { get; set; }
        public string EmloyeeEmail { get; set; }

        public Position EmloyeePosition { get; set; }
    }
}

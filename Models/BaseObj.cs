using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace Course2.Models
{
    public abstract class BaseObj
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
    }
}

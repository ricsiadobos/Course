using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace Course2.Models.DTOs
{
    public class UpLoadVideo 
    {

        public Video Video { get; set; }

        [DisplayName(displayName: "Pozíció")]
        public IEnumerable<SelectListItem> Positions { get; set; }

    }
}

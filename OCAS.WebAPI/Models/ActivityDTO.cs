using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace OCAS.WebAPI.Models
{
    public class ActivityDTO
    {
        public int ActivityId { get; set; }

        [Required]
        public string ActivityName { get; set; }

    }

}

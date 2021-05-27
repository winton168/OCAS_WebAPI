
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OCAS.WebAPI.Models
{
    public class UserDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "First Name is too long")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Last Name is too long")]
        public string LastName { get; set; }


        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Email Address is too long")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string EmailAddress { get; set; }


        [StringLength(maximumLength: 1000, ErrorMessage = "Comments are too long")]
        public string Comments { get; set; }

        public int ActivityId { get; set; }

        public string ActivityName { get; set; }


    }


}

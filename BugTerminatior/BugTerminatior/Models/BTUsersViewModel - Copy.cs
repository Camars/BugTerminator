using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BugTerminatior.Models
{
    public class BTUsersViewModel
    {
        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string AspNetUserId { get; set; }
    }
}
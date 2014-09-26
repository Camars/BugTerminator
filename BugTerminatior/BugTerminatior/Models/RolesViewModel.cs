using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BugTerminatior.Models
{
    public class RolesViewModel
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        public string RolesId { get; set; }
    }
    public class UserRolesViewModel
    {
        public string RolesId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Users")]

        public System.Web.Mvc.MultiSelectList Users { get; set; }

        public string[] SelectedUsers { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BugTerminatior.Models
{
    public class ProjectUsersViewModel
    {
        public int ProjectId { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Projects")]
        public System.Web.Mvc.MultiSelectList Users { get; set; }

        public string[] SelectedUsers { get; set; }
    }
}
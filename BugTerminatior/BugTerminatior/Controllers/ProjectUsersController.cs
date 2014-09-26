using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BugTerminatior.Models;

namespace BugTerminatior.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProjectUsersController : Controller
    {

        private BugTrackerEntities btdb = new BugTrackerEntities();

        // GET: Assignusers
        public ActionResult AssignUsers(int id)
        {

            var project = btdb.Projects.Find(id);
            var model = new ProjectUsersViewModel { ProjectId = project.Id, ProjectName = project.Name };
            List<BTUser> userlist = new List<BTUser>();
            foreach (var user in btdb.BTUsers.ToList())
            {
                if (!user.IsOnProject(id))
                {
                    userlist.Add(user);
                }
            }

            model.Users = new MultiSelectList(userlist.OrderBy(u => u.DisplayName), "AspNetUserId", "DisplayName", null);

            return View(model);

        }

        // POST: Assignusers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignUsers(ProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    foreach (string username in model.SelectedUsers)
                    {
                        //locate the user in the database (aspnetusers
                        var user = btdb.BTUsers.Find(username);
                        // add the user to the role
                        user.AddUserToProject(model.ProjectId);
                        // if the user is in the "unassigned" role, remove from that role
                        
                    }
                }
                return RedirectToAction("Index", "Projects");
                //redirect to the roles list
            }
            return View(model);
            //if we got here, there's a problem - return the view with the model
        }

        // GET: UsersInRole
        public ActionResult IsOnProject(int id)
        {

            var project = btdb.Projects.Find(id);
            var model = new ProjectUsersViewModel { ProjectId = project.Id, ProjectName = project.Name };
            List<BTUser> userlist = new List<BTUser>();
            foreach (var user in btdb.BTUsers)
            {
                if (user.IsOnProject(id))
                {
                    userlist.Add(user);
                }
            }
            model.Users = new MultiSelectList(userlist.OrderBy(u => u.DisplayName), "AspNetUserId", "DisplayName", null);

            return View(model);

        }


        // POST: UsersInRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersInRole(ProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        var user = btdb.BTUsers.Find(id);
                        user.RemoveUsersFromProject(model.ProjectId);
                    }
                }
                return RedirectToAction("Index", "Projects");
                //redirect to the roles list
            }
            return View(model);
        }

    }
}

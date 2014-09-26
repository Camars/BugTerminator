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
    public class UserRolesController : Controller
    {
        private ApplicationDbContext aspdb = new ApplicationDbContext();
        private BugTrackerEntities btdb = new BugTrackerEntities();

        // GET: Assignusers
        public ActionResult AssignUsers(string id)
        {
            
            var role = aspdb.Roles.Find(id);
            var model = new UserRolesViewModel { RolesId = role.Id, RoleName = role.Name };
            List<BTUser> userlist = new List<BTUser>();
            foreach (var user in aspdb.Users)
            {
                if (!user.IsInRole(model.RoleName))
                {
                    userlist.Add(btdb.BTUsers.FirstOrDefault(u => u.AspNetUserId == user.Id));
                }
            }

            model.Users = new MultiSelectList(userlist.OrderBy(u => u.DisplayName), "AspNetUserId", "DisplayName", null);

            return View(model);

        }

        // POST: Assignusers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignUsers(UserRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        //locate the user in the database (aspnetusers
                        var user = aspdb.Users.Find(id);
                        // add the user to the role
                        user.AddUserToRole(model.RoleName);
                        // if the user is in the "unassigned" role, remove from that role
                        if (user.IsInRole("Unassigned"))
                        {
                            user.RemoveUserFromRole("Unassigned");
                        }
                        
                    }
                }
                return RedirectToAction("Index", "Roles");
                //redirect to the roles list
            }
            return View(model);
            //if we got here, there's a problem - return the view with the model
        }

        // GET: UsersInRole
        public ActionResult UsersInRole(string id)
        {

            var role = aspdb.Roles.Find(id);
            var model = new UserRolesViewModel { RolesId = role.Id, RoleName = role.Name };
            List<BTUser> userlist = new List<BTUser>();
            foreach (var user in aspdb.Users)
            {
                if (user.IsInRole(model.RoleName))
                {
                    userlist.Add(btdb.BTUsers.FirstOrDefault(u => u.AspNetUserId == user.Id));
                }
            }
            model.Users = new MultiSelectList(userlist.OrderBy(u => u.DisplayName), "AspNetUserId", "DisplayName", null);

            return View(model);

        }


        // POST: UsersInRole
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult UsersInRole(UserRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        var user = aspdb.Users.Find(id);
                        user.RemoveUserFromRole(model.RoleName);
                        if (user.Roles.Count == 0)
                        {
                            user.AddUserToRole("Unassigned");
                        }

                    }
                }
                return RedirectToAction("Index", "Roles");
                //redirect to the roles list
            }
            return View(model);
        }


    }
}
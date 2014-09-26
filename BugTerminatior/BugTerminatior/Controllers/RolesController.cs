using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using BugTerminatior.Models;

namespace BugTerminatior.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var roles = db.Roles.ToList();
            var model = new List<RolesViewModel>();
            foreach (var item in roles)
            {
                model.Add(new RolesViewModel { RolesId = item.Id, RoleName = item.Name });
            }
            return View(model);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var result = db.Roles.Add(new IdentityRole(model.RoleName));
                if (result != null)
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            // if we get this far, something went wrong... return to create view.
            return View(model);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            // 1) get to the DB
            ApplicationDbContext db = new ApplicationDbContext();
            // 2) locate the role and get it
            var role = db.Roles.Find(id);
            // 3) build the view model
            var model = new RolesViewModel { RolesId = id, RoleName = role.Name };
            // 4) sent the model to the view
            return View(model);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RolesViewModel model)
        {
            // 1) get to the DB
            ApplicationDbContext db = new ApplicationDbContext();

            // 2) locate the role and get it
            var role = db.Roles.Find(model.RolesId);

            // 3) change the role name to match what's in the model
            role.Name = model.RoleName;

            // 4) tell the db the role entry has been modified
            db.Entry(role).State = EntityState.Modified;
            // 5) save the changes
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            // 1) get to the DB
            ApplicationDbContext db = new ApplicationDbContext();
            // 2) locate the role and get it
            var role = db.Roles.Find(id);
            // 3) build the view model
            var model = new RolesViewModel { RolesId = id, RoleName = role.Name };
            // 4) sent the model to the view
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Roles/Delete/5
        public ActionResult Delete(RolesViewModel model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var role = db.Roles.Find(model.RolesId);
            // remove the role from the db
            db.Roles.Remove(role);
            // save
            db.SaveChanges();
            //redirect back to the roles list view
            return RedirectToAction("Index");
        }
    }
}

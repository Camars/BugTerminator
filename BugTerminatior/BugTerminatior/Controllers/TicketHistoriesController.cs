using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTerminatior.Models;

namespace BugTerminatior.Controllers
{
    [Authorize(Roles = "Administrator, Developer, Submitter")]
    public class TicketHistoriesController : Controller
    {
        private BugTrackerEntities db = new BugTrackerEntities();

        // GET: TicketHistories
        public ActionResult Index()
        {
            var ticketHistories = db.TicketHistories.Include(t => t.BTUser).Include(t => t.Ticket);
            return View(ticketHistories.ToList());
        }

        // GET: TicketHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketHistory ticketHistory = db.TicketHistories.Find(id);
            if (ticketHistory == null)
            {
                return HttpNotFound();
            }
            return View(ticketHistory);
        }

    }
}

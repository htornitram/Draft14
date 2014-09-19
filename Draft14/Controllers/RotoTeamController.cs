using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Draft14.Models;

namespace Draft14.Controllers
{
    public class RotoTeamController : Controller
    {
        private Draft14_I95Entities db = new Draft14_I95Entities();

        // GET: /RotoTeam/
        public ActionResult Index()
        {
            return View(db.RotoTeams.ToList());
        }

        // GET: /RotoTeam/Roster/5
        public ActionResult Roster(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RotoTeam rototeam = db.RotoTeams.Find(id);
            if (rototeam == null)
            {
                return HttpNotFound();
            }
            return View(rototeam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

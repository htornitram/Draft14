using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Draft14.Models;
using Draft14.ViewModels;

namespace Draft14.Controllers
{
    public class MLBTeamController : Controller
    {
        private Draft14_I95Entities db = new Draft14_I95Entities();

        // GET: /MLBTeam/
        public ActionResult Index()
        {
            return View(db.MLBTeams.ToList());
        }

        // GET: /MLBTeam/Roster/5
        public ActionResult Roster(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MLBTeam mlbteam = db.MLBTeams.Find(id);
            if (mlbteam == null)
            {
                return HttpNotFound();
            }

            RotoTeam rotoTeam = db.RotoTeams.Where(x => x.Owned > 0).First(); // ok if null... 

            List<Position> positions = db.Positions.ToList();

            MLBRosterViewModel vm = new MLBRosterViewModel(mlbteam, rotoTeam, positions);

            return View(vm);
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

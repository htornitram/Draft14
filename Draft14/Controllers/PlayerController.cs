using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.WebPages;
using System.Web.Mvc;
using Draft14.Models;

namespace Draft14.Controllers
{
    public class PlayerController : Controller
    {
        private Draft14_I95Entities db = new Draft14_I95Entities();

        // GET: /Player/DraftActive/5
        public ActionResult DraftActive(int? id)
        {
            return DraftBase(id);
        }

        public ActionResult DraftTaxi(int? id)
        {
            return DraftBase(id);
        }

        private ActionResult DraftBase(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }

            Drafted drafted = new Drafted();
            drafted.PlayerId = player.PlayerId;
            drafted.Player = player;

            SetupSelectLists(player);

            return View(drafted);
        }

        private void SetupSelectLists(Player player, int selectedPos = 0)
        {

            Dictionary<int, string> teamInfo = new Dictionary<int, string>();

            foreach (RotoTeam t in db.RotoTeams.ToList())
            {
                string info = "Have: " + (100 - (t.Drafteds.Sum(x => x.Price))).ToString("F2") +
                    " ... Need: " + t.RotoTeamPosNeeded.PosNeeded +
                    (t.RotoTeamPosNeeded.PosMoves.IsEmpty() ? "" : " ... Moves: " + t.RotoTeamPosNeeded.PosMoves);

                teamInfo.Add(t.RotoTeamId, info);
            }

            ViewBag.RotoTeamId = new SelectList(db.RotoTeams, "RotoTeamId", "RotoTeamName");
            ViewBag.PositionId = new SelectList(db.Positions.Where(x => (x.PosMask & player.PosCode) > 0),
                "PosMask", "PosName", selectedPos);
            ViewBag.TeamInfo = teamInfo;

        }


        // POST: /Player/DraftActive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DraftActive([Bind(Include = "PlayerId,RotoTeamId,PositionId,Price")] Drafted drafted)
        {
            // defaults for active
            drafted.ActiveFlag = 1;
            drafted.KeeperFlag = 0;
            drafted.TypeYear = "S3";

            //check the price... can't be zero for active..
            if (drafted.Price == 0) ModelState.AddModelError("Price", "Price can't be zero");
            if (drafted.Price * 4 != Math.Round(drafted.Price * 4)) ModelState.AddModelError("Price", "Must be .25 increment");

            Player player = db.Players.Find(drafted.PlayerId);
            if (player == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                db.Drafteds.Add(drafted);
                db.SaveChanges();
                return RedirectToAction("Roster", "MLBTeam", new { id = player.MLBTeam1.MLBTeamAbbr });
            }

            drafted.Player = player;

            SetupSelectLists(player);

            return View(drafted);

        }

        // POST: /Player/DraftTaxi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DraftTaxi([Bind(Include = "PlayerId,RotoTeamId")] Drafted drafted)
        {
            Player player = db.Players.Find(drafted.PlayerId);
            if (player == null)
            {
                return HttpNotFound();
            }

            // defaults for taxi
            drafted.ActiveFlag = 0;
            drafted.KeeperFlag = 0;
            drafted.TypeYear = "L3";
            drafted.Price = 0;
            drafted.PositionId = (db.Positions.Where(x => (x.PosMask & player.PosCode) > 0)
                    .OrderBy(x => x.PosPriority).First()).PosMask;

            if (ModelState.IsValid)
            {
                db.Drafteds.Add(drafted);
                db.SaveChanges();
                return RedirectToAction("Index", "Taxi");
            }

            drafted.Player = player;

            SetupSelectLists(player);

            return View(drafted);

        }        
        
        // GET: /Player/Move/5
        public ActionResult Move(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drafted drafted = db.Drafteds.Find(id);
            if (drafted == null)
            {
                return HttpNotFound();
            }

            SetupSelectLists(drafted.Player, drafted.PositionId);
            return View(drafted);

        }

        // POST: /Player/Move
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Move(int? id, FormCollection fc)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drafted drafted = db.Drafteds.Find(id);
            if (drafted == null)
            {
                return HttpNotFound();
            }

            UpdateModel(drafted, new string[] { "PositionId" });

            if (ModelState.IsValid)
            {
                db.Entry(drafted).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Roster", "RotoTeam", new { id = drafted.RotoTeamId });
            }

            SetupSelectLists(drafted.Player);
            return View(drafted);

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

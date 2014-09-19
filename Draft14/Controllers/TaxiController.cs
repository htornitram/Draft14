using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Draft14.Models;

namespace Draft14.Controllers
{
    public class TaxiController : Controller
    {

        private Draft14_I95Entities db = new Draft14_I95Entities();

        //
        // GET: /Taxi/
        public ActionResult Index(string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) sortBy = "EstValue_desc";

            ViewBag.NameSort = (sortBy == "Name") ? "Name_desc" : "Name";
            ViewBag.TeamSort = (sortBy == "Team") ? "Team_desc" : "Team";
            ViewBag.ValSort = (sortBy == "EstValue_desc") ? "EstValue" : "EstValue_desc";
            ViewBag.TierSort = (sortBy == "Tier_desc") ? "Tier" : "Tier_desc";

            var players = db.Players.Where(x => (x.Drafted == null) && (x.Ineligible == 0));

            switch(sortBy)
            {
                case "Name":
                    players = players.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
                    break;
                case "Name_desc":
                    players = players.OrderByDescending(x => x.LastName).ThenByDescending(x => x.FirstName);
                    break;
                case "Team":
                    players = players.OrderBy(x => x.MLBTeam).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
                    break;
                case "Team_desc":
                    players = players.OrderByDescending(x => x.MLBTeam).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
                    break;
                case "EstValue":
                    players = players.OrderBy(x => x.EstValue).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
                    break;
                case "EstValue_desc":
                    players = players.OrderByDescending(x => x.EstValue).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
                    break;
                case "Tier":
                    players = players.OrderBy(x => x.TierColor ?? 0).ThenByDescending(x => x.TierNum ?? 0).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
                    break;
                case "Tier_desc":
                    players = players.OrderByDescending(x => x.TierColor ?? 0).ThenBy(x => x.TierNum ?? 0).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
                    break;
            }

            return View(players.ToList());
        }
	}
}
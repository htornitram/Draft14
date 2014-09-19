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
    public class PositionController : Controller
    {

        private Draft14_I95Entities db = new Draft14_I95Entities();
        
        //
        // GET: /Position/
        public ActionResult Index()
        {
            return View(db.Positions.ToList());
        }
	}
}
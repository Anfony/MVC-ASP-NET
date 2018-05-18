using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class LoadingController : Controller
    {
        //
        // GET: /Loading/

       [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

       [HttpPost]
        public ActionResult LoadingPartial(int? PK_IdCampania)
        {
            ViewData["PK_IdCampania"] = PK_IdCampania;
            return PartialView("_Loading");
        }
    }
}
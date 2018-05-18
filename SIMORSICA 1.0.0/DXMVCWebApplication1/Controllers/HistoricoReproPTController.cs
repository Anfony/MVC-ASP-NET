using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class HistoricoReproPTController : Controller
    {
        // GET: TipoReporte      

        [HttpPost]
        public ActionResult HistoricoPT(int? IdCampania)
        {

            ViewData["IdCampania"] = IdCampania;
            return PartialView("_HistoticoReproPT");
        }

        [HttpPost]
        public ActionResult SelectReporte(int? IdCampania)
        {
            ViewData["IdCampania"] = IdCampania;
            return PartialView("_SelectRepro");
        }
    }
}
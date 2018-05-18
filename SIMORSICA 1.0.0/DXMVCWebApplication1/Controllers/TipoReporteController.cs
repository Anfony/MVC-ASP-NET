using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class TipoReporteController : Controller
    {
        // GET: TipoReporte      

        [HttpPost]
        public ActionResult TipodeReporteByPantalla(int? IdCampania)
        {

            ViewData["IdCampania"] = IdCampania;
            return PartialView("_TipoReporte");
        }

        [HttpPost]
        public ActionResult SelectReporte(int? IdCampania)
        {
            ViewData["IdCampania"] = IdCampania;
            return PartialView("_SelectReporte");
        }
    }
}
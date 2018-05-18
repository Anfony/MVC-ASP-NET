using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class CancelaReprogramacionController : Controller
    {
        [HttpPost]
        public ActionResult CancelaRepro_Campania(int? IdCampania)
        {

            ViewData["IdCampania"] = IdCampania;
            return PartialView("_CancelaReprogramacion");
        }

        [HttpPost]
        public ActionResult SowMensaje(int? IdCampania)
        {
            ViewData["IdCampania"] = IdCampania;
            return PartialView("_Mensaje");
        }
        [HttpPost]
        public ActionResult aceptaCancelar(int? PK_IdCampania)
        {
            if (ModelState.IsValid)
            {
                DB_SENASICA_Storeds storeds = new DB_SENASICA_Storeds();
                ObjectParameter mensaje = new ObjectParameter("mensaje", typeof(string));
                storeds.SP_CancelaReprogramacion_Campania(PK_IdCampania, mensaje);
            }

            string currentController = Session["CurrentController"].ToString();

            return RedirectToAction("Index", currentController == null ? "Home" : currentController);
        }
    }
}
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)]
    public class ProyectoPresupuestoEstController : Controller
    {
        // GET: ProyectoPresupuestoEst
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ProyectoPresupuestoGridViewPartial()
        {
            return PartialView("_GridViewPartialUR_UCE", Senasica.GetProyectosPresupuestalesPorElEstado());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProyectoPresupuestoGridViewPartialUpdate(ProyectoPresupuesto item)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.ProyectoPresupuestoes;
            string Error = "";

            try
            {
                var modelItem = model.FirstOrDefault(it => it.Pk_IdProyectoPresupuesto == item.Pk_IdProyectoPresupuesto);
                if (modelItem != null)
                {
                    modelItem.CT_User = User.Identity.GetUserId();
                    modelItem.CT_Date = DateTime.Now;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }

            ViewData["dataItem"] = item;

            return PartialView("_GridViewPartialUR_UCE", Senasica.GetProyectosPresupuestalesPorElEstado());
        }

        public ActionResult _ComboBoxProyectoAutorizado()
        {
            var IdSubComponente = Convert.ToInt32(Request.Params["IdSubComponente"]);

            return PartialView(Senasica.GetProyectosAutorizadosByUR(IdSubComponente));
        }
    }
}
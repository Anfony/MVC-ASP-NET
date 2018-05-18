using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                         + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.UR_ANIMAL
                         + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                         + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                         + "," + RolesUsuarios.PUESTO_GERENTE
                         + "," + RolesUsuarios.PUESTO_COOR_REGIONAL
                         + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                         + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                         + "," + RolesUsuarios.SUP_ESTATAL
                         + "," + RolesUsuarios.SUP_REGIONAL
        )]
    public class SACAtendidoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /SACAtendido/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SACAtendidoGridViewPartial(int? IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_SACAtendidoGridViewPartial", Senasica.GetSACAtendidoByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SACAtendidoGridViewPartialAddNew(SAC_Atendido item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SAC_Atendido;
            item.Fk_IdCampania = IdCampania;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    Console.Write(e);
                }
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_SACAtendidoGridViewPartial", Senasica.GetSACAtendidoByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SACAtendidoGridViewPartialUpdate(SAC_Atendido item, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SAC_Atendido;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdSAC_Atendido == item.Pk_IdSAC_Atendido);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_SACAtendidoGridViewPartial", Senasica.GetSACAtendidoByCampania(IdCampania));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SACAtendidoGridViewPartialDelete(System.Int32 Pk_IdSAC_Atendido, int IdCampania, int? IdEstado)
        {
            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var model = db.SAC_Atendido;
            if (Pk_IdSAC_Atendido >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdSAC_Atendido == Pk_IdSAC_Atendido);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_SACAtendidoGridViewPartial", Senasica.GetSACAtendidoByCampania(IdCampania));
        }
    }
}
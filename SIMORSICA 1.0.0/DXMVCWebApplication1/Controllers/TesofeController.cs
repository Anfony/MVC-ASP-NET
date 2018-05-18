using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DXMVCWebApplication1.Negocio;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class TesofeController : Controller
    {
        // GET: Tesofe
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult TesoFeGridViewPartial()
        {
            Permisos();

            return PartialView("_GridViewPartial", Senasica.GetTesoFe());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TesoFeAddNew(TESOFE item)
        {
            Permisos();

            if (ModelState.IsValid)
            {
                if (item.Importe == 0)
                {
                    return JavaScript("El Importe debe ser diferente de 0");
                }
                else
                {
                    try
                    {
                        DB_SENASICAEntities db = new DB_SENASICAEntities();
                        int IdIE = FuncionesAuxiliares.GetCurrent_IdUnidadEjecutora();
                        int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
                        var model = db.TESOFEs;

                        #region ValidaRegistroDuplicado
                        ValidaRegistroDuplicado ValidaDuplicado = new ValidaRegistroDuplicado(0, IdIE, item.Fk_IdTipodeRecurso, item.Fk_IdTipoReembolso, item.Importe, IdAnioPres);
                        if (ValidaDuplicado.EsRegistroDuplicado() == "NoValido")
                        {
                            return JavaScript("El registro ya existe");
                        }
                        #endregion

                        #region ValidaReintegroTesoFe
                        //falta analizar bien este caso, por el momento no se utiliza
                        //ValidaReintegroTesoFe ValidaDetalle = new ValidaReintegroTesoFe(0, IdIE, item.Fk_IdTipodeRecurso, item.Fk_IdTipoReembolso, item.Importe, IdAnioPres );
                        //if (ValidaDetalle.EsGastoValido() == "NoValido")
                        //{
                        //    return JavaScript("El importe que desea guardar, debe ser igual al importe total del reintegro");
                        //}
                        #endregion

                        item.Fk_IdUnidadEjecutora = IdIE;
                        item.Fk_IdAnio = IdAnioPres;
                        item.CT_User = User.Identity.GetUserId();
                        item.CT_Date = DateTime.Now;
                        model.Add(item);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_GridViewPartial", Senasica.GetTesoFe());
        }

        public ActionResult TesoFeUpdate(TESOFE item)
        {
            Permisos();

            if (ModelState.IsValid)
            {
                if (item.Importe == 0)
                {
                    return JavaScript("El Importe debe ser diferente de 0");
                }
                else
                {
                    try
                    {
                        DB_SENASICAEntities db = new DB_SENASICAEntities();
                        var model = db.TESOFEs;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdTesofe == item.Pk_IdTesofe);

                        if (modelItem != null)
                        {

                            #region ValidaRegistroDuplicado
                            ValidaRegistroDuplicado ValidaDuplicado = new ValidaRegistroDuplicado(modelItem.Pk_IdTesofe, modelItem.Fk_IdUnidadEjecutora, item.Fk_IdTipodeRecurso, item.Fk_IdTipoReembolso, item.Importe, modelItem.Fk_IdAnio);
                            if (ValidaDuplicado.EsRegistroDuplicado() == "NoValido")
                            {
                                return JavaScript("El registro ya existe");
                            }
                            #endregion

                            #region ValidaReintegroTesoFe
                            //falta analizar bien este caso, por el momento no se utiliza
                            //ValidaReintegroTesoFe ValidaDetalle = new ValidaReintegroTesoFe(modelItem.Pk_IdTesofe, modelItem.Fk_IdUnidadEjecutora, item.Fk_IdTipodeRecurso, item.Fk_IdTipoReembolso, item.Importe, modelItem.Fk_IdAnio);
                            //if (ValidaDetalle.EsGastoValido() == "NoValido")
                            //{
                            //    return JavaScript("El importe que desea guardar, debe ser igual al importe total del reintegro");
                            //}
                            #endregion

                            modelItem.CT_User = User.Identity.GetUserId();
                            modelItem.CT_Date = DateTime.Now;
                            this.UpdateModel(modelItem);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_GridViewPartial", Senasica.GetTesoFe());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TesoFeDelete(System.Int32 Pk_IdTesofe)
        {
            Permisos();

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.TESOFEs;
            if (Pk_IdTesofe >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdTesofe == Pk_IdTesofe);
                    if (item != null)
                    {
                        model.Remove(item);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            return PartialView("_GridViewPartial", Senasica.GetTesoFe());
        }
        private void Permisos()
        {
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_TESOFE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];

            string anioPres = FuncionesAuxiliares.AnioPresupuestal(FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal());

            ViewData["FechaIni"] = "01/01/" + anioPres;
            ViewData["FechaFin"] = "31/12/" + anioPres;
        }
    }
}
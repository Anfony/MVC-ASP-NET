using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Negocio;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class TesofeDetalleController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: TesofeDetalle
        [ValidateInput(false)]
        public ActionResult TesofeDetalleGridViewPartial(int? IdTesofe)
        {
            Permisos();

            ViewData["IdTesofe"] = IdTesofe;

            return PartialView("_GridViewPartial", Senasica.GetTesoFeDetalleByTESOFE(IdTesofe));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TesofeDetalleGridViewPartialAddNew(TesofeDetalle item, int IdTesofe)
        {
            Permisos();

            ViewData["IdTesofe"] = IdTesofe;
            var model = db.TesofeDetalles;

            if (ModelState.IsValid)
            {
                if (item.ImporteDetalle == 0)
                {
                    return JavaScript("El Importe debe ser diferente de 0");
                }
                else
                {
                    try
                    {
                        #region ValidaReintegroTesoFe_Detalle

                        int IdAnio = db.TESOFEs.Where(i => i.Pk_IdTesofe == IdTesofe).Select(i => i.Fk_IdAnio).First();
                        ValidaReintegroTesoFe_Detalle ValidaDetalle = new ValidaReintegroTesoFe_Detalle(0, IdTesofe, item.Fk_IdProyectoPresupuesto, item.ImporteDetalle, IdAnio);
                        if (ValidaDetalle.EsGastoValido() == "NoValido")
                        {
                            return JavaScript("Posibles Causas:"
                                    + "\n1 Rebasó el importe autorizado para el Poyecto seleccionado"
                                    + "\n2 El importe que se desea agregar supera el importe total del reintegro");
                        }
                        #endregion

                        item.Fk_IdTesofe = IdTesofe;
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_GridViewPartial", Senasica.GetTesoFeDetalleByTESOFE(IdTesofe));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TesofeDetalleGridViewPartialUpdate(TesofeDetalle item, int IdTesofe)
        {
            Permisos();
            ViewData["IdTesofe"] = IdTesofe;

            if (ModelState.IsValid)
            {
                if (item.ImporteDetalle == 0)
                {
                    return JavaScript("El Importe debe ser diferente de 0");
                }
                else
                {
                    try
                    {
                        var model = db.TesofeDetalles;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdTesofeDetalle == item.Pk_IdTesofeDetalle);
                        if (modelItem != null)
                        {

                            #region ValidaReintegroTesoFe_Detalle
                            int IdAnio = db.TESOFEs.Where(i => i.Pk_IdTesofe == IdTesofe).Select(i => i.Fk_IdAnio).First();
                            ValidaReintegroTesoFe_Detalle ValidaDetalle = new ValidaReintegroTesoFe_Detalle(modelItem.Pk_IdTesofeDetalle, IdTesofe, item.Fk_IdProyectoPresupuesto, item.ImporteDetalle, IdAnio);
                            if (ValidaDetalle.EsGastoValido() == "NoValido")
                            {
                                return JavaScript("Posibles Causas:"
                                    + "\n1 Rebasó el importe autorizado para el Poyecto seleccionado"
                                    + "\n2 El importe que se desea agregar supera el importe total del reintegro");
                            }
                            #endregion

                            modelItem.Fk_IdTesofe = IdTesofe;
                            modelItem.Fk_IdProyectoPresupuesto = item.Fk_IdProyectoPresupuesto;
                            modelItem.ImporteDetalle = item.ImporteDetalle;
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

            ViewData["dataItem"] = item;

            return PartialView("_GridViewPartial", Senasica.GetTesoFeDetalleByTESOFE(IdTesofe));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TesofeDetalleGridViewPartialDelete(System.Int32 Pk_IdTesofeDetalle, int IdTesofe)
        {
            Permisos();

            ViewData["IdTesofe"] = IdTesofe;
            
            var model = db.TesofeDetalles;
            if (Pk_IdTesofeDetalle >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdTesofeDetalle == Pk_IdTesofeDetalle);
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

            return PartialView("_GridViewPartial", Senasica.GetTesoFeDetalleByTESOFE(IdTesofe));
        }

        #region Obtiene permisos del usuario logeado
        private void Permisos()
        {
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_TESOFE_DETALLE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
        }
        #endregion
    }
}
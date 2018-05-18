using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Negocio;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ImportanciaEnfermedadSAController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult ImportanciaEnfermedadSAGridViewPartial(int? IdCampania, int? IdEstado)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return PartialView("_ImportanciaEnfermedadSAGridViewPartial", Senasica.GetImportanciaEnfermedadSAByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaEnfermedadSAAddNew(ImportanciaEnfermedadSA item, ImportanciaEnfermedadSA_Repro itemR, int IdCampania, int IdEstado)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.ImportanciaEnfermedadSAs;
                        item.CT_User = User.Identity.GetUserId();
                        item.CT_Date = DateTime.Now;
                        item.Fk_IdCampania = IdCampania;
                        model.Add(item);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = item;
            }
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.ImportanciaEnfermedadSA_Repro;
                        itemR.CT_User = User.Identity.GetUserId();
                        itemR.CT_Date = DateTime.Now;
                        itemR.Fk_IdCampania = IdCampania;
                        model.Add(itemR);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = itemR;
            }
            return PartialView("_ImportanciaEnfermedadSAGridViewPartial", Senasica.GetImportanciaEnfermedadSAByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaEnfermedadSAUpdate(ImportanciaEnfermedadSA item, ImportanciaEnfermedadSA_Repro itemR, int IdCampania, int IdEstado)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.ImportanciaEnfermedadSAs;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdImportanciaEnfermedadSA == item.Pk_IdImportanciaEnfermedadSA);

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
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = item;
            }
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.ImportanciaEnfermedadSA_Repro;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdImportanciaEnfermedadSA == itemR.Pk_IdImportanciaEnfermedadSA);

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
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = itemR;
            }
            return PartialView("_ImportanciaEnfermedadSAGridViewPartial", Senasica.GetImportanciaEnfermedadSAByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaEnfermedadSADelete(int Pk_IdImportanciaEnfermedadSA, int IdCampania, int IdEstado)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdEstado"] = IdEstado;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                if (Pk_IdImportanciaEnfermedadSA > 0)
                {
                    try
                    {
                        var model = db.ImportanciaEnfermedadSAs;
                        var item = model.FirstOrDefault(it => it.Pk_IdImportanciaEnfermedadSA == Pk_IdImportanciaEnfermedadSA);

                        if (item != null)
                        {
                            ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                            db.SP_DELETE_ImportanciaEnfermedadSA(Pk_IdImportanciaEnfermedadSA, User.Identity.GetUserId(), Error);
                        }
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
            }
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                if (Pk_IdImportanciaEnfermedadSA > 0)
                {
                    try
                    {
                        var model = db.ImportanciaEnfermedadSA_Repro;
                        var item = model.FirstOrDefault(it => it.Pk_IdImportanciaEnfermedadSA == Pk_IdImportanciaEnfermedadSA);

                        if (item != null)
                        {
                            model.Remove(item);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
            }
            return PartialView("_ImportanciaEnfermedadSAGridViewPartial", Senasica.GetImportanciaEnfermedadSAByCampania(IdCampania, StatusActual));
        }
    }
}
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ImportanciaInoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult ImportanciaInoGridViewPartial(int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return PartialView("_ImportanciaInoGridViewPartial", Senasica.GetImportanciaInoByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaInoAddNew(ImportanciaIno item, ImportanciaIno_Repro itemR, int IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                if (item.esAfectado == null) item.esAfectado = false;

                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.ImportanciaInoes;
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
                if (itemR.esAfectado == null) itemR.esAfectado = false;

                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.ImportanciaIno_Repro;
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
            return PartialView("_ImportanciaInoGridViewPartial", Senasica.GetImportanciaInoByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaInoUpdate(ImportanciaIno item, ImportanciaIno_Repro itemR, int IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                if (item.esAfectado == null) item.esAfectado = false;

                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.ImportanciaInoes;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdImportanciaIno == item.Pk_IdImportanciaIno);

                        if (modelItem != null)
                        {
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
                if (itemR.esAfectado == null) itemR.esAfectado = false;

                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.ImportanciaIno_Repro;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdImportanciaIno == itemR.Pk_IdImportanciaIno);

                        if (modelItem != null)
                        {
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
            return PartialView("_ImportanciaInoGridViewPartial", Senasica.GetImportanciaInoByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaInoDelete(int Pk_IdImportanciaIno, int IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                if (Pk_IdImportanciaIno > 0)
                {
                    try
                    {
                        var model = db.ImportanciaInoes;
                        var item = model.FirstOrDefault(it => it.Pk_IdImportanciaIno == Pk_IdImportanciaIno);

                        if (item != null)
                            model.Remove(item);

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
                if (Pk_IdImportanciaIno > 0)
                {
                    try
                    {
                        var model = db.ImportanciaIno_Repro;
                        var item = model.FirstOrDefault(it => it.Pk_IdImportanciaIno == Pk_IdImportanciaIno);

                        if (item != null)
                            model.Remove(item);

                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
            }
            return PartialView("_ImportanciaInoGridViewPartial", Senasica.GetImportanciaInoByCampania(IdCampania, StatusActual));
        }
    }
}
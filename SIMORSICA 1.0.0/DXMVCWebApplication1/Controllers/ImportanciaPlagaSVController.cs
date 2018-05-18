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
    public class ImportanciaPlagaSVController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        // GET: ImportanciaPlagaSV
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ImportanciaPlagaSVGridViewPartial(int? IdCampania, int? IdEstado)
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
            return PartialView("_ImportanciaPlagaSVGridviewPartial", Senasica.GetImportanciaPlagaSVByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaPlagaSVGridViewPartialAddNew(ImportanciaPlagaSV item, ImportanciaPlagaSV_Repro itemR, int IdCampania, int IdEstado)
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
                        var model = db.ImportanciaPlagaSVs;

                        item.Fk_IdCampania = IdCampania;
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
                        var model = db.ImportanciaPlagaSV_Repro;

                        itemR.Fk_IdCampania = IdCampania;
                        itemR.CT_User = User.Identity.GetUserId();
                        itemR.CT_Date = DateTime.Now;

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
            return PartialView("_ImportanciaPlagaSVGridviewPartial", Senasica.GetImportanciaPlagaSVByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaPlagaSVGridViewPartialUpdate(ImportanciaPlagaSV item, ImportanciaPlagaSV_Repro itemR, int IdCampania, int IdEstado)
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
                        var model = db.ImportanciaPlagaSVs;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdImportanciaPlagaSV == item.Pk_IdImportanciaPlagaSV);

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
                        var model = db.ImportanciaPlagaSV_Repro;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdImportanciaPlagaSV == itemR.Pk_IdImportanciaPlagaSV);

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
            return PartialView("_ImportanciaPlagaSVGridviewPartial", Senasica.GetImportanciaPlagaSVByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ImportanciaPlagaSVGridViewPartialDelete(int Pk_IdImportanciaPlagaSV, int IdCampania, int IdEstado)
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
                if (Pk_IdImportanciaPlagaSV > 0)
                {
                    try
                    {
                        var model = db.ImportanciaPlagaSVs;
                        var item = model.FirstOrDefault(it => it.Pk_IdImportanciaPlagaSV == Pk_IdImportanciaPlagaSV);

                        if (item != null)
                        {
                            ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                            db.SP_DELETE_ImportanciaPlagaSV(Pk_IdImportanciaPlagaSV, User.Identity.GetUserId(), Error);
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
                if (Pk_IdImportanciaPlagaSV > 0)
                {
                    try
                    {
                        var model = db.ImportanciaPlagaSV_Repro;
                        var item = model.FirstOrDefault(it => it.Pk_IdImportanciaPlagaSV == Pk_IdImportanciaPlagaSV);

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
            return PartialView("_ImportanciaPlagaSVGridviewPartial", Senasica.GetImportanciaPlagaSVByCampania(IdCampania, StatusActual));
        }
    }
}
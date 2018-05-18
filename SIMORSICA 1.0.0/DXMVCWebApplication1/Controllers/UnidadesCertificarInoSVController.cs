using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class UnidadesCertificarInoSVController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult UnidadesCertificarInoSVGridViewPartial(int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return PartialView("_UnidadesCertificarInoSVGridViewPartial", Senasica.GetUnidadesCertificarInoSVByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadesCertificarInoSVAddNew(UnidadesCertificarInoSV item, UnidadesCertificarInoSV_Repro itemR, int IdCampania)
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
                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.UnidadesCertificarInoSVs;
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
                        var model = db.UnidadesCertificarInoSV_Repro;
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
            return PartialView("_UnidadesCertificarInoSVGridViewPartial", Senasica.GetUnidadesCertificarInoSVByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadesCertificarInoSVUpdate(UnidadesCertificarInoSV item, UnidadesCertificarInoSV_Repro itemR, int IdCampania)
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
                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.UnidadesCertificarInoSVs;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdUnidadesCertificarInoSV == item.Pk_IdUnidadesCertificarInoSV);

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
                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.UnidadesCertificarInoSV_Repro;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdUnidadesCertificarInoSV == itemR.Pk_IdUnidadesCertificarInoSV);

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
            return PartialView("_UnidadesCertificarInoSVGridViewPartial", Senasica.GetUnidadesCertificarInoSVByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadesCertificarInoSVDelete(int Pk_IdUnidadesCertificarInoSV, int IdCampania)
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
                if (Pk_IdUnidadesCertificarInoSV > 0)
                {
                    try
                    {
                        var model = db.UnidadesCertificarInoSVs;
                        var item = model.FirstOrDefault(it => it.Pk_IdUnidadesCertificarInoSV == Pk_IdUnidadesCertificarInoSV);

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
                if (Pk_IdUnidadesCertificarInoSV > 0)
                {
                    try
                    {
                        var model = db.UnidadesCertificarInoSV_Repro;
                        var item = model.FirstOrDefault(it => it.Pk_IdUnidadesCertificarInoSV == Pk_IdUnidadesCertificarInoSV);

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
            return PartialView("_UnidadesCertificarInoSVGridViewPartial", Senasica.GetUnidadesCertificarInoSVByCampania(IdCampania, StatusActual));
        }
    }
}
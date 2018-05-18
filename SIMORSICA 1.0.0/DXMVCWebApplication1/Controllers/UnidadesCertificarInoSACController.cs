using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class UnidadesCertificarInoSACController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult UnidadesCertificarInoSACGridViewPartial(int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return PartialView("_UnidadesCertificarInoSACGridViewPartial", Senasica.GetUnidadesCertificarInoSACByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadesCertificarInoSACAddNew(UnidadesCertificarInoSAC item, UnidadesCertificarInoSAC_Repro itemR, int IdCampania)
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
                        var model = db.UnidadesCertificarInoSACs;
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
                        var model = db.UnidadesCertificarInoSAC_Repro;
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
            return PartialView("_UnidadesCertificarInoSACGridViewPartial", Senasica.GetUnidadesCertificarInoSACByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadesCertificarInoSACUpdate(UnidadesCertificarInoSAC item, UnidadesCertificarInoSAC_Repro itemR, int IdCampania)
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
                        var model = db.UnidadesCertificarInoSACs;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdUnidadesCertificarInoSAC == item.Pk_IdUnidadesCertificarInoSAC);

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
                        var model = db.UnidadesCertificarInoSAC_Repro;
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdUnidadesCertificarInoSAC == itemR.Pk_IdUnidadesCertificarInoSAC);

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
            return PartialView("_UnidadesCertificarInoSACGridViewPartial", Senasica.GetUnidadesCertificarInoSACByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UnidadesCertificarInoSACDelete(int Pk_IdUnidadesCertificarInoSAC, int IdCampania)
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
                if (Pk_IdUnidadesCertificarInoSAC > 0)
                {
                    try
                    {
                        var model = db.UnidadesCertificarInoSACs;
                        var item = model.FirstOrDefault(it => it.Pk_IdUnidadesCertificarInoSAC == Pk_IdUnidadesCertificarInoSAC);

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
                if (Pk_IdUnidadesCertificarInoSAC > 0)
                {
                    try
                    {
                        var model = db.UnidadesCertificarInoSAC_Repro;
                        var item = model.FirstOrDefault(it => it.Pk_IdUnidadesCertificarInoSAC == Pk_IdUnidadesCertificarInoSAC);

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
            return PartialView("_UnidadesCertificarInoSACGridViewPartial", Senasica.GetUnidadesCertificarInoSACByCampania(IdCampania, StatusActual));
        }
    }
}
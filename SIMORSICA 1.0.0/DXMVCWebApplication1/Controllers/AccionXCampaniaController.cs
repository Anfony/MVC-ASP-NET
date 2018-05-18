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
    [Authorize]
    public class AccionXCampaniaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        // GET: AccionXCampania
        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult AccionXCampaniaGridViewPartial(int? IdCampania, int? IdProyectoAutorizado, int? IdUnidadEjecutora)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdProyectoAutorizado"] = IdProyectoAutorizado;
            ViewData["IdUnidadEjecutora"] = IdUnidadEjecutora;

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return PartialView("_AccionXCampaniaGridViewPartial", Senasica.GetAccionesPorCampaniaByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AccionXCampaniaGridViewPartialAddNew(AccionXCampania item, AccionXCampania_Repro itemR, int IdCampania, int IdProyectoAutorizado, int IdUnidadEjecutora)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdProyectoAutorizado"] = IdProyectoAutorizado;
            ViewData["IdUnidadEjecutora"] = IdUnidadEjecutora;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var model = db.AccionXCampanias;
                        item.Fk_IdCampania__UE = IdCampania;
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
                        var model = db.AccionXCampania_Repro;
                        itemR.Fk_IdCampania__UE = IdCampania;
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
            return PartialView("_AccionXCampaniaGridViewPartial", Senasica.GetAccionesPorCampaniaByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AccionXCampaniaGridViewPartialUpdate(AccionXCampania item, AccionXCampania_Repro itemR, int IdCampania, int IdProyectoAutorizado, int IdUnidadEjecutora)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdProyectoAutorizado"] = IdProyectoAutorizado;
            ViewData["IdUnidadEjecutora"] = IdUnidadEjecutora;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                var model = db.AccionXCampanias;
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.PK_IdAccionXCampania == item.PK_IdAccionXCampania);
                        if (modelItem != null)
                        {
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
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = item;
            }
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                var model = db.AccionXCampania_Repro;
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.PK_IdAccionXCampania == itemR.PK_IdAccionXCampania);
                        if (modelItem != null)
                        {
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
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = itemR;
            }
            return PartialView("_AccionXCampaniaGridViewPartial", Senasica.GetAccionesPorCampaniaByCampania(IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AccionXCampaniaGridViewPartialDelete(int PK_IdAccionXCampania, int IdCampania, int IdProyectoAutorizado, int IdUnidadEjecutora)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdCampania"] = IdCampania;
            ViewData["IdProyectoAutorizado"] = IdProyectoAutorizado;
            ViewData["IdUnidadEjecutora"] = IdUnidadEjecutora;
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                var model = db.AccionXCampanias;
                if (PK_IdAccionXCampania >= 0)
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.PK_IdAccionXCampania == PK_IdAccionXCampania);
                        if (item != null)
                        {
                            ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                            db.SP_DELETE_AccionXCampania(PK_IdAccionXCampania, User.Identity.GetUserId(), Error);
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
                var model = db.AccionXCampania_Repro;

                if (PK_IdAccionXCampania >= 0)
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.PK_IdAccionXCampania == PK_IdAccionXCampania);
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
            return PartialView("_AccionXCampaniaGridViewPartial", Senasica.GetAccionesPorCampaniaByCampania(IdCampania, StatusActual));
        }
    }
}
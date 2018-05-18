using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using DXMVCWebApplication1.Negocio;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class ActividadXAccionController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /ActividadXAccion/
        public ActionResult Index()
        {
            return View();
        }

        #region ActividadXAccionGridView

        [ValidateInput(false)]
        public ActionResult ActividadXAccionGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ACTIVIDAD_ACCION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
            int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? IdPersona = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdPersona__SIS);
            int? IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string rolusuraio = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            ViewData["IdUnidadEjecutora"] = UEID;

            return PartialView("_ActividadXAccionGridViewPartial", Senasica.GetActividadesParaAsignarByCampania(rolusuraio, UEID, IdPersona, IdEstado, IdAnio));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadXAccionGridViewPartialAddNew(ActividadXAccion item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ACTIVIDAD_ACCION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
            int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? IdPersona = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdPersona__SIS);
            int? IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string rolusuraio = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            ViewData["IdUnidadEjecutora"] = UEID;

            var model = db.ActividadXAccions;
            if (ModelState.IsValid)
            {
                try
                {
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

            return PartialView("_ActividadXAccionGridViewPartial", Senasica.GetActividadesParaAsignarByCampania(rolusuraio, UEID, IdPersona, IdEstado, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadXAccionGridViewPartialUpdate(ActividadXAccion item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ACTIVIDAD_ACCION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
            int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? IdPersona = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdPersona__SIS);
            int? IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string rolusuraio = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            ViewData["IdUnidadEjecutora"] = UEID;

            var model = db.ActividadXAccions;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdActividadXAccion == item.Pk_IdActividadXAccion);
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

            return PartialView("_ActividadXAccionGridViewPartial", Senasica.GetActividadesParaAsignarByCampania(rolusuraio, UEID, IdPersona, IdEstado, IdAnio));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadXAccionGridViewPartialDelete(System.Int32 Pk_IdActividadXAccion)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ACTIVIDAD_ACCION);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
            int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? IdPersona = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdPersona__SIS);
            int? IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string rolusuraio = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            ViewData["IdUnidadEjecutora"] = UEID;

            var model = db.ActividadXAccions;
            if (Pk_IdActividadXAccion >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdActividadXAccion == Pk_IdActividadXAccion);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_ActividadXAccion(Pk_IdActividadXAccion, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ActividadXAccionGridViewPartial", Senasica.GetActividadesParaAsignarByCampania(rolusuraio, UEID, IdPersona, IdEstado, IdAnio));
        }
        #endregion

        #region ActividadAsignadaGridView

        [ValidateInput(false)]
        public ActionResult ActividadAsignadaGridViewPartial(int IdActividadXAccion)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ASIGNACION_ACTIVIDADES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Permisos(IdActividadXAccion);

            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewData["IdUnidadEjecutora"] = _Fk_IdUnidadEjecutora;
            ViewData["IdActividadXAccion"] = IdActividadXAccion;
            var model = db.Actividads;
            return PartialView("_ActividadAsignadaGridViewPartial", Senasica.GetActividadesByActividadXAccion(IdActividadXAccion));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadAsignadaGridViewPartialAddNew(Actividad item, int IdActividadXAccion)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ASIGNACION_ACTIVIDADES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Permisos(IdActividadXAccion);

            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewData["IdUnidadEjecutora"] = _Fk_IdUnidadEjecutora;

            ViewData["IdActividadXAccion"] = IdActividadXAccion;
            var model = db.Actividads;
            item.Fk_IdActividadXAccion = IdActividadXAccion;
            if (ModelState.IsValid)
            {
                try
                {
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_ActividadAsignadaGridViewPartial", Senasica.GetActividadesByActividadXAccion(IdActividadXAccion));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadAsignadaGridViewPartialUpdate(Actividad item, int IdActividadXAccion)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ASIGNACION_ACTIVIDADES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Permisos(IdActividadXAccion);

            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewData["IdUnidadEjecutora"] = _Fk_IdUnidadEjecutora;

            ViewData["IdActividadXAccion"] = IdActividadXAccion;
            var model = db.Actividads;

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdActividad == item.Pk_IdActividad);
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_ActividadAsignadaGridViewPartial", Senasica.GetActividadesByActividadXAccion(IdActividadXAccion));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadAsignadaGridViewPartialDelete(System.Int32 Pk_IdActividad, int IdActividadXAccion)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_ASIGNACION_ACTIVIDADES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Permisos(IdActividadXAccion);

            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewData["IdUnidadEjecutora"] = _Fk_IdUnidadEjecutora;

            ViewData["IdActividadXAccion"] = IdActividadXAccion;
            var model = db.Actividads;
            if (Pk_IdActividad >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdActividad == Pk_IdActividad);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_Actividad(Pk_IdActividad, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ActividadAsignadaGridViewPartial", Senasica.GetActividadesByActividadXAccion(IdActividadXAccion));
        }
        #endregion

        #region ActividadesXAccionGridView
        [ValidateInput(false)]
        public ActionResult ActividadesXAccionGridViewPartial(int? IdAccionXCamp, int? IdTipoDeAccion, int? IdTipoActividad, int? IdUnidadEjecutora, int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña

            //var _query = db.ActividadXAccions.Where(item => item.Fk_IdAccionXCampania == IdAccionXCamp).Select(item => item.AccionXCampania.Campania.Pk_IdCampania);
            //int? IdCampania = _query.Count() == 0 ? -1 : _query.First();

            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdAccionXCamp"] = IdAccionXCamp;
            ViewData["IdTipoDeAccion"] = IdTipoDeAccion;
            ViewData["IdTipoActividad"] = IdTipoActividad;
            ViewData["IdUnidadEjecutora"] = IdUnidadEjecutora;
            ViewData["IdCampania"] = IdCampania;


            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                var _IdAccionXCamp_Repro = db.AccionXCampania_Repro.Where(it => it.PK_IdAccionXCampania == IdAccionXCamp).Select(it => it.PK_IdAccionXCampania);
                int? accion = _IdAccionXCamp_Repro.Count() == 0 ? -1 : _IdAccionXCamp_Repro.First();
                return PartialView("_ActividadesXAccionGridViewPartial", Senasica.GetActividadesByAccion(accion, IdCampania, StatusActual));
            }

            return PartialView("_ActividadesXAccionGridViewPartial", Senasica.GetActividadesByAccion(IdAccionXCamp, IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadesXAccionGridViewPartialAddNew(ActividadXAccion item, ActividadXAccion_Repro itemR, int IdAccionXCamp, int? IdTipoDeAccion, int? IdTipoActividad, int? IdUnidadEjecutora, int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña

            //var _query = db.ActividadXAccions.Where(it => it.Fk_IdAccionXCampania == IdAccionXCamp).Select(it => it.AccionXCampania.Campania.Pk_IdCampania);
            //int IdCampania = _query.Count() == 0 ? -1 : _query.First();

            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdAccionXCamp"] = IdAccionXCamp;
            ViewData["IdTipoDeAccion"] = IdTipoDeAccion;
            ViewData["IdTipoActividad"] = IdTipoActividad;
            ViewData["IdUnidadEjecutora"] = IdUnidadEjecutora;
            ViewData["IdCampania"] = IdCampania;

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                var model = db.ActividadXAccions;
                item.Fk_IdAccionXCampania = IdAccionXCamp;

                item.Fk_IdTipoDeRecurso = 1;  // Valor HardCoded por que ya no se usa en la actividad X Accion

                var IDUnidadDeMedida = (from tipoActividads in db.TipoActividads
                                        where tipoActividads.Pk_IdTipoActividad == item.Fk_IdTipoActividad
                                        select tipoActividads.FK_IdUnidadDeMedida);

                item.Fk_IdUnidadDeMedida = IDUnidadDeMedida.First();

                if (ModelState.IsValid)
                {
                    decimal metaAnual = (item.Meta_Ene ?? 0) + (item.Meta_Feb ?? 0) + (item.Meta_Mar ?? 0) + (item.Meta_Abr ?? 0)
                                    + (item.Meta_May ?? 0) + (item.Meta_Jun ?? 0) + (item.Meta_Jul ?? 0) + (item.Meta_Ago ?? 0)
                                    + (item.Meta_Sep ?? 0) + (item.Meta_Oct ?? 0) + (item.Meta_Nov ?? 0) + (item.Meta_Dic ?? 0);

                    if (metaAnual <= 0) ViewData["EditError"] = "La meta anual debe ser diferente de cero";
                    else
                    {
                        try
                        {
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
            }
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                var model = db.ActividadXAccion_Repro;
                var _IdAccionXCamp_Repro = db.AccionXCampania_Repro.Where(it => it.PK_IdAccionXCampania == IdAccionXCamp).Select(it => it.PK_IdAccionXCampania).First();
                itemR.Fk_IdAccionXCampania = Convert.ToInt32(_IdAccionXCamp_Repro);

                itemR.Fk_IdTipoDeRecurso = 1;  // Valor HardCoded por que ya no se usa en la actividad X Accion

                var IDUnidadDeMedida = (from tipoActividads in db.TipoActividads
                                        where tipoActividads.Pk_IdTipoActividad == itemR.Fk_IdTipoActividad
                                        select tipoActividads.FK_IdUnidadDeMedida);

                itemR.Fk_IdUnidadDeMedida = IDUnidadDeMedida.First();

                if (ModelState.IsValid)
                {
                    decimal metaAnual = (itemR.Meta_Ene ?? 0) + (itemR.Meta_Feb ?? 0) + (itemR.Meta_Mar ?? 0) + (itemR.Meta_Abr ?? 0)
                                    + (itemR.Meta_May ?? 0) + (itemR.Meta_Jun ?? 0) + (itemR.Meta_Jul ?? 0) + (itemR.Meta_Ago ?? 0)
                                    + (itemR.Meta_Sep ?? 0) + (itemR.Meta_Oct ?? 0) + (itemR.Meta_Nov ?? 0) + (itemR.Meta_Dic ?? 0);

                    if (metaAnual <= 0) ViewData["EditError"] = "La meta anual debe ser diferente de cero";
                    else
                    {
                        try
                        {
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
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = itemR;

                return PartialView("_ActividadesXAccionGridViewPartial", Senasica.GetActividadesByAccion(_IdAccionXCamp_Repro, IdCampania, StatusActual));
            }

            return PartialView("_ActividadesXAccionGridViewPartial", Senasica.GetActividadesByAccion(IdAccionXCamp, IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadesXAccionGridViewPartialUpdate(ActividadXAccion item, ActividadXAccion_Repro itemR, int IdAccionXCamp, int? IdTipoDeAccion, int? IdTipoActividad, int? IdUnidadEjecutora, int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña

            //var _query = db.ActividadXAccions.Where(it => it.Fk_IdAccionXCampania == IdAccionXCamp).Select(it => it.AccionXCampania.Campania.Pk_IdCampania);
            //int IdCampania = _query.Count() == 0 ? -1 : _query.First();

            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdAccionXCamp"] = IdAccionXCamp;
            ViewData["IdTipoDeAccion"] = IdTipoDeAccion;
            ViewData["IdTipoActividad"] = IdTipoActividad;
            ViewData["IdUnidadEjecutora"] = IdUnidadEjecutora;
            ViewData["IdCampania"] = IdCampania;

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                var IDUnidadDeMedida = (from tipoActividads in db.TipoActividads
                                        where tipoActividads.Pk_IdTipoActividad == item.Fk_IdTipoActividad
                                        select tipoActividads.FK_IdUnidadDeMedida);

                item.Fk_IdUnidadDeMedida = IDUnidadDeMedida.First();

                var model = db.ActividadXAccions;
                if (ModelState.IsValid)
                {
                    decimal metaAnual = (item.Meta_Ene ?? 0) + (item.Meta_Feb ?? 0) + (item.Meta_Mar ?? 0) + (item.Meta_Abr ?? 0)
                                    + (item.Meta_May ?? 0) + (item.Meta_Jun ?? 0) + (item.Meta_Jul ?? 0) + (item.Meta_Ago ?? 0)
                                    + (item.Meta_Sep ?? 0) + (item.Meta_Oct ?? 0) + (item.Meta_Nov ?? 0) + (item.Meta_Dic ?? 0);

                    if (metaAnual <= 0) ViewData["EditError"] = "La meta anual debe ser diferente de cero";
                    else
                    {
                        try
                        {
                            var modelItem = model.FirstOrDefault(it => it.Pk_IdActividadXAccion == item.Pk_IdActividadXAccion);
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
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = item;
            }
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                var IDUnidadDeMedida = (from tipoActividads in db.TipoActividads
                                        where tipoActividads.Pk_IdTipoActividad == itemR.Fk_IdTipoActividad
                                        select tipoActividads.FK_IdUnidadDeMedida);

                itemR.Fk_IdUnidadDeMedida = IDUnidadDeMedida.First();

                var model = db.ActividadXAccion_Repro;
                if (ModelState.IsValid)
                {
                    decimal metaAnual = (itemR.Meta_Ene ?? 0) + (itemR.Meta_Feb ?? 0) + (itemR.Meta_Mar ?? 0) + (itemR.Meta_Abr ?? 0)
                                    + (itemR.Meta_May ?? 0) + (itemR.Meta_Jun ?? 0) + (itemR.Meta_Jul ?? 0) + (itemR.Meta_Ago ?? 0)
                                    + (itemR.Meta_Sep ?? 0) + (itemR.Meta_Oct ?? 0) + (itemR.Meta_Nov ?? 0) + (itemR.Meta_Dic ?? 0);

                    if (metaAnual <= 0) ViewData["EditError"] = "La meta anual debe ser diferente de cero";
                    else
                    {
                        try
                        {
                            var modelItem = model.FirstOrDefault(it => it.Pk_IdActividadXAccion == itemR.Pk_IdActividadXAccion);
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
                }
                else
                    ViewData["EditError"] = "Por favor, corrija los errores marcados. ";

                ViewData["dataItem"] = itemR;

                var _IdAccionXCamp_Repro = db.AccionXCampania_Repro.Where(it => it.PK_IdAccionXCampania == IdAccionXCamp).Select(it => it.FK_IdAccionXCampania_Orig).First();
                return PartialView("_ActividadesXAccionGridViewPartial", Senasica.GetActividadesByAccion(_IdAccionXCamp_Repro, IdCampania, StatusActual));
            }

            return PartialView("_ActividadesXAccionGridViewPartial", Senasica.GetActividadesByAccion(IdAccionXCamp, IdCampania, StatusActual));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActividadesXAccionGridViewPartialDelete(int Pk_IdActividadXAccion, int? IdAccionXCamp, int? IdTipoDeAccion, int? IdTipoActividad, int? IdUnidadEjecutora, int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña

            //var _query = db.ActividadXAccions.Where(item => item.Fk_IdAccionXCampania == IdAccionXCamp).Select(item => item.AccionXCampania.Campania.Pk_IdCampania);
            //int IdCampania = _query.Count() == 0 ? -1 : _query.First();

            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdAccionXCamp"] = IdAccionXCamp;
            ViewData["IdTipoDeAccion"] = IdTipoDeAccion;
            ViewData["IdTipoActividad"] = IdTipoActividad;
            ViewData["IdUnidadEjecutora"] = IdUnidadEjecutora;
            ViewData["IdCampania"] = IdCampania;

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                var model = db.ActividadXAccions;
                if (Pk_IdActividadXAccion >= 0)
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.Pk_IdActividadXAccion == Pk_IdActividadXAccion);
                        if (item != null)
                        {
                            ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                            db.SP_DELETE_ActividadXAccion(Pk_IdActividadXAccion, User.Identity.GetUserId(), Error);
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
                var model = db.ActividadXAccion_Repro;
                if (Pk_IdActividadXAccion >= 0)
                {
                    try
                    {
                        // si encuentra avances registrados, no permite eliminar el registro
                        var _m = from a in db.Actividads
                                 join axa in db.ActividadXAccions on a.Fk_IdActividadXAccion equals axa.Pk_IdActividadXAccion
                                 join axa_r in db.ActividadXAccion_Repro on axa.Pk_IdActividadXAccion equals axa_r.Pk_IdActividadXAccion
                                 where axa_r.Pk_IdActividadXAccion == Pk_IdActividadXAccion
                                 select (a.Pk_IdActividad);

                        int? Count_ActAsignada = Convert.ToInt32(_m.Count());

                        bool Act = Count_ActAsignada == 0 ? true : false;
                        if (Act == false)
                        {
                            return JavaScript("¡Error al Eliminar!  \n \nLa Actividad está Asignada en el Catálogo de Asignación de Actividades");
                        }
                        //
                        var item = model.FirstOrDefault(it => it.Pk_IdActividadXAccion == Pk_IdActividadXAccion);
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
                var _IdAccionXCamp_Repro = db.AccionXCampania_Repro.Where(it => it.PK_IdAccionXCampania == IdAccionXCamp).Select(it => it.FK_IdAccionXCampania_Orig).First();

                return PartialView("_ActividadesXAccionGridViewPartial", Senasica.GetActividadesByAccion(_IdAccionXCamp_Repro, IdCampania, StatusActual));
            }

            return PartialView("_ActividadesXAccionGridViewPartial", Senasica.GetActividadesByAccion(IdAccionXCamp, IdCampania, StatusActual));
        }
        #endregion

        #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
        private void Permisos(int IdActividadXAccion)
        {
            string StatusActual;
            int? IdCampania = db.ActividadXAccions.Where(i => i.Pk_IdActividadXAccion == IdActividadXAccion).Select(i => i.AccionXCampania.Fk_IdCampania__UE).First();
                   
            StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                ViewData["Status_Lectura"] = false;
                ViewData["Status_Escritura"] = false;
            }
            else
            {
                ViewData["Status_Lectura"] = true;
                ViewData["Status_Escritura"] = true;
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
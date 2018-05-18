using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;
using DXMVCWebApplication1.ViewModels;
using DXMVCWebApplication1.Negocio;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class ProgramaAnualAdqUController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index()
        {
            Session["CurrentController"] = "ProgramaAnualAdqU";
            return View();
        }

        #region ProgramaAnualGridView
        [ValidateInput(false)]
        public ActionResult ProgramaAnualGridViewPartial()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? _Fk_IdUnidadResponsable = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string _rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            if (_rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                    || _rolusuario == RolesUsuarios.IE_ANIMAL
                    || _rolusuario == RolesUsuarios.IE_INOCUIDAD
                    || _rolusuario == RolesUsuarios.IE_MOVILIZACION
                    || _rolusuario == RolesUsuarios.IE_VEGETAL
                    || _rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                    || _rolusuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                    || _rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                    || _rolusuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                    || _rolusuario == RolesUsuarios.PUESTO_GERENTE
                    || _rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA)
            {
                return PartialView("_ProgramaAnualGridViewPartial", Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
            else
            {
                return PartialView("_ProgramaAnualGridViewPartial2", Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramaAnualGridViewPartialAddNew(ProgramaAnualAdqVM itemVM)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? _Fk_IdUnidadResponsable = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string _rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ProgramaAnualAdq item = null;

            if (_Fk_IdUnidadEjecutora != null && _Fk_IdUnidadEjecutora != 0)
            {
                var model = db.ProgramaAnualAdqs;

                item = new ProgramaAnualAdq
                {
                    Fk_IdAnio__SIS = IdAnio,
                    Fk_IdUnidadEjecutora__UE = _Fk_IdUnidadEjecutora,
                    Fk_IdBienOServ__SIS = itemVM.Fk_IdBienOServ__SIS,
                    Fk_IdUnidadDeMedida__SIS = itemVM.Fk_IdUnidadDeMedida__SIS,
                    Justificacion = itemVM.Justificacion,
                    CT_User = User.Identity.GetUserId(),
                    CT_Date = DateTime.Now,
                    Cant_Ene = itemVM.Cant_Ene,
                    Cant_Feb = itemVM.Cant_Feb,
                    Cant_Mar = itemVM.Cant_Mar,
                    Cant_Abr = itemVM.Cant_Abr,
                    Cant_May = itemVM.Cant_May,
                    Cant_Jun = itemVM.Cant_Jun,
                    Cant_Jul = itemVM.Cant_Jul,
                    Cant_Ago = itemVM.Cant_Ago,
                    Cant_Sep = itemVM.Cant_Sep,
                    Cant_Oct = itemVM.Cant_Oct,
                    Cant_Nov = itemVM.Cant_Nov,
                    Cant_Dic = itemVM.Cant_Dic,
                    imp_Ene = itemVM.imp_Ene,
                    imp_Feb = itemVM.imp_Feb,
                    imp_Mar = itemVM.imp_Mar,
                    imp_Abr = itemVM.imp_Abr,
                    imp_May = itemVM.imp_May,
                    imp_Jun = itemVM.imp_Jun,
                    imp_Jul = itemVM.imp_Jul,
                    imp_Ago = itemVM.imp_Ago,
                    imp_Sep = itemVM.imp_Sep,
                    imp_Oct = itemVM.imp_Oct,
                    imp_Nov = itemVM.imp_Nov,
                    imp_Dic = itemVM.imp_Dic,
                    esGastoU = true
                };

                switch (itemVM.TipoPresupuesto)
                {
                    case 1: item.RecFed = itemVM.PresupuestoTotal; break; //Federal
                    case 2: item.RecEst = itemVM.PresupuestoTotal; break; //Estatal
                    case 3: item.RecProp = itemVM.PresupuestoTotal; break; //Propios
                }

                if (ModelState.IsValid)
                {
                    if (!validaMontos(itemVM)) ViewData["EditError"] = "Las cantidades o importes anuales deben de ser mayores a cero";
                    else
                    {
                        try
                        {
                            #region Validación del 2 % de los Gastos Genéricos
                            //Realiza Validación del 2 % para los Gastos Genéricos, en el catálogo de GFO.
                            decimal? porcentajeMaximo = db.BienOServs.Where(it => it.Pk_IdBienOServ == item.Fk_IdBienOServ__SIS).Select(it => it.porcentajeMaximo).First();
                            ValidaMontos_GFO Valida_GFO = new ValidaMontos_GFO(0, _Fk_IdUnidadEjecutora, IdAnio, item.esGastoU, item.RecFed, item.RecEst, porcentajeMaximo);

                            if (Valida_GFO.EsGastoGenericoValido() == "NoValido" && porcentajeMaximo != null)
                            {
                                return JavaScript("¡Permiso Denegado!  \n \nRebasó el 2% de los Gastos Genéricos");
                            }
                            #endregion

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
                    ViewData["EditError"] = "Por favor, corrija los errores marcados.";
            }
            else
                ViewData["EditError"] = "Este Usuario no puede registrar nuevas necesidades.";

            ViewData["dataItem"] = item;

            if (_rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                    || _rolusuario == RolesUsuarios.IE_ANIMAL
                    || _rolusuario == RolesUsuarios.IE_INOCUIDAD
                    || _rolusuario == RolesUsuarios.IE_MOVILIZACION
                    || _rolusuario == RolesUsuarios.IE_VEGETAL
                    || _rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                    || _rolusuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                    || _rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                    || _rolusuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                    || _rolusuario == RolesUsuarios.PUESTO_GERENTE
                    || _rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA)
            {
                return PartialView("_ProgramaAnualGridViewPartial", Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
            else
            {
                return PartialView("_ProgramaAnualGridViewPartial2", Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramaAnualGridViewPartialUpdate(ProgramaAnualAdqVM itemVM)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? _Fk_IdUnidadResponsable = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string _rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            var model = db.ProgramaAnualAdqs;
            ProgramaAnualAdq item = null;

            if (ModelState.IsValid)
            {
                if (!validaMontos(itemVM)) ViewData["EditError"] = "Las cantidades o importes anuales deben de ser mayores a cero";
                else
                {
                    try
                    {
                        item = model.FirstOrDefault(it => it.Pk_IdProgramaAnualAdq == itemVM.Pk_IdProgramaAnualAdq);
                        if (item != null)
                        {
                            item.Fk_IdAnio__SIS = IdAnio;
                            item.Fk_IdBienOServ__SIS = itemVM.Fk_IdBienOServ__SIS;
                            item.Fk_IdUnidadDeMedida__SIS = itemVM.Fk_IdUnidadDeMedida__SIS;
                            item.Justificacion = itemVM.Justificacion;
                            item.CT_User = User.Identity.GetUserId();
                            item.CT_Date = DateTime.Now;
                            item.Cant_Ene = itemVM.Cant_Ene;
                            item.Cant_Feb = itemVM.Cant_Feb;
                            item.Cant_Mar = itemVM.Cant_Mar;
                            item.Cant_Abr = itemVM.Cant_Abr;
                            item.Cant_May = itemVM.Cant_May;
                            item.Cant_Jun = itemVM.Cant_Jun;
                            item.Cant_Jul = itemVM.Cant_Jul;
                            item.Cant_Ago = itemVM.Cant_Ago;
                            item.Cant_Sep = itemVM.Cant_Sep;
                            item.Cant_Oct = itemVM.Cant_Oct;
                            item.Cant_Nov = itemVM.Cant_Nov;
                            item.Cant_Dic = itemVM.Cant_Dic;
                            item.imp_Ene = itemVM.imp_Ene;
                            item.imp_Feb = itemVM.imp_Feb;
                            item.imp_Mar = itemVM.imp_Mar;
                            item.imp_Abr = itemVM.imp_Abr;
                            item.imp_May = itemVM.imp_May;
                            item.imp_Jun = itemVM.imp_Jun;
                            item.imp_Jul = itemVM.imp_Jul;
                            item.imp_Ago = itemVM.imp_Ago;
                            item.imp_Sep = itemVM.imp_Sep;
                            item.imp_Oct = itemVM.imp_Oct;
                            item.imp_Nov = itemVM.imp_Nov;
                            item.imp_Dic = itemVM.imp_Dic;

                            item.RecFed = null;
                            item.RecEst = null;
                            item.RecProp = null;

                            switch (itemVM.TipoPresupuesto)
                            {
                                case 1: item.RecFed = itemVM.PresupuestoTotal; break; //Federal
                                case 2: item.RecEst = itemVM.PresupuestoTotal; break; //Estatal
                                case 3: item.RecProp = itemVM.PresupuestoTotal; break; //Propios
                            }

                            #region Validación del 2 % de los Gastos Genéricos
                            //Realiza Validación del 2 % para los Gastos Genéricos, en el catálogo de GFO.
                            decimal? porcentajeMaximo = db.BienOServs.Where(it => it.Pk_IdBienOServ == item.Fk_IdBienOServ__SIS).Select(it => it.porcentajeMaximo).First();
                            ValidaMontos_GFO Valida_GFO = new ValidaMontos_GFO(item.Pk_IdProgramaAnualAdq, _Fk_IdUnidadEjecutora, IdAnio, item.esGastoU, item.RecFed, item.RecEst, porcentajeMaximo);

                            if (Valida_GFO.EsGastoGenericoValido() == "NoValido" && porcentajeMaximo != null)
                            {
                                return JavaScript("¡Permiso Denegado!  \n \nRebasó el 2% de los Gastos Genéricos");
                            }
                            #endregion

                            this.UpdateModel(item);
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
                ViewData["EditError"] = "Por favor, corrija los errores marcados.";

            ViewData["dataItem"] = item;

            if (_rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                    || _rolusuario == RolesUsuarios.IE_ANIMAL
                    || _rolusuario == RolesUsuarios.IE_INOCUIDAD
                    || _rolusuario == RolesUsuarios.IE_MOVILIZACION
                    || _rolusuario == RolesUsuarios.IE_VEGETAL
                    || _rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                    || _rolusuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                    || _rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                    || _rolusuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                    || _rolusuario == RolesUsuarios.PUESTO_GERENTE
                    || _rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA)
            {
                return PartialView("_ProgramaAnualGridViewPartial", Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
            else
            {
                return PartialView("_ProgramaAnualGridViewPartial2", Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramaAnualGridViewPartialDelete(System.Int32 Pk_IdProgramaAnualAdq)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? _Fk_IdUnidadResponsable = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string _rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            var model = db.ProgramaAnualAdqs;
            if (Pk_IdProgramaAnualAdq >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Pk_IdProgramaAnualAdq == Pk_IdProgramaAnualAdq);
                    if (item != null)
                    {
                        ObjectParameter Error = new ObjectParameter("Error", typeof(string));

                        db.SP_DELETE_ProgramaAnualAdq(Pk_IdProgramaAnualAdq, User.Identity.GetUserId(), Error);
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            if (_rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                    || _rolusuario == RolesUsuarios.IE_ANIMAL
                    || _rolusuario == RolesUsuarios.IE_INOCUIDAD
                    || _rolusuario == RolesUsuarios.IE_MOVILIZACION
                    || _rolusuario == RolesUsuarios.IE_VEGETAL
                    || _rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                    || _rolusuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                    || _rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                    || _rolusuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                    || _rolusuario == RolesUsuarios.PUESTO_GERENTE
                    || _rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA)
            {
                return PartialView("_ProgramaAnualGridViewPartial", Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
            else
            {
                return PartialView("_ProgramaAnualGridViewPartial2", Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
        }
        #endregion

        #region Programas Gastos Transversales
        [ValidateInput(false)]
        public ActionResult ProgramasGastosTransversalesGridViewPartial(int? IdProgramaAnualAdq)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            ViewData["IdProgramaAnualAdq"] = IdProgramaAnualAdq;

            return PartialView("_ProgramasGastosTransversalesGridViewPartial", Senasica.GetProgramasGastosTransversales(IdProgramaAnualAdq));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramasGastosTransversalesAddNew(ProgramasGastosTransversale item, int IdProgramaAnualAdq)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            var Campania = db.Campanias.Where(i => i.Fk_IdProyectoPresupuesto == item.Fk_IdProyectoPresupuesto).Select(i => i.Pk_IdCampania);
            int IdCampania = Campania.Count() == 0 ? 0 : Convert.ToInt32(Campania.First());
            string StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));

            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos_Campania = permisosFlujoCampania.ObtienePermisos();
            bool Escritura = permisos_Campania["Escritura"];
            ViewData["Lectura_Campania"] = permisos_Campania["Lectura"];
            ViewData["Escritura_Campania"] = permisos_Campania["Escritura"];
            #endregion

            ViewData["IdProgramaAnualAdq"] = IdProgramaAnualAdq;
            var model = db.ProgramasGastosTransversales;

            if (ModelState.IsValid)
            {
                if (IdCampania == 0)
                {   
                    //Si la campaña no se ha creado, no permite registrar
                    return JavaScript("¡Permiso Denegado!"
                                        + " \nEs necesario crear la Campaña, para el Proyecto que intenta registrar");
                }
                else
                {
                    if (Escritura == false)
                    {
                        // Dependiendo del Estatus de la campaña, se permite registrar o no el origen de los gastos
                        return JavaScript("¡Permiso Denegado!"
                                            + " \nLa Campaña se encuentra con el Estatus: ''" + StatusActual + "''");
                    }
                    else
                    {
                        try
                        {
                            item.Fk_IdProgramaAnualAdq = IdProgramaAnualAdq;
                            if (item.Monto == 0)
                            {
                                return JavaScript("El Monto debe ser diferente de 0");
                            }

                            #region 1. ValidaMontos_DetalleGFO
                            //1. Realiza Validación, si el tipo de recurso que estoy agregando, no está asignado a la campaña, lo rechaza.                    
                            ValidaMontos_DetalleGFO Valida_GFO = new ValidaMontos_DetalleGFO(0, item.Fk_IdProgramaAnualAdq, item.Fk_IdProyectoPresupuesto, item.Monto);

                            if (Valida_GFO.EsGastoGenericoValido() == "NoValido")
                            {
                                return JavaScript("¡Permiso Denegado!"
                                                    + " \n \nPosibles Causas:"
                                                    + " \n \n1. La Campaña, no tiene asignado este Tipo de Recurso"
                                                    + " \n \n2. Rebasó el Recurso Asignado");
                            }
                            #endregion

                            #region 2. ValidaMontos_Autorizado_Solicitado_GFO
                            //2. Valída que el Presupuesto Total Ingresado en los "Detalle de origen del Recurso",
                            // sea menor o igual al presupuesto Autorizado del BienoServ
                            int? Fk_IdUnidadEjecutora__UE = db.ProgramaAnualAdqs.Where(it => it.Pk_IdProgramaAnualAdq == item.Fk_IdProgramaAnualAdq).Select(it => it.Fk_IdUnidadEjecutora__UE).First();
                            int Fk_IdBienOServ__SIS = db.ProgramaAnualAdqs.Where(it => it.Pk_IdProgramaAnualAdq == item.Fk_IdProgramaAnualAdq).Select(it => it.Fk_IdBienOServ__SIS).First();
                            bool? esGastoU = db.ProgramaAnualAdqs.Where(it => it.Pk_IdProgramaAnualAdq == item.Fk_IdProgramaAnualAdq).Select(it => it.esGastoU).First();
                            ValidaMontos_Autorizado_Solicitado_GFO Valida_GFOs = new ValidaMontos_Autorizado_Solicitado_GFO(0, item.Fk_IdProgramaAnualAdq, Fk_IdUnidadEjecutora__UE, Fk_IdBienOServ__SIS, esGastoU, item.Monto);

                            if (Valida_GFOs.EsGastoValido() == "NoValido")
                            {
                                return JavaScript("¡Permiso Denegado!"
                                                    + " \n \nEl Presupuesto de Origen de Recurso, es mayor al Presupuesto Autorizado que tiene el Gasto Fijo de Operación");
                            }
                            #endregion

                            model.Add(item);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            ViewData["EditError"] = e.Message;
                        }
                    }
                }
            }
            else
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return PartialView("_ProgramasGastosTransversalesGridViewPartial", Senasica.GetProgramasGastosTransversales(IdProgramaAnualAdq));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramasGastosTransversalesUpdate(ProgramasGastosTransversale item, int IdProgramaAnualAdq)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            int IdCampania = db.Campanias.Where(i => i.Fk_IdProyectoPresupuesto == item.Fk_IdProyectoPresupuesto).Select(i => i.Pk_IdCampania).First();
            string StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));

            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos_Campania = permisosFlujoCampania.ObtienePermisos();
            bool Escritura = permisos_Campania["Escritura"];
            ViewData["Lectura_Campania"] = permisos_Campania["Lectura"];
            ViewData["Escritura_Campania"] = permisos_Campania["Escritura"];
            #endregion

            ViewData["IdProgramaAnualAdq"] = IdProgramaAnualAdq;
            var model = db.ProgramasGastosTransversales;

            if (ModelState.IsValid)
            {
                if (Escritura == false)
                {
                    return JavaScript("¡Permiso Denegado!"
                                        + " \nLa Campaña se encuentra con el Estatus: ''" + StatusActual + "''");
                }
                else
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.Pk_IdProgramasGastosTransversales == item.Pk_IdProgramasGastosTransversales);
                        if (modelItem != null)
                        {
                            if (item.Monto == 0)
                            {
                                return JavaScript("El Monto debe ser diferente de 0");
                            }

                            #region 1. ValidaMontos_DetalleGFO
                            //1. Realiza Validación, si el tipo de recurso que estoy agregando, no está asignado a la campaña, lo rechaza.                    
                            ValidaMontos_DetalleGFO Valida_GFO = new ValidaMontos_DetalleGFO(item.Pk_IdProgramasGastosTransversales, modelItem.Fk_IdProgramaAnualAdq, item.Fk_IdProyectoPresupuesto, item.Monto);

                            if (Valida_GFO.EsGastoGenericoValido() == "NoValido")
                            {
                                return JavaScript("¡Permiso Denegado!"
                                                    + " \n \nPosibles Causas:"
                                                    + " \n \n1. La Campaña, no tiene asignado este Tipo de Recurso"
                                                    + " \n \n2. Rebasó el Recurso Asignado");
                            }
                            #endregion

                            #region 2. ValidaMontos_Autorizado_Solicitado_GFO
                            //2. Valída que el Presupuesto Total Ingresado en los "Detalle de origen del Recurso",
                            // sea menor o igual al presupuesto Autorizado del BienoServ
                            int? Fk_IdUnidadEjecutora__UE = db.ProgramaAnualAdqs.Where(it => it.Pk_IdProgramaAnualAdq == modelItem.Fk_IdProgramaAnualAdq).Select(it => it.Fk_IdUnidadEjecutora__UE).First();
                            int Fk_IdBienOServ__SIS = db.ProgramaAnualAdqs.Where(it => it.Pk_IdProgramaAnualAdq == modelItem.Fk_IdProgramaAnualAdq).Select(it => it.Fk_IdBienOServ__SIS).First();
                            bool? esGastoU = db.ProgramaAnualAdqs.Where(it => it.Pk_IdProgramaAnualAdq == modelItem.Fk_IdProgramaAnualAdq).Select(it => it.esGastoU).First();
                            ValidaMontos_Autorizado_Solicitado_GFO Valida_GFOs = new ValidaMontos_Autorizado_Solicitado_GFO(item.Pk_IdProgramasGastosTransversales, modelItem.Fk_IdProgramaAnualAdq, Fk_IdUnidadEjecutora__UE, Fk_IdBienOServ__SIS, esGastoU, item.Monto);

                            if (Valida_GFOs.EsGastoValido() == "NoValido")
                            {
                                return JavaScript("¡Permiso Denegado!"
                                                    + " \n \nEl Presupuesto de Origen de Recurso, es mayor al Presupuesto Autorizado que tiene el Gasto Fijo de Operación");
                            }
                            #endregion

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
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            return PartialView("_ProgramasGastosTransversalesGridViewPartial", Senasica.GetProgramasGastosTransversales(IdProgramaAnualAdq));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProgramasGastosTransversalesDelete(int Pk_IdProgramasGastosTransversales, int IdProgramaAnualAdq)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_PROGRAMA_ANUAL_ADQUISICIONES);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            int Fk_IdProyectoPresupuesto = db.ProgramasGastosTransversales.Where(i => i.Pk_IdProgramasGastosTransversales == Pk_IdProgramasGastosTransversales).Select(i => i.Fk_IdProyectoPresupuesto).First();
            int IdCampania = db.Campanias.Where(i => i.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(i => i.Pk_IdCampania).First();
            string StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));

            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos_Campania = permisosFlujoCampania.ObtienePermisos();
            bool Escritura = permisos_Campania["Escritura"];
            ViewData["Lectura_Campania"] = permisos_Campania["Lectura"];
            ViewData["Escritura_Campania"] = permisos_Campania["Escritura"];
            #endregion

            ViewData["IdProgramaAnualAdq"] = IdProgramaAnualAdq;

            var model = db.ProgramasGastosTransversales;
            if (Pk_IdProgramasGastosTransversales >= 0)
            {
                if (Escritura == false)
                {
                    return JavaScript("¡Permiso Denegado!"
                                        + " \nLa Campaña se encuentra con el Estatus: ''" + StatusActual + "''");
                }
                else
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.Pk_IdProgramasGastosTransversales == Pk_IdProgramasGastosTransversales);
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

            return PartialView("_ProgramasGastosTransversalesGridViewPartial", Senasica.GetProgramasGastosTransversales(IdProgramaAnualAdq));
        }
        #endregion

        public ActionResult ComboBoxPartialBienOServicio()
        {
            return PartialView("_ComboBoxPartialBienOServicio");
        }
        public ActionResult ComboBoxPartialUnidadMedida()
        {
            int Fk_IdBienOServ = (Request.Params["Fk_IdBienOServ__SIS"] != null) ? int.Parse(Request.Params["Fk_IdBienOServ__SIS"]) : -1;
            return PartialView("_ComboBoxPartialUnidadMedidaTransversal", Senasica.GetBienesOServByUnidadMedida(Fk_IdBienOServ));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Devuelve true si el registro es válido
        /// 
        /// Un registro es válido si la cantidad anula y el importe anual son mayores que cero.
        /// En otro caso es inválida.
        /// </summary>
        /// <param name="itemVM"></param>
        /// <returns></returns>
        private bool validaMontos(ProgramaAnualAdqVM itemVM)
        {
            decimal CantidadAnual = (itemVM.Cant_Ene ?? 0) + (itemVM.Cant_Feb ?? 0) + (itemVM.Cant_Mar ?? 0) + (itemVM.Cant_Abr ?? 0) +
                        (itemVM.Cant_May ?? 0) + (itemVM.Cant_Jun ?? 0) + (itemVM.Cant_Jul ?? 0) + (itemVM.Cant_Ago ?? 0) +
                        (itemVM.Cant_Sep ?? 0) + (itemVM.Cant_Oct ?? 0) + (itemVM.Cant_Nov ?? 0) + (itemVM.Cant_Dic ?? 0);

            decimal importeAnual = (itemVM.imp_Ene ?? 0) + (itemVM.imp_Feb ?? 0) + (itemVM.imp_Mar ?? 0) + (itemVM.imp_Abr ?? 0) +
                                    (itemVM.imp_May ?? 0) + (itemVM.imp_Jun ?? 0) + (itemVM.imp_Jul ?? 0) + (itemVM.imp_Ago ?? 0) +
                                    (itemVM.imp_Sep ?? 0) + (itemVM.imp_Oct ?? 0) + (itemVM.imp_Nov ?? 0) + (itemVM.imp_Dic ?? 0);

            if (CantidadAnual <= 0 || importeAnual <= 0) return false;
            else return true;
        }

        public JsonResult GetBienOServDetails(int IdBienOServ)
        {
            var _query = db.BienOServs.Where(item => item.Pk_IdBienOServ == IdBienOServ)
                                        .Select(item => new
                                        {
                                            item.Pk_IdBienOServ,
                                            item.Nombre,
                                            item.porcentajeMaximo,
                                            item.precioMaximo
                                        }).First();

            return Json(_query, JsonRequestBehavior.AllowGet);
        }
    }
}
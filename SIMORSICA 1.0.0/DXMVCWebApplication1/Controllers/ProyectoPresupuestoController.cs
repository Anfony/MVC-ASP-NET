using System;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using System.Collections;
using System.Linq;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;

namespace DXMVCWebApplication1.Controllers
{
    /// <summary>
    /// 
    /// El siguiente controlador gestiona la distribución del recurso a los proyectos.
    /// 
    /// El recurso está dividido en tres partes:
    ///     - Federal
    ///     - Estatal
    ///     - Productores
    ///     
    /// De acuerdo a su Rol, los usuarios podrán realizar modificaciones sobre algun tipo de recurso
    /// 
    ///     - Superusuario: Solo para supervisar, no podrá hacer alguna modificación. Visualizará todos los programas
    ///     - UR: Puede agregar programas y hacer las modificaciones necesarias al recurso federal de acuerdo al concepto de apoyo que pertenece.
    ///     - SUP Regional: Solo visualiza la información de los programas de acuerdo a los estados que le corresponde.
    ///     - SUP Estatal: Podrá hacer modificaciones a los recursos estatales de acuerdo a los proyectos que existen en su estado
    ///     - IE: Podrá gestionar el recurso de productores de acuerdo a los proyectos que tienen asignado
    ///     
    /// </summary>
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                       + "," + RolesUsuarios.UR_INOCUIDAD
                       + "," + RolesUsuarios.UR_MOVILIZACION
                       + "," + RolesUsuarios.UR_ANIMAL
                       + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                       + "," + RolesUsuarios.UR_VEGETAL
                       + "," + RolesUsuarios.SUP_REGIONAL
                       + "," + RolesUsuarios.SUP_ESTATAL
                       + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                       + "," + RolesUsuarios.IE_ANIMAL
                       + "," + RolesUsuarios.IE_INOCUIDAD
                       + "," + RolesUsuarios.IE_MOVILIZACION
                       + "," + RolesUsuarios.IE_VEGETAL
                       + "," + RolesUsuarios.UR_UPV
                       + "," + RolesUsuarios.IE_PROGRAMAS_U
                       + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                       + "," + RolesUsuarios.SUP_NACIONAL
      )]
    public class ProyectoPresupuestoController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index(string Estado, string Error)
        {
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            ViewData["Error"] = Error == null ? "" : Error;

            if (rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA ||
                rolUsuario == RolesUsuarios.UR_ANIMAL ||
                rolUsuario == RolesUsuarios.UR_INOCUIDAD ||
                rolUsuario == RolesUsuarios.UR_MOVILIZACION ||
                rolUsuario == RolesUsuarios.UR_VEGETAL)
            {
                var presupuestoYaOtorgado = PresupuestoYaOtorgado();
                var _montoOtorgado = MontoOtorgado(Estado);
                var _montoEjercido = MontoEjercido(Estado);

                ViewData["Estado"] = Estado;
                ViewData["presupuestoYaOtorgado"] = presupuestoYaOtorgado;
                ViewData["MontoOtorgado"] = _montoOtorgado;
                ViewData["MontoRestante"] = _montoOtorgado - _montoEjercido;
                ViewData["esRecursoCerrado"] = recursosCerrados(Estado);
            }

            return View();
        }

        /// <summary>
        /// Devuelve una vista parcial dependiendo del rol del usuario:
        /// 
        ///     - SUPERUSUARIO: Puede ver todos los proyectos
        ///     - SUP_REGIONAL: Puede ver los proyectos correspondientes a su región
        ///     - AUR: Puede ver los proyectos correspondientes a su Unidad Responsable
        ///     - SUP_ESTATAL: Puede ver los proyectos correspondientes a su Estado
        ///     - AIE: Puede ver los proyectos correspondientes a su comité
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ProyectoPresupuestoGridViewPartial(string Estado)
        {
            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            string vistaParcial = "";
            IEnumerable data = null;

            if (rolUsuario == RolesUsuarios.SUPERUSUARIO)
            {
                vistaParcial = "_GridViewPartial";
                data = Senasica.GetProyectosPresupuestalesByRol();
            }

            if (rolUsuario == RolesUsuarios.UR_INOCUIDAD
                || rolUsuario == RolesUsuarios.UR_MOVILIZACION
                || rolUsuario == RolesUsuarios.UR_ANIMAL
                || rolUsuario == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.UR_VEGETAL
                || rolUsuario == RolesUsuarios.UR_UPV)
            {
                vistaParcial = "_GridViewPartialUR";

                ViewData["IdUnidadResponsable"] = FuncionesAuxiliares.getIdUnidadResponsable(FuncionesAuxiliares.GetCurrent_RolUsuario());
                ViewData["IdAnioPresupuestal"] = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
                ViewData["esRecursoCerrado"] = recursosCerrados(Estado);
                ViewData["Estado"] = Estado;

                data = Estado == null ? Senasica.GetProyectosPresupuestalesByRol() : Senasica.GetProyectosPresupuestalByEstado(Estado);
            }

            if (rolUsuario == RolesUsuarios.SUP_ESTATAL)
            {
                vistaParcial = "_GridViewPartialEST";
                data = Senasica.GetProyectosPresupuestalesByRol();
            }

            if (rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U)
            {
                vistaParcial = "_GridViewPartialAIE";
                data = Senasica.GetProyectosPresupuestalesByRol();
            }

            if (rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL
                || rolUsuario == RolesUsuarios.SUP_REGIONAL
                || rolUsuario == RolesUsuarios.SUP_NACIONAL)
            {
                vistaParcial = "_GridViewPartial_soloLectura";
                data = Senasica.GetProyectosPresupuestalesByRol();
            }
            return PartialView(vistaParcial, data);
        }

        /// <summary>
        /// EL único rol que está previsto para agregar elementos es el de Unidad Responsable,
        /// los demás sólo podrán editar sobre los elementos que ya existen y solamente podrán editar sus
        /// tipos de presupuesto (estatal para SUPERVISOR ESTATAL y propios para Instancia Ejecutora):
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Estado_param"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult ProyectoPresupuestoGridViewPartialAddNew(ProyectoPresupuesto item, string Estado_param)
        {
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            var model = db.ProyectoPresupuestoes;
            string Error = "";

            if (ModelState.IsValid)
            {
                try
                {
                    item.Fk_IdAnio = db.Anios.Where(a => a.Pk_IdAnio == IdAnioPresupuestal).Select(a => a.Pk_IdAnio).First();
                    item.CierraPresupuesto = false;
                    item.CT_User = User.Identity.GetUserId();
                    item.CT_Date = DateTime.Now;
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Error = e.Message;
                }
            }
            else Error = "Error al ingresar los valores";

            ViewData["dataItem"] = item;

            return RedirectToAction("Index", new { Estado = Estado_param, Error });
        }

        /// <summary>
        /// La actualización a los registros lo podrán hacer dependiendo del rol
        /// 
        ///     - Superusuario: Solo actualiza la Unidad responsable o el comité
        ///     - AUR: Actualiza Proyecto y cualquier mes en tanto a los montos federales
        ///     - SUP_EST: Actualiza cualquier mes en tanto a los montos estatales
        ///     - AIE: Actualiza cualquier mes en tanto a los montos Productores
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Estado_param"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult ProyectoPresupuestoGridViewPartialUpdate(ProyectoPresupuesto item, string Estado_param)
        {
            var model = db.ProyectoPresupuestoes;
            string Error = "";

            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Pk_IdProyectoPresupuesto == item.Pk_IdProyectoPresupuesto);
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
                    Error = e.Message;
                }
            }
            else Error = "Error al ingresar los valores";

            ViewData["dataItem"] = item;

            return RedirectToAction("Index", new { Estado = Estado_param, Error });
        }

        /// <summary>
        /// El único rol que puede eliminar es solo el de AUR
        /// </summary>
        /// <param name="Pk_IdProyectoPresupuesto"></param>
        /// <param name="Estado_param"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult ProyectoPresupuestoGridViewPartialDelete(int Pk_IdProyectoPresupuesto, string Estado_param)
        {
            var model = db.ProyectoPresupuestoes;
            string Error = "";

            if (Pk_IdProyectoPresupuesto >= 0)
            {
                try
                {

                    // Valida si se utiliza el Proyecto, no permite eliminar el registro
                    var _Campania = db.Campanias.Where(i => i.Fk_IdProyectoPresupuesto == Pk_IdProyectoPresupuesto).Select(i => i.Fk_IdProyectoPresupuesto);
                    var _Tesofe = db.TesofeDetalles.Where(i => i.Fk_IdProyectoPresupuesto == Pk_IdProyectoPresupuesto).Select(i => i.Fk_IdProyectoPresupuesto);
                    var _PAA = db.ProgramasGastosTransversales.Where(i => i.Fk_IdProyectoPresupuesto == Pk_IdProyectoPresupuesto).Select(i => i.Fk_IdProyectoPresupuesto);

                    int Campania = _Campania.Count() == null ? 0 : Convert.ToInt32(_Campania.Count());
                    int Tesofe = _Tesofe.Count() == null ? 0 : Convert.ToInt32(_Tesofe.Count());
                    int PAA = _PAA.Count() == null ? 0 : Convert.ToInt32(_PAA.Count());

                    bool seUtiliza = Campania == 0 && Tesofe == 0 && PAA == 0 ? true : false;
                    if (seUtiliza == false)
                    {
                        return JavaScript("¡Error al Eliminar!  \n \nEl Proyecto, ya está siendo utilizado");
                    }
                    else
                    {
                        var item = model.FirstOrDefault(it => it.Pk_IdProyectoPresupuesto == Pk_IdProyectoPresupuesto);
                        if (item != null)
                        {
                            ObjectParameter Error2 = new ObjectParameter("Error", typeof(string));

                            db.SP_DELETE_ProyectoPresupuesto(Pk_IdProyectoPresupuesto, User.Identity.GetUserId(), Error2);
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Error = e.Message;
                }
            }
            return RedirectToAction("Index", new { Estado = Estado_param, Error });
        }

        #region Cierra Distribución del Recurso Federal
        /// <summary>
        /// Realiza el cierre del presupuesto que se ha otorgado a los distintos programas
        /// 
        /// Este movimiento es exclusivamente de la Unidad Responsable
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CierraDistribucionRecurso(string Estado)
        {
            if (Estado == "" || Estado == null) return RedirectToAction("Index");

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            string Error = "";

            int IdUnidadResponsable = FuncionesAuxiliares.getIdUnidadResponsable(rolUsuario);
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            try
            {
                var _data = from unidadResponsableXSubcomponente in db.UnidadResponsableXSubComponentePres
                            join SC in db.SubComponentes on unidadResponsableXSubcomponente.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                            join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                            join PP in db.ProyectoPresupuestoes on PA.Pk_IdProyectoAutorizado equals PP.Fk_IdProyectoAutorizado
                            where unidadResponsableXSubcomponente.Fk_IdUnidadResponsable == IdUnidadResponsable
                            && PP.Fk_IdAnio == IdAnioPresupuestal
                            && PP.Estado.Nombre == Estado
                            orderby PP.Estado.Nombre ascending, SC.Nombre ascending
                            select PP;

                foreach (var item in _data)
                {               
                    item.CierraPresupuesto = true;
                    //ct_user y ct_date
                    item.CT_User = User.Identity.GetUserId();
                    item.CT_Date = DateTime.Now;
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Error = e.Message;
            }

            return RedirectToAction("Index", new { Estado, Error });
        }
        #endregion

        #region Fun: MostrarTodo
        /// <summary>
        /// Muestra todo los registros de acuerdo al usuario logeado
        /// </summary>
        /// <returns></returns>
        public ActionResult MostrarTodo()
        {
            return RedirectToAction("Index");
        }
        #endregion

        #region Fun: FiltraDistribucionRecurso
        /// <summary>
        /// Filtra la información del DataGrid de acuerdo a un Estado para saber el presupuesto
        /// ya otorgado
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FiltraDistribucionRecurso(string Estado)
        {
            return RedirectToAction("Index", new { Estado });
        }
        #endregion

        #region Fun: PresupuestoYaOtorgado
        /// <summary>
        /// Verifica si la Unidad Responsable ya cerró la distribución del dinero a los estados
        /// 
        /// Se necesita validar esto para poder hacer la distribución del dinero a los distintos proyectos a su cargo
        /// </summary>
        /// <returns></returns>
        private bool PresupuestoYaOtorgado()
        {
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            if (IdAnio == 2) return true;  //para el ejercicio presupuestal 2016 todavía se sigue modificando

            int IdUnidadResponsable = FuncionesAuxiliares.getIdUnidadResponsable(FuncionesAuxiliares.GetCurrent_RolUsuario());

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.PresupuestoXEstadoes.Where(p => p.PorcentajeXDireccionGeneral.Anio.Pk_IdAnio == IdAnio
                                                        && p.PorcentajeXDireccionGeneral.FK_IdUnidadResponsable == IdUnidadResponsable
                                                        && p.PresupuestoCerrado == true);

            return _query.Count() != 0 ? true : false;
        }
        #endregion

        #region Fun: MontoOtorgado
        /// <summary>
        /// Obtiene el presupuesto que se la ha otorgado por Estado de acuerdo a la Unidad Responsable
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        private decimal MontoOtorgado(string Estado)
        {
            int IdUnidadResponsable = FuncionesAuxiliares.getIdUnidadResponsable(FuncionesAuxiliares.GetCurrent_RolUsuario());
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var _query = db.PresupuestoXEstadoes.Where(p => p.PorcentajeXDireccionGeneral.Anio.Pk_IdAnio == IdAnioPresupuestal
                                                                    && p.PorcentajeXDireccionGeneral.FK_IdUnidadResponsable == IdUnidadResponsable
                                                                    && p.Estado.Nombre == Estado).Select(p => p.CantidadSolicitada);

            return _query.Count() == 0 ? 0 : Convert.ToDecimal(_query.First());
        }
        #endregion

        #region Fun: MontoEjercido
        /// <summary>
        /// Devuelve el monto que se ha ejercido la Unidad Responsable al Estado en cuestión
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        private decimal MontoEjercido(string Estado)
        {
            int IdUnidadResponsable = FuncionesAuxiliares.getIdUnidadResponsable(FuncionesAuxiliares.GetCurrent_RolUsuario());
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var _query = db.PresupuestoXEstadoes.Where(p => p.PorcentajeXDireccionGeneral.Anio.Pk_IdAnio == IdAnioPresupuestal
                                                        && p.PorcentajeXDireccionGeneral.FK_IdUnidadResponsable == IdUnidadResponsable
                                                        && p.Estado.Nombre == Estado).Select(p => p.PresupuestoAsignadoXEstado_DG);

            return _query.Count() == 0 ? 0 : Convert.ToDecimal(_query.First());
        }
        #endregion

        #region Fun: recursosCerrados
        /// <summary>
        /// Verifica si los recursos ingresados están ya cerrados (ya no se pueden editar)
        /// </summary>
        /// <returns></returns>
        private bool recursosCerrados(string Estado)
        {
            if (Estado == null) return true;

            int IdUnidadResponsable = FuncionesAuxiliares.getIdUnidadResponsable(FuncionesAuxiliares.GetCurrent_RolUsuario());
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var _data = from unidadResponsableXSubcomponente in db.UnidadResponsableXSubComponentePres
                        join SC in db.SubComponentes on unidadResponsableXSubcomponente.Fk_IdSubComponente equals SC.Pk_IdSubComponente
                        join PA in db.ProyectoAutorizadoes on SC.Pk_IdSubComponente equals PA.Fk_IdSubComponente__SIS
                        join PP in db.ProyectoPresupuestoes on PA.Pk_IdProyectoAutorizado equals PP.Fk_IdProyectoAutorizado
                        where unidadResponsableXSubcomponente.Fk_IdUnidadResponsable == IdUnidadResponsable
                        && PP.Fk_IdAnio == IdAnioPresupuestal
                        && PP.Estado.Nombre == Estado
                        && PP.CierraPresupuesto == true
                        orderby PP.Estado.Nombre ascending, SC.Nombre ascending
                        select PP;

            return _data.Count() != 0 ? true : false;
        }
        #endregion

        #region Combo: ProyectoPresupuesto
        public ActionResult _ComboBoxProyectoAutorizado()
        {
            var IdSubComponente = Convert.ToInt32(Request.Params["IdSubComponente"]);

            return PartialView(Senasica.GetProyectosAutorizadosByUR(IdSubComponente));
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)]
    public class PresupuestoXDireccionGeneralController : Controller
    {
        private static int IdDGSA = FuncionesAuxiliares.getIdDGSA();
        private static int IdDGSV = FuncionesAuxiliares.getIdDGSV();
        private static int IdDGIAAP = FuncionesAuxiliares.getIdDGIAAP();
        private static int IdDGIF = FuncionesAuxiliares.getIdDGIF();

        public ActionResult Index()
        {
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            if(IdAnioPres == 3)
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();

                var _query = db.PEFXEstadoes.Where(p => p.FK_IdAnio == IdAnioPres && p.PresupuestoCerrado == true);

                ViewData["esRecursoCerrado"] = recursosCerrados();
                ViewData["recursosYaDistribuidos"] = recursosYaDistribuidos();
                ViewData["presupuestoYaOtorgado"] = _query.Count() == 0 ? false : true;

                LlenaViewData();

                return View();
            }
            else
            {
                return View("Index2");
            }
            
        }

        public ActionResult PresGridViewPartial()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var _data = db.PresXAURs.Where(item => item.Fk_IdAnio == IdAnioPres).ToList();

            return PartialView("_PresGridViewPartial", _data);
        }

        public ActionResult ResumenXEstado()
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.PRES_PRESUPUESTOXDIRECCIONGENERAL);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            LlenaViewData();

            return PartialView("_ResumenXEstado_DG", Senasica.GetPresupuestoXDFG());
        }

        private void LlenaViewData()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.PRES_PRESUPUESTOXDIRECCIONGENERAL);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var GastoInversionTotal = db.PEFXEstadoes.Where(p => p.FK_IdAnio == IdAnioPres).Select(p => p.Componentes);
            var DGSA = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnioPres && p.FK_IdUnidadResponsable == IdDGSA).Select(p => p.PresupuestoAsignado);
            var DGSV = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnioPres && p.FK_IdUnidadResponsable == IdDGSV).Select(p => p.PresupuestoAsignado);
            var DGIAAP = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnioPres && p.FK_IdUnidadResponsable == IdDGIAAP).Select(p => p.PresupuestoAsignado);
            var DGIF = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnioPres && p.FK_IdUnidadResponsable == IdDGIF).Select(p => p.PresupuestoAsignado);

            ViewData["GastoInversionTotal"] = GastoInversionTotal.Count() == 0 ? 0 : GastoInversionTotal.Sum();
            ViewData["DGSA"] = DGSA.Count() == 0 ? 0 : DGSA.First();
            ViewData["DGSV"] = DGSV.Count() == 0 ? 0 : DGSV.First();
            ViewData["DGIAAP"] = DGIAAP.Count() == 0 ? 0 : DGIAAP.First();
            ViewData["DGIF"] = DGIF.Count() == 0 ? 0 : DGIF.First();
        }

        [HttpPost]
        public ActionResult GuardaPresupuestoAsignado(string DGSA, string DGSV, string DGIAAP, string DGIF)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.PRES_PRESUPUESTOXDIRECCIONGENERAL);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            double presupuestoDGSA = DGSA == "" ? 0 : Convert.ToDouble(DGSA);
            double presupuestoDGSV = DGSV == "" ? 0 : Convert.ToDouble(DGSV);
            double presupuestoDGIAAP = DGIAAP == "" ? 0 : Convert.ToDouble(DGIAAP);
            double presupuestoDGIF = DGIF == "" ? 0 : Convert.ToDouble(DGIF);

            if (!esValidaDistribucion(presupuestoDGSA, presupuestoDGSV, presupuestoDGIAAP, presupuestoDGIF)) return PartialView("_errorDistribucion");

            try
            {
                if(recursosYaDistribuidos())
                {
                    var item1 = db.PorcentajeXDireccionGenerals.FirstOrDefault(elemento => elemento.FK_IdAnio == IdAnioPres && elemento.FK_IdUnidadResponsable == IdDGSV);
                    var item2 = db.PorcentajeXDireccionGenerals.FirstOrDefault(elemento => elemento.FK_IdAnio == IdAnioPres && elemento.FK_IdUnidadResponsable == IdDGSA);
                    var item3 = db.PorcentajeXDireccionGenerals.FirstOrDefault(elemento => elemento.FK_IdAnio == IdAnioPres && elemento.FK_IdUnidadResponsable == IdDGIAAP);
                    var item4 = db.PorcentajeXDireccionGenerals.FirstOrDefault(elemento => elemento.FK_IdAnio == IdAnioPres && elemento.FK_IdUnidadResponsable == IdDGIF);

                    if (item1 != null)  item1.PresupuestoAsignado = presupuestoDGSV;
                    if (item2 != null)  item2.PresupuestoAsignado = presupuestoDGSA;
                    if (item3 != null)  item3.PresupuestoAsignado = presupuestoDGIAAP;
                    if (item4 != null)  item4.PresupuestoAsignado = presupuestoDGIF;
                }
                else
                {
                    PorcentajeXDireccionGeneral[] _elementos =
                    {
                        new PorcentajeXDireccionGeneral { FK_IdAnio = IdAnioPres, FK_IdUnidadResponsable = IdDGSV, PresupuestoAsignado = presupuestoDGSV },
                        new PorcentajeXDireccionGeneral { FK_IdAnio = IdAnioPres, FK_IdUnidadResponsable = IdDGSA, PresupuestoAsignado = presupuestoDGSA },
                        new PorcentajeXDireccionGeneral { FK_IdAnio = IdAnioPres, FK_IdUnidadResponsable = IdDGIAAP, PresupuestoAsignado = presupuestoDGIAAP },
                        new PorcentajeXDireccionGeneral { FK_IdAnio = IdAnioPres, FK_IdUnidadResponsable = IdDGIF, PresupuestoAsignado = presupuestoDGIF }
                    };

                    foreach (var item in _elementos) db.PorcentajeXDireccionGenerals.Add(item);
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Realiza el cirre de la distribución del recurso.
        /// 
        /// El recurso ya no podrá ser modificado
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CierraDistribucionRecurso()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.PRES_PRESUPUESTOXDIRECCIONGENERAL);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            try
            {
                int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

                var _elementos = db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnioPres);

                foreach (var item in _elementos) item.PresupuestoCerrado = true;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Se debe de verificar que se haya distribuido todo el recurso en todas las Direcciones Generales
        /// </summary>
        /// <param name="DGSA">Dirección General de Salud Animal</param>
        /// <param name="DGSV">Dirección General de Sanidad Vegetal</param>
        /// <param name="DGIAAP">Dirección General de Inocuidad Agroalimentaria Acuícola y Pesquera</param>
        /// <param name="DGIF">Dirección General de Inspección Fitozoosanitaria</param>
        /// <returns></returns>
        private bool esValidaDistribucion(double? DGSA, double? DGSV, double? DGIAAP, double? DGIF)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            double? GastoInversionTotal = (double) db.PEFXEstadoes.Where(p => p.FK_IdAnio == IdAnioPres).Select(p => p.Componentes).Sum();
            double? TotalDGs = DGSA + DGSV + DGIAAP + DGIF;

            return GastoInversionTotal == TotalDGs ? true : false;
        }

        /// <summary>
        /// Verifica si los recursos ingresados están ya cerrados (que ya no se pueden editar)
        /// </summary>
        /// <returns></returns>
        private bool recursosCerrados()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnioPres && p.PresupuestoCerrado == true).Count() != 0 ? true : false;
        }

        /// <summary>
        /// Verifica que ya se haya hecho la distribución de los recursos en las distintas Direcciones Generales
        /// </summary>
        /// <returns></returns>
        private bool recursosYaDistribuidos()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            return db.PorcentajeXDireccionGenerals.Where(p => p.FK_IdAnio == IdAnioPres).Count() != 0 ? true : false;
        }
    }
}
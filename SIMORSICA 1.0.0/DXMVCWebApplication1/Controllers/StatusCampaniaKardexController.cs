using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Negocio;
using System;
using System.Data.Entity.Core.Objects;
using System.Web.Mvc;
using System.Linq;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize]
    public class StatusCampaniaKardexController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        public ActionResult Index() { return View(); }

        [ValidateInput(false)]
        public ActionResult BtnValidaCampania(int? IdCampania)
        {
            ViewData["IdCampania"] = IdCampania;

            return PartialView("_BtnVerificaCampania");
        }

        [ValidateInput(false)]
        public ActionResult StatusCampaniaGridViewPartial(int? IdCampania)
        {
            ViewData["IdCampania"] = IdCampania;

            return PartialView("_StatusCampaniaGridViewPartial", Senasica.GetStatusCampaniaKardexesByCampania(IdCampania));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult StatusCampaniaGridViewPartialAddNew(StatusCampaniaKardex item, StatusCampaniaKardex_Repro itemR, int IdCampania)
        {
            ViewData["IdCampania"] = IdCampania;
            item.FK_IdCampania__UE = IdCampania;

            DB_SENASICA_Storeds storeds = new DB_SENASICA_Storeds();
            /*
             * Inicia
             * Ejecuta los SP correspondientes cuando la Campaña
             * "Autorizan la Reprogramación" o "Termina la Reprogramación"
            */
            int id_Satus = Convert.ToInt32(item.FK_IdStatusCampania__SIS);
            string _rolusuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            System.Data.SqlClient.SqlConnection _cnn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CubosOlapSIMOSICAConnection"].ConnectionString);

            string RolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if (RolUsuario == RolesUsuarios.IE_ANIMAL
                || RolUsuario == RolesUsuarios.IE_VEGETAL
                || RolUsuario == RolesUsuarios.IE_INOCUIDAD
                || RolUsuario == RolesUsuarios.IE_MOVILIZACION
                || RolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || RolUsuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                || RolUsuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || RolUsuario == RolesUsuarios.PUESTO_COOR_REGIONAL
                || RolUsuario == RolesUsuarios.PUESTO_GERENTE
                || RolUsuario == RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                || RolUsuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                || RolUsuario == RolesUsuarios.SUP_ESTATAL)
            {
                if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
                {
                    ValidaMontosCampania validaCampania = new ValidaMontosCampania(IdCampania);

                    if (!validaCampania.EsCampaniaValida())
                    {
                        //Presupuesto Otorgado a la campaña (de acuerdo al proyecto autorizado asignado)
                        ViewData["presFedAsignado"] = validaCampania.GetPresAsignadoFed();
                        ViewData["presEstAsignado"] = validaCampania.GetPresAsignadoEst();
                        ViewData["presProAsignado"] = validaCampania.GetPresAsignadoPro();

                        //Presupuesto Reportado en la campaña (en los detalles de la campaña)
                        ViewData["presFedReportado"] = validaCampania.GetPresReportadoFed();
                        ViewData["presEstReportado"] = validaCampania.GetPresReportadoEst();
                        ViewData["presProReportado"] = validaCampania.GetPresReportadoPro();

                        //Status de los recursos
                        ViewData["StatusRecFed"] = validaCampania.StatusRecFed();
                        ViewData["StatusRecEst"] = validaCampania.StatusRecEst();
                        ViewData["StatusRecPro"] = validaCampania.StatusRecPro();

                        ViewData["presFedGFO"] = validaCampania.GetPresGFOFed();
                        ViewData["presEstGFO"] = validaCampania.GetPresGFOEst();
                        ViewData["presProGFO"] = validaCampania.GetPresGFOPro();

                        return PartialView("_ErrorCambioStatus");
                    }
                }

                if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
                {
                    ValidaMontosCampania_Repro validaCampania_Repro = new ValidaMontosCampania_Repro(IdCampania);

                    if (!validaCampania_Repro.EsCampaniaValida())
                    {
                        //Presupuesto Otorgado a la campaña (de acuerdo al proyecto autorizado asignado)
                        ViewData["presFedAsignado"] = validaCampania_Repro.GetPresAsignadoFed();
                        ViewData["presEstAsignado"] = validaCampania_Repro.GetPresAsignadoEst();
                        ViewData["presProAsignado"] = validaCampania_Repro.GetPresAsignadoPro();

                        //Presupuesto Reportado en la campaña (en los detalles de la campaña)
                        ViewData["presFedReportado"] = validaCampania_Repro.GetPresReportadoFed();
                        ViewData["presEstReportado"] = validaCampania_Repro.GetPresReportadoEst();
                        ViewData["presProReportado"] = validaCampania_Repro.GetPresReportadoPro();

                        //Status de los recursos
                        ViewData["StatusRecFed"] = validaCampania_Repro.StatusRecFed();
                        ViewData["StatusRecEst"] = validaCampania_Repro.StatusRecEst();
                        ViewData["StatusRecPro"] = validaCampania_Repro.StatusRecPro();

                        ViewData["presFedGFO"] = validaCampania_Repro.GetPresGFOFed();
                        ViewData["presEstGFO"] = validaCampania_Repro.GetPresGFOEst();
                        ViewData["presProGFO"] = validaCampania_Repro.GetPresGFOPro();

                        return PartialView("_ErrorCambioStatus");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                /*
                 * * EJECUTA EL SP CUANDO TERMINA LA REPROGRAMACIÓN DE LA CAMPAÑA
                 */
                if (id_Satus == 23 || id_Satus == 35) //Modificaciones Validadas
                {
                    storeds.SP_CRUD_ProcesoReprogramacion(IdCampania);
                }
                else
                {
                    try
                    {
                        var model = db.StatusCampaniaKardexes;
                        item.Fecha = DateTime.Now;
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
                ViewData["EditError"] = "Por Favor Corrija los errores marcados.";

            if (ViewData["EditError"] != null) return PartialView("ErrorForm");


            /*
             * EJECUTA EL SP CUANDO INICIA LA REPROGRAMACIÓN DE LA CAMPAÑA
             */
            if (id_Satus == 24 || id_Satus == 36)//Se Autoriza hacer Modificaciones
            {
                ObjectParameter mensaje = new ObjectParameter("mensaje", typeof(string));
                storeds.SP_Clona_TablasProcesoDeReprogramacion(IdCampania, mensaje);
            }

            string currentController = Session["CurrentController"].ToString();

            return RedirectToAction("Index", currentController == null ? "Home" : currentController);
        }

        [HttpPost]
        public ActionResult ValidaCampania(int IdCampania)
        {
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));

            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                ValidaMontosCampania validaCampania = new ValidaMontosCampania(IdCampania);

                ViewData["esValidaCampania"] = validaCampania.EsCampaniaValida();

                //Presupuesto Otorgado a la campaña (de acuerdo al proyecto autorizado asignado)
                ViewData["presFedAsignado"] = validaCampania.GetPresAsignadoFed();
                ViewData["presEstAsignado"] = validaCampania.GetPresAsignadoEst();
                ViewData["presProAsignado"] = validaCampania.GetPresAsignadoPro();

                //Presupuesto Reportado en la campaña (en los detalles de la campaña)
                ViewData["presFedReportado"] = validaCampania.GetPresReportadoFed();
                ViewData["presEstReportado"] = validaCampania.GetPresReportadoEst();
                ViewData["presProReportado"] = validaCampania.GetPresReportadoPro();

                ViewData["presFedGFO"] = validaCampania.GetPresGFOFed();
                ViewData["presEstGFO"] = validaCampania.GetPresGFOEst();
                ViewData["presProGFO"] = validaCampania.GetPresGFOPro();

                //Status de los recursos
                ViewData["StatusRecFed"] = validaCampania.StatusRecFed();
                ViewData["StatusRecEst"] = validaCampania.StatusRecEst();
                ViewData["StatusRecPro"] = validaCampania.StatusRecPro();

            }
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                ValidaMontosCampania_Repro validaCampania_Repro = new ValidaMontosCampania_Repro(IdCampania);

                ViewData["esValidaCampania"] = validaCampania_Repro.EsCampaniaValida();

                //Presupuesto Otorgado a la campaña (de acuerdo al proyecto autorizado asignado)
                ViewData["presFedAsignado"] = validaCampania_Repro.GetPresAsignadoFed();
                ViewData["presEstAsignado"] = validaCampania_Repro.GetPresAsignadoEst();
                ViewData["presProAsignado"] = validaCampania_Repro.GetPresAsignadoPro();

                //Presupuesto Reportado en la campaña (en los detalles de la campaña)
                ViewData["presFedReportado"] = validaCampania_Repro.GetPresReportadoFed();
                ViewData["presEstReportado"] = validaCampania_Repro.GetPresReportadoEst();
                ViewData["presProReportado"] = validaCampania_Repro.GetPresReportadoPro();

                ViewData["presFedGFO"] = validaCampania_Repro.GetPresGFOFed();
                ViewData["presEstGFO"] = validaCampania_Repro.GetPresGFOEst();
                ViewData["presProGFO"] = validaCampania_Repro.GetPresGFOPro();

                //Status de los recursos
                ViewData["StatusRecFed"] = validaCampania_Repro.StatusRecFed();
                ViewData["StatusRecEst"] = validaCampania_Repro.StatusRecEst();
                ViewData["StatusRecPro"] = validaCampania_Repro.StatusRecPro();
            }

            return PartialView("_VerificaCampania");
        }

        public ActionResult EliminaStatus(int? IdStatusCampaniaKardex)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.StatusCampaniaKardexes;
            if (IdStatusCampaniaKardex >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.PK_IdStatusCampaniaKardex == IdStatusCampaniaKardex);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            string currentController = Session["CurrentController"].ToString();

            return RedirectToAction("Index", currentController == null ? "Home" : currentController);
        }
    }
}
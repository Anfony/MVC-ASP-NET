using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)]
    public class ReprogramacionPresFedController : Controller
    {
        // GET: ReprogramacionPresFed
        public ActionResult Index()
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            reproFed.SP_CleanMovRepRecursoFed();
            reproFed.SP_CleanCampaniaXEstado();

            return View();
        }

        [ValidateInput(false)]
        public ActionResult PresCampaniaGridViewPartial()
        {
            return PartialView("_PresCampaniaGridViewPartial", Senasica.GetPresupuestoCampanias());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult IniciaReprogramacion(EstadoVM itemEstado)
        {
            //si el usuario no elige alguna campaña
            if (itemEstado.IdEstado == 0) return RedirectToAction("Index");

            DB_SENASICAEntities db = new DB_SENASICAEntities();
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            ViewData["IdEstado"] = itemEstado.IdEstado;
            ViewData["NombreEstado"] = db.Estadoes.Where(item => item.Pk_IdEstado == itemEstado.IdEstado)
                                                    .Select(item => item.Nombre)
                                                    .First()
                                                    .ToUpper();

            if(reproFed.CampaniasXEstado.Count() == 0)
            {
                int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

                reproFed.SP_RespaldoCampaniasXEstado(itemEstado.IdEstado, IdAnioPres);
            }

            return View("IndexRepro");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RegresaPantalla(int IdEstado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            ViewData["IdEstado"] = IdEstado;
            ViewData["NombreEstado"] = db.Estadoes.Where(item => item.Pk_IdEstado == IdEstado)
                                                    .Select(item => item.Nombre)
                                                    .First()
                                                    .ToUpper();

            return View("IndexRepro");
        }

        [HttpPost]
        public ActionResult CancelaReprogramacion()
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            reproFed.SP_CleanCampaniaXEstado();
            reproFed.SP_CleanMovRepRecursoFed();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EjecutaReprogramacion()
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            var _listaCampanias = reproFed.MovRepRecursoFed.Where(item => item.esHistorico == false)
                                                            .OrderBy(item => item.FechaMovimiento);

            //Tendrá la lista de IdCampanias que se han mandado a reprogramación, para que no se vuelvan a madar
            List<int> _listaIdCampanias = new List<int>();

            foreach (var item in _listaCampanias)
            {
                //Agrega el status de "Se Autoriza hacer Modificaciones" y manda a reprogramación a la campaña
                //avisando por email al usuario
                if(item.Fk_IdCampaniaOrigen != null)
                {
                    if (!_listaIdCampanias.Contains(Convert.ToInt32(item.Fk_IdCampaniaOrigen)))
                    {
                        AgregaStatusParaModificaciones(Convert.ToInt32(item.Fk_IdCampaniaOrigen));
                        _listaIdCampanias.Add(Convert.ToInt32(item.Fk_IdCampaniaOrigen));
                    }

                    string mensajeRecursoOrigen = "Por reprogramación de recurso, a la campaña:\n\r" + item.NombreCampaniaOrigen
                            + "\n\r se le ha QUITADO " + item.MontoTransferido.ToString("C") + " por lo que deberá de reajustar sus gastos programados";

                    MandaEmailAvisoDeReprogramacion(Convert.ToInt32(item.Fk_IdCampaniaOrigen), mensajeRecursoOrigen);
                }

                if (!_listaIdCampanias.Contains(item.Fk_IdCampaniaDestino))
                {
                    AgregaStatusParaModificaciones(item.Fk_IdCampaniaDestino);
                    _listaIdCampanias.Add(item.Fk_IdCampaniaDestino);
                }

                string mensajeRecursoDestino = "Por reprogramación de recurso, a la campaña:\n\r" + item.NombreCampaniaDestino
                            + "\n\r se le ha AGREGADO " + item.MontoTransferido.ToString("C") + " por lo que deberá de reajustar sus gastos programados";

                MandaEmailAvisoDeReprogramacion(item.Fk_IdCampaniaDestino, mensajeRecursoDestino);
            }

            reproFed.SP_CleanCampaniaXEstado();
            reproFed.SP_EjecutaReproFed();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Agrega el Status "Se Autoriza hacer Modificaciones" para que los comités puedan 
        /// hacer las adecuaciones a sus campañas después de las reprogramación de su recurso
        /// </summary>
        /// <param name="IdCampania"></param>
        private void AgregaStatusParaModificaciones(int IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            DB_SENASICA_Storeds storeds = new DB_SENASICA_Storeds();
            int IdAnio = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            StatusCampaniaKardex item = new StatusCampaniaKardex()
            {
                FK_IdCampania__UE = IdCampania,
                Comentarios = ConstantesGlobales.REPRO_RECURSO_CAMPANIA,
                Fecha = DateTime.Now
            };

            var model = db.StatusCampaniaKardexes;
            item.FK_IdStatusCampania__SIS = IdAnio == 3 ? 24 : 36;
            item.Fecha = DateTime.Now;
            model.Add(item);

            if (item.FK_IdStatusCampania__SIS == 24 || item.FK_IdStatusCampania__SIS == 36)//Se Autoriza hacer Modificaciones
            {
                ObjectParameter mensaje = new ObjectParameter("mensaje", typeof(string));
                storeds.SP_Clona_TablasProcesoDeReprogramacion(IdCampania, mensaje);
            }

            db.SaveChanges();
            storeds.SaveChanges();
        }

        /// <summary>
        /// Manda un correo electrónico al comité avisando que se ha hecho una reprogramació de su campaña,
        /// ya sea que se le haya agregado o quitado recurso
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <param name="Mensaje"></param>
        private async void MandaEmailAvisoDeReprogramacion(int IdCampania, string Mensaje)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            // Obtenemos el correo del comité
            int? IdUnidadEjecutora = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                                .Select(item => item.ProyectoPresupuesto.Fk_IdUnidadEjecutora)
                                                .First();

            var esProduccion = System.Configuration.ConfigurationManager.AppSettings["esProduccion"];
            string _email;

            if (esProduccion != null && esProduccion != "" && Convert.ToBoolean(esProduccion))
            {
                _email = db.UnidadEjecutoras.Where(item => item.Pk_IdUnidadEjecutora == IdUnidadEjecutora)
                                            .Select(item => item.CorreoElectronico)
                                            .First();
            }
            else
            {
                _email = ConstantesGlobales.EMAIL_SOPORTE;
            }

            Correo _nuevoCorreo = new Correo(_email, ConstantesGlobales.ASUNTO_CORREO_REPRO_CAMPANIA, Mensaje);
            await _nuevoCorreo.Enviar();
        }

        /// <summary>
        /// Obtiene los tipos de dineros de una campaña seleccionada
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <returns></returns>
        public JsonResult GetPresupustoByCampania(int IdCampania)
        {
            DB_ReprogramacionRecFed db = new DB_ReprogramacionRecFed();

            var _query = db.CampaniasXEstado.Where(item => item.Pk_IdCampania == IdCampania)
                                                    .Select(item => new {
                                                        item.RecFed,
                                                        item.RecSol_Fed,
                                                        item.Rec_Gastado,
                                                        item.SaldoDisponible
                                                    }).First();

            return Json(_query, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Devuelve los movimientos realizados por el usuario en este momento de la operación
        /// </summary>
        /// <param name="IdEstado"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult MovimientosReprogramacionViewPartial()
        {
            return PartialView("_MovimientosRealizadosGridViewPartial", Senasica.GetMotivoMovimiento(GetIdentificador()));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MuestraHistoricoByEstado(int IdEstado)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            ViewData["IdEstado"] = IdEstado;
            ViewData["NombreEstado"] = db.Estadoes.Where(item => item.Pk_IdEstado == IdEstado)
                                        .Select(item => item.Nombre)
                                        .First()
                                        .ToUpper();

            return View("IndexHistorico");
        }

        [ValidateInput(false)]
        public ActionResult HistoricoMovimientosReprogramacionFed(int IdEstado)
        {
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_HistMovimientosRep", Senasica.GetHistMotivoMovimiento(IdEstado));
        }

        /// <summary>
        /// Registra un movimiento del usuario
        /// </summary>
        /// <param name="IdCampaniaOrigen"></param>
        /// <param name="IdCampaniaDestino"></param>
        /// <param name="IdMotivoIncremento"></param>
        /// <param name="Monto"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public JsonResult AgregaMovimiento(int IdCampaniaOrigen, int IdCampaniaDestino, int IdMotivoIncremento, decimal Monto)
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            reproFed.SP_AgregaElementoReproFed(GetIdentificador(), IdCampaniaOrigen, IdCampaniaDestino, IdMotivoIncremento, Monto);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// El identificador será un número consecutivo que indicará un momento en la reprogramación.
        /// 
        /// Cada vez que haya una reprogramación, se generará un Identificador el cual acompañará a los registros que se
        /// crearán en la reprogramación
        /// </summary>
        /// <returns></returns>
        private int GetIdentificador()
        {
            DB_ReprogramacionRecFed reproFed = new DB_ReprogramacionRecFed();

            return reproFed.MovRepRecursoFed
                            .Where(item => item.esHistorico == true)
                            .Count() == 0 ? 1 : reproFed.MovRepRecursoFed.Where(item => item.esHistorico == true)
                                                                            .Max(item => item.Identificador) + 1;
        }

        /// <summary>
        /// Obtiene la información con la que se va a alimentar los combos
        /// </summary>
        /// <returns></returns>
        public ActionResult ComboBoxCampaniaOrigen()
        {
            return PartialView("_ComboBoxCampaniaOrigen", Senasica.GetCampaniasReproRecFed());
        }

        public ActionResult ComboBoxCampaniaDestino()
        {
            return PartialView("_ComboBoxCampaniaDestino", Senasica.GetCampaniasReproRecFed());
        }
    }
}
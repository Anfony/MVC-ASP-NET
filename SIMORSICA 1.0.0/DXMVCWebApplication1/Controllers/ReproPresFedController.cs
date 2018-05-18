using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)]
    public class ReproPresFedController : Controller
    {
        // GET: PresupuestosCampania
        public ActionResult Index()
        {
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
            if (itemEstado.IdEstado == 0) return RedirectToAction("Index");

            DB_SENASICAEntities db = new DB_SENASICAEntities();

            ViewData["IdEstado"] = itemEstado.IdEstado;
            ViewData["NombreEstado"] = db.Estadoes.Where(item => item.Pk_IdEstado == itemEstado.IdEstado)
                                                    .Select(item => item.Nombre)
                                                    .First()
                                                    .ToUpper();

            return View("IndexRepro");
        }

        [ValidateInput(false)]
        public ActionResult ReproPresEstadoViewPartial(int IdEstado)
        {
            ViewData["IdEstado"] = IdEstado;

            return PartialView("_PresEstadoDetallesGridViewPartial", Senasica.GetPresupuestoCampanias(IdEstado));
        }

        public JsonResult GetPresupustoByCampania(int IdCampania)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.VW_PresupuestoCampania.Where(item => item.Pk_IdCampania == IdCampania)
                                                    .Select(item => new {
                                                        item.RecFed,
                                                        item.RecSol_Fed,
                                                        item.Rec_Gastado,
                                                        item.SaldoDisponible
                                                    }).First();

            return Json(_query, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult ReprogramaRecurso(int IdCampaniaOrigen, int IdCampaniaDestino, decimal Monto)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            if(IdCampaniaOrigen != -1) ActualizaMontoAsignado(IdCampaniaOrigen, Monto * -1); //Resta el monto a la campaña de origen
            ActualizaMontoAsignado(IdCampaniaDestino, Monto); //Aumenta el monto a la campaña de destino

            //Agrega el Status de "Se Autoriza hacer Modificaciones" a las campañas para su modificaciones
            //después de la reprogramación de su recurso
            if (IdCampaniaOrigen != -1) AgregaStatusParaModificaciones(IdCampaniaOrigen);
            AgregaStatusParaModificaciones(IdCampaniaDestino);

            //Prepara los mensaje a enviar a los correos
            // Se manda el mensaje sólo si se ha elegido una campaña de origen

            if(IdCampaniaOrigen != -1)
            {
                string campaniaRecursoOrigen = db.VW_PresupuestoCampania.Where(item => item.Pk_IdCampania == IdCampaniaOrigen)
                                                        .Select(item => item.Proyecto)
                                                        .First();

                string mensajeRecursoOrigen = "Por reprogramación de recurso, a la campaña:\n\r" + campaniaRecursoOrigen
                                            + "\n\r se le ha QUITADO " + Monto.ToString("C") + " por lo que deberá de reajustar sus gastos programados";

                MandaEmailAvisoDeReprogramacion(IdCampaniaOrigen, mensajeRecursoOrigen);
            }


            string campaniaRecursoDestino = db.VW_PresupuestoCampania.Where(item => item.Pk_IdCampania == IdCampaniaDestino)
                                                        .Select(item => item.Proyecto)
                                                        .First();

            string mensajeRecursoDestino = "Por reprogramación de recurso, a la campaña:\n\r" + campaniaRecursoDestino
                                        + "\n\r se le ha AGREGADO " + Monto.ToString("C") + " por lo que deberá de reajustar sus gastos programados";

            MandaEmailAvisoDeReprogramacion(IdCampaniaDestino, mensajeRecursoDestino);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// En SIS.ProyectoPresupuesto actualiza el monto de la campaña
        /// </summary>
        /// <param name="IdCampania"></param>
        /// <param name="Monto"></param>
        private void ActualizaMontoAsignado(int IdCampania, decimal Monto)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            int? IdProyectoPresupuesto = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                                    .Select(item => item.Fk_IdProyectoPresupuesto)
                                                    .First();

            var model = db.ProyectoPresupuestoes;
            var modelItem = model.FirstOrDefault(item => item.Pk_IdProyectoPresupuesto == IdProyectoPresupuesto);

            if (modelItem != null)
            {
                modelItem.RF_Mar = modelItem.RF_Mar + Monto;
                UpdateModel(modelItem);
                db.SaveChanges();
            }
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

            StatusCampaniaKardex item = new StatusCampaniaKardex()
            {
                FK_IdStatusCampania__SIS = 24,
                FK_IdCampania__UE = IdCampania,
                Comentarios = ConstantesGlobales.REPRO_RECURSO_CAMPANIA,
                Fecha = DateTime.Now
            };
            
            var model = db.StatusCampaniaKardexes;
            item.Fecha = DateTime.Now;
            model.Add(item);
            db.SaveChanges();

            if (item.FK_IdStatusCampania__SIS == 24)//Se Autoriza hacer Modificaciones
            {
                ObjectParameter mensaje = new ObjectParameter("mensaje", typeof(string));
                storeds.SP_Clona_TablasProcesoDeReprogramacion(IdCampania, mensaje);
            }
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

            if(esProduccion != null && esProduccion != "" && Convert.ToBoolean(esProduccion))
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
    }
}
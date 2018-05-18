using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.ViewModels;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                      + "," + RolesUsuarios.SUP_NACIONAL
                      + "," + RolesUsuarios.SUP_REGIONAL
                      + "," + RolesUsuarios.SUP_ESTATAL
                      + "," + RolesUsuarios.REPRESENTANTE_ESTATAL
                      + "," + RolesUsuarios.UR_INOCUIDAD
                      + "," + RolesUsuarios.UR_MOVILIZACION
                      + "," + RolesUsuarios.UR_ANIMAL
                      + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                      + "," + RolesUsuarios.UR_VEGETAL
                      + "," + RolesUsuarios.IE_ANIMAL
                      + "," + RolesUsuarios.IE_VEGETAL
                      + "," + RolesUsuarios.IE_INOCUIDAD
                      + "," + RolesUsuarios.IE_MOVILIZACION
                      + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                      + "," + RolesUsuarios.IE_PROGRAMAS_U
                      + "," + RolesUsuarios.SUP_AUDITOR
     )]
    public class CierreMensualController : Controller
    {
        // GET: CierreMensual
        public ActionResult Index()
        {
            string vista = "";

            string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

            if (rolUsuario == RolesUsuarios.IE_VEGETAL
                || rolUsuario == RolesUsuarios.IE_ANIMAL
                || rolUsuario == RolesUsuarios.IE_INOCUIDAD
                || rolUsuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || rolUsuario == RolesUsuarios.IE_MOVILIZACION
                || rolUsuario == RolesUsuarios.IE_PROGRAMAS_U
                || rolUsuario == RolesUsuarios.SUP_ESTATAL)
            {
                vista = "Index";
            }
            else
            {
                vista = "IndexGeneral";
            }

            return View(vista);
        }

        /// <summary>
        /// Pantalla que será para mostrar solo los registros ya cerrados
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult CierreMensualGridViewPartialGeneral()
        {
            return PartialView("_CierreMensualSuperusuario", Senasica.GetCierreMensual());
        }

        /// <summary>
        /// Muestra los registros creados por la AIE
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult CierreMensualGridViewPartial()
        {
            return PartialView("_CierreMensualGridViewPartial", Senasica.GetCierreMensual());
        }

        /// <summary>
        /// El usuario crea de manera preliminar un cierreMensual de una campaña
        /// </summary>
        /// <returns></returns>
        //[HttpPost, ValidateInput(false)]
        public ActionResult CierraCampania(CierreMensualVM item)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.CierreMensuals;

            if (item.Fk_IdCampania != 0 && item.Fk_IdMes != 0 && !esCampaniaYaCerrada(item.Fk_IdCampania, item.Fk_IdMes))
            {
                CierreMensual cierreMensual = new CierreMensual()
                {
                    Fk_IdCampania = item.Fk_IdCampania,
                    Fk_IdMes = item.Fk_IdMes,
                    Comentarios = item.Comentarios,
                    esCerrado = false,
                    FechaCreacion = DateTime.Now
                };

                model.Add(cierreMensual);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Verifica si la campaña ya está cerrado para el mes seleccionado
        /// 
        /// Devuelve true si ya está cerrado, false si no está cerrado
        /// </summary>
        /// <returns></returns>
        private bool esCampaniaYaCerrada(int IdCampania, int IdMes)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.CierreMensuals.Where(item => item.Fk_IdCampania == IdCampania
                                                && item.Fk_IdMes == IdMes);

            return _query.Count() != 0;
        }

        /// <summary>
        /// Actualiza los comentarios de un registro
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ActionResult ActualizaComentarios(CierreMensualVM itemVM)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var model = db.CierreMensuals;
            var modelItem = model.FirstOrDefault(item => item.Pk_IdCierreMensual == itemVM.IdCierreUpdate);

            if (modelItem != null)
            {
                modelItem.Comentarios = itemVM.ComentariosUpdate;
                UpdateModel(modelItem);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Carga el reporte al servidor (inicio de cierre de la campaña)
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        /// <returns></returns>
        public ActionResult CargaReporteCierre(CierreMensualVM item)
        {
            Session["IdCierreMensual"] = item.IdCierre;

            UploadControlExtension.GetUploadedFiles("UploadControl", ValidationSettings, FileUploadComplete);

            return null;
        }

        /// <summary>
        /// Empieza la carga del archivo pdf firmado para el cierre de la campaña
        /// </summary>
        public DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".pdf" },
            MaxFileSize = 9000000
        };

        /// <summary>
        /// El usuario desea solicitar la apertura de una campaña
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult ReopenReporteCierre(int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            var _data = db.CierreMensuals.Where(item => item.Pk_IdCierreMensual == IdCierreMensual).First();

            if (!esSolicitudApertura(IdCierreMensual))
            {
                return View("_ReopenReport", _data);
            }
            else
            {
                return View("_CampaniaYaSolicitada", _data);
            }
        }

        /// <summary>
        /// Averigua si la campaña ya fue solicitada para su apertura
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        /// <returns></returns>
        private bool esSolicitudApertura(int IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            return db.CierreMensualSolicitudAperturas
                            .Where(item => item.Fk_IdCierreMensual == IdCierreMensual && item.estaAtendido == false)
                            .Count() != 0;
        }

        /// <summary>
        /// El usuario valida la solicitu de apertura de la campaña
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult ConfirmaSolicitudAperturaCampania(AperturaCampaniaVM item, int IdCierreMensual)
        {
            if (!esSolicitudApertura(IdCierreMensual))
            {
                DB_SENASICA_Storeds storeds = new DB_SENASICA_Storeds();

                storeds.SP_SolicitudAperturaCampania(IdCierreMensual, item.JustificacionApertura);
                storeds.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// El reporte se ha cargado en el servidor y se guarda el nombre de dicho reporte y se cierra la campaña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                DB_SENASICAEntities db = new DB_SENASICAEntities();
                DB_SENASICA_Storeds storeds = new DB_SENASICA_Storeds();

                int IdCierreMensual = Convert.ToInt32(Session["IdCierreMensual"]);

                string nombreArchivo = IdCierreMensual + "_CIERRE_MENSUAL_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoCierreMensual"].ToString();
                string rutaArchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutaArchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutaArchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.CierreMensuals;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdCierreMensual == IdCierreMensual);

                if (modelItem != null)
                {
                    modelItem.NombreRepCierre = nombreArchivo;
                    modelItem.esCerrado = true;
                    modelItem.FechaCierre = DateTime.Now;
                    modelItem.MotivosApertura = null;
                    modelItem.RespuestaApertura = null;
                    modelItem.Comentarios = modelItem.Comentarios == ConstantesGlobales.CAMPANIA_CERRADA_AUTOMATICAMENTE ? "" : modelItem.Comentarios;
                    UpdateModel(modelItem);

                    storeds.Cierra_RepActividad(modelItem.Fk_IdCampania, modelItem.Fk_IdMes, true);
                    storeds.Cierra_RepAdquisicion(modelItem.Fk_IdCampania, modelItem.Fk_IdMes, true);

                    storeds.SaveChanges();
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Empieza la descarga del reporte que el usuario subió al sistema
        /// </summary>
        /// <param name="IdCierreMensual"></param>
        /// <returns></returns>
        public ActionResult DescargaCierreMensual(int? IdCierreMensual)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            IQueryable<string> _query;
            string contentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoCierreMensual"].ToString();
            _query = db.CierreMensuals.Where(c => c.Pk_IdCierreMensual == IdCierreMensual).Select(c => c.NombreRepCierre);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (directorio == "" || nombreArchivo == "" || !System.IO.File.Exists(directorio + nombreArchivo)) return View("FileNotFound");

            return new FilePathResult(directorio + nombreArchivo, contentType);
        }

        public ActionResult _ComboCampania()
        {
            var IdMes = Convert.ToInt32(Request.Params["IdMes"]);

            return PartialView(Senasica.GetCampaniasFaltantesCerrarByMes(IdMes));
        }
    }
}
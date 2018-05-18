using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DevExpress.Web.Mvc;
using System.IO;
using System.Web.UI;

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
                      + "," + RolesUsuarios.UR_UPV
                      //+ "," + RolesUsuarios.IE_ANIMAL
                      //+ "," + RolesUsuarios.IE_VEGETAL
                      //+ "," + RolesUsuarios.IE_INOCUIDAD
                      //+ "," + RolesUsuarios.IE_MOVILIZACION
                      //+ "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                      //+ "," + RolesUsuarios.IE_PROGRAMAS_U
                      + "," + RolesUsuarios.SUP_AUDITOR
     )]
    public class ActaComisionController : Controller
    {
        // GET: ActaComision
        public ActionResult Index()
            {
                string vista = "";

                string rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();

                if (rolUsuario == RolesUsuarios.REPRESENTANTE_ESTATAL)
                {
                    vista = "Index";
                }
                else
                {
                    vista = "IndexGeneral";
                }

                return View(vista);
        }
        public ActionResult ActaComisionAddNew(ActaComision item)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdEstado = FuncionesAuxiliares.GetCurrent_IdEstado();
            int IdAniPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var model = db.ActaComisions;
            if (ModelState.IsValid)
            {
                if (IdAniPres < 5 && item.Fk_IdMes == null)
                {
                    return JavaScript("El Mes es campo requerido");
                }
                if (IdAniPres >= 5 && item.Fk_IdMesTrimestre == null)
                {
                    return JavaScript("El periodo es campo requerido");
                }
                else
                {
                    try
                    {
                        item.Fk_IdEstado = IdEstado;
                        item.Fk_IdAnio = IdAniPres;
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
                ViewData["EditError"] = "Por favor corrija los errores marcados.";

            ViewData["dataItem"] = item;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// View Representante Estatal
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ActaComisionGridViewPartial()
        {
            return PartialView("_ActaComisionGridViewPartial", Senasica.GetActaComision());
        }

        /// <summary>
        /// View General
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ActaComisionGridViewPartialGeneral()
        {
            return PartialView("_ActaComisionGridViewPartialGeneral", Senasica.GetActaComision());
        }

        /// <summary>
        /// Carga el reporte al servidor
        /// </summary>
        /// <param name="IdActaComision"></param>
        /// <returns></returns>
        public ActionResult CargaReporte(ActaComision item)
        {
            Session["IdActaComision"] = item.Pk_IdActaComision;

            UploadControlExtension.GetUploadedFiles("UploadControl", ValidationSettings, FileUploadComplete);

            return null;
        }

        /// <summary>
        /// Empieza la carga del archivo pdf
        /// </summary>
        public DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".pdf" },
            MaxFileSize = 9000000
        };
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

                int IdActaComision = Convert.ToInt32(Session["IdActaComision"]);

                string nombreArchivo = IdActaComision + "_ActaComisión_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioActaComision"].ToString();
                string rutaArchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutaArchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutaArchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.ActaComisions;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdActaComision == IdActaComision);

                if (modelItem != null)
                {
                    modelItem.Documento = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Empieza la descarga del reporte que el usuario subió al sistema
        /// </summary>
        /// <param name="IdActaComision"></param>
        /// <returns></returns>
        public ActionResult DescargaActaComision(int? IdActaComision)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            IQueryable<string> _query;
            string contentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioActaComision"].ToString();
            _query = db.ActaComisions.Where(c => c.Pk_IdActaComision == IdActaComision).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (directorio == "" || nombreArchivo == "" || !System.IO.File.Exists(directorio + nombreArchivo)) return View("FileNotFound");

            //return new FilePathResult(directorio + nombreArchivo, contentType);
            string rutadearchivo = directorio + nombreArchivo;
            return File(rutadearchivo, contentType, nombreArchivo);   
        }
    }
}
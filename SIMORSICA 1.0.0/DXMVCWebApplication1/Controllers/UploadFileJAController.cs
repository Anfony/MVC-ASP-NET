using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DXMVCWebApplication1.Controllers
{
    public class UploadFileJAController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /UploadFileJA/
        public ActionResult Index(int Pk_IdJuntaAclaracion)
        {
            Session["Pk_IdJuntaAclaracion"] = Pk_IdJuntaAclaracion != null ? Pk_IdJuntaAclaracion : 0;
            ViewData["Pk_IdJuntaAclaracion"] = Pk_IdJuntaAclaracion != null ? Pk_IdJuntaAclaracion : 0;

            IQueryable<string> _query;

            if (Pk_IdJuntaAclaracion == null) ViewData["existeArchivo"] = false;
            else
            {
                _query = db.JuntaAclaracions.Where(c => c.Pk_IdJuntaAclaracion == Pk_IdJuntaAclaracion).Select(c => c.Documento);
                ViewData["existeArchivo"] = _query.Count() == 0 || _query.First() == null ? false : true;
            }
            return PartialView();
        }

        public DevExpress.Web.ASPxUploadControl.ValidationSettings Validation = new DevExpress.Web.ASPxUploadControl.ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".pdf" },
            MaxFileSize = 4000000
        };

        public ActionResult Upload(string TypeFile)
        {
            UploadControlExtension.GetUploadedFiles("UploadArchivo", Validation, FileUploadCompleteArchivo);
            return null;
        }

        public void FileUploadCompleteArchivo(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int Pk_IdJuntaAclaracion = Convert.ToInt32(Session["Pk_IdJuntaAclaracion"]);
                var _query = db.JuntaAclaracions.Where(c => c.Pk_IdJuntaAclaracion == Pk_IdJuntaAclaracion).Select(c => c.Documento);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(Pk_IdJuntaAclaracion, "Archivo");

                string nombreArchivo = Pk_IdJuntaAclaracion + "_JuntaAclaracion_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoJuntaAclaracion"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.JuntaAclaracions;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdJuntaAclaracion == Pk_IdJuntaAclaracion);

                if (modelItem != null)
                {
                    modelItem.Documento = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public ActionResult Download(int? Pk_IdJuntaAclaracion)
        {
            IQueryable<string> _query;
            string contentType = System.Net.Mime.MediaTypeNames.Text.Xml;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoJuntaAclaracion"].ToString();
            _query = db.JuntaAclaracions.Where(c => c.Pk_IdJuntaAclaracion == Pk_IdJuntaAclaracion).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (directorio == "" || nombreArchivo == "" || !System.IO.File.Exists(directorio + nombreArchivo)) return PartialView("FileNotFound");

            string rutadearchivo = directorio + nombreArchivo;
            return File(rutadearchivo, contentType, nombreArchivo);
        }

        public void EliminaArchivo(int? Pk_IdJuntaAclaracion, string TypeFile)
        {
            IQueryable<string> _query;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoJuntaAclaracion"].ToString();
            _query = db.JuntaAclaracions.Where(c => c.Pk_IdJuntaAclaracion == Pk_IdJuntaAclaracion).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (!(directorio == "" || nombreArchivo == ""))
            {
                System.IO.File.Delete(directorio + nombreArchivo);

                var model = db.JuntaAclaracions;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdJuntaAclaracion == Pk_IdJuntaAclaracion);

                if (modelItem != null)
                {
                    modelItem.Documento = null;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }
	}
}
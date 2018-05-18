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
    public class UploadFileFalloController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /UploadFileFallo/
        public ActionResult Index(int Pk_IdFallo)
        {
            Session["Pk_IdFallo"] = Pk_IdFallo != null ? Pk_IdFallo : 0;
            ViewData["Pk_IdFallo"] = Pk_IdFallo != null ? Pk_IdFallo : 0;

            IQueryable<string> _query;

            if (Pk_IdFallo == null) ViewData["existeArchivo"] = false;
            else
            {
                _query = db.Falloes.Where(c => c.Pk_IdFallo == Pk_IdFallo).Select(c => c.Documento);
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
                int Pk_IdFallo = Convert.ToInt32(Session["Pk_IdFallo"]);
                var _query = db.Falloes.Where(c => c.Pk_IdFallo == Pk_IdFallo).Select(c => c.Documento);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(Pk_IdFallo, "Archivo");

                string nombreArchivo = Pk_IdFallo + "_Fallo_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoFallo"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.Falloes;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdFallo == Pk_IdFallo);

                if (modelItem != null)
                {
                    modelItem.Documento = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public ActionResult Download(int? Pk_IdFallo)
        {
            IQueryable<string> _query;
            string contentType = System.Net.Mime.MediaTypeNames.Text.Xml;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoFallo"].ToString();
            _query = db.Falloes.Where(c => c.Pk_IdFallo == Pk_IdFallo).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (directorio == "" || nombreArchivo == "" || !System.IO.File.Exists(directorio + nombreArchivo)) return PartialView("FileNotFound");

            string rutadearchivo = directorio + nombreArchivo;
            return File(rutadearchivo, contentType, nombreArchivo);
        }

        public void EliminaArchivo(int? Pk_IdFallo, string TypeFile)
        {
            IQueryable<string> _query;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoFallo"].ToString();
            _query = db.Falloes.Where(c => c.Pk_IdFallo == Pk_IdFallo).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (!(directorio == "" || nombreArchivo == ""))
            {
                System.IO.File.Delete(directorio + nombreArchivo);

                var model = db.Falloes;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdFallo == Pk_IdFallo);

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
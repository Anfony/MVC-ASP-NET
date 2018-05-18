using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace DXMVCWebApplication1.Controllers
{
    public class ArchivosCampaniaController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        #region Archivos Anexo e Informe
        [ValidateInput(false)]
        public ActionResult Index(int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado de acuerdo al Status de la Campaña
            PermisosFlujoCampania permisosFlujoCampania = new PermisosFlujoCampania(IdCampania);
            Dictionary<string, bool> permisos = permisosFlujoCampania.ObtienePermisos();

            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"] || Senasica.GetStatusCampaniaN(Convert.ToInt32(IdCampania)) != 0;
            #endregion

            var rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            string controlador = Session["CurrentController"].ToString();

            if (rolUsuario == RolesUsuarios.SUP_ESTATAL && (controlador == "CampaniaU" || controlador == "CampaniaN"))
            {
                ViewData["Escritura"] = false;
            }

            Session["IdCampania"] = IdCampania != null ? IdCampania : 0;
            ViewData["IdCampania"] = IdCampania != null ? IdCampania : 0;

            if (IdCampania == null)
            {
                ViewData["existeAnexo"] = false;
                ViewData["existeInforme"] = false;
            }
            else
            {
                var _queryAnexo = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreArchivoAnexo);
                var _queryInforme = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreArchivoInforme);

                ViewData["existeAnexo"] = _queryAnexo.Count() == 0 || _queryAnexo.First() == null ? false : true;
                ViewData["existeInforme"] = _queryInforme.Count() == 0 || _queryInforme.First() == null ? false : true;
            }

            return PartialView();
        }

        public void FileUploadCompleteAnexo(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int IdCampania = Convert.ToInt32(Session["IdCampania"]);
                var _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreArchivoAnexo);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if(existeArchivo) EliminaArchivo(IdCampania, "ANEXO");

                string nombreArchivo = IdCampania + "_ANEXO_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioAnexosCampanias"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.Campanias;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdCampania == IdCampania);

                if (modelItem != null)
                {
                    modelItem.NombreArchivoAnexo = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public void FileUploadCompleteInforme(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int IdCampania = Convert.ToInt32(Session["IdCampania"]);
                var _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreArchivoInforme);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(IdCampania, "INFORME");

                string nombreArchivo = IdCampania + "_INFORME_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesCampanias"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.Campanias;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdCampania == IdCampania);

                if (modelItem != null)
                {
                    modelItem.NombreArchivoInforme = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }
        #endregion

        #region Informes Trimestrales
        public ActionResult IndexInfoTrim(int? IdCampania)
        {
            #region Obtiene permisos del usuario logeado, sin importar en que Status esté la Campaña
            bool permiso = FuncionesAuxiliares.GetPermisoInfoTrimestral();
            ViewData["Escritura"] = permiso;
            #endregion

            var rolUsuario = FuncionesAuxiliares.GetCurrent_RolUsuario();
            string controlador = Session["CurrentController"].ToString();

            if (rolUsuario == RolesUsuarios.SUP_ESTATAL && (controlador == "CampaniaU" || controlador == "CampaniaN"))
            {
                ViewData["Escritura"] = false;
            }

            Session["IdCampania"] = IdCampania != null ? IdCampania : 0;
            ViewData["IdCampania"] = IdCampania != null ? IdCampania : 0;


            if (IdCampania == null)
            {
                ViewData["primerTrim"] = false;
                ViewData["segundoTrim"] = false;
                ViewData["tercerTrim"] = false;
                ViewData["cuartoTrim"] = false;
            }
            else
            {
                var _queryPrimerTrim = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_PrimerTrim);
                var _querySegundoTrim = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_SegundoTrim);
                var _queryTercerTrim = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_TercerTrim);
                var _queryCuartoTrim = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_CuartoTrim);

                ViewData["primerTrim"] = _queryPrimerTrim.Count() == 0 || _queryPrimerTrim.First() == null ? false : true;
                ViewData["segundoTrim"] = _querySegundoTrim.Count() == 0 || _querySegundoTrim.First() == null ? false : true;
                ViewData["tercerTrim"] = _queryTercerTrim.Count() == 0 || _queryTercerTrim.First() == null ? false : true;
                ViewData["cuartoTrim"] = _queryCuartoTrim.Count() == 0 || _queryCuartoTrim.First() == null ? false : true;
            }

            return PartialView();
        }

        public void FileUploadCompletePrimerTrim(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int IdCampania = Convert.ToInt32(Session["IdCampania"]);
                var _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_PrimerTrim);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(IdCampania, "PRIMER_TRIM");

                string nombreArchivo = IdCampania + "_PRIMER_TRIM_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.Campanias;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdCampania == IdCampania);

                if (modelItem != null)
                {
                    modelItem.NombreInf_PrimerTrim = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public void FileUploadCompleteSegundoTrim(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int IdCampania = Convert.ToInt32(Session["IdCampania"]);
                var _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_SegundoTrim);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(IdCampania, "SEGUNDO_TRIM");

                string nombreArchivo = IdCampania + "_SEGUNDO_TRIM_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.Campanias;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdCampania == IdCampania);

                if (modelItem != null)
                {
                    modelItem.NombreInf_SegundoTrim = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public void FileUploadCompleteTercerTrim(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int IdCampania = Convert.ToInt32(Session["IdCampania"]);
                var _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_TercerTrim);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(IdCampania, "TERCER_TRIM");

                string nombreArchivo = IdCampania + "_TERCER_TRIM_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.Campanias;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdCampania == IdCampania);

                if (modelItem != null)
                {
                    modelItem.NombreInf_TercerTrim = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public void FileUploadCompleteCuartoTrim(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int IdCampania = Convert.ToInt32(Session["IdCampania"]);
                var _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_CuartoTrim);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(IdCampania, "CUARTO_TRIM");

                string nombreArchivo = IdCampania + "_CUARTO_TRIM_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.Campanias;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdCampania == IdCampania);

                if (modelItem != null)
                {
                    modelItem.NombreInf_CuartoTrim = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }
        #endregion

        public DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".pdf" },
            MaxFileSize = 9000000
        };

        /// <summary>
        /// Empieza la carga del Anexo
        /// </summary>
        /// <returns></returns>
        public ActionResult CargaArchivo(string TypeFile)
        {
            switch (TypeFile)
            {
                case "ANEXO":
                    UploadControlExtension.GetUploadedFiles("UploadControlAnexo", ValidationSettings, FileUploadCompleteAnexo);
                    break;
                case "INFORME":
                    UploadControlExtension.GetUploadedFiles("UploadControlInforme", ValidationSettings, FileUploadCompleteInforme);
                    break;
                case "PRIMER_TRIM":
                    UploadControlExtension.GetUploadedFiles("UploadControlPrimerTrim", ValidationSettings, FileUploadCompletePrimerTrim);
                    break;
                case "SEGUNDO_TRIM":
                    UploadControlExtension.GetUploadedFiles("UploadControlSegundoTrim", ValidationSettings, FileUploadCompleteSegundoTrim);
                    break;
                case "TERCER_TRIM":
                    UploadControlExtension.GetUploadedFiles("UploadControlTercerTrim", ValidationSettings, FileUploadCompleteTercerTrim);
                    break;
                case "CUARTO_TRIM":
                    UploadControlExtension.GetUploadedFiles("UploadControlCuartoTrim", ValidationSettings, FileUploadCompleteCuartoTrim);
                    break;
            }

            return null;
        }

        public void EliminaArchivo(int? IdCampania, string TypeFile)
        {
            IQueryable<string> _query;
            string directorio;
            string nombreArchivo;

            switch(TypeFile)
            {
                case "ANEXO":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioAnexosCampanias"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreArchivoAnexo);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "INFORME":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesCampanias"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreArchivoInforme);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "PRIMER_TRIM":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_PrimerTrim);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "SEGUNDO_TRIM":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_SegundoTrim);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "TERCER_TRIM":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_TercerTrim);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "CUARTO_TRIM":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_CuartoTrim);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                default:
                    directorio = nombreArchivo = "";
                    break;
            }

            if(!(directorio == "" || nombreArchivo == ""))
            {
                System.IO.File.Delete(directorio + nombreArchivo);

                var model = db.Campanias;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdCampania == IdCampania);

                if (modelItem != null)
                {
                    switch (TypeFile)
                    {
                        case "ANEXO": modelItem.NombreArchivoAnexo = null; break;
                        case "INFORME": modelItem.NombreArchivoInforme = null; break;
                        case "PRIMER_TRIM": modelItem.NombreInf_PrimerTrim = null; break;
                        case "SEGUNDO_TRIM": modelItem.NombreInf_SegundoTrim = null; break;
                        case "TERCER_TRIM": modelItem.NombreInf_TercerTrim = null; break;
                        case "CUARTO_TRIM": modelItem.NombreInf_CuartoTrim = null; break;
                    }

                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public ActionResult DescargaArchivo(int? IdCampania, string TypeFile)
        {
            IQueryable<string> _query;
            string contentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            string directorio;
            string nombreArchivo;

            switch(TypeFile)
            {
                case "ANEXO":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioAnexosCampanias"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreArchivoAnexo);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "INFORME":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesCampanias"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreArchivoInforme);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "PRIMER_TRIM":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_PrimerTrim);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "SEGUNDO_TRIM":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_SegundoTrim);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "TERCER_TRIM":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_TercerTrim);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                case "CUARTO_TRIM":
                    directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioInformesTrimestrales"].ToString();
                    _query = db.Campanias.Where(c => c.Pk_IdCampania == IdCampania).Select(c => c.NombreInf_CuartoTrim);
                    nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();
                    break;
                default:
                    directorio = nombreArchivo = "";
                    break;
            }

            if (directorio == "" || nombreArchivo == "" || !System.IO.File.Exists(directorio + nombreArchivo)) return PartialView("FileNotFound");

            return new FilePathResult(directorio + nombreArchivo, contentType);
        }
    }
}
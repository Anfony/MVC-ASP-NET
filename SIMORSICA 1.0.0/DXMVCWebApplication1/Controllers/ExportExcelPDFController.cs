using DevExpress.Web.ASPxClasses;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Web.UI.WebControls;
using System.Collections;


namespace DXMVCWebApplication1.Controllers
{
    public class ExportExcelPDFController : Controller
    {

        #region Unidad Ejecutora
        public ActionResult exportUnidadEjecutora(bool isPDF)
        {
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            return isPDF ? GridViewExtension.ExportToPdf(GetGridSettingsUnidadEjecutora(), Senasica.GetUnidadEjecutoraByUsuario(rolusuario, FuncionesAuxiliares.GetCurrent_IdUnidadResponsable(), _Fk_IdEstado, _Fk_IdUnidadEjecutora))
                        : GridViewExtension.ExportToXls(GetGridSettingsUnidadEjecutora(), Senasica.GetUnidadEjecutoraByUsuario(rolusuario, FuncionesAuxiliares.GetCurrent_IdUnidadResponsable(), _Fk_IdEstado, _Fk_IdUnidadEjecutora));
        }
        private GridViewSettings GetGridSettingsUnidadEjecutora()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Catálogo de Instancias Ejecutoras";

            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Catalogo de Instancia Ejecutora";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdUnidadEjecutora";
            if (FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_VEGETAL
                || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_INOCUIDAD
                || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_ANIMAL
                || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_ACUICOLA_PESQUERA
                || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_MOVILIZACION
                || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_UPV)
            {
                settings.Columns.Add("TipoDeUnidad", "Tipo de Unidad");
                settings.Columns.Add("UnidadResponsable", "Unidad Responsable");
            }
            else
            {
                settings.Columns.Add("TipoDeUnidad.Nombre", "Tipo de Unidad");
                settings.Columns.Add("UnidadResponsable.Nombre", "Unidad Responsable");
            }
            settings.Columns.Add("Nombre", "Nombre de la Unidad");
            settings.Columns.Add("Telefono", "Teléfono");
            settings.Columns.Add("StatusKardexUE", "Estatus");
            settings.Columns.Add("RFC");

            return settings;
        }
        #endregion

        #region Personal
        public ActionResult exportPersonal(bool isPDF, int IdUnidadEjecutora)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetPersonaGridSettings(), Senasica.GetPersonaByUnidadEjecutora(IdUnidadEjecutora))
                        : GridViewExtension.ExportToXls(GetPersonaGridSettings(), Senasica.GetPersonaByUnidadEjecutora(IdUnidadEjecutora));
        }

        private GridViewSettings GetPersonaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Personal";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Personal de la Instancia Ejecutora";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdPersona";
            settings.Columns.Add("Nombre", "Nombre");
            settings.Columns.Add("Ap_Paterno", "Ap. Paterno");
            settings.Columns.Add("Ap_Materno", "Ap. Materno");
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Colonia", "Colonia/Localidad");
            settings.Columns.Add("Telefono", "Teléfono");
            settings.Columns.Add("Email", "Correo Electrónico");
            settings.Columns.Add("Cargo.Nombre", "Puesto");

            return settings;
        }
        #endregion

        #region Vigencia
        public ActionResult exportVigencia(bool isPDF, int IdUnidadEjecutora)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetVigenciasGridSettings(), Senasica.GetVigenciasByUnidadEjecutora(IdUnidadEjecutora))
                        : GridViewExtension.ExportToXls(GetVigenciasGridSettings(), Senasica.GetVigenciasByUnidadEjecutora(IdUnidadEjecutora)); ;
        }

        private GridViewSettings GetVigenciasGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Vigencia";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Documentos que avalan la vigencia";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdVigencia";
            settings.Columns.Add("FechaInicio", "Inicio de Vigencia");
            settings.Columns.Add("FechaFin", "Fin de Vigencia");
            settings.Columns.Add("NumeroRegistro", "Número  de Registro");

            return settings;
        }
        #endregion

        #region Instalaciones
        public ActionResult exportInstalaciones(bool isPDF, int IdUnidadEjecutora)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInstalacionesGridSettings(), Senasica.GetInstalacionesByUnidadEjecutora(IdUnidadEjecutora))
                        : GridViewExtension.ExportToXls(GetInstalacionesGridSettings(), Senasica.GetInstalacionesByUnidadEjecutora(IdUnidadEjecutora));
        }

        private GridViewSettings GetInstalacionesGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Instalaciones registradas";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Instalaciones registradas";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdInstalacion";
            settings.Columns.Add("TipoInstalacion.Nombre", "Tipo de Instalación");
            settings.Columns.Add("Nombre", "Nombre");
            settings.Columns.Add("Ubicacion", "Ubicación");
            settings.Columns.Add("Utilizacion", "Utilización");
            settings.Columns.Add("region", "Región");
            settings.Columns.Add("Estatus", "Estado de los bienes");
            settings.Columns.Add("Certificado");
            settings.Columns.Add("TipoAdquisicion.Nombre", "Tipo de Adquisición");

            return settings;
        }
        #endregion

        #region Inventario
        public ActionResult exportInventario(bool isPDF, int IdUnidadEjecutora)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInventarioGridSettings(), Senasica.GetInventarioByUnidadEjecutora(IdUnidadEjecutora))
                        : GridViewExtension.ExportToXls(GetInventarioGridSettings(), Senasica.GetInventarioByUnidadEjecutora(IdUnidadEjecutora));
        }

        private GridViewSettings GetInventarioGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inventario";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Bienes Muebles a cargo de la Instancia";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdInstalacion";
            settings.Columns.Add("Clave", "No. de Inventario Simosica");
            settings.Columns.Add("ClaveAnt", "Clave Aterior");
            settings.Columns.Add("Marca");
            settings.Columns.Add("Modelo");
            settings.Columns.Add("Serie", "No. de Serie");
            settings.Columns.Add("BienOServ.Anio.Anio1", "Año. Reg. Sistema");
            settings.Columns.Add("BienOServ.Nombre", "Bien o Servicio");
            settings.Columns.Add("Factura", "No. de Factura");
            settings.Columns.Add("Costo", "Valor Factura");
            settings.Columns.Add("EstadoBien.Nombre", "Estado del Bien");
            settings.Columns.Add("TipoDeRecurso.Nombre", "Tipo de Recurso");
            settings.Columns.Add("Descripcion", "Actividad/Descripción");
            return settings;
        }
        #endregion


        //////////////  CAMPANIA  ///////////////////////////////

        #region ActividadesXAccionPrincipal
        public ActionResult exportActividadXAccion(bool isPDF)
        {
            int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
            int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            int? IdPersona = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdPersona__SIS);
            int? IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string rolusuraio = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);

            return isPDF ? GridViewExtension.ExportToPdf(GetActividadXAccionPGridSettings(), Senasica.GetActividadesParaAsignarByCampania(rolusuraio, UEID, IdPersona, IdEstado, IdAnio))
                        : GridViewExtension.ExportToXls(GetActividadXAccionPGridSettings(), Senasica.GetActividadesParaAsignarByCampania(rolusuraio, UEID, IdPersona, IdEstado, IdAnio));
        }


        private GridViewSettings GetActividadXAccionPGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Actividad";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "ACTIVIDADES A ASIGNAR ";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdActividadXAccion";
            settings.Columns.Add("AccionXCampania.Campania.ProyectoAutorizado.Nombre", "Campaña");
            settings.Columns.Add("AccionXCampania.TipoDeAccion.Nombre", "Acción");
            settings.Columns.Add("TipoActividad.Nombre", "Tipo de Actividad");
            settings.Columns.Add("Actividad");
            settings.Columns.Add("UnidadDeMedida.Nombre", "Unidad de Medida");
            settings.Columns.Add("Meta_Anual", "Meta Anual");
            settings.Columns.Add("Asignadas");
            settings.Columns.Add("EstadoDeAsignacion", "Estado de Asignación");
            settings.Columns.Add("TipoDeRecurso.Nombre", "Tipo de Recurso");
            settings.Columns.Add("Persona.NombreCompleto", "Responsable de la Actividad");

            return settings;
        }
        #endregion

        #region ImportanciaEconomica
        public ActionResult exportSVImportanciaCultivo(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSVImportanciaCultivoGridSettings(), Senasica.GetSVImportanciaCultivoByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSVImportanciaCultivoGridSettings(), Senasica.GetSVImportanciaCultivoByCampania(IdCampania));
        }

        private GridViewSettings GetSVImportanciaCultivoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nuevo Cultivo";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Importancia Económica del Cultivo";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaCultivoSV";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("NumUnidadesProduccion", "Núm. Unidades de Producción");
            settings.Columns.Add("Cultivo");
            settings.Columns.Add("Superficie");
            settings.Columns.Add("NumProductores", "Núm. de Productores");
            settings.Columns.Add("Produccion", "Producción (toneladas)");
            settings.Columns.Add("ValorProduccion", "Valor de la Producción ($)").Width = 80;
            settings.Columns.Add("DestinoProduccion.Nombre", "Destino de la Producción").Width = 80;
            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Superficie").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumProductores").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Produccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ValorProduccion").DisplayFormat = "c";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion

        #region  Atendido
        public ActionResult exportSVAtendido(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSVAtendidoGridSettings(), Senasica.GetSVAtendidoByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSVAtendidoGridSettings(), Senasica.GetSVAtendidoByCampania(IdCampania));
        }


        private GridViewSettings GetSVAtendidoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Atendido";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Atendido por la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdAtendidoSV";

            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("NumUnidadesProduccion", "Núm. Unidades de Producción").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("Cultivo", "Tipo Cultivo");
            settings.Columns.Add("Superficie", "Superficie (hectáreas)").PropertiesEdit.DisplayFormatString = "n";
            //Año Anterior (estimado)
            settings.Columns.Add("NumUnidProdAtendida_Ant", "Núm. U. Producción Atendidas").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("SuperficieAtendida_Ant", "Superficie Atendidas (Has)").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("Prevalencia_Ant", "Prevalencia (%)").PropertiesEdit.DisplayFormatString = "n";

            //settings.Caption = "Año Actual (estimado)";
            settings.Columns.Add("NumUnidProdAtendida_Act", "Núm. U. Producción Atendidas").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("SuperficieAtendida_Act", "Superficie Atendidas (Has)").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("Prevalencia_Act", "Prevalencia (%)").PropertiesEdit.DisplayFormatString = "n";
            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Superficie").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidProdAtendida_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "SuperficieAtendida_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidProdAtendida_Act").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "SuperficieAtendida_Act").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion

        #region SVImportanciaPlaga
        public ActionResult exportSVImportanciaPlaga(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSVImportanciaPlagaGridSettings(), Senasica.GetSVImportanciaPlagaByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSVImportanciaPlagaGridSettings(), Senasica.GetSVImportanciaPlagaByCampania(IdCampania));
        }


        private GridViewSettings GetSVImportanciaPlagaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Plaga";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Importancia de la Plaga";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaPlagaSV";
            
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Cultivo", "Tipo Cultivo");
            settings.Columns.Add("SuperficieAfectada", "Superficie Afectada").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumUnidadesProdAfectada", "Núm. Unidades de Producción Afectada").PropertiesEdit.DisplayFormatString = "n";

            settings.Columns.Add("PrevalenciaInfestacion", "Prevalencia o Infestación (%)").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("VolumenPerdida", "Volumen de la Pérdida (toneladas)").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("ValorPerdida", "Valor de la Pérdida ($)").PropertiesEdit.DisplayFormatString = "c";
            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "SuperficieAfectada").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProdAfectada").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "VolumenPerdida").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ValorPerdida").DisplayFormat = "c";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion

        #region AccionesXCamapañaSV
        public ActionResult exportAccionXCampania(bool isPDF, int IdCampania)
        {
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return isPDF ? GridViewExtension.ExportToPdf(GetAccionesPorCampaniaGridSettings(), Senasica.GetAccionesPorCampaniaByCampania(IdCampania, StatusActual))
                         : GridViewExtension.ExportToXls(GetAccionesPorCampaniaGridSettings(), Senasica.GetAccionesPorCampaniaByCampania(IdCampania, StatusActual));
        }
        private GridViewSettings GetAccionesPorCampaniaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Acción";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Acciones a realizar en la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "PK_IdAccionXCampania";
            settings.Columns.Add("TipoAccion", "Tipo de Acción");
            settings.Columns.Add("Persona", "Encargado");
            settings.Columns.Add("Justificacion", "Justificación");

            return settings;
        }
        #endregion

        #region ActividadesXAccion
        public ActionResult exportActividadXAccions(bool isPDF, int IdAccionXCamp)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _query = db.ActividadXAccions.Where(item => item.Fk_IdAccionXCampania == IdAccionXCamp).Select(item => item.AccionXCampania.Campania.Pk_IdCampania);
            int IdCampania = _query.Count() == 0 ? -1 : _query.First();
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return isPDF ? GridViewExtension.ExportToPdf(GetActividadXAccionGridSettings(), Senasica.GetActividadesByAccion(IdAccionXCamp, IdCampania, StatusActual))
                         : GridViewExtension.ExportToXls(GetActividadXAccionGridSettings(), Senasica.GetActividadesByAccion(IdAccionXCamp, IdCampania, StatusActual));
        }
        private GridViewSettings GetActividadXAccionGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Actividad";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Actividades a realizar para completar la Acción";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdActividadXAccion";
            settings.Columns.Add("TipoActividad", "Tipo de Actividad");
            settings.Columns.Add("Actividad", "Descripción");
            settings.Columns.Add("Meta_Anual", "Meta Anual");
            settings.Columns.Add("UnidadDeMedida", "Unidad de Medida");
            settings.Columns.Add("TipoDeRecurso", "Tipo de Recurso");
            settings.Columns.Add("Persona", "Responsable");

            return settings;
        }
        #endregion

        #region NecesidadesXAcción
        public ActionResult exportNecesidadXAccion(bool isPDF, int IdAccionXCamp, int? IdCampania)
        {
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return isPDF ? GridViewExtension.ExportToPdf(GetNecesidadesxAccionGridSettings(), Senasica.GetProgramaAnualAdqByAccionXCampania(IdAccionXCamp, IdAnioPresupuestal, IdCampania, StatusActual))
                         : GridViewExtension.ExportToXls(GetNecesidadesxAccionGridSettings(), Senasica.GetProgramaAnualAdqByAccionXCampania(IdAccionXCamp, IdAnioPresupuestal, IdCampania, StatusActual));
        }
        private GridViewSettings GetNecesidadesxAccionGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Necesidad";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Necesidades para completar la Acción";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProgramaAnualAdq";
            settings.Columns.Add("BienOServ", "Bien/Servicio");
            settings.Columns.Add("UnidadDeMedida", "U/Medida");
            settings.Columns.Add("RecFed", "Recursos Federales");
            settings.Columns.Add("RecEst", "Recursos Estatales");
            settings.Columns.Add("RecProp", "Recursos Propios");

            return settings;
        }
        #endregion

        #region GastosRelativosCamapañaSV
        public ActionResult exportGastosRelCampania(bool isPDF, int? IdCampania)
        {
            int IdAnioPresupuestal = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return isPDF ? GridViewExtension.ExportToPdf(GetGastorRelCampGridSettings(), Senasica.GetProgramaAnualByCampania(IdCampania, IdAnioPresupuestal, StatusActual))
                         : GridViewExtension.ExportToXls(GetGastorRelCampGridSettings(), Senasica.GetProgramaAnualByCampania(IdCampania, IdAnioPresupuestal, StatusActual));
        }
        private GridViewSettings GetGastorRelCampGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nuevo Gasto";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Gastos Relativos a la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProgramaAnualAdq";
            settings.Columns.Add("BienOServ", "Bien/Servicio");
            settings.Columns.Add("UnidadDeMedida", "U/Medida");
            settings.Columns.Add("Cant_Anual", "Total Adquisiciones").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("imp_Anual", "Recursos Estimados").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("RecFed", "Federales").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("RecEst", "Estatales").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("RecProp", "Propios").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("Justificacion", "Justificación");


            return settings;
        }
        #endregion

        #region STATUSSV
        public ActionResult exportStatusCampaniaKardex(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetStatusCampaniaKardexesGridSettings(), Senasica.GetStatusCampaniaKardexesByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetStatusCampaniaKardexesGridSettings(), Senasica.GetStatusCampaniaKardexesByCampania(IdCampania));
        }
        private GridViewSettings GetStatusCampaniaKardexesGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Cambiar Estado";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PageHeader.Center = "Histórico de cambio de Estatus";
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "PK_IdStatusCampaniaKardex";
            settings.Columns.Add("StatusCampania.Nombre", "Nuevo Estado");
            settings.Columns.Add("Fecha", "Fecha");
            settings.Columns.Add("Comentarios").ColumnType = MVCxGridViewColumnType.Memo;


            return settings;
        }
        #endregion

        //////////////////////  CAMPANIASA///////////////***************

        #region ImportanciaEconómicaSA
        public ActionResult exportSAImportanciaEconomica(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSAPoblacionGridSettings(), Senasica.GetSAPoblacionByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSAPoblacionGridSettings(), Senasica.GetSAPoblacionByCampania(IdCampania));
        }
        private GridViewSettings GetSAPoblacionGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Población";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Importancia Económica de la Población";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaEconomicaSA";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("NumUnidadesProduccion", "Núm. Unidades Producción").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("Especie.Nombre", "Especie");
            settings.Columns.Add("NumCabezas", "Núm. Cabezas").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("FuncionZootecnica.Nombre", "Fun. Zootécnica");

            settings.Columns.AddBand(AnioAntBand =>
            {
                AnioAntBand.Caption = "Año Anterior al del Programa en Curso";
                AnioAntBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioAntBand.Columns.Add("CantidadProduccion", "Cant. Producción").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("UnidadMedidaProduccion.Nombre", "U. Med. Producción");
                AnioAntBand.Columns.Add("ValorProduccion", "Valor de la Producción").PropertiesEdit.DisplayFormatString = "c";
                AnioAntBand.Columns.Add("NumAnimalesEnPieExportados", "Núm. Animales en pie exportados").PropertiesEdit.DisplayFormatString = "n";
            });
            settings.Columns.Add("DestinoProduccion.Nombre", "Dest. Producción");

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumCabezas").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "CantidadProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ValorProduccion").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumAnimalesEnPieExportados").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;

            return settings;
        }
        #endregion

        #region AtendidoXCampañaSA
        public ActionResult exportSAAtendido(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSAAtendidoGridSettings(), Senasica.GetSAAtendidoByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSAAtendidoGridSettings(), Senasica.GetSAAtendidoByCampania(IdCampania));
        }
        private GridViewSettings GetSAAtendidoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Población";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Atendido por la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdAtendidoSA";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Especie.Nombre", "Especie");
            settings.Columns.Add("NumUnidadesProduccion", "Núm. Unidades de Producción").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumCabezas", "Núm. Cabezas").PropertiesEdit.DisplayFormatString = "n";

            settings.Columns.AddBand(AnioAntBand =>
            {
                AnioAntBand.Caption = "Año Anterior al del Programa en Curso";
                AnioAntBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioAntBand.Columns.Add("UProduccionAtend_Ant", "U. Producción Atendidas").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("CabezasAct_Ant", "Cabezas Atendidas").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("PrevEnfermedad_Ant", "Prev. de la Enfermadad (%)").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("NumCuarentenas_Ant", "Núm. Cuarentenas").PropertiesEdit.DisplayFormatString = "n";
            });

            settings.Columns.AddBand(AnioActBand =>
            {
                AnioActBand.Caption = "Año Actual (estimado)";
                AnioActBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioActBand.Columns.Add("UProduccionAtend_Act", "U. Producción Atendidas").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("CabezasAct_Anct", "Cabezas Atendidas").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("PrevEnfermedad_Act", "Prev. de la Enfermadad (%)").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("NumCuarentenas_Act", "Núm. Cuarentenas").PropertiesEdit.DisplayFormatString = "n";
            });

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumCabezas").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "UProduccionAtend_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "CabezasAct_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumCuarentenas_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "UProduccionAtend_Act").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "CabezasAct_Anct").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumCuarentenas_Act").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;

            return settings;
        }
        #endregion

        #region  ImportanciaEnfermedadSA
        public ActionResult exportSAImportanciaEnfermedad(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSAImportanciaGridSettings(), Senasica.GetSAImportanciaByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSAImportanciaGridSettings(), Senasica.GetSAImportanciaByCampania(IdCampania));
        }
        private GridViewSettings GetSAImportanciaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Enfermedad";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Importancia de la Enfermedad";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaEnfermedadSA";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("NumUnidadesProduccion", "Núm. Unidades de Producción").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("Especie.Nombre", "Especie");
            settings.Columns.Add("Enfermedad", "Enfermedad");
            settings.Columns.Add("UnidadMedidaProduccion.Nombre", "Unidad de Medida de la Pérdida");
            settings.Columns.Add("NumCabezas", "Núm. Cabezas").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("PrevEnfermedad", "Prevalencia de la Enfermadad (%)").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.AddBand(ImpactoBand =>
            {
                ImpactoBand.Caption = "Impacto Económico de la Enfermedad";
                ImpactoBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                ImpactoBand.Columns.Add("CantEstimadasPerdidas", "Cantidad Estimada Pérdidas").PropertiesEdit.DisplayFormatString = "n";
                ImpactoBand.Columns.Add("ValorEstimadoPerdida", "Valor Estimado de la Pérdida ($)").PropertiesEdit.DisplayFormatString = "n";

            });

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumCabezas").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "CantEstimadasPerdidas").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ValorEstimadoPerdida").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;


            return settings;
        }
        #endregion

        #region AccionesxCamapañaSA
        public ActionResult exportAccionXCampanias(bool isPDF, int IdCampania)
        {
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            return isPDF ? GridViewExtension.ExportToPdf(GetAccionesXCampaniaSAGridSettings(), Senasica.GetAccionesPorCampaniaByCampania(IdCampania, StatusActual))
                         : GridViewExtension.ExportToXls(GetAccionesXCampaniaSAGridSettings(), Senasica.GetAccionesPorCampaniaByCampania(IdCampania, StatusActual));
        }
        private GridViewSettings GetAccionesXCampaniaSAGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Acción";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Acciones a realizar en la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "PK_IdAccionXCampania";
            settings.Columns.Add("TipoDeAccion", "Tipo de Acción");
            settings.Columns.Add("Persona", "Encargado");
            settings.Columns.Add("Justificacion", "Justificación");

            return settings;
        }
        #endregion

        #region GastosRelativosCamapañaSA
        public ActionResult exportAccionXCampaniaSA(bool isPDF, int IdCampania)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));

            return isPDF ? GridViewExtension.ExportToPdf(GetProgramaAnualSAdqGridSettings(), Senasica.GetProgramaAnualByCampania(IdCampania, IdAnio, StatusActual))
                         : GridViewExtension.ExportToXls(GetProgramaAnualSAdqGridSettings(), Senasica.GetProgramaAnualByCampania(IdCampania, IdAnio, StatusActual));
        }
        private GridViewSettings GetProgramaAnualSAdqGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Nueva Gasto";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Gastos Relativos a la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProgramaAnualAdq";
            settings.Columns.Add("BienOServ", "Bien/Servicio");
            settings.Columns.Add("UnidadDeMedida", "U/Medida");
            settings.Columns.Add("Cant_Anual", "Total Adquisiciones").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("imp_Anual", "Recursos Estimados").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("RecFed", "Federales").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("RecEst", "Estatales").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("RecProp", "Propios").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("Justificacion", "Justificación");



            return settings;
        }
        #endregion

        #region STATUSSA
        public ActionResult exportStatusCampaniaSAKardex(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetStatusCampaniaKardexessGridSettings(), Senasica.GetStatusCampaniaKardexesByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetStatusCampaniaKardexessGridSettings(), Senasica.GetStatusCampaniaKardexesByCampania(IdCampania));
        }
        private GridViewSettings GetStatusCampaniaKardexessGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Cambiar Estado";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.PageHeader.Center = "Histórico de cambio de Estatus";
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "PK_IdStatusCampaniaKardex";
            settings.Columns.Add("StatusCampania.Nombre", "Nuevo Estado");
            settings.Columns.Add("Fecha", "Fecha");
            settings.Columns.Add("Comentarios").ColumnType = MVCxGridViewColumnType.Memo;


            return settings;
        }
        #endregion

        //////////////////////  CAMPANIA INO SA///////////////

        #region UE_ATENDIDO_INO_SA
        public ActionResult exportInoSAAtendido(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSAAtendidoGridSettings(), Senasica.GetInoCompSAByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSAAtendidoGridSettings(), Senasica.GetInoCompSAByCampania(IdCampania));
        }
        private GridViewSettings GetInoSAAtendidoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SA_Atendido";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Pecuaria - Atendido";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdAtendidoIno";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("TipoUnidad_Atendido.Nombre", "Tipo de Unidad");
            settings.Columns.Add("Especie_Cultivo", "Cultivo o Especie");
            settings.Columns.Add("NumUnidades", "Núm. Unidades").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumProductores", "Productores a Atender").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumUnidadesProductivas", "Unidades Productivas a Atender").PropertiesEdit.DisplayFormatString = "n";

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidades").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumProductores").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProductivas").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion

        #region UE_UNIDADES_CERTIFICAR_INO_SA
        public ActionResult exportInoSAUnidadesACertificar(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSAUnidadesACertificarGridSettings(), Senasica.GetInoUnidadesCertSAByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSAUnidadesACertificarGridSettings(), Senasica.GetInoUnidadesCertSAByCampania(IdCampania));
        }
        private GridViewSettings GetInoSAUnidadesACertificarGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SA_UnidadesACertificar";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Pecuaria - Unidades A Certificar";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdUnidadesCertificarInoSA";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Especie.Nombre", "Especie");
            settings.Columns.Add("FuncionZootecnica.Nombre", "Función Zootécnica");


            settings.Columns.AddBand(AnioAntBand =>
            {
                AnioAntBand.Caption = "Año Anterior";
                AnioAntBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioAntBand.Columns.Add("AntProgramado", "Programado").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("AntRealizado", "Realizado").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("AntPorcentajeCumplimiento", "% cumplimiendo").PropertiesEdit.DisplayFormatString = "n";
            });

            settings.Columns.AddBand(AnioActBand =>
            {
                AnioActBand.Caption = "Año Actual";
                AnioActBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioActBand.Columns.Add("ActProgramado", "Programado").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("ActRealizado", "Realizado").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("ActPorcentajeCumplimiento", "% cumplimiendo").PropertiesEdit.DisplayFormatString = "n";
            });
            return settings;
        }
        #endregion

        #region UE_IMPORTANCIA_INO_SA
        public ActionResult exportInoSAImportanciaCampania(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSAImportanciaCampaniaGridSettings(), Senasica.GetInoImportanciaSAByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSAImportanciaCampaniaGridSettings(), Senasica.GetInoImportanciaSAByCampania(IdCampania));
        }
        private GridViewSettings GetInoSAImportanciaCampaniaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SA_ImportanciaCampania";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Pecuaria - Importancia de la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaIno";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Especie_Cultivo", "Especie o cultivo");
            settings.Columns.Add("NumUnidadesEspecie", "Número de Unidades por especie").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("TipoProduccion.Nombre", "Tipo de Producción");
            settings.Columns.Add("DestinoProduccion.Nombre", "Mercado Destino");
            settings.Columns.Add("TendenciaProduccion.Nombre", "Tendencia de la Producción");
            settings.Columns.Add("TendenciaProduccion.EsAfectado", "Afectado por Contaminantes");

            settings.Columns.Add("NumUnidadesAfectadas", "Num. Unidades Afectadas").PropertiesEdit.DisplayFormatString = "n";

            return settings;
        }
        #endregion

        //////////////////////  CAMPANIA INO SAP///////////////

        #region UE_ATENDIDO_INO_SAP
        public ActionResult exportInoSAPAtendido(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSAPAtendidoGridSettings(), Senasica.GetInoCompSACByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSAPAtendidoGridSettings(), Senasica.GetInoCompSACByCampania(IdCampania));
        }
        private GridViewSettings GetInoSAPAtendidoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SAP_Atendido";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Acuícola y Pesquera - Atendido";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdAtendidoIno";
            //settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("TipoUnidad_Atendido.Nombre", "Tipo de Unidad");
            settings.Columns.Add("Especie_Cultivo", "Cultivo o Especie");
            settings.Columns.Add("NumUnidades", "Núm. Unidades").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumProductores", "Productores a Atender").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumUnidadesProductivas", "Unidades Productivas a Atender").PropertiesEdit.DisplayFormatString = "n";

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidades").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumProductores").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProductivas").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion

        #region UE_UNIDADES_CERTIFICAR_INO_SAP
        public ActionResult exportInoSAPUnidadesACertificar(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSAPUnidadesACertificarGridSettings(), Senasica.GetInoUnidadesCertSACByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSAPUnidadesACertificarGridSettings(), Senasica.GetInoUnidadesCertSACByCampania(IdCampania));
        }
        private GridViewSettings GetInoSAPUnidadesACertificarGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SAP_UnidadesACertificar";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Acuícola y Pesquera - Unidades A Certificar";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdUnidadesCertificarInoSAC";
            settings.Columns.Add("TipoUnidad_UCAC.Nombre", "Tipo de Unidad");
            settings.Columns.Add("Especie", "Especie");

            settings.Columns.AddBand(AnioAntBand =>
            {
                AnioAntBand.Caption = "Año Anterior";
                AnioAntBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioAntBand.Columns.Add("AntProgramado", "Programado").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("AntRealizado", "Realizado").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("AntPorcentajeCumplimiento", "% cumplimiendo").PropertiesEdit.DisplayFormatString = "n";
            });

            settings.Columns.AddBand(AnioActBand =>
            {
                AnioActBand.Caption = "Año Actual";
                AnioActBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioActBand.Columns.Add("ActProgramado", "Programado").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("ActRealizado", "Realizado").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("ActPorcentajeCumplimiento", "% cumplimiendo").PropertiesEdit.DisplayFormatString = "n";
            });
            return settings;
        }
        #endregion

        #region UE_IMPORTANCIA_INO_SAP
        public ActionResult exportInoSAPImportanciaCampania(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSAPImportanciaCampaniaGridSettings(), Senasica.GetInoImportanciaSACByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSAPImportanciaCampaniaGridSettings(), Senasica.GetInoImportanciaSACByCampania(IdCampania));
        }
        private GridViewSettings GetInoSAPImportanciaCampaniaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SAP_ImportanciaCampania";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Acuícola y Pesquera - Importancia de la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaIno";
            settings.Columns.Add("Especie_Cultivo", "Especie o cultivo");
            settings.Columns.Add("NumUnidadesEspecie", "Número de Unidades por especie").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("TipoProduccion.Nombre", "Tipo de Producción");
            settings.Columns.Add("DestinoProduccion.Nombre", "Mercado Destino");
            settings.Columns.Add("TendenciaProduccion.Nombre", "Tendencia de la Producción");
            settings.Columns.Add("TendenciaProduccion.EsAfectado", "Afectado por Contaminantes");

            settings.Columns.Add("NumUnidadesAfectadas", "Num. Unidades Afectadas").PropertiesEdit.DisplayFormatString = "n";

            return settings;
        }
        #endregion
        //////////////////////  CAMPANIA INO SV///////////////

        #region UE_ATENDIDO_INO_SAV
        public ActionResult exportInoSVAtendido(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSVAtendidoGridSettings(), Senasica.GetInoCompSVByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSVAtendidoGridSettings(), Senasica.GetInoCompSVByCampania(IdCampania));
        }
        private GridViewSettings GetInoSVAtendidoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SV_Atendido";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Agrícola - Atendido";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdAtendidoIno";
            settings.Columns.Add("TipoUnidad_Atendido.Nombre", "Tipo de Unidad");
            settings.Columns.Add("Especie_Cultivo", "Cultivo o Especie");
            settings.Columns.Add("NumUnidades", "Núm. Unidades").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumProductores", "Productores a Atender").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumUnidadesProductivas", "Unidades Productivas a Atender").PropertiesEdit.DisplayFormatString = "n";

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidades").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumProductores").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProductivas").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion

        #region UE_UNIDADES_CERTIFICAR_INO_SAV
        public ActionResult exportInoSVUnidadesACertificar(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSVUnidadesACertificarGridSettings(), Senasica.GetInoUnidadesCertSVByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSVUnidadesACertificarGridSettings(), Senasica.GetInoUnidadesCertSVByCampania(IdCampania));
        }
        private GridViewSettings GetInoSVUnidadesACertificarGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SV_UnidadesACertificar";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Agrícola - Unidades A Certificar";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdUnidadesCertificarInoSV";
            settings.Columns.Add("TipoUnidad_UCSV.Nombre", "Tipo de Unidad");
            settings.Columns.Add("NumUnidadesTipo", "Num. Unidades por Tipo").PropertiesEdit.DisplayFormatString = "n";

            settings.Columns.AddBand(AnioAntBand =>
            {
                AnioAntBand.Caption = "Año Anterior";
                AnioAntBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioAntBand.Columns.Add("AntProgramado", "Programado").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("AntRealizado", "Realizado").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("AntPorcentajeCumplimiento", "% cumplimiendo").PropertiesEdit.DisplayFormatString = "n";
            });

            settings.Columns.AddBand(AnioActBand =>
            {
                AnioActBand.Caption = "Año Actual";
                AnioActBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioActBand.Columns.Add("ActProgramado", "Programado").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("ActRealizado", "Realizado").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("ActPorcentajeCumplimiento", "% cumplimiendo").PropertiesEdit.DisplayFormatString = "n";
            });

            return settings;
        }
        #endregion

        #region UE_IMPORTANCIA_INO_SAV
        public ActionResult exportInoSVImportanciaCampania(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetInoSVImportanciaCampaniaGridSettings(), Senasica.GetInoImportanciaSVByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetInoSVImportanciaCampaniaGridSettings(), Senasica.GetInoImportanciaSVByCampania(IdCampania));
        }
        private GridViewSettings GetInoSVImportanciaCampaniaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Inocuidad_SV_ImportanciaCampania";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Inocuidad Agrícola - Importancia de la Campaña";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaIno";
            settings.Columns.Add("Especie_Cultivo", "Especie o cultivo");
            settings.Columns.Add("NumUnidadesEspecie", "Número de Unidades por especie").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("TipoProduccion.Nombre", "Tipo de Producción");
            settings.Columns.Add("DestinoProduccion.Nombre", "Mercado Destino");
            settings.Columns.Add("TendenciaProduccion.Nombre", "Tendencia de la Producción");
            settings.Columns.Add("TendenciaProduccion.EsAfectado", "Afectado por Contaminantes");

            settings.Columns.Add("NumUnidadesAfectadas", "Num. Unidades Afectadas").PropertiesEdit.DisplayFormatString = "n";

            return settings;
        }
        #endregion
        //////////////////////  CAMPANIA MOV///////////////

        #region UE_IMPORTANCIA_MOV
        public ActionResult exportMOVImportancia(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetMOVImportanciaGridSettings(), Senasica.GetMovImportanciaPIVByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetMOVImportanciaGridSettings(), Senasica.GetMovImportanciaPIVByCampania(IdCampania));
        }
        private GridViewSettings GetMOVImportanciaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "MOV_ImportanciaPVIS";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Movilización - Importancia PVIS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaPIVMov";
            settings.Columns.Add("NumPIVsFitosanitario", "Núm. PVI's Fitosanitarios").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumPIVsZoosanitario", "Núm PVI's Zoosanitarios").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumPIVsFitoZoosaniatrio", "Núm. PVI's Fitozoosanitarios").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumSitiosInspeccion", "Núm. Sitios de Inspección").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumRutasItinerantes", "Núm. de Rutas Itinerantes").PropertiesEdit.DisplayFormatString = "n";

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumPIVsFitosanitario").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumPIVsZoosanitario").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumPIVsFitoZoosaniatrio").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumSitiosInspeccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumRutasItinerantes").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion

        #region UE_ATENDIDO_MOV
        public ActionResult exportMOVAtendido(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetMOVAtendidoGridSettings(), Senasica.GetMovAtendidoByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetMOVAtendidoGridSettings(), Senasica.GetMovAtendidoByCampania(IdCampania));
        }
        private GridViewSettings GetMOVAtendidoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "MOV_Atendido_Programa";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Movilización - Atendido por el Programa";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdAtendidoMov";
            settings.Columns.Add("NombrePVI_SitioInspeccion", "Nombre del PVI, Sitio de Inspección o Ruta Itinerante");
            settings.Columns.Add("MateriaInspeccion", "Materia de Inspección");
            settings.Columns.AddBand(AnioAntBand =>
            {
                AnioAntBand.Caption = "Año Anterior";
                AnioAntBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioAntBand.Columns.Add("NumInspeccion_Ant", "Núm. de Inspecciones").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("NumMedidasAplicadas_Ant", "Núm. de Medidas Aplicadas").PropertiesEdit.DisplayFormatString = "n";
            });

            settings.Columns.AddBand(AnioActBand =>
            {
                AnioActBand.Caption = "Año Actual (estimado)";
                AnioActBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioActBand.Columns.Add("NumInspeccion_Act", "Núm. de Inspecciones").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("NumMedidasAplicadas_Act", "Núm. de Medidas Aplicadas").PropertiesEdit.DisplayFormatString = "n";
            });

            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumInspeccion_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumMedidasAplicadas_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumInspeccion_Act").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumMedidasAplicadas_Act").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;

            return settings;
        }
        #endregion

        #region UE_IMPORTANCIA_MOV_PROGRAMA
        public ActionResult exportMOVImportanciaPrograma(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetMOVImportanciaProgramaGridSettings(), Senasica.GetMovImportanciaProgByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetMOVImportanciaProgramaGridSettings(), Senasica.GetMovImportanciaProgByCampania(IdCampania));
        }
        private GridViewSettings GetMOVImportanciaProgramaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "MOV_ImportanciaPrograma";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Movilización - Importancia del Programa";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaProgramaMov";
            settings.Columns.Add("NumTotInspeccion", "Núm. Total de Inspecciones").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumRetenciones", "Núm. de Retenciones").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumRetornos", "Núm. de Retornos").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumDestrucciones", "Núm. de Destrucciones").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumGuardaCustodia", "Núm. de Guarda Custodia").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumTratamiento_Acon", "Núm. de Tratamientos / Acondicionamientos").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumCuarentenas", "Núm. de Cuarentenas").PropertiesEdit.DisplayFormatString = "n";

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumTotInspeccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumRetenciones").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumRetornos").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumDestrucciones").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumGuardaCustodia").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumTratamiento_Acon").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumCuarentenas").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion
        //////////////////////  CAMPANIA SAC///////////////

        #region UE_IMPORTANCIA_SAP
        public ActionResult exportSAPImportancia(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSAPImportanciaGridSettings(), Senasica.GetSACImportanciaCultByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSAPImportanciaGridSettings(), Senasica.GetSACImportanciaCultByCampania(IdCampania));
        }
        private GridViewSettings GetSAPImportanciaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "SAP_ImportanciaCultivo";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Sanidad Acuícola y Pesquera - Importancia Económica del Cultivo";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaCultivoSAC";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Especie.Nombre", "Especie");
            settings.Columns.Add("FuncionZootecnica.Nombre", "Fun. Zootécnica");
            settings.Columns.Add("SistemaProduccion.Nombre", "Sistema de Producción");
            settings.Columns.Add("UnidadMedidaProduccion.Nombre", "Unidad de Medida de la Producción");
            settings.Columns.Add("NumUnidadesProduccion", "Núm. Unidades Producción").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("CantidadProduccion", "Cantidad de la producción").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("ValorProduccion", "Valor de la Producción ($)").PropertiesEdit.DisplayFormatString = "c";

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "CantidadProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ValorProduccion").DisplayFormat = "c";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion

        #region UE_ATENDIDO_SAP
        public ActionResult exportSAPAtendido(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSAPAtendidoGridSettings(), Senasica.GetSACAtendidoByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSAPAtendidoGridSettings(), Senasica.GetSACAtendidoByCampania(IdCampania));
        }
        private GridViewSettings GetSAPAtendidoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "SAP_Atendido_Programa";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Sanidad Acuícola y Pesquera - Atendido por el Programa";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdAtendidoSAC";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Especie.Nombre", "Especie");
            settings.Columns.Add("NumUnidadesProduccion", "Núm. Unidades de Producción").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("Enfermedad", "Nombre de la Enfermedad");
            settings.Columns.Add("PrevEnfermedad", "Prevalencia de la Enfermedad").PropertiesEdit.DisplayFormatString = "n";

            settings.Columns.AddBand(AnioAntBand =>
            {
                AnioAntBand.Caption = "Año Anterior";
                AnioAntBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioAntBand.Columns.Add("UProduccionAtend_Ant", "U. Producción Atendidas").PropertiesEdit.DisplayFormatString = "n";
                AnioAntBand.Columns.Add("NumCuarentenas_Ant", "Número de Cuarentenas").PropertiesEdit.DisplayFormatString = "n";
            });

            settings.Columns.AddBand(AnioActBand =>
            {
                AnioActBand.Caption = "Año Actual (estimado)";
                AnioActBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                AnioActBand.Columns.Add("UProduccionAtend_Act", "U. Producción Atendidas").PropertiesEdit.DisplayFormatString = "n";
                AnioActBand.Columns.Add("NumCuarentenas_Act", "Número de Cuarentenas").PropertiesEdit.DisplayFormatString = "n";
            });

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "PrevEnfermedad").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "UProduccionAtend_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumCuarentenas_Ant").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "UProduccionAtend_Act").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumCuarentenas_Act").DisplayFormat = "n";
            settings.Settings.ShowFooter = true;

            return settings;
        }
        #endregion

        #region UE_IMPORTANCIA_SAP_ENFERMEDAD
        public ActionResult exportSAPImportanciaEnfermedad(bool isPDF, int IdCampania)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSAPImportanciaEnfermedadGridSettings(), Senasica.GetSACImportanciaEnfermedadesByCampania(IdCampania))
                         : GridViewExtension.ExportToXls(GetSAPImportanciaEnfermedadGridSettings(), Senasica.GetSACImportanciaEnfermedadesByCampania(IdCampania));
        }
        private GridViewSettings GetSAPImportanciaEnfermedadGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "SAP_ImportanciaEnfermedad";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Sanidad Acuícola y Pesquera - Importancia de la Enfermedad";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdImportanciaEnfermedadSAC";
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Especie.Nombre", "Especie");
            settings.Columns.Add("UnidadMedidaProduccion.Nombre", "Unidad de Medida de la Pérdida");
            settings.Columns.Add("NumUnidadesProduccion", "Núm. Unidades de Producción").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("NumUnidadesProduccionAfectadas", "Núm. Unidades de Producción Afectadas").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("Enfermedad", "Enfermedad / Plaga");
            settings.Columns.Add("CantidadPerdida", "Cantidad de la Pérdida").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("ValorProduccion", "Valor de la Producción ($)").PropertiesEdit.DisplayFormatString = "c";

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProduccion").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "NumUnidadesProduccionAfectadas").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "CantidadPerdida").DisplayFormat = "n";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ValorProduccion").DisplayFormat = "c";
            settings.Settings.ShowFooter = true;
            return settings;
        }
        #endregion
        //////////// CATÁLOGOS DEL SISTEMA ////////////////////

        #region PROGRAMA
        public ActionResult exportPrograma(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetProgramaGridSettings(), Senasica.GetProgramas())
                         : GridViewExtension.ExportToXls(GetProgramaGridSettings(), Senasica.GetProgramas());
        }
        private GridViewSettings GetProgramaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridView2Partial" };

            settings.SettingsExport.FileName = "Programa";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 600;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE PROGRAMAS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdPrograma";
            settings.Columns.Add("Nombre", "Nombre").Width = 600;
            settings.CommandColumn.Width = 600;

            return settings;
        }
        #endregion

        #region COMPONENTE
        public ActionResult exportComponente(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetComponenteGridSettings(), Senasica.GetComponente())
                         : GridViewExtension.ExportToXls(GetComponenteGridSettings(), Senasica.GetComponente());
        }
        private GridViewSettings GetComponenteGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "Componente";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 450;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE COMPONENTES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdComponente";
            settings.Columns.Add("Programa.Nombre", "Programa Institucional").Width = 450;
            settings.Columns.Add("Nombre", "Nombre").Width = 450;
            settings.CommandColumn.Width = 450;
            return settings;
        }
        #endregion

        #region INCENTIVO
        public ActionResult exportIncentivo(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetIncentivoGridSettings(), Senasica.GetIncentivo())
                         : GridViewExtension.ExportToXls(GetIncentivoGridSettings(), Senasica.GetIncentivo());
        }
        private GridViewSettings GetIncentivoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "IncentivoGridViewPartial" };

            settings.SettingsExport.FileName = "INCENTIVOS";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 450;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE INCENTIVOS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdComponente";
            settings.Columns.Add("Componente.Nombre", "Componente de los Programas").Width = 450;
            settings.Columns.Add("Nombre", "Nombre").Width = 450;
            settings.CommandColumn.Width = 450;

            return settings;
        }
        #endregion

        #region SUBCOMPONETE
        public ActionResult exportSubComponente(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetSubComponenteGridSettings(), Senasica.GetSubComponente())
                         : GridViewExtension.ExportToXls(GetSubComponenteGridSettings(), Senasica.GetSubComponente());
        }
        private GridViewSettings GetSubComponenteGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "SubComponente";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 450;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE SUBCOMPONENTES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdIncentivo";
            settings.Columns.Add("Incentivo.Nombre", "Incentivo").Width = 450;
            settings.Columns.Add("Nombre", "Sub-Componente").Width = 450;
            settings.CommandColumn.Width = 450;

            return settings;
        }
        #endregion

        #region PROYECTOAUTORIZADO
        public ActionResult exportProyectoAutorizado(bool isPDF)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            return isPDF ? GridViewExtension.ExportToPdf(GetProyectoAutorizadoGridSettings(), Senasica.GetProyectoAutorizadoXUsuarios_Pres(rolusuario, IdAnio))
                         : GridViewExtension.ExportToXls(GetProyectoAutorizadoGridSettings(), Senasica.GetProyectoAutorizadoXUsuarios_Pres(rolusuario, IdAnio));
        }
        private GridViewSettings GetProyectoAutorizadoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "ProyectoAutorizado";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 450;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 120;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE PROYECTOS AUTORIZADOS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProyectoAutorizado";
            settings.Columns.Add("SubComponente.Nombre", "Subcomponente").Width = 450;
            settings.Columns.Add("Nombre", "Nombre del Proyecto Autorizado").Width = 450;
            settings.CommandColumn.Width = 450;

            return settings;
        }
        #endregion

        #region PROYECTOPROSUPUESTO
        public ActionResult exportProyectoPresupuesto(bool isPDF, string estado_param)
        {
            int IdAnioPresupuestal = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            IEnumerable model = null;
            model = estado_param == null ? Senasica.GetProyectosPresupuestalesByRol() : Senasica.GetProyectosPresupuestalByEstado(estado_param);

            return isPDF ? GridViewExtension.ExportToPdf(GetProyectoPresupuestoGridSettings(), model)
                         : GridViewExtension.ExportToXls(GetProyectoPresupuestoGridSettings(), model);
        }
        private GridViewSettings GetProyectoPresupuestoGridSettings()
        {

            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "PROYECTOS AUTORIZADOS POR ESTADO";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "PRESUPUESTO PARA LOS PROYECTOS AUTORIZADOS POR ESTADO";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProyectoPresupuesto";
            settings.Columns.Add("UnidadResponsable.Nombre", "Unidad Responsable");
            settings.Columns.Add("UnidadEjecutora.Nombre", "Instancia Ejecutora");
            settings.Columns.Add("Estado.Nombre", "Estado");
            settings.Columns.Add("ProyectoAutorizado.SubComponente.Nombre", "Concepto de Apoyo");
            settings.Columns.Add("ProyectoAutorizado.Nombre", "Proyecto Autorizado");
            settings.Columns.Add("TipoCampania.Nombre", "Tipo Campaña");
            settings.Columns.Add("RF_Anual", "Recurso Federal").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("RE_Anual", "Recurso Estatal").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("RP_Anual", "Recurso Productores").PropertiesEdit.DisplayFormatString = "c";

            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RF_Anual").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RE_Anual").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RP_Anual").DisplayFormat = "c";
            return settings;
        }
        #endregion

        #region TIPOACCION
        public ActionResult exportTipoAccion(bool isPDF)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            return isPDF ? GridViewExtension.ExportToPdf(GetTipoAccionGridSettings(), Senasica.GetTiposDeAccionXSubComp(rolusuario, IdAnio))
                         : GridViewExtension.ExportToXls(GetTipoAccionGridSettings(), Senasica.GetTiposDeAccionXSubComp(rolusuario, IdAnio));
        }

        private GridViewSettings GetTipoAccionGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };
            settings.SettingsExport.FileName = "TIPO DE ACCIÓN";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO TIPO DE ACCIÓN";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IDTipoDeAccion";
            settings.Columns.Add("ProyectoAutorizado.SubComponente.Nombre", "SubComponente");
            settings.Columns.Add("ProyectoAutorizado.Nombre", "Proyecto Autorizado");
            settings.Columns.Add("Nombre", "Nombre del Tipo de Acción");

            return settings;
        }
        #endregion

        #region TIPODEACTIVIDAD
        public ActionResult exportTipoActividad(bool isPDF)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();
            ViewData["rolusuario"] = rolusuario;
            return isPDF ? GridViewExtension.ExportToPdf(GetTipoActividadGridSettings(), Senasica.GetTiposDeActividadesXSubComp(rolusuario, IdAnio))
                         : GridViewExtension.ExportToXls(GetTipoActividadGridSettings(), Senasica.GetTiposDeActividadesXSubComp(rolusuario, IdAnio));
        }

        private GridViewSettings GetTipoActividadGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "TIPO DE ACTIVIDAD";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 50;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO TIPO DE ACTIVIDAD";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdTipoActividad";
            settings.Columns.Add("TipoDeAccion.ProyectoAutorizado.SubComponente.Nombre", "SubComponente");
            settings.Columns.Add("TipoDeAccion.ProyectoAutorizado.Nombre", "Proyecto Autorizado");
            settings.Columns.Add("TipoDeAccion.Nombre", "Tipo de Acción");
            settings.Columns.Add("Nombre", "Nombre del Tipo de Actividad");
            settings.Columns.Add("UnidadDeMedida.Nombre", "Unidad de Medida");

            return settings;
        }
        #endregion

        #region ESTADO
        public ActionResult exportEstado(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetEstadoGridSettings(), Senasica.GetEstados())
                         : GridViewExtension.ExportToXls(GetEstadoGridSettings(), Senasica.GetEstados());
        }

        private GridViewSettings GetEstadoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "Estado";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 600;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 150;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE ESTADOS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProyectoAutorizado";
            settings.Columns.Add("Nombre", "Nombre").Width = 600;
            settings.CommandColumn.Width = 600;

            return settings;
        }
        #endregion

        #region MUNICIPIO
        public ActionResult exportMunicipio(bool isPDF)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            var model = db.Municipios;
            return isPDF ? GridViewExtension.ExportToPdf(GetMunicipoGridSettings(), model.ToList())
                         : GridViewExtension.ExportToXls(GetMunicipoGridSettings(), model.ToList());
        }

        private GridViewSettings GetMunicipoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "Municipios";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 600;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 150;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE MUNICIPIOS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdMunicipio";
            settings.Columns.Add("Estado.Nombre", "Estado").Width = 600;
            settings.Columns.Add("Nombre", "Municipio").Width = 600;
            settings.CommandColumn.Width = 600;


            return settings;
        }
        #endregion

        #region TiposDeUnidad
        public ActionResult exportTiposDeUnidad(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetTiposDeUnidadGridSettings(), Senasica.GetTiposDeUnidad())
                         : GridViewExtension.ExportToXls(GetTiposDeUnidadGridSettings(), Senasica.GetTiposDeUnidad());
        }

        private GridViewSettings GetTiposDeUnidadGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "TIPO DE INSTANCIA EJECUTORA";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 600;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 150;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO TIPO DE INSTANCIA EJECUTORA";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdTipoDeUnidad";
            settings.Columns.Add("Nombre", "Nombre").Width = 600;
            settings.CommandColumn.Width = 600;


            return settings;
        }
        #endregion

        #region UnidadesResponsables
        public ActionResult exportUnidadesResponsables(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetUnidadesResponsablesGridSettings(), Senasica.GetUnidadesResponsables())
                         : GridViewExtension.ExportToXls(GetUnidadesResponsablesGridSettings(), Senasica.GetUnidadesResponsables());
        }

        private GridViewSettings GetUnidadesResponsablesGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "UNIDAD RESPONSABLE"; settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 800;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 150;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE UNIDADES RESPONSABLES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;



            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProyectoAutorizado";

            settings.Columns.Add("Nombre", "Nombre").Width = 800;
            settings.CommandColumn.Width = 800;

            return settings;
        }
        #endregion

        #region TIPOSDERECURSOS
        public ActionResult exportTiposDeRecurso(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetTiposDeRecursoGridSettings(), Senasica.GetTiposDeRecurso())
                         : GridViewExtension.ExportToXls(GetTiposDeRecursoGridSettings(), Senasica.GetTiposDeRecurso());
        }
        private GridViewSettings GetTiposDeRecursoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "TIPOS DE RECURSOS";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 800;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 150;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE TIPOS DE RECURSOS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdTipoDeRecurso";
            settings.Columns.Add("Nombre", "Nombre").Width = 800;
            settings.CommandColumn.Width = 800;


            return settings;
        }
        #endregion

        #region TIPOINSTALACIÓN
        public ActionResult exportTipoDeInstalacion(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetTipoDeInstalacionGridSettings(), Senasica.GetTipoDeInstalacion())
                         : GridViewExtension.ExportToXls(GetTipoDeInstalacionGridSettings(), Senasica.GetTipoDeInstalacion());
        }
        private GridViewSettings GetTipoDeInstalacionGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "TIPOS DE INSTALACIÓN";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 800;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 150;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO TIPO DE INSTALACIÓN";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdTipoDeInstalacion";
            settings.Columns.Add("Nombre", "Nombre").Width = 800;
            settings.CommandColumn.Width = 800;

            return settings;
        }
        #endregion

        #region UNIDADDEMEDIDA
        public ActionResult exportUnidadesDeMedida(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetUnidadesDeMedidaGridSettings(), Senasica.GetUnidadesDeMedida())
                         : GridViewExtension.ExportToXls(GetUnidadesDeMedidaGridSettings(), Senasica.GetUnidadesDeMedida());
        }
        private GridViewSettings GetUnidadesDeMedidaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "UNIDAD DE MEDIDA";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 800;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 150;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE UNIDADES DE MEDIDA";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdUnidadDeMedida";
            settings.Columns.Add("Nombre", "Nombre").Width = 800;
            settings.CommandColumn.Width = 800;

            return settings;
        }
        #endregion

        #region PRODUCTORES
        public ActionResult exportProductores(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetProductoresGridSettings(), Senasica.GetProductores())
                         : GridViewExtension.ExportToXls(GetProductoresGridSettings(), Senasica.GetProductores());
        }
        private GridViewSettings GetProductoresGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "Padrón de Unidades de Producción";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 80;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = " REGISTRO DEL PADRÓN DE UNIDADES DE PRODUCCIÓN";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProductor";
            settings.Columns.Add("RazonSocial", "Razón Social");
            settings.Columns.Add("ApellidoPaterno", "Apellido Paterno");
            settings.Columns.Add("ApellidoMaterno", "Apellido Materno");
            settings.Columns.Add("Nombre");
            settings.Columns.Add("Direccion", "Dirección");
            settings.Columns.Add("Colonia");
            settings.Columns.Add("Estado.Nombre", "Estado");
            settings.Columns.Add("Municipio.Nombre", "Municipio");
            settings.Columns.Add("Telefono", "Teléfono");
            settings.Columns.Add("Email");
            settings.Columns.Add("Ubicacion.Latitude", "Latitud");
            settings.Columns.Add("Ubicacion.Longitude", "Longitud");

            return settings;
        }
        #endregion

        #region ESTADOBIEN
        public ActionResult exportEstadoBien(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetEstadoBienGridSettings(), Senasica.GetEstadoBien())
                         : GridViewExtension.ExportToXls(GetEstadoBienGridSettings(), Senasica.GetEstadoBien());
        }
        private GridViewSettings GetEstadoBienGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "ESTADO DE BIENES";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 800;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 150;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO ESTADO DE LOS BIENES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.KeyFieldName = "Pk_IdEstadoBien";
            settings.Columns.Add("Nombre", "Nombre").Width = 800;
            settings.CommandColumn.Width = 800;

            return settings;
        }
        #endregion

        #region PROVEEDORES
        public ActionResult exportProveedor(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetProveedorGridSettings(), Senasica.GetProveedor())
                         : GridViewExtension.ExportToXls(GetProveedorGridSettings(), Senasica.GetProveedor());
        }
        private GridViewSettings GetProveedorGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "PROVEEDORES";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 250;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 30;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE PROVEEDORES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProveedor";
            settings.Columns.Add("RazonSocial_Nombre", "Nombre o Razon Social").Width = 250;
            settings.Columns.Add("RFC").Width = 250;
            settings.Columns.Add("Direccion", "Dirección calle y número").Width = 250;
            settings.Columns.Add("Colonia", "Colonia/Localidad").Width = 250;
            settings.Columns.Add("Estado.Nombre", "Estado").Width = 250;
            settings.Columns.Add("Telefono", "Teléfono").Width = 250;
            settings.Columns.Add("Email", "Correo Electrónico").Width = 250;
            settings.CommandColumn.Width = 250;


            return settings;
        }
        #endregion

        #region PersonasRegistradas
        public ActionResult exportUsers(bool isPDF)
        {
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);

            return isPDF ? GridViewExtension.ExportToPdf(GetUsersGridSettings(), Senasica.GetUsersWithID(_Fk_IdUnidadEjecutora))
                        : GridViewExtension.ExportToXls(GetUsersGridSettings(), Senasica.GetUsersWithID(_Fk_IdUnidadEjecutora));
        }

        private GridViewSettings GetUsersGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "PERSONAS CON ACCESO AL SISTEMA";

            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 250;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 0;
            settings.SettingsExport.PageHeader.Center = "PERSONAS CON ACCESO AL SISTEMA";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Estado", "Estado").Width = 70;
            settings.Columns.Add("InstanciaEjecutora", "Instancia Ejecutora").Width = 70;
            settings.Columns.Add("UserName", "ID Usuario").Width = 70;
            settings.Columns.Add("NombrePersona", "Nombre de Usuario").Width = 70;
            settings.Columns.Add("Email", "Correo Electrónico").Width = 70;
            settings.Columns.Add("Puesto", "Puesto").Width = 70;
            settings.CommandColumn.Width = 70;
            return settings;
        }
        #endregion

        /////////////////////  CATÁLOGOS DEL ADMINISTRATIVOS  ///////


        #region CapituloPartida
        public ActionResult exportCapitulosPartida(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetCapitulosPartidaGridSettings(), Senasica.GetCapitulosPartida())
                         : GridViewExtension.ExportToXls(GetCapitulosPartidaGridSettings(), Senasica.GetCapitulosPartida());
        }

        private GridViewSettings GetCapitulosPartidaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "CONCEPTO PARTIDA";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 800;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE CONCEPTOS DE GASTOS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdCapituloPartida";
            settings.Columns.Add("Nombre").Width = 800;
            settings.CommandColumn.Width = 800;

            return settings;
        }
        #endregion

        #region CONCEPTOPARTIDA
        public ActionResult exportConceptosPartida(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetConceptosPartidaGridSettings(), Senasica.GetConceptosPartida())
                         : GridViewExtension.ExportToXls(GetConceptosPartidaGridSettings(), Senasica.GetConceptosPartida());
        }
        private GridViewSettings GetConceptosPartidaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "CONCEPTO PARTIDA";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 800;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE CONCEPTOS PRESUPUESTALES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdConceptoPartida";
            settings.Columns.Add("CapituloPartida1.Nombre", "Capítulo").Width = 800;
            settings.Columns.Add("Nombre").Width = 800;
            settings.CommandColumn.Width = 800;

            return settings;
        }
        #endregion

        #region PARTIDA
        public ActionResult exportPartida(bool isPDF)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            var model = db.Partidas;
            return isPDF ? GridViewExtension.ExportToPdf(GetPartidaGridSettings(), model.ToList())
                         : GridViewExtension.ExportToXls(GetPartidaGridSettings(), model.ToList());
        }


        private GridViewSettings GetPartidaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "CATALOGO DE PARTIDAS";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE PARTIDAS PRESUPUESTALES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdPartida";
            settings.Columns.Add("ConceptoPartida.CapituloPartida1.Nombre", "Capítulo de Gasto");
            settings.Columns.Add("ConceptoPartida.Nombre", "Concepto de Gasto");
            settings.Columns.Add("Nombre", "Partida");

            return settings;
        }
        #endregion

        #region TIPODEBIEN
        public ActionResult exportTiposDeBien(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetTiposDeBienGridSettings(), Senasica.GetTiposDeBien())
                         : GridViewExtension.ExportToXls(GetTiposDeBienGridSettings(), Senasica.GetTiposDeBien());
        }

        private GridViewSettings GetTiposDeBienGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "TIPO DE BIEN";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 200;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE TIPOS DE BIENES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdTipoDeBien";
            settings.Columns.Add("Partida.ConceptoPartida.CapituloPartida1.Nombre", "Capítulo");
            settings.Columns.Add("Partida.ConceptoPartida.Nombre", "Concepto");
            settings.Columns.Add("Partida.Nombre", "Partida");
            settings.Columns.Add("Nombre");

            return settings;
        }
        #endregion

        #region BIENOSERVICIO
        public ActionResult exportBienesOServ(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetBienesOServGridSettings(), Senasica.GetBienesOServ())
                         : GridViewExtension.ExportToXls(GetBienesOServGridSettings(), Senasica.GetBienesOServ());
        }
        private GridViewSettings GetBienesOServGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "BIEN O SERVICIO";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 500;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 220;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "CATÁLOGO DE BIENES O SERVICIOS";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 14;

            settings.SettingsExport.ReportHeader = "Catalogo de Municipios";
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdBienOServ";
            settings.Columns.Add("TipoDeBien", "Tipo de Bien").Width = 500;
            settings.Columns.Add("CABMS").Caption = "CLAVE";
            settings.Columns.Add("Nombre").Width = 500;
            settings.Columns.Add("Especificacion", "Especificación").Width = 500;
            settings.CommandColumn.Width = 500;

            return settings;
        }
        #endregion


        /// ///////////////////// REPORTES ////////////////////////

        #region ReporteActividadesRealizadas
        public ActionResult exportActividadesRealizadas(bool isPDF, string Estado)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? persona = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdPersona__SIS);
            int? IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            String rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            return isPDF ? GridViewExtension.ExportToPdf(ActividadesAsignadasGridSettings(), Senasica.GetActividadesAsignadasByPersona(rolusuario, IdUnidadEjecutora, persona, IdAnio, Estado))
                       : GridViewExtension.ExportToXls(ActividadesAsignadasGridSettings(), Senasica.GetActividadesAsignadasByPersona(rolusuario, IdUnidadEjecutora, persona, IdAnio, Estado));
        }

        private GridViewSettings ActividadesAsignadasGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Reporte de Actividades Realizadas";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Reporte de Actividades Realizadas";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdActividad";
            settings.Columns.Add("Persona.NombreCompleto", "Actividad Asignada a:");
            settings.Columns.Add("Descripcion");
            settings.Columns.Add("Fecha_Inicio");
            settings.Columns.Add("FechaFin");
            settings.Columns.Add("Asignado_Anual");
            settings.Columns.Add("StatusActividad.Nombre");

            return settings;
        }
        #endregion

        #region ProgramaAnualAdq
        public ActionResult exportProgramaAnual(bool isPDF, string Estado)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;
            return isPDF ? GridViewExtension.ExportToPdf(GetProgramaAnlGridSettings(), Senasica.GetProgramaAnualAdqCompletoByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio, Estado))
                         : GridViewExtension.ExportToXls(GetProgramaAnlGridSettings(), Senasica.GetProgramaAnualAdqCompletoByUnidadEjecutora(_Fk_IdUnidadEjecutora, IdAnio, Estado));
        }
        private GridViewSettings GetProgramaAnlGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "REPORTE DE ADQUISICIONES";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 250;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 30;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "REPORTE DE ADQUISICIONES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProgramaAnualAdq";
            settings.Columns.Add("IE", "Instancia Ejecutora").Width = 250;
            settings.Columns.Add("Campania", "Campaña").Width = 250;
            settings.Columns.Add("AccionXCampania", "Accion").Width = 250;
            settings.Columns.Add("BienOServ", "Bien o Servicio").Width = 250;
            settings.Columns.Add("UnidadMedida", "Unidad de Medida").Width = 250;
            settings.Columns.Add("Cant_Anual", "Cantidad Anual").Width = 250;
            settings.Columns.Add("imp_Anual", "Importe ").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("TipoRecurso", "Tipo de Recurso").Width = 100;
            settings.Columns.Add("Gastos").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("EstadoDeGasto", "Estado de Gastos").Width = 250;
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "imp_Anual").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Cant_Anual");
            settings.CommandColumn.Width = 250;
            return settings;
        }
        #endregion


        //////// APENDICE///////

        #region PEFXEstado
        public ActionResult exportPEFXEstado(bool isPDF)
        {

            return isPDF ? GridViewExtension.ExportToPdf(PEFXEstadoGridSettings(), Senasica.GetPEFXEstado())
                       : GridViewExtension.ExportToXls(PEFXEstadoGridSettings(), Senasica.GetPEFXEstado());
        }

        private GridViewSettings PEFXEstadoGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Distribución del Presupuesto de Egresos de la Federación por Estado";

            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.MaxColumnWidth = 99;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Distribución del Presupuesto de Egresos de la Federación por Estado";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdPEFXEstado";

            settings.Columns.Add("Anio.Anio1", "Año");
            settings.Columns.Add("Estado.Nombre", "Estado");

            settings.Columns.AddBand(TransversalesBand =>
            {
                TransversalesBand.Caption = "Proyectos Estrategicos Transversales";
                TransversalesBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                TransversalesBand.Columns.Add("SistemaInform", "Sistema Informático").PropertiesEdit.DisplayFormatString = "C";
                TransversalesBand.Columns.Add("Capacitacion", "Capacitación").PropertiesEdit.DisplayFormatString = "C";
                TransversalesBand.Columns.Add("Divulgacion", "Divulgacion").PropertiesEdit.DisplayFormatString = "C";
                TransversalesBand.Columns.Add("Emergencias", "Emergencias Sanitarias").PropertiesEdit.DisplayFormatString = "C";
                TransversalesBand.Columns.Add("UISEstatales", "UIS Estatales").PropertiesEdit.DisplayFormatString = "C";
                TransversalesBand.Columns.Add("Gtotransv", "Total").PropertiesEdit.DisplayFormatString = "C";
            });

            settings.Columns.AddBand(ComponentesBand =>
            {
                ComponentesBand.Caption = "Componentes Previstos en Proyectos del PEF";
                ComponentesBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                ComponentesBand.Columns.Add("Comp_CampFitoZoo", "Campañas FitoZoosanitarias").PropertiesEdit.DisplayFormatString = "C";
                ComponentesBand.Columns.Add("Comp_InspVig", "Insp. y Vig. No Cuarentenaria").PropertiesEdit.DisplayFormatString = "C";
                ComponentesBand.Columns.Add("Comp_VigCuarentenaria", "Vig. Cuarentenaria").PropertiesEdit.DisplayFormatString = "C";
                ComponentesBand.Columns.Add("Comp_IAAP", "Inocuidad Agroalim. Ac. y Pesq.").PropertiesEdit.DisplayFormatString = "C";
                ComponentesBand.Columns.Add("Componentes", "Total").PropertiesEdit.DisplayFormatString = "C";


            });

            settings.Columns.Add("GtoInversion", "Gastos de Inversión").PropertiesEdit.DisplayFormatString = "C";
            settings.Columns.Add("GtoOperacion", "Gastos de Operación").PropertiesEdit.DisplayFormatString = "C";
            settings.Columns.Add("Importe", "Presupuesto Autorizado").PropertiesEdit.DisplayFormatString = "C";

            //Totales
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Gtotransv").DisplayFormat = "c";

            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Comp_CampFitoZoo").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Comp_InspVig").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Comp_VigCuarentenaria").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Comp_IAAP").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Componentes").DisplayFormat = "c";

            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "GtoInversion").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "GtoOperacion").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Importe").DisplayFormat = "c";

            settings.Settings.ShowFooter = true;
            settings.CommandColumn.Width = 99;


            return settings;
        }
        #endregion

        #region PresupuestoXDireccionGeneral
        public ActionResult exportPresupuestoXDireccionGeneral(bool isPDF)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();

            var _data = db.VW_PresupuestoXDFG.ToList();

            return isPDF ? GridViewExtension.ExportToPdf(PresupuestoXDireccionGeneralGridSettings(), _data.ToList())
                       : GridViewExtension.ExportToXls(PresupuestoXDireccionGeneralGridSettings(), _data.ToList());
        }

        private GridViewSettings PresupuestoXDireccionGeneralGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "PRESUPUESTO POR ESTADOS Y DIRECCIONES GENERALES";

            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 600;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 90;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "PRESUPUESTO POR ESTADOS Y DIRECCIONES GENERALES";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Id";
            settings.Columns.Add("Estado", "Estado");
            settings.Columns.Add("DGSA", "DGSA").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("DGSV", "DGSV").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("DGIAAP", "DGIAAP").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("DGIF", "DGIF").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("TotalEstado", "Total suma DG").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("GastoInversion", "Gastos de Inversión").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("Restante", "Presupuesto Disponible").PropertiesEdit.DisplayFormatString = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "DGSA").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "DGSV").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "DGIAAP").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "DGIF").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalEstado").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "GastoInversion").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Restante").DisplayFormat = "c";
            settings.Settings.ShowFooter = true;
            settings.CommandColumn.Width = 600;




            return settings;
        }
        #endregion

        /// /////// MINISTRACIÓN//////////////

        #region Ministracion
        public ActionResult exportMinistracion(bool isPDF)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();


            return isPDF ? GridViewExtension.ExportToPdf(MinistracionGridSettings(), Senasica.GetMinistraciones())
                       : GridViewExtension.ExportToXls(MinistracionGridSettings(), Senasica.GetMinistraciones());

        }

        private GridViewSettings MinistracionGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Ministración por Comité";

            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 600;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 90;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "MINISTRACIÓN POR COMITÉ";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsText.ContextMenuSortAscending = "asc";
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdMinistracionesXComite";
            settings.Columns.Add("UnidadEjecutora.Nombre", "Comité");
            settings.Columns.Add("TipoDeRecurso.Nombre", "Recurso");
            settings.Columns.Add("Fecha", MVCxGridViewColumnType.DateEdit);
            settings.Columns.Add("Importe", "Importe Total").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("ImporteDetalles", "Reportado en la Campaña").PropertiesEdit.DisplayFormatString = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Importe").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ImporteDetalles").DisplayFormat = "c";
            settings.Settings.ShowFooter = true;
            settings.CommandColumn.Width = 600;
            return settings;
        }
        #endregion

        #region Ministracion detalle
        public ActionResult exportMinistracion_Detalle(bool isPDF, int? IdMinistracionesXComite)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetMinistracionDetalleGridSettings(), Senasica.GetDetalleMinistracionByMinistracion(IdMinistracionesXComite))
                         : GridViewExtension.ExportToXls(GetMinistracionDetalleGridSettings(), Senasica.GetDetalleMinistracionByMinistracion(IdMinistracionesXComite));
        }
        private GridViewSettings GetMinistracionDetalleGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Ministraciones Detalle";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Ministraciones Detalle";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdMinistracionDetalle";
            settings.Columns.Add("Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre", "Comité");
            settings.Columns.Add("Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre", "Proyecto Autorizado");
            settings.Columns.Add("MinistracionesXComite.Fecha", "Fecha").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("MinistracionesXComite.TipoDeRecurso.Nombre", "Tipo de Recurso");
            settings.Columns.Add("Importe", "Importe Registrado").PropertiesEdit.DisplayFormatString = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Importe").DisplayFormat = "c";
            return settings;
        }
        #endregion

        #region Camapania
        public ActionResult exportCampania_N(bool isPDF)
        {

            return isPDF ? GridViewExtension.ExportToPdf(GetGridSettingsCampanias_N_Excel(), Senasica.GetCampanias())
                         : GridViewExtension.ExportToXls(GetGridSettingsCampanias_N_Excel(), Senasica.GetCampanias());
        }

        public ActionResult exportCampania_U(bool isPDF)
        {

            return isPDF ? GridViewExtension.ExportToPdf(GetGridSettingsCampanias_N_Excel(), Senasica.GetCampaniasU())
                         : GridViewExtension.ExportToXls(GetGridSettingsCampanias_N_Excel(), Senasica.GetCampaniasU());
        }

        private GridViewSettings GetGridSettingsCampanias_N_Excel()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Proyectos de Campañas";

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "REGISTRO DE PROGRAMAS DE TRABAJO";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdCampania";
            settings.Columns.Add("IE", "Instancia Ejecutora");
            settings.Columns.Add("SubComponente", "Concepto de Apoyo");
            settings.Columns.Add("ProyectoAutorizado", "Proyecto Autorizado");
            settings.Columns.Add("FechaInicio", "Fecha Inicio").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("FechaFin", "Fecha Fin").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("StatusActual", "Status Actual");
            settings.Columns.Add("FechaModificacionStatus", "Fecha Modificación Status").PropertiesEdit.DisplayFormatString = "d";

            settings.Columns.AddBand(band =>
            {
                band.Caption = "Recurso Federal";
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                band.Columns.Add("RecFed", "Autorizado").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("RecSol_Fed", "GRC").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("MontoTransversales_Fed", "GFO").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("presSinRegistrar_Fed", "Por Registrar").PropertiesEdit.DisplayFormatString = "c";
            });

            settings.Columns.AddBand(band =>
            {
                band.Caption = "Recurso Estatal";
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                band.Columns.Add("RecEst", "Autorizado").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("RecSol_Est", "GRC").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("MontoTransversales_Est", "GFO").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("presSinRegistrar_Est", "Por Registrar").PropertiesEdit.DisplayFormatString = "c";
            });

            settings.Columns.AddBand(band =>
            {
                band.Caption = "Recurso Productores";
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                band.Columns.Add("RecPro", "Autorizado").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("RecSol_Pro", "GRC").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("MontoTransversales_Pro", "GFO").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("presSinRegistrar_Pro", "Por Registrar").PropertiesEdit.DisplayFormatString = "c";
            });

            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecSol_Fed").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecSol_Est").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecSol_Pro").DisplayFormat = "c";
            return settings;
        }
        #endregion

        #region ProgramaAnualAdq_
        public ActionResult exportProgramaAnualAdq_(bool isPDF)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;
            int? _Fk_IdUnidadResponsable = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string _rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            if (_rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || _rolusuario == RolesUsuarios.IE_ANIMAL
                || _rolusuario == RolesUsuarios.IE_INOCUIDAD
                || _rolusuario == RolesUsuarios.IE_MOVILIZACION
                || _rolusuario == RolesUsuarios.IE_VEGETAL
                || _rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                || _rolusuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                || _rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || _rolusuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                || _rolusuario == RolesUsuarios.PUESTO_GERENTE
                || _rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA)
            {
                return isPDF ? GridViewExtension.ExportToPdf(GetProgramaAnualGridSettings(), Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, false))
                             : GridViewExtension.ExportToXls(GetProgramaAnualGridSettings(), Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, false));
            }
            else
            {
                return isPDF ? GridViewExtension.ExportToPdf(GetProgramaAnualGridSettings2(), Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, false))
                             : GridViewExtension.ExportToXls(GetProgramaAnualGridSettings2(), Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, false));
            }
        }

        public ActionResult exportProgramaAnualAdq_U(bool isPDF)
        {
            int IdAnio = Convert.ToInt32(Session["IdAnio"]);
            int? _Fk_IdUnidadEjecutora = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            ViewBag.UEID = _Fk_IdUnidadEjecutora;
            int? _Fk_IdUnidadResponsable = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadResponsable__UE);
            int? _Fk_IdEstado = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdEstado__SIS);
            string _rolusuario = ((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString();

            if (_rolusuario == RolesUsuarios.IE_ACUICOLA_PESQUERA
                || _rolusuario == RolesUsuarios.IE_ANIMAL
                || _rolusuario == RolesUsuarios.IE_INOCUIDAD
                || _rolusuario == RolesUsuarios.IE_MOVILIZACION
                || _rolusuario == RolesUsuarios.IE_VEGETAL
                || _rolusuario == RolesUsuarios.IE_PROGRAMAS_U
                || _rolusuario == RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                || _rolusuario == RolesUsuarios.COORDINADOR_CAMPANIA
                || _rolusuario == RolesUsuarios.PUESTO_COOR_PROYECTO
                || _rolusuario == RolesUsuarios.PUESTO_GERENTE
                || _rolusuario == RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA)
            {
                return isPDF ? GridViewExtension.ExportToPdf(GetProgramaAnualGridSettings(), Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true))
                             : GridViewExtension.ExportToXls(GetProgramaAnualGridSettings(), Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
            else
            {
                return isPDF ? GridViewExtension.ExportToPdf(GetProgramaAnualGridSettings2(), Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true))
                             : GridViewExtension.ExportToXls(GetProgramaAnualGridSettings2(), Senasica.GetProgramaAnualAdqByUnidadEjecutora(_rolusuario, _Fk_IdUnidadEjecutora, _Fk_IdUnidadResponsable, IdAnio, _Fk_IdEstado, true));
            }
        }

        private GridViewSettings GetProgramaAnualGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "Programa Anual de Adquisiciones";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 250;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 30;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Programa Anual de Adquisiciones";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProgramaAnualAdq";
            settings.Columns.Add("UnidadEjecutora.Nombre", "Instancia Ejecutora");
            settings.Columns.Add("BienOServ.Nombre", "Bien/Servicio");
            settings.Columns.Add("UnidadDeMedida.Nombre", "U/Medida");
            settings.Columns.Add("Cant_Anual", "Cantidad Anual").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("imp_Anual", "Importe Anual").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.AddBand(RecursosBand =>
            {
                RecursosBand.Caption = "Origen de Recursos";
                RecursosBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                RecursosBand.Columns.Add("RecFed", "Federales").PropertiesEdit.DisplayFormatString = "c";
                RecursosBand.Columns.Add("RecEst", "Estatales").PropertiesEdit.DisplayFormatString = "c";
                RecursosBand.Columns.Add("RecProp", "Propios").PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add("Justificacion", "Justificación");
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecFed").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecEst").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecProp").DisplayFormat = "c";
            settings.CommandColumn.Width = 250;
            return settings;
        }

        private GridViewSettings GetProgramaAnualGridSettings2()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "Programa Anual de Adquisiciones";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 250;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 30;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Programa Anual de Adquisiciones";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdProgramaAnualAdq";
            settings.Columns.Add("IE", "Instancia Ejecutora");
            settings.Columns.Add("BienOServ", "Bien/Servicio");
            settings.Columns.Add("UnidadDeMedida", "U/Medida");
            settings.Columns.Add("Cant_Anual", "Cantidad Anual").PropertiesEdit.DisplayFormatString = "n";
            settings.Columns.Add("imp_Anual", "Importe Anual").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.AddBand(RecursosBand =>
            {
                RecursosBand.Caption = "Origen de Recursos";
                RecursosBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                RecursosBand.Columns.Add("RecFed", "Federales").PropertiesEdit.DisplayFormatString = "c";
                RecursosBand.Columns.Add("RecEst", "Estatales").PropertiesEdit.DisplayFormatString = "c";
                RecursosBand.Columns.Add("RecProp", "Propios").PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add("Justificacion", "Justificación");
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecFed").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecEst").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecProp").DisplayFormat = "c";
            settings.CommandColumn.Width = 250;
            return settings;
        }
        #endregion

        #region Reprogramación Presupuesto Federal
        public ActionResult exportReprogramacionPresFed(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetexportReprogramacionPresFedGridSettings(), Senasica.GetPresupuestoCampanias())
                         : GridViewExtension.ExportToXls(GetexportReprogramacionPresFedGridSettings(), Senasica.GetPresupuestoCampanias());
        }

        public ActionResult exportReprogramacionPresFedByEstado(bool isPDF, int IdEstado)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetexportReprogramacionPresFedGridSettings(), Senasica.GetPresupuestoCampanias(IdEstado))
                         : GridViewExtension.ExportToXls(GetexportReprogramacionPresFedGridSettings(), Senasica.GetPresupuestoCampanias(IdEstado));
        }

        private GridViewSettings GetexportReprogramacionPresFedGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Reprogramación del Recurso Federal";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 250;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 30;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Reprogramación del Recurso Federal";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdCampania";

            settings.Columns.Add("UnidadResponsable", "Unidad Responsable").Width = Unit.Percentage(20);
            settings.Columns.Add("Estado", "Estado").Width = Unit.Percentage(15);
            settings.Columns.Add("UnidadEjecutora", "Instancia Ejecutora").Width = Unit.Percentage(20);
            settings.Columns.Add("Proyecto", "Campaña").Width = Unit.Percentage(20);
            settings.Columns.Add("StatusKardex", "Status").Width = Unit.Percentage(18);

            settings.Columns.AddBand(band =>
            {
                band.Caption = "Recurso Federal";
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                band.Columns.Add("RecFed", "Autorizado").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("RecSol_Fed", "Programado").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("Rec_Gastado", "Gastado").PropertiesEdit.DisplayFormatString = "c";
                band.Columns.Add("SaldoDisponible", "Disponible").PropertiesEdit.DisplayFormatString = "c";
            });

            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecFed").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "RecSol_Fed").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Rec_Gastado").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "SaldoDisponible").DisplayFormat = "c";

            settings.CommandColumn.Width = 250;
            return settings;
        }
        #endregion

        #region CierreMensual
        public ActionResult exportCierreMensual(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetCierreMensualGridSettings(), Senasica.GetCierreMensual())
                         : GridViewExtension.ExportToXls(GetCierreMensualGridSettings(), Senasica.GetCierreMensual());
        }
        private GridViewSettings GetCierreMensualGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "Cierre Mensual";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 250;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 30;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Cierre Mensual";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdCierreMensual";
            var cerrado = settings.Columns.Add("cerrado", "¿Reporte Cerrado?");
            var solApertura = settings.Columns.Add("SolApertura", "¿Solicitud de Apertura?");

            cerrado.UnboundType = DevExpress.Data.UnboundColumnType.String;
            solApertura.UnboundType = DevExpress.Data.UnboundColumnType.String;

            settings.CustomUnboundColumnData = (sender, e) =>
            {
                if (e.Column.FieldName == "cerrado")
                {
                    if (Convert.ToBoolean(e.GetListSourceFieldValue("esCerrado"))) e.Value = "Sí";
                    else e.Value = "No";

                    e.Column.ReadOnly = true;
                }

                if (e.Column.FieldName == "SolApertura")
                {
                    if (Convert.ToBoolean(e.GetListSourceFieldValue("esSolicitadoParaApertura"))) e.Value = "Sí, en espera de respuesta";
                    else e.Value = "No";

                    e.Column.ReadOnly = true;
                }
            };
            settings.Columns.Add("Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre", "Instancia Ejecutora");
            settings.Columns.Add("Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre", "Campaña");
            settings.Columns.Add("Campania.RecSol_Fed", "Recurso Federal Solicitado").PropertiesEdit.DisplayFormatString = "C";
            settings.Columns.Add("Campania.RecSol_Est", "Recurso Estatal Solicitado").PropertiesEdit.DisplayFormatString = "C";
            settings.Columns.Add("Me.Descripcion", "Mes");
            settings.Columns.Add("Comentarios", "Comentarios");
            settings.CommandColumn.Width = 250;
            return settings;
        }
        #endregion

        #region CierreMensual_SoloApertura
        public ActionResult exportCierreMensual_Apertura(bool isPDF)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetCierreMensual_AperturaGridSettings(), Senasica.GetCierreMensualSolAperturaUCE())
                         : GridViewExtension.ExportToXls(GetCierreMensual_AperturaGridSettings(), Senasica.GetCierreMensualSolAperturaUCE());
        }
        private GridViewSettings GetCierreMensual_AperturaGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsExport.FileName = "Solicitud de Apertura de Campaña";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 250;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 30;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Lista de las campañas en solicitud de apertura";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;

            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdCierreMensual";
            settings.Columns.Add("Campania.ProyectoPresupuesto.UnidadEjecutora.Nombre", "Instancia Ejecutora");
            settings.Columns.Add("Campania.ProyectoPresupuesto.ProyectoAutorizado.Nombre", "Campaña");
            settings.Columns.Add("Campania.RecSol_Fed", "Recurso Federal Solicitado").PropertiesEdit.DisplayFormatString = "C";
            settings.Columns.Add("Campania.RecSol_Est", "Recurso Estatal Solicitado").PropertiesEdit.DisplayFormatString = "C";
            settings.Columns.Add("Me.Descripcion", "Mes");
            settings.Columns.Add("Comentarios", "Comentarios");
            settings.CommandColumn.Width = 250;
            return settings;
        }
        #endregion

                /// /////// TESORERÍA DE LA FEDERACIÓN //////////////

        #region TesoFe
        public ActionResult exportTesoFe(bool isPDF)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();


            return isPDF ? GridViewExtension.ExportToPdf(exportTesoFeGridSettings(), Senasica.GetTesoFe())
                       : GridViewExtension.ExportToXls(exportTesoFeGridSettings(), Senasica.GetTesoFe());

        }

        private GridViewSettings exportTesoFeGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Reintegros";

            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 600;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 90;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "REINTEGROS POR COMITÉ";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsText.ContextMenuSortAscending = "asc";
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdTesofe";
            settings.Columns.Add("UnidadEjecutora.Nombre", "Comité");
            settings.Columns.Add("TipoDeRecurso.Nombre", "Tipo de Recurso");
            settings.Columns.Add("TipoReembolso.Nombre", "Tipo de Reintegro");
            settings.Columns.Add("Fecha", "Fecha de Reintegro").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("Importe", "Total Reintegro").PropertiesEdit.DisplayFormatString = "c";
            settings.Columns.Add("ImporteDetalle", "Total Registrado").PropertiesEdit.DisplayFormatString = "c";
            var diferenciaimporte = settings.Columns.Add("diferenciaimporte", "Status Importe");
            var documento = settings.Columns.Add("documento", "Publicación de Comprobante");
            diferenciaimporte.UnboundType = DevExpress.Data.UnboundColumnType.String;
            documento.UnboundType = DevExpress.Data.UnboundColumnType.String;

            settings.CustomUnboundColumnData = (sender, e) =>
            {
                if (e.Column.FieldName == "diferenciaimporte")
                {
                    bool a = Convert.ToString(e.GetListSourceFieldValue("Importe")) == Convert.ToString(e.GetListSourceFieldValue("ImporteDetalle")) ? true : false;
                    if (a) e.Value = "CORRECTO";
                    else e.Value = "-- AJUSTAR REINTEGRO --";

                    e.Column.ReadOnly = true;
                }

                if (e.Column.FieldName == "documento")
                {
                    if (e.GetListSourceFieldValue("Documento") != null) e.Value = "LISTO";
                    else e.Value = "-- PENDIENTE --";

                    e.Column.ReadOnly = true;
                }
            };
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Importe").DisplayFormat = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ImporteDetalle").DisplayFormat = "c";
            settings.Settings.ShowFooter = true;
            settings.CommandColumn.Width = 600;
            return settings;
        }
        #endregion

        #region Tesofe_ Detalle
        public ActionResult exportTesoFe_Detalle(bool isPDF, int? IdTesofe)
        {
            return isPDF ? GridViewExtension.ExportToPdf(GetTesoFeDetalleGridSettings(), Senasica.GetTesoFeDetalleByTESOFE(IdTesofe))
                         : GridViewExtension.ExportToXls(GetTesoFeDetalleGridSettings(), Senasica.GetTesoFeDetalleByTESOFE(IdTesofe));
        }
        private GridViewSettings GetTesoFeDetalleGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Detalle de los Reintegros por Proyectos";


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Transverse;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 0;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "Detalle de Reintegros";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdTesofeDetalle";
            settings.Columns.Add("ProyectoPresupuesto.UnidadEjecutora.Nombre", "Comité");
            settings.Columns.Add("ProyectoPresupuesto.ProyectoAutorizado.Nombre", "Proyecto Autorizado");
            settings.Columns.Add("TESOFE.Fecha", "Fecha de Reintegro").PropertiesEdit.DisplayFormatString = "d";
            settings.Columns.Add("TESOFE.TipoDeRecurso.Nombre", "Tipo de Recurso");
            settings.Columns.Add("TESOFE.TipoReembolso.Nombre", "Tipo de Reintegro");
            settings.Columns.Add("ImporteDetalle", "Reintegro Registrado").PropertiesEdit.DisplayFormatString = "c";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "ImporteDetalle").DisplayFormat = "c";
            return settings;
        }
        #endregion


        #region ActaComision
        public ActionResult exportActaComision(bool isPDF)
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();


            return isPDF ? GridViewExtension.ExportToPdf(exportActaComisionGridSettings(), Senasica.GetActaComision())
                       : GridViewExtension.ExportToXls(exportActaComisionGridSettings(), Senasica.GetActaComision());

        }

        private GridViewSettings exportActaComisionGridSettings()
        {
            DB_SENASICAEntities db = new DB_SENASICAEntities();
            int IdAnioPres = FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();
            int IdAnio = db.Anios.Where(i => i.Pk_IdAnio == IdAnioPres).Select(i => i.Anio1).First();
            var settings = new GridViewSettings();
            settings.Name = "GridView";

            settings.SettingsExport.FileName = "Actas de Comisión";

            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated;
            settings.SettingsExport.Landscape = true;
            settings.SettingsExport.MaxColumnWidth = 600;
            settings.SettingsExport.Styles.Cell.Font.Name = "arial";
            settings.SettingsExport.Styles.Cell.Font.Size = 10;
            settings.SettingsExport.TopMargin = 130;
            settings.SettingsExport.LeftMargin = 130;
            settings.SettingsExport.RightMargin = 90;
            settings.SettingsExport.BottomMargin = 90;
            settings.SettingsExport.PageHeader.Center = "ACTAS DE COMISIÓN";
            settings.SettingsExport.PageHeader.Font.Name = "Arial";
            settings.SettingsExport.PageHeader.Font.Size = 20;
            settings.SettingsExport.PageFooter.Center = "[Page # of Pages #]";
            settings.SettingsExport.PageFooter.Left = "FECHA: [Date Printed]";
            settings.SettingsExport.PageFooter.Right = "HORA: [Time Printed]";
            settings.SettingsExport.PageFooter.Font.Name = "Arial";
            settings.SettingsExport.PageFooter.Font.Size = 12;
            settings.SettingsText.ContextMenuSortAscending = "asc";
            settings.SettingsExport.ReportFooter = "www.simosica.org";
            settings.KeyFieldName = "Pk_IdActaComision";
            if (IdAnio < 2018)
            {
                if (FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_VEGETAL
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_INOCUIDAD
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_ANIMAL
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_ACUICOLA_PESQUERA
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_MOVILIZACION
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_UPV)
                {
                    settings.Columns.Add("Estado");
                    settings.Columns.Add("Mes");
                }
                else
                {
                    settings.Columns.Add("Estado.Nombre", "Estado");
                    settings.Columns.Add("Me.Descripcion", "Mes");
                }
            }
            else
            {
                if (FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_VEGETAL
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_INOCUIDAD
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_ANIMAL
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_ACUICOLA_PESQUERA
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_MOVILIZACION
                    || FuncionesAuxiliares.GetCurrent_RolUsuario() == RolesUsuarios.UR_UPV)
                {
                    settings.Columns.Add("Estado");
                    settings.Columns.Add("Periodo");
                }
                else
                {
                    settings.Columns.Add("Estado.Nombre", "Estado");
                    settings.Columns.Add("MesByTrimestre.Descripcion", "Periodo");
                }
            }
            settings.Columns.Add("CT_Date", "Fecha de Publicación").PropertiesEdit.DisplayFormatString = "D";
            var documento = settings.Columns.Add("documento", "Actas de Comisión");
            documento.UnboundType = DevExpress.Data.UnboundColumnType.String;

            settings.CustomUnboundColumnData = (sender, e) =>
            {
                if (e.Column.FieldName == "documento")
                {
                    if (e.GetListSourceFieldValue("Documento") != null ) e.Value = "LISTO";
                    else e.Value = "-- SIN DOCUMENTO --";

                    e.Column.ReadOnly = true;
                }
            };
            settings.Settings.ShowFooter = true;
            settings.CommandColumn.Width = 600;
            return settings;
        }
        #endregion

        public string Caption { get; set; }
    }
}
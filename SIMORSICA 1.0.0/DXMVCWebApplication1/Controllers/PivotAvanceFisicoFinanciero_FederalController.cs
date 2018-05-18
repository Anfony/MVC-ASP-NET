using DevExpress.Web.Mvc;
using DevExpress.XtraPivotGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                   + "," + RolesUsuarios.UR_INOCUIDAD
                   + "," + RolesUsuarios.UR_MOVILIZACION
                   + "," + RolesUsuarios.UR_ANIMAL
                   + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                   + "," + RolesUsuarios.UR_VEGETAL
                   + "," + RolesUsuarios.SUP_NACIONAL
                   + "," + RolesUsuarios.UR_UPV
                   + "," + RolesUsuarios.SUP_AUDITOR
  )]
    public class PivotAvanceFisicoFinanciero_FederalController : Controller
    {
        string nomStored = "[UE].[SP_AvanceFisicoFinanciero]";
        int tipoRecurso = 1; // Federal
        //
        // GET: /PivotAvanceFisicoFinanciero/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PivotAvanceFisicoFinanciero_Federal()
        {

            return PartialView("PivotAvanceFisicoFinancieroPartial_Federal", DXMVCWebApplication1.Models.Senasica.getDataSetCubos(nomStored, tipoRecurso));
        }
        public ActionResult ExportToXLS()
        {
            return PivotGridExtension.ExportToXls(PivotGridHelperAFF_Fed.SettingsAFF_Fed, DXMVCWebApplication1.Models.Senasica.getDataSetCubos(nomStored, tipoRecurso));
        }
    }
}

public static class PivotGridHelperAFF_Fed
{
    private static PivotGridSettings _pivotGridSettingsAFF_Fed;
    private static PivotGridField _PivotGridField;
    public static PivotGridSettings SettingsAFF_Fed
    {
        get
        {
            if (_pivotGridSettingsAFF_Fed == null)
                _pivotGridSettingsAFF_Fed = CreatePivotGridSettingsAFF();
            return _pivotGridSettingsAFF_Fed;
        }
    }

    private static PivotGridSettings CreatePivotGridSettingsAFF()
    {
        PivotGridSettings settings = new PivotGridSettings();
        settings.Name = "AvanceFF_Federal";
        settings.OptionsCustomization.AllowDrag = true;
        settings.OptionsCustomization.AllowCustomizationForm = true;
        settings.OptionsPager.RowsPerPage = 20;
        settings.OptionsPager.Position = PagerPosition.Bottom;
        settings.Style["Width"] = "100%";
        settings.Theme = "Default";
        settings.CallbackRouteValues = new { Controller = "PivotAvanceFisicoFinanciero_Federal", Action = "PivotAvanceFisicoFinanciero_Federal" };

        List<string> visibleFields = new List<string>()
        {
                        "Justificacion",
                        "Cant_Ene",
                        "Cant_Feb",
                        "Cant_Mar",
                        "Cant_Abr",
                        "Cant_May",
                        "Cant_Jun",
                        "Cant_Jul",
                        "Cant_Ago",
                        "Cant_Sep",
                        "Cant_Oct",
                        "Cant_Nov",
                        "Cant_Dic",
                        "Cant_Anual",
                        "imp_Ene",
                        "imp_Feb",
                        "imp_Mar",
                        "imp_Abr",
                        "imp_May",
                        "imp_Jun",
                        "imp_Jul",
                        "imp_Ago",
                        "imp_Sep",
                        "imp_Oct",
                        "imp_Nov",
                        "imp_Dic",
                        "ImporteAnual_Federal",
            			"EjercidoEne_Federal",
            			"EjercidoFeb_Federal",
            			"EjercidoMar_Federal",
            			"EjercidoAbr_Federal",
            			"EjercidoMay_Federal",
            			"EjercidoJun_Federal",
            			"Ejercidojul_Federal",
            			"EjercidoAgo_Federal",
            			"EjercidoSep_Federal",
            			"EjercidoOct_Federal",
            			"EjercidoNov_Federal",
            			"EjercidoDic_Federal",
                        "EjercidoAnual_Federal",
        };

        foreach (PivotGridField field in settings.Fields)
        {
            if (!visibleFields.Contains(field.FieldName))
                field.Options.ShowInCustomizationForm = false;
        }

                settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "ConceptoApoyo";
            field.Caption = "ConceptoApoyo";
        });

                settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Componente";
            field.Caption = "Componente";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Anio";
            field.Caption = "Año";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Estado";
            field.Caption = "Estado";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "UnidadResponsable";
            field.Caption = "Unidad Responsable";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "UnidadEjecutora";
            field.Caption = "Unidad Ejecutora";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Campania";
            field.Caption = "Campaña";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Accion";
            field.Caption = "Acción";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "BienOServicio";
            field.Caption = "Bien ó Servicio";
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "UnidadDeMedida";
            field.Caption = "Unidad de Medida";
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Justificacion";
            field.Caption = "Justificación";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Ene";
            field.Caption = "Cant. Enero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Feb";
            field.Caption = "Cant. Febrero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Mar";
            field.Caption = "Cant. Marzo";
            field.Visible = false;

        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Abr";
            field.Caption = "Cant. Abril";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_May";
            field.Caption = "Cant. Mayo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Jun";
            field.Caption = "Cant. Junio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Jul";
            field.Caption = "Cant. Julio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Ago";
            field.Caption = "Cant. Agosto";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Sep";
            field.Caption = "Cant. Septiembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Oct";
            field.Caption = "Cant. Octubre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Nov";
            field.Caption = "Cant. Noviembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Dic";
            field.Caption = "Cant. Diciembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "RecFed";
            field.Caption = "Recurso Federal Autorizado";
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Ene";
            field.Caption = "Imp. Enero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Feb";
            field.Caption = "Imp. Febrero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Mar";
            field.Caption = "Imp. Marzo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Abr";
            field.Caption = "Imp. Abril";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_May";
            field.Caption = "Imp. Mayo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Jun";
            field.Caption = "Imp. Junio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Jul";
            field.Caption = "Imp. Julio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Ago";
            field.Caption = "Imp. Agosto";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Sep";
            field.Caption = "Imp. Septiembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Oct";
            field.Caption = "Imp. Octubre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Nov";
            field.Caption = "Imp. Noviembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "imp_Dic";
            field.Caption = "Imp. Diciembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Cant_Anual";
            field.Caption = "Cantidad Anual";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "ImporteAnual_Federal";
            field.Caption = "Recurso Federal Programado";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoEne_Federal";
            field.Caption = "Ejercido Enero Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoFeb_Federal";
            field.Caption = "Ejercido Febrero Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoMar_Federal";
            field.Caption = "Ejercido Marzo Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoAbr_Federal";
            field.Caption = "Ejercido Abril Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoMay_Federal";
            field.Caption = "Ejercido Mayo Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoJun_Federal";
            field.Caption = "Ejercido Junio Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoJul_Federal";
            field.Caption = "Ejercido Julio Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoAgo_Federal";
            field.Caption = "Ejercido Agosto Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoSep_Federal";
            field.Caption = "Ejercido Septiembre Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoOct_Federal";
            field.Caption = "Ejercido Octubre Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoNov_Federal";
            field.Caption = "Ejercido Noviembre Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoDic_Federal";
            field.Caption = "Ejercido Diciembre Federal";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoAnual_Federal";
            field.Caption = "Recurso Federal Ejercido";
            field.Visible = false;
        });
        return settings;
    }
}
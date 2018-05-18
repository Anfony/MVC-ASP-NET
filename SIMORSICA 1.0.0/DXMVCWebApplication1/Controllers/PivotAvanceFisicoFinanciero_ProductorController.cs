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
    public class PivotAvanceFisicoFinanciero_ProductorController : Controller
    {
        string nomStored = "[UE].[SP_AvanceFisicoFinanciero]";
        int tipoRecurso = 3; // Productor
        //
        // GET: /PivotAvanceFisicoFinanciero/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PivotAvanceFisicoFinanciero_Productor()
        {

            return PartialView("PivotAvanceFisicoFinancieroPartial_Productor", DXMVCWebApplication1.Models.Senasica.getDataSetCubos(nomStored, tipoRecurso));
        }
        public ActionResult ExportToXLS()
        {
            return PivotGridExtension.ExportToXls(PivotGridHelperAFF_Prod.SettingsAFF_Prod, DXMVCWebApplication1.Models.Senasica.getDataSetCubos(nomStored, tipoRecurso));
        }
    }
}

public static class PivotGridHelperAFF_Prod
{
    private static PivotGridSettings _pivotGridSettingsAFF_Prod;
    private static PivotGridField _PivotGridField;
    public static PivotGridSettings SettingsAFF_Prod
    {
        get
        {
            if (_pivotGridSettingsAFF_Prod == null)
                _pivotGridSettingsAFF_Prod = CreatePivotGridSettingsAFF();
            return _pivotGridSettingsAFF_Prod;
        }
    }

    private static PivotGridSettings CreatePivotGridSettingsAFF()
    {
        PivotGridSettings settings = new PivotGridSettings();
        settings.Name = "AvanceFF_Productor";
        settings.OptionsCustomization.AllowDrag = true;
        settings.OptionsCustomization.AllowCustomizationForm = true;
        settings.OptionsPager.RowsPerPage = 20;
        settings.OptionsPager.Position = PagerPosition.Bottom;
        settings.Style["Width"] = "100%";
        settings.Theme = "Default";
        settings.CallbackRouteValues = new { Controller = "PivotAvanceFisicoFinanciero_Productor", Action = "PivotAvanceFisicoFinanciero_Productor" };

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
                        "ImporteAnual_Propio",                        
            			"EjercidoEne_Propio",
            			"EjercidoFeb_Propio",
            			"EjercidoMar_Propio",
            			"EjercidoAbr_Propio",
            			"EjercidoMay_Propio",
            			"EjercidoJun_Propio",
            			"Ejercidojul_Propio",
            			"EjercidoAgo_Propio",
            			"EjercidoSep_Propio",
            			"EjercidoOct_Propio",
            			"EjercidoNov_Propio",
            			"EjercidoDic_Propio",
                        "EjercidoAnual_Propio"
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
            field.FieldName = "RecPro";
            field.Caption = "Recurso Productor Autorizado";
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
            field.FieldName = "ImporteAnual_Propio";
            field.Caption = "Recurso Productor Programado";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoEne_Propio";
            field.Caption = "Ejercido Enero Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoFeb_Propio";
            field.Caption = "Ejercido Febrero Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoMar_Propio";
            field.Caption = "Ejercido Marzo Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoAbr_Propio";
            field.Caption = "Ejercido Abril Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoMay_Propio";
            field.Caption = "Ejercido Mayo Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoJun_Propio";
            field.Caption = "Ejercido Junio Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoJul_Propio";
            field.Caption = "Ejercido Julio Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoAgo_Propio";
            field.Caption = "Ejercido Agosto Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoSep_Propio";
            field.Caption = "Ejercido Septiembre Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoOct_Propio";
            field.Caption = "Ejercido Octubre Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoNov_Propio";
            field.Caption = "Ejercido Noviembre Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoDic_Propio";
            field.Caption = "Ejercido Diciembre Propio";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "EjercidoAnual_Propio";
            field.Caption = "Recurso Productor Ejercido";
            field.Visible = false;
        });
        return settings;
    }
}
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
    public class PivotAvanceFisicoController : Controller
    {
        string nomStored = "[UE].[SP_AvanceFisico]";
        //
        // GET: /PivotAvanceFisico/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PivotAvanceFisico()
        {

            return PartialView("_PivotAvanceFisicoPartial", DXMVCWebApplication1.Models.Senasica.getDataSetCubos_(nomStored));
        }
        public ActionResult ExportToXLS()
        {
            return PivotGridExtension.ExportToXls(PivotGridHelperAF.SettingsAF, DXMVCWebApplication1.Models.Senasica.getDataSetCubos_(nomStored));
        }
	}
}

public static class PivotGridHelperAF
{
    private static PivotGridSettings _pivotGridSettingsAF;
    private static PivotGridField _PivotGridField;
    public static PivotGridSettings SettingsAF
    {
        get
        {
            if (_pivotGridSettingsAF == null)
                _pivotGridSettingsAF = CreatePivotGridSettingsAF();
            return _pivotGridSettingsAF;
        }
    }

    private static PivotGridSettings CreatePivotGridSettingsAF()
    {
        PivotGridSettings settings = new PivotGridSettings();
        settings.Name = "Avance Físico";
        settings.OptionsCustomization.AllowDrag = true;
        settings.OptionsCustomization.AllowCustomizationForm = true;
        settings.OptionsPager.RowsPerPage = 20;
        settings.OptionsPager.Position = PagerPosition.Bottom;
        settings.Style["Width"] = "100%";
        settings.Style["left"] = "100px";
        settings.Theme = "Default";
        settings.CallbackRouteValues = new { Controller = "PivotAvanceFisico", Action = "PivotAvanceFisico" };

        List<string> visibleFields = new List<string>()
        {
            "Justificacion",
			"Meta_Ene",
            "Meta_Feb",
            "Meta_Mar",
            "Meta_Abr",
            "Meta_May",
            "Meta_Jun",
            "Meta_Jul",
            "Meta_Ago",
            "Meta_Sep",
            "Meta_Oct",
            "Meta_Nov",
            "Meta_Dic",
			"Avance_Ene",
			"Avance_Feb",
			"Avance_Mar",
			"Avance_Abr",
			"Avance_May",
			"Avance_Jun",
			"Avance_Jul",
			"Avance_Ago",
			"Avance_Sep",
			"Avance_Oct",
			"Avance_Nov",
			"Avance_Dic"
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
            field.FieldName = "Estado";
            field.Caption = "Estado";
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
            field.FieldName = "UnidadEjecutora";
            field.Caption = "Unidad Ejecutora";
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
            field.FieldName = "TipoDeActividad";
            field.Caption = "Tipo de Actividad";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Actividad";
            field.Caption = "Actividad";
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
            field.FieldName = "Indicador";
            field.Caption = "Indicador";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Ene";
            field.Caption = "Meta Enero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Feb";
            field.Caption = "Meta Febrero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Mar";
            field.Caption = "Meta Marzo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Abr";
            field.Caption = "Meta Abril";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_May";
            field.Caption = "Meta Mayo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Jun";
            field.Caption = "Meta Junio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Jul";
            field.Caption = "Meta Julio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Ago";
            field.Caption = "Meta Agosto";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Sep";
            field.Caption = "Meta Septiembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Oct";
            field.Caption = "Meta Octubre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Nov";
            field.Caption = "Meta Noviembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Dic";
            field.Caption = "Meta Diciembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Ene";
            field.Caption = "Avance Enero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Feb";
            field.Caption = "Avance Febrero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Mar";
            field.Caption = "Avance Marzo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Abr";
            field.Caption = "Avance Abril";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_May";
            field.Caption = "Avance Mayo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Jun";
            field.Caption = "Avance Junio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Jul";
            field.Caption = "Avance Julio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Ago";
            field.Caption = "Avance Agosto";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Sep";
            field.Caption = "Avance Septiembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Oct";
            field.Caption = "Avance Octubre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Nov";
            field.Caption = "Avance Noviembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Avance_Dic";
            field.Caption = "Avance Diciembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "Meta_Anual";
            field.Caption = "Meta Anual";
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.FieldName = "TotalAvance";
            field.Caption = "Total Avance";
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
            field.Width = 50;
            field.FieldName = "StatusCampania";
            field.Caption = "Estatus Campaña";
        });

        return settings;
    }
}
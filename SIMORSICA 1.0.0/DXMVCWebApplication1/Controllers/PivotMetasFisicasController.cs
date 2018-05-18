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
      )]
    public class PivotMetasFisicasController : Controller
    {
        string nomStored = "[UE].[SP_MetasFisicas]";
        //
        // GET: /PivotMetasFisicas/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PivotMetasFisicas()
        {

            return PartialView("_PivotMetasFisicasPartial", DXMVCWebApplication1.Models.Senasica.getDataSetCubos_(nomStored));
        }
        public ActionResult ExportToXLS()
        {
            return PivotGridExtension.ExportToXls(PivotGridHelperMFS.SettingsMFS, DXMVCWebApplication1.Models.Senasica.getDataSetCubos_(nomStored));
        }
	}
}


public static class PivotGridHelperMFS
{
    private static PivotGridSettings _pivotGridSettingsMFS;
    private static PivotGridField _PivotGridField;
    public static PivotGridSettings SettingsMFS
    {
        get
        {
            if (_pivotGridSettingsMFS == null)
                _pivotGridSettingsMFS = CreatePivotGridSettingsMFS();
            return _pivotGridSettingsMFS;
        }
    }

    private static PivotGridSettings CreatePivotGridSettingsMFS()
    {
        PivotGridSettings settings = new PivotGridSettings();
        settings.Name = "Metas Físicas";
        settings.OptionsCustomization.AllowDrag = true;
        settings.OptionsCustomization.AllowCustomizationForm = true;
        settings.OptionsPager.RowsPerPage = 20;
        settings.OptionsPager.Position = PagerPosition.Bottom;
        settings.Style["Width"] = "100%";
        settings.Style["Left"] = "100px";
        settings.Theme = "Default";
        settings.CallbackRouteValues = new { Controller = "PivotMetasFisicas", Action = "PivotMetasFisicas" };

        List<string> visibleFields = new List<string>()
        {
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
            "Justificacion"
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
            field.Width = 50;
            field.FieldName = "UnidadEjecutora";
            field.Caption = "Unidad Ejecutora";
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Campania";
            field.Caption = "Campaña";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "TipoDeAccion";
            field.Caption = "Tipo de Acción";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "TipoDeActividad";
            field.Caption = "Tipo de Actividad";
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "UnidadDeMedida";
            field.Caption = "Unidad de Medida";
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Actividad";
            field.Caption = "Actividad";
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Indicador";
            field.Caption = "Indicador";
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Ene";
            field.Caption = "Meta Enero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Feb";
            field.Caption = "Meta Febrero";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Mar";
            field.Caption = "Meta Marzo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Abr";
            field.Caption = "Meta Abril";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_May";
            field.Caption = "Meta Mayo";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Jun";
            field.Caption = "Meta Junio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Jul";
            field.Caption = "Meta Julio";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Ago";
            field.Caption = "Meta Agosto";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Sep";
            field.Caption = "Meta Septiembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Oct";
            field.Caption = "Meta Octubre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Nov";
            field.Caption = "Meta Noviembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Dic";
            field.Caption = "Meta Diciembre";
            field.Visible = false;
        });
        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Justificacion";
            field.Caption = "Justificación";
            field.Visible = false;
        });

        settings.Fields.Add(field =>
        {
            field.Area = PivotArea.FilterArea;
            field.Width = 50;
            field.FieldName = "Meta_Anual";
            field.Caption = "Meta Anual";
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
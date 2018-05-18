using System;
using System.Linq;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                           + "," + RolesUsuarios.IE_INOCUIDAD
                           + "," + RolesUsuarios.IE_MOVILIZACION
                           + "," + RolesUsuarios.IE_ANIMAL
                           + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                           + "," + RolesUsuarios.IE_VEGETAL
                           + "," + RolesUsuarios.UR_INOCUIDAD
                           + "," + RolesUsuarios.UR_MOVILIZACION
                           + "," + RolesUsuarios.UR_ANIMAL
                           + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                           + "," + RolesUsuarios.UR_VEGETAL
                           + "," + RolesUsuarios.PUESTO_COOR_ADMINISTRATIVO
                           + "," + RolesUsuarios.COORDINADOR_CAMPANIA
                           + "," + RolesUsuarios.PUESTO_COOR_PROYECTO
                           + "," + RolesUsuarios.PUESTO_GERENTE
                           + "," + RolesUsuarios.PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS
                           + "," + RolesUsuarios.PUESTO_PROF_RESP_INFORMATICA
                           + "," + RolesUsuarios.IE_PROGRAMAS_U
          )]
    public class Actividades2Controller : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: /Actividades2/
        public ActionResult Index()
        {
            return View();
        }

        DB_SENASICAEntities appointmentContext = new DB_SENASICAEntities();
        DB_SENASICAEntities resourceContext = new DB_SENASICAEntities();

        public ActionResult ActividadSchedulerPartial()
        {
            int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
            int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
            int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);
            
            System.Collections.IEnumerable AppointmentBinding = Senasica.GetActividadesByCampania(_Fk_IdCampania, UEID);    //appointmentContext.Actividads.ToList();
            System.Collections.IEnumerable ResourceBinding = Senasica.GetPersonasByUnidadEjecutora(UEID);                   //resourceContext.Personas.ToList();

            ViewData["Appointments"] = AppointmentBinding;
            ViewData["Resources"] = ResourceBinding;

            return PartialView("_ActividadSchedulerPartial");
        }
        public ActionResult ActividadSchedulerPartialEditAppointment()
        {
            int? _Fk_IdAccion = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdAccion__UE);
            int? _Fk_IdCampania = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdCampania__UE);
            int? UEID = ((UserData)Session["DataUsuario"]).setFilter(((UserData)Session["DataUsuario"]).FK_IdUnidadEjecutora__UE);

            var AppointmentBinding = Senasica.GetActividadesByCampania(_Fk_IdCampania, UEID);           //  appointmentContext.Actividads.ToList();
            var ResourceBinding = Senasica.GetPersonasByUnidadEjecutora(UEID);    //resourceContext.Personas.ToList();

            try
            {
                Actividades2ControllerControllerActividadSchedulerSettings.UpdateEditableDataObject(appointmentContext, resourceContext);
            }
            catch (Exception e)
            {
                ViewData["SchedulerErrorText"] = e.Message;
            }

            ViewData["Appointments"] = AppointmentBinding;
            ViewData["Resources"] = ResourceBinding;

            return PartialView("_ActividadSchedulerPartial");
        }
    }
     #region SchedulerSettings
     public class Actividades2ControllerControllerActividadSchedulerSettings
     {
         static DevExpress.Web.Mvc.MVCxAppointmentStorage appointmentStorage;
         public static DevExpress.Web.Mvc.MVCxAppointmentStorage AppointmentStorage
         {
             get
             {
                 if (appointmentStorage == null)
                 {
                     appointmentStorage = new DevExpress.Web.Mvc.MVCxAppointmentStorage();
                     appointmentStorage.Mappings.AppointmentId = "Pk_IdActividad";
                     appointmentStorage.Mappings.Start = "Fecha_Inicio";
                     appointmentStorage.Mappings.End = "FechaFin";
                     appointmentStorage.Mappings.Subject = "Tema";
                     appointmentStorage.Mappings.Description = "Descripcion";
                     appointmentStorage.Mappings.Location = "Ubicacion";
                     appointmentStorage.Mappings.AllDay = "";
                     appointmentStorage.Mappings.Type = "Fk_IdTipoActividad";
                     appointmentStorage.Mappings.RecurrenceInfo = "";
                     appointmentStorage.Mappings.ReminderInfo = "";
                     appointmentStorage.Mappings.Label = "Fk_IdAccion__UE";
                     appointmentStorage.Mappings.Status = "Fk_IdStatusActividad__SIS";
                     appointmentStorage.Mappings.ResourceId = "Fk_IdPersona__SIS";
                 }
                 return appointmentStorage;
             }
         }

         static DevExpress.Web.Mvc.MVCxResourceStorage resourceStorage;
         public static DevExpress.Web.Mvc.MVCxResourceStorage ResourceStorage
         {
             get
             {
                 if (resourceStorage == null)
                 {
                     resourceStorage = new DevExpress.Web.Mvc.MVCxResourceStorage();
                     resourceStorage.Mappings.ResourceId = "Pk_IdPersona";
                     resourceStorage.Mappings.Caption = "NombreCompleto";
                 }
                 return resourceStorage;
             }
         }

         public static void UpdateEditableDataObject(DB_SENASICAEntities appointmentContext, DB_SENASICAEntities resourceContext)
         {
             InsertAppointments(appointmentContext, resourceContext);
             UpdateAppointments(appointmentContext, resourceContext);
             DeleteAppointments(appointmentContext, resourceContext);
         }

         static void InsertAppointments(DB_SENASICAEntities appointmentContext, DB_SENASICAEntities resourceContext)
         {
             var appointments = appointmentContext.Actividads.ToList();
             var resources = resourceContext.Personas.ToList();

             var newAppointments = DevExpress.Web.Mvc.SchedulerExtension.GetAppointmentsToInsert<Actividad>("ActividadScheduler", appointments, resources,
                 AppointmentStorage, ResourceStorage);
             foreach (var appointment in newAppointments)
             {
                 appointments.Add(appointment);
             }
             appointmentContext.SaveChanges();
         }
         static void UpdateAppointments(DB_SENASICAEntities appointmentContext, DB_SENASICAEntities resourceContext)
         {
             var appointments = appointmentContext.Actividads.ToList();
             var resources = resourceContext.Personas.ToList();

             var updAppointments = DevExpress.Web.Mvc.SchedulerExtension.GetAppointmentsToUpdate<Actividad>("ActividadScheduler", appointments, resources,
                 AppointmentStorage, ResourceStorage);
             foreach (var appointment in updAppointments)
             {
                 //Es necesario crear el código para guardar 
             }
             appointmentContext.SaveChanges();
         }

         static void DeleteAppointments(DB_SENASICAEntities appointmentContext, DB_SENASICAEntities resourceContext)
         {
             var appointments = appointmentContext.Actividads.ToList();
             var resources = resourceContext.Personas.ToList();

             var delAppointments = DevExpress.Web.Mvc.SchedulerExtension.GetAppointmentsToRemove<Actividad>("ActividadScheduler", appointments, resources,
                 AppointmentStorage, ResourceStorage);
             foreach (var appointment in delAppointments)
             {
                 var delAppointment = appointments.FirstOrDefault(a => a.Pk_IdActividad == appointment.Pk_IdActividad);
                 if (delAppointment != null)
                     appointments.Remove(appointment);
             }
             appointmentContext.SaveChanges();
         }
     } 
     #endregion

}

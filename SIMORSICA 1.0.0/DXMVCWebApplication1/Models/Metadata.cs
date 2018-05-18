using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Models
{
    //Accion
    public class AccionMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdCampania__UE")]
        public int Fk_IdCampania__UE;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdTipoDeRecurso__SIS")]
        public int Fk_IdTipoDeRecurso__SIS;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdTipoDeAccion__SIS")]
        public int Fk_IdTipoDeAccion__SIS;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdUnidadDeMedida__SIS")]
        public int Fk_IdUnidadDeMedida__SIS;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "FK_IdPersona__SIS")]
        public int FK_IdPersona__SIS;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Actividad")]
        public string Actividad;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Costo")]
        public double Costo;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Justificacion")]
        public string Justificacion;
    }
    //AccionXCampania
    public class AccionXCampaniaMetadata
    {
        [Required(ErrorMessage = "El Tipo de Acción es obligatorio")]
        [Display(Name = "Fk_IdTipoDeAccion__SIS")]
        public int Fk_IdTipoDeAccion__SIS;
        [Required(ErrorMessage = "El Coordinador de Campaña es obligatorio")]
        [Display(Name = "FK_IdPersona__SIS")]
        public int FK_IdPersona__SIS;
        [StringLength(450, ErrorMessage = "Por favor, introduzca no más de 450 caracteres")]
        public string Justificacion;
    }

    //Actividad
    public class ActividadMetadata
    {
        [Required(ErrorMessage = "Seleccione a la persona Responsable de Realizar la actividad")]
        [Display(Name = "Persona Asignada")]
        public int Fk_IdPersona__SIS;
        [Required(ErrorMessage = "Seleccione la fecha en que se deben iniciar las Actividades")]
        [Display(Name = "Fecha Programada para Iniciar la Actividad")]
        public System.DateTime Fecha_Inicio;
        [Required(ErrorMessage = "Seleccione la fecha en que se deben terminar las Actividades")]
        [Display(Name = "Fecha Programada para Iniciar la Actividad")]
        public System.DateTime FechaFin;
        [Required(ErrorMessage = "Describa la Actividad a Realizar")]
        [Display(Name = "Descripcion")]
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Descripcion;
        [Required(ErrorMessage = "Seleccione la actividad de la campaña")]
        [Display(Name = "Actividad de la Campaña")]
        public int Fk_IdActividadXAccion;
    }
    //ActividadXAccion
    public class ActividadXAccionMetadata
    {
        [Required(ErrorMessage = "El Tipo de  Actividad es obligatori0")]
        [Display(Name = "Fk_IdTipoActividad")]
        public string Fk_IdTipoActividad;
        [Required(ErrorMessage = "El Tipo de Recurso es obligatorio")]
        [Display(Name = "Fk_IdTipoDeRecurso")]
        public int Fk_IdTipoDeRecurso;
        [Required(ErrorMessage = "El Responsable de la Actividad es obligatorio")]
        [Display(Name = "FK_IdPersona")]
        public int FK_IdPersona;
        [Required(ErrorMessage = "La Actividad es obligatoria")]
        [Display(Name = "Actividad")]
        public string Actividad;
        [Required(ErrorMessage = "La Justificación es obligatoria")]
        [Display(Name = "Justificacion")]
        public string Justificacion;

    }
    public class AnioMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Anio")]
        public int Anio1;
    }
    public class BeneficiarioMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdActividad__UE")]
        public int Fk_IdActividad__UE;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdProductor__UE")]
        public int Fk_IdProductor__UE;

    }
    public class BienOServMetadata
    {
        [Required(ErrorMessage = "El Tipo de Bien es obligatorio")]
        [Display(Name = "Fk_IdTipoDeBien")]
        public int Fk_IdTipoDeBien;
        [StringLength(12, ErrorMessage = "Por favor, introduzca no más de 12 caracteres")]
        public string CABMS;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Nombre;
        [Required(ErrorMessage = "La Especificación es Obligatorio")]
        [Display(Name = "Especificacion")]
        public string Especificacion;
    }
    public class CampaniaMetadata
    {
        [Required(ErrorMessage = "El Proyecto es obligatorio")]
        [Display(Name = "Fk_IdProyectoPresupuesto")]
        public int Fk_IdProyectoPresupuesto;

        [Required(ErrorMessage = "La Indroducción es obligatoria")]
        [Display(Name = "Introduccion")]
        public string Introduccion;

        [Required(ErrorMessage = "La Situación Actual es obligatoria")]
        [Display(Name = "SituacionActual")]
        public string SituacionActual;

        [Required(ErrorMessage = "El Objetivo Estratégico de la Campaña es obligatorio")]
        [Display(Name = "ObjetivoEstrategico")]
        public string ObjetivoEstrategico;

        [Required(ErrorMessage = "La Meta Anual es obligatoria")]
        [Display(Name = "MetaAnual")]
        public string MetaAnual;

        [Required(ErrorMessage = "La Fecha Inicio es obligatoria")]
        [Display(Name = "Fecha Inicio")]
        public System.DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La Fecha Fin es obligatoria")]
        [Display(Name = "Fecha Fin")]
        public System.DateTime FechaFin { get; set; }
    }
    public class CapituloPartidaMetadata
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Nombre;
    }

    public class CargoMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdTipoDeUnidad")]
        public int Fk_IdTipoDeUnidad;
        [StringLength(250, ErrorMessage = "Por favor, introduzca no más de 250 caracteres")]
        public string Nombre;
    }

    public class CertificacionMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Nombre;
    }

    public class CertificadoCargoMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdCargo")]
        public int Fk_IdCargo;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdCertificado")]
        public int Fk_IdCertificado;
    }

    public class CertificadoPersonaMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdPersona")]
        public int Fk_IdPersona;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdCertificado")]
        public int Fk_IdCertificado;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Documento;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "FechaRegistro")]
        public System.DateTime FechaRegistro;
    }

    public class ComponenteMetadata
    {
        [Required(ErrorMessage = "El Programa Institucional es obligatorio")]
        [Display(Name = "Fk_IdPrograma__SIS")]
        public int Fk_IdPrograma__SIS;
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        public string Nombre;
    }

    public class ConceptoPartidaMetadata
    {
        [Required(ErrorMessage = "El Capitulo es obligatorio")]
        [Display(Name = "Fk_IdCapituloPartida__SIS")]
        public int Fk_IdCapituloPartida__SIS;
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Nombre;
    }

    public class EstadoMetadata
    {
        [Required(ErrorMessage = "El Estado es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        public string Nombre;
    }

    public class EstadoBienMetadata
    {
        [Required(ErrorMessage = "El Estado de Bien es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Nombre;
    }

    public class FlujoActividadMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdEstatusOrigen")]
        public int Fk_IdEstatusOrigen;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdEstatusDestino")]
        public int Fk_IdEstatusDestino;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Rol;
    }

    public class FlujoAlcanceMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdEstatusOrigen")]
        public int Fk_IdEstatusOrigen;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdEstatusDestino")]
        public int Fk_IdEstatusDestino;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Rol;
    }

    public class FlujoCampaniaMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdEstatusOrigen")]
        public int Fk_IdEstatusOrigen;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdEstatusDestino")]
        public int Fk_IdEstatusDestino;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Rol;
    }

    public class GirosXProvMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdProveedor")]
        public int Fk_IdProveedor;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdTipoDeBien")]
        public int Fk_IdTipoDeBien;
    }

    public class IncentivoMetadata
    {
        [Required(ErrorMessage = "El Componente de los Programas es obligatorio")]
        [Display(Name = "Fk_IdComponente")]
        public int Fk_IdComponente;
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(250, ErrorMessage = "Por favor, introduzca no más de 250 caracteres")]
        public string Nombre;
    }

    public class InoCompSAMetadata
    {

        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Producto;
    }

    public class InoCompSACMetadata
    {

        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Producto;
    }

    public class InoCompSVMetadata
    {

        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Cultivo;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Empresa;
    }

    public class InoImportanciaSAMetadata
    {

        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
    }

    public class InoImportanciaSACMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
    }

    public class InoImportanciaSVMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
    }

    public class InoUnidadesCertSAMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
    }

    public class InoUnidadesCertSACMetadata
    {

        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
    }

    public class InoUnidadesCertSVMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
    }

    public class InstalacionMetadata
    {
        [Required(ErrorMessage = "El Nombre de la Instancia Ejecutora es obligatorio")]
        [Display(Name = "Fk_IdUnidadEjecutora")]
        public int Fk_IdUnidadEjecutora;
        [Required(ErrorMessage = "El Tipo de Instalación es obligatorio")]
        [Display(Name = "Fk_IdTipoDeInstalacion")]
        public int Fk_IdTipoDeInstalacion;
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Nombre;
        [StringLength(250, ErrorMessage = "Por favor, introduzca no más de 250 caracteres")]
        public string Ubicacion;
        [StringLength(250, ErrorMessage = "Por favor, introduzca no más de 250 caracteres")]
        public string Utilizacion;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string region;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Estatus;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Certificado;
        [Required(ErrorMessage = "El Tipo de Adquisición es obligatorio")]
        [Display(Name = "Fk_IdTipoAdquisicion_SIS")]
        public int Fk_IdTipoAdquisicion_SIS;
    }

    public class InventarioMetadata
    {
        [Required(ErrorMessage = "El Año De Adquisición es obligatorio")]
        [Display(Name = "FK_IdAnio")]
        public int FK_IdAnio;
        [Required(ErrorMessage = "El Estado del Bien es obligatorio")]
        [Display(Name = "FK_IdEstadoBien__SIS")]
        public int FK_IdEstadoBien__SIS;
        [Required(ErrorMessage = "El No. De Bien es obligatorio")]
        [Display(Name = "FK_IdBienOServ")]
        public int FK_IdBienOServ;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Clave;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string ClaveAnt;
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Descripcion;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Marca;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Modelo;
        [StringLength(30, ErrorMessage = "Por favor, introduzca no más de 30 caracteres")]
        public string Serie;
        [StringLength(20, ErrorMessage = "Por favor, introduzca no más de 20 caracteres")]
        public string Factura;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Referencia;
        [Required(ErrorMessage = "El Tipo de Recurso es Obligatorio")]
        [Display(Name = "Fk_TipodeRecurso_SIS")]
        public int Fk_TipodeRecurso_SIS;
    }

    public class Mov_AtendidoMetadata
    {

        [Required(ErrorMessage = "El Estado es obligatorio")]
        [Display(Name = "Fk_IdEstado")]
        public int Fk_IdEstado;
    }

    public class Mov_ImportanciaPIVMetadata
    {
        [Required(ErrorMessage = "El Estado es obligatorio")]
        [Display(Name = "Fk_IdEstado")]
        public int Fk_IdEstado;
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
    }

    public class Mov_ImportanciaProgMetadata
    {

        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
    }

    public class MunicipioMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        public string Nombre;
    }

    public class NecesidadXAccionMetadata
    {
        [Required(ErrorMessage = "El Bien o Servicio es obligatorio")]
        [Display(Name = "Fk_IdBienOServ")]
        public int Fk_IdBienOServ;
        [Required(ErrorMessage = "La Unidad de Medida es obligatoria")]
        [Display(Name = "Fk_IdUnidadDeMedida")]
        public int Fk_IdUnidadDeMedida;
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Descripcion;
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Justificacion;
    }

    public class PartidaMetadata
    {
        [Required(ErrorMessage = "El Concepto de Gasto es obligatorio")]
        [Display(Name = "Fk_IdConceptoPartida__SIS")]
        public int Fk_IdConceptoPartida__SIS;
        [Required(ErrorMessage = "La Partida es obligatoria")]
        [Display(Name = "Nombre")]
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Nombre;
    }

    public class PersonaMetadata
    {

        [Required(ErrorMessage = "El Apellido Paterno es obligatorio")]
        [Display(Name = "Ap_Paterno")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Ap_Paterno;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Ap_Materno;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Nombre;
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        public string Direccion;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Colonia;
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipioPersona__SIS")]
        public int Fk_IdMunicipioPersona__SIS;
        [StringLength(15, ErrorMessage = "Por favor, introduzca no más de 15 caracteres")]
        public string Telefono;
        [Required(ErrorMessage = "El Correo Electrónico es obligatorio")]
        [Display(Name = "Email")]
        [StringLength(40, ErrorMessage = "Por favor, introduzca no más de 40 caracteres")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "El Correo es invalido")]
        public string Email;
        [StringLength(15, ErrorMessage = "Por favor, introduzca no más de 15 caracteres")]
        public string RFC;
        [StringLength(18, ErrorMessage = "Por favor, introduzca no más de 18 caracteres")]
        public string CURP;
        [StringLength(20, ErrorMessage = "Por favor, introduzca no más de 20 caracteres")]
        public string NoCedulaProfesional;
        [StringLength(10, ErrorMessage = "Por favor, introduzca no más de 10 caracteres")]
        public string CodigoPostal;
        [StringLength(200, ErrorMessage = "Por favor, introduzca no más de 200 caracteres")]
        public string Profesion;
        [StringLength(300, ErrorMessage = "Por favor, introduzca no más de 30 caracteres")]
        public string Especialidad;
        [StringLength(20, ErrorMessage = "Por favor, introduzca no más de 20 caracteres")]
        public string NoLicenciaManejo;
        //[Required(ErrorMessage = "El campo es requerido")]
        //[Display(Name = "Fk_IdCargo")]
        //public int Fk_IdCargo;
    }

    public class PredioMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdProductor__UE")]
        public int Fk_IdProductor__UE;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "NombrePredio")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string NombrePredio;
    }

    public class PresupuestoMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Pk_IdEstado")]
        public int Pk_IdEstado;
    }

    public class ProductorMetadata
    {
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string RazonSocial;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string ApellidoPaterno;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string ApellidoMaterno;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Nombre;
        [Required(ErrorMessage = "La Dirección es obligatoria")]
        [Display(Name = "Direccion")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Direccion;
        [Required(ErrorMessage = "La Colonia es obligatoria")]
        [Display(Name = "Colonia")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Colonia;
        [Required(ErrorMessage = "El Estado es obligatorio")]
        [Display(Name = "Fk_IdEstado__SIS")]
        public int Fk_IdEstado__SIS;
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio__SIS")]
        public int Fk_IdMunicipio__SIS;
        [StringLength(15, ErrorMessage = "Por favor, introduzca no más de 15 caracteres")]
        public string Telefono;
        [Display(Name = "Email:")]
        [StringLength(40, ErrorMessage = "Por favor, introduzca no más de 40 caracteres")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "El Correo es invalido")]
        public string Email;
    }

    public class ProgramaMetadata
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        public string Nombre;
    }

    public class ProgramaAnualAdqMetadata
    {
        [Required(ErrorMessage = "El Bien o Servicio es obligatorio")]
        [Display(Name = "Fk_IdBienOServ__SIS")]
        public int Fk_IdBienOServ__SIS;
        [Required(ErrorMessage = "La Unidad de Medida es obligatoria")]
        [Display(Name = "Fk_IdUnidadDeMedida__SIS")]
        public int Fk_IdUnidadDeMedida__SIS;
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Justificacion;

    }

    public class ProveedorMetadata
    {
        [StringLength(300, ErrorMessage = "Por favor, introduzca no más de 300 caracteres")]
        public string RazonSocial_Nombre;
        [Required(ErrorMessage = "El RFC es obligatorio")]
        [Display(Name = "RFC")]
        [StringLength(300, ErrorMessage = "Por favor, introduzca no más de 300 caracteres")]
        public string RFC;
        [Required(ErrorMessage = "La Dirección es obligatoria")]
        [Display(Name = "Direccion")]
        [StringLength(300, ErrorMessage = "Por favor, introduzca no más de 300 caracteres")]
        public string Direccion;
        [Required(ErrorMessage = "La Colonia es obligatoria")]
        [Display(Name = "Colonia")]
        [StringLength(200, ErrorMessage = "Por favor, introduzca no más de 200 caracteres")]
        public string Colonia;
        [Required(ErrorMessage = "El Estado es obligatorio")]
        [Display(Name = "Fk_IdEstado")]
        public int Fk_IdEstado;
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [StringLength(20, ErrorMessage = "Por favor, introduzca no más de 20 caracteres")]
        public string Telefono;
        [Display(Name = "Email:")]
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "El Correo es invalido")]
        public string Email;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string NumeroEscritura;
        [StringLength(2000, ErrorMessage = "Por favor, introduzca no más de 2000 caracteres")]
        public string NombreNotario;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string NumeroNotaria;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string FoliodeInscripcion;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string RPPyC;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string EstadoNotaria;
        [StringLength(200, ErrorMessage = "Por favor, introduzca no más de 200 caracteres")]
        public string RepresentanteLegal;
        [StringLength(10, ErrorMessage = "Por favor, introduzca no más de 10 caracteres")]
        public string CodigoPostal;
    }

    public class ProyectoAutorizadoMetadata
    {
        [Required(ErrorMessage = "El Concepto de Apoyo es obligatorio")]
        [Display(Name = "Fk_IdSubComponente__SIS")]
        public int Fk_IdSubComponente__SIS;
        [Required(ErrorMessage = "El Año es obligatorio")]
        [Display(Name = "Fk_IdAnio")]
        public int Fk_IdAnio;
        [Required(ErrorMessage = "El Nombre del Proyecto Autorizado es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(250, ErrorMessage = "Por favor, introduzca no más de 250 caracteres")]
        public string Nombre;
    }

    public class ProyectoPresupuestoMetadata
    {
        [Required(ErrorMessage = "El Proyecto Autorizado es obligatorio")]
        [Display(Name = "Fk_IdProyectoAutorizado")]
        public int Fk_IdProyectoAutorizado;
    }

    public class RepActividadMetadata
    {
        [Required(ErrorMessage = "La Actividad asignada no esta definida")]
        [Display(Name = "Fk_IdActividad")]
        public int Fk_IdActividad;
        [Required(ErrorMessage = "Seleccione a la Persona que realizo la actividad")]
        [Display(Name = "Fk_IdPersona")]
        public int Fk_IdPersona;

        [Required(ErrorMessage = "Seleccione la fecha del Informe")]
        [Display(Name = "Fecha Informe")]
        public DateTime FechaInforme;

        [Required(ErrorMessage = "Seleccione la fecha en la que terminó de realizar la actividad")]
        [Display(Name = "Fecha Fin")]
        public DateTime FechaFin;

        [Required(ErrorMessage = "Digite la cantidad de metas alcanzadas para este avance")]
        [Display(Name = "Metas Alcanzadas en el periodo")]
        public DateTime CantidadMetas;

    }

    public class RepAdquisicionMetadata
    {
        [Required(ErrorMessage = "Seleccione la necesidad a la que corresponde el gasto")]
        [Display(Name = "Fk_IdProgramaAnualAdq")]
        public int Fk_IdProgramaAnualAdq;

        [Required(ErrorMessage = "Seleccione la fecha en la que realizó la adquisición o pagó el servicio")]
        [Display(Name = "Fecha de Gasto")]
        public int FechaAdquisicion;

        [Required(ErrorMessage = "Describa el gasto realizado")]
        [Display(Name = "Descripción del Gasto")]
        [StringLength(250, ErrorMessage = "Por favor, introduzca no más de 250 caracteres")]
        public int Concepto;

        [Required(ErrorMessage = "Registre el importe del gasto incluyendo IVA")]
        [Display(Name = "Importe del Gasto")]
        public int Importe;
        [StringLength(250, ErrorMessage = "Por favor, introduzca no más de 250 caracteres")]
        public int Documento;
    }

    public class RepGastoMetadata
    {
        [Required(ErrorMessage = "Seleccione el reporte de Actividad")]
        [Display(Name = "Fk_IdRepActividad")]
        public int Fk_IdRepActividad;

        [Required(ErrorMessage = "Seleccione la fecha en la que realizó el gasto")]
        [Display(Name = "Fecha de Gasto")]
        public int FechaGasto;
        [StringLength(250, ErrorMessage = "Por favor, introduzca no más de 250 caracteres")]
        public int Concepto;
    }

    public class RepresentanteMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdUnidadEjecutora__UE")]
        public int Fk_IdUnidadEjecutora__UE;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Ap_Paterno")]
        [StringLength(20, ErrorMessage = "Por favor, introduzca no más de 20 caracteres")]
        public string Ap_Paterno;
        [StringLength(20, ErrorMessage = "Por favor, introduzca no más de 20 caracteres")]
        public string Ap_Materno;
        [StringLength(40, ErrorMessage = "Por favor, introduzca no más de 40 caracteres")]
        public string Nombre;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Direccion;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Colonia;
        [StringLength(15, ErrorMessage = "Por favor, introduzca no más de 15 caracteres")]
        public string Telefono;
        [Display(Name = "Email:")]
        [StringLength(40, ErrorMessage = "Por favor, introduzca no más de 40 caracteres")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "El Correo es invalido")]
        public string Email;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdCargo")]
        public int Fk_IdCargo;
    }

    public class SA_AtendidoMetadata
    {
        [Required(ErrorMessage = "La Especie es obligatoria")]
        [Display(Name = "Especie")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Especie;
    }

    //public class SA_ImportanciaEnfermedadMetadata { }

    //public class SA_PoblacionMetadata {
    //    [Required(ErrorMessage = "El campo es requerido")]
    //    [Display(Name = "Fk_IdCampania")]
    //    public int Fk_IdCampania;
    //}

    public class SAC_AtendidoMetadata
    {

        [Required(ErrorMessage = "El Cultivo es obligatorio")]
        [Display(Name = "Cultivo::")]
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Cultivo;
    }

    public class SACImportanciaCultivoMetadata
    {
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Cultivo;
    }

    public class SACImportanciaEnfermedadMetadata
    {
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Cultivo;
    }

    public class StatusAccionMetadata
    {
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Nombre;
    }

    public class StatusActividadMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Nombre;
    }

    public class StatusActividadKardexMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "FK_IdStatusActividad__SIS")]
        public int FK_IdStatusActividad__SIS;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "FK_IdActividad__UE")]
        public int FK_IdActividad__UE;
    }

    public class StatusAlcanceMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre;
    }

    public class StatusAlcanceKardexMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "FK_IdStatusAlcance__SIS")]
        public int FK_IdStatusAlcance__SIS;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "FK_IdAlcance__UE")]
        public int FK_IdAlcance__UE;
    }

    public class StatusCampaniaMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre;
    }

    public class StatusCampaniaKardexMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "FK_IdStatusCampania__SIS")]
        public int FK_IdStatusCampania__SIS;

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Comentarios")]
        public string Comentarios;
    }

    public class StatusUEMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Nombre;
    }

    public class SubComponenteMetadata
    {
        [Required(ErrorMessage = "El Incentivo es obligatorio")]
        [Display(Name = "Fk_IdIncentivo__SIS")]
        public int Fk_IdIncentivo__SIS;
        [Required(ErrorMessage = "El Nombre de SubComponente es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre;
    }

    public class SubPredioMetadata
    {
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string UsoPrincipal;
        [StringLength(500, ErrorMessage = "Por favor, introduzca no más de 500 caracteres")]
        public string Comentarios;
    }

    public class SVAtendidoMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdCampania")]
        public int Fk_IdCampania;
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Cultivo")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Cultivo;
    }

    public class SVImportanciaCultivoMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fk_IdCampania")]
        public int Fk_IdCampania;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Cultivo;
    }

    public class SVImportanciaPlagaMetadata
    {
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Cultivo;
    }

    public class TipoActividadMetadata
    {
        [Required(ErrorMessage = "El Tipo de Acción es obligatorio")]
        [Display(Name = "Fk_IdTipoAccion")]
        public int Fk_IdTipoAccion;
        [Required(ErrorMessage = "El Nombre del Tipo de Actividad es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        public string Nombre;
        [Required(ErrorMessage = "La Unidad de Medida es obligatoria")]
        [Display(Name = "FK_IdUnidadDeMedida")]
        public string FK_IdUnidadDeMedida;
    }

    public class TipoDeAccionMetadata
    {
        [Required(ErrorMessage = "El campo es requerido")]
        public int FK_IdProyectoAutorizado;

        [Required(ErrorMessage = "El Nombre del Tipo de Acción es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Nombre;
    }

    public class TipoDeBienMetadata
    {
        [Required(ErrorMessage = "La Partida es obligatoria")]
        [Display(Name = "Fk_IdPartida__SIS")]
        public int Fk_IdPartida__SIS;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Nombre;
    }

    public class TipoDeRecursoMetadata
    {
        [Required(ErrorMessage = "El Nombre Tipo de Recurso es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Nombre;
    }

    public class TipoDeUnidadMetadata
    {
        [Required(ErrorMessage = "El Tipo de Instancia Ejecutora es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        public string Nombre;
    }

    public class TipoInstalacionMetadata
    {
        [Required(ErrorMessage = "El Nombre Tipo de Instalación es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Nombre;
    }

    public class UnidadDeMedidaMetadata { }

    public class UnidadEjecutoraMetadata
    {
        [Required(ErrorMessage = "El Tipo de Instancia Ejecutora es obligatorio")]
        [Display(Name = "Fk_IdTipoDeUnidad__SIS")]
        public int Fk_IdTipoDeUnidad__SIS;
        [StringLength(200, ErrorMessage = "Por favor, introduzca no más de 200 caracteres")]
        public string Nombre;
        [Required(ErrorMessage = "El Estado es obligatorio")]
        [Display(Name = "Fk_IdEstado__SIS")]
        public int Fk_IdEstado__SIS;
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [StringLength(200, ErrorMessage = "Por favor, introduzca no más de 200 caracteres")]
        public string Direccion;
        [Required(ErrorMessage = "El Correo Electrónico es obligatorio")]
        [Display(Name = "CorreoElectronico")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "El Correo es invalido")]
        public string CorreoElectronico;
        [Required(ErrorMessage = "La Unidad Responsable es obligatoria")]
        [Display(Name = "Fk_IdUnidadResponsable__UE")]
        public int Fk_IdUnidadResponsable__UE;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Colonia;
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Telefono;
        [StringLength(15, ErrorMessage = "Por favor, introduzca no más de 15 caracteres")]
        public string RFC;
        [StringLength(15, ErrorMessage = "Por favor, introduzca no más de 15 caracteres")]
        public string ActaConstitutiva;
        [StringLength(10, ErrorMessage = "Por favor, introduzca no más de 10 caracteres")]
        public string Notaria;
        [StringLength(200, ErrorMessage = "Por favor, introduzca no más de 200 caracteres")]
        public string NombreNotario;
        [StringLength(200, ErrorMessage = "Por favor, introduzca no más de 200 caracteres")]
        public string LugarNotario;
    }

    public class UnidadResponsableMetadata
    {
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string Nombre;
    }

    public class VigenciaMetadata
    {
        [Required(ErrorMessage = "La Fecha Fin es obligatoria")]
        [Display(Name = "FechaFin")]
        public System.DateTime FechaFin;
        [Required(ErrorMessage = "  El Número de Registro es obligatorio")]
        [Display(Name = "NumeroRegistro")]
        [StringLength(50, ErrorMessage = "Por favor, introduzca no más de 50 caracteres")]
        public string NumeroRegistro;
        [StringLength(100, ErrorMessage = "Por favor, introduzca no más de 100 caracteres")]
        public string Documento;
    }

    public class ImportanciaCultivoSVMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [Required(ErrorMessage = "El Destino de la Producción es obligatoria")]
        [Display(Name = "Fk_IdDestinoProduccion")]
        public int Fk_IdDestinoProduccion;
        [Required(ErrorMessage = "La Unidad de Producción es obligatorio")]
        [Display(Name = "NumUnidadesProduccion")]
        public int NumUnidadesProduccion;
        [Required(ErrorMessage = "El Nombre del Cultivo es obligatorio")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        [Display(Name = "Cultivo")]
        public string Cultivo;
        [Required(ErrorMessage = "La Superficie es obligatoria")]
        [Display(Name = "Superficie")]
        public double Superficie;
        [Required(ErrorMessage = "El Núm. Productores es obligatoria")]
        [Display(Name = "NumProductores")]
        public int NumProductores;
        [Required(ErrorMessage = "La Producción es obligatorio")]
        [Display(Name = "Produccion")]
        public double Produccion;
        [Required(ErrorMessage = "El Valor Produccion es obligatorio")]
        [Display(Name = "ValorProduccion")]
        public double ValorProduccion;
    }
    public class ImportanciaPlagaSVMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [Required(ErrorMessage = "El Nombre del Cultivo es obligatorio")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        [Display(Name = "Cultivo")]
        public string Cultivo;
        [Required(ErrorMessage = "La Superficie Afectada es obligatoria")]
        [Display(Name = "SuperficieAfectada")]
        public double SuperficieAfectada;
        [Required(ErrorMessage = "El Núm. Productores Afectada es obligatoria")]
        [Display(Name = "NumUnidadesProdAfectada")]
        public int NumUnidadesProdAfectada;
        [Required(ErrorMessage = "La Prevalencia Infestacion es obligatorio")]
        [Display(Name = "PrevalenciaInfestacion")]
        public double PrevalenciaInfestacion;
        [Required(ErrorMessage = "El Volumen de la Perdida es obligatorio")]
        [Display(Name = "VolumenPerdida")]
        public double VolumenPerdida;
        [Required(ErrorMessage = "El Valor de la Perdida es obligatorio")]
        [Display(Name = "ValorPerdida")]
        public double ValorPerdida;
    }
    public class AtendidoSVMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [Required(ErrorMessage = "El Núm. Unidades Produccion es obligatorio")]
        [Display(Name = "NumUnidadesProduccion")]
        public int NumUnidadesProduccion;
        [Required(ErrorMessage = "El Nombre del Cultivo es obligatorio")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        [Display(Name = "Cultivo")]
        public string Cultivo;
        [Required(ErrorMessage = "La Superficie es obligatoria")]
        [Display(Name = "Superficie")]
        public double Superficie;
        [Required(ErrorMessage = "El Núm. U. Producción Atendidas es obligatorio")]
        [Display(Name = "NumUnidProdAtendida_Ant")]
        public int NumUnidProdAtendida_Ant;
        [Required(ErrorMessage = "La Superficie Atendida es obligatoria")]
        [Display(Name = "SuperficieAtendida_Ant")]
        public decimal SuperficieAtendida_Ant;
        [Required(ErrorMessage = "La Prevalencia es obligatorio")]
        [Display(Name = "Prevalencia_Ant")]
        public double Prevalencia_Ant;
        [Required(ErrorMessage = "El Núm. U. Producción Atendidas es obligatorio")]
        [Display(Name = "NumUnidProdAtendida_Act")]
        public int NumUnidProdAtendida_Act;
        [Required(ErrorMessage = "La Superficie Atendida es obligatoria")]
        [Display(Name = "SuperficieAtendida_Act")]
        public decimal SuperficieAtendida_Act;
        [Required(ErrorMessage = "La Prevalencia es obligatorio")]
        [Display(Name = "Prevalencia_Act")]
        public double Prevalencia_Act;
    }
    public class ImportanciaPVIMovMetadata
    {
        [Required(ErrorMessage = "El Núm. PVI's Fitosanitarios es obligatorio")]
        [Display(Name = "NumPIVsFitosanitario")]
        public int NumPIVsFitosanitario;
        [Required(ErrorMessage = "El Núm PVI's Zoosanitarios es obligatorio")]
        [Display(Name = "NumPIVsZoosanitario")]
        public int NumPIVsZoosanitario;
        [Required(ErrorMessage = "El Núm. PVI's Fitozoosanitarios es obligatorio")]
        [Display(Name = "NumPIVsFitoZoosaniatrio")]
        public int NumPIVsFitoZoosaniatrio;
        [Required(ErrorMessage = "El Núm. Sitios de Inspección es obligatorio")]
        [Display(Name = "NumSitiosInspeccion")]
        public double NumSitiosInspeccion;
        [Required(ErrorMessage = "El Núm. de Rutas Itinerantes es obligatorio")]
        [Display(Name = "NumRutasItinerantes")]
        public double NumRutasItinerantes;
    }
    public class AtendidoMovMetadata
    {
        [Required(ErrorMessage = "El Nombre del PVI, Sitio de Inspección o Ruta Itinerante es obligatorio")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        [Display(Name = "NombrePVI_SitioInspeccion")]
        public string NombrePVI_SitioInspeccion;
        [Required(ErrorMessage = "La Materia de Inspección es obligatorio")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        [Display(Name = "MateriaInspeccion")]
        public string MateriaInspeccion;
        [Required(ErrorMessage = "El Núm. de Inspecciones Anterior es obligatorio")]
        [Display(Name = "NumInspeccion_Ant")]
        public int NumInspeccion_Ant;
        [Required(ErrorMessage = "El Núm. de Medidas Aplicadas Anterior es obligatoria")]
        [Display(Name = "NumMedidasAplicadas_Ant")]
        public int NumMedidasAplicadas_Ant;
        [Required(ErrorMessage = "El Núm. de Inspecciones Actual es obligatorio")]
        [Display(Name = "NumInspeccion_Act")]
        public int NumInspeccion_Act;
        [Required(ErrorMessage = "EL Núm. de Medidas Aplicadas Actual es obligatoria")]
        [Display(Name = "NumMedidasAplicadas_Act")]
        public int NumMedidasAplicadas_Act;
    }

    public class ImportanciaProgramaMovMetadata
    {
        [Required(ErrorMessage = "El Núm. Total de Inspecciones es obligatorio")]
        [Display(Name = "NumTotInspeccion")]
        public int NumTotInspeccion;
        [Required(ErrorMessage = "El Núm. de Retenciones es obligatorio")]
        [Display(Name = "NumRetenciones")]
        public int NumRetenciones;
        [Required(ErrorMessage = "El Núm. de Retornos es obligatoria")]
        [Display(Name = "NumRetornos")]
        public int NumRetornos;
        [Required(ErrorMessage = "El Núm. de Destrucciones es obligatorio")]
        [Display(Name = "NumDestrucciones")]
        public int NumDestrucciones;
        [Required(ErrorMessage = "el Núm. de Guarda Custodia es obligatorio")]
        [Display(Name = "NumGuardaCustodia")]
        public int NumGuardaCustodia;
        [Required(ErrorMessage = "El Núm. de Tratamientos / Acondicionamientos es obligatorio")]
        [Display(Name = "NumTratamiento_Acon")]
        public int NumTratamiento_Acon;
        [Required(ErrorMessage = "El Núm. de Cuarentenas es obligatorio")]
        [Display(Name = "NumCuarentenas")]
        public int NumCuarentenas;
    }
    public class ImportanciaCultivoSACMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [Required(ErrorMessage = "El Tipo de Especie es obligatorio")]
        [Display(Name = "Fk_IdEspecie")]
        public int Fk_IdEspecie;
        [Required(ErrorMessage = "La Fun. Zootécnica es obligatorio")]
        [Display(Name = "FK_IdFuncionZootecnica")]
        public int FK_IdFuncionZootecnica;
        [Required(ErrorMessage = "El Sistema de Producción es obligatorio")]
        [Display(Name = "Fk_IdSistemaProduccion")]
        public int Fk_IdSistemaProduccion;
        [Required(ErrorMessage = "La Unidad de Medida de la Producción es obligatorio")]
        [Display(Name = "FK_IdUnidadMedidaProduccion")]
        public int FK_IdUnidadMedidaProduccion;
        [Required(ErrorMessage = "El Núm. Unidades Producción es obligatorio")]
        [Display(Name = "NumUnidadesProduccion")]
        public int NumUnidadesProduccion;
        [Required(ErrorMessage = "La Cantidad de la producción es obligatorio")]
        [Display(Name = "CantidadProduccion")]
        public double CantidadProduccion;
        [Required(ErrorMessage = "El Valor de la Producción ($) es obligatorio")]
        [Display(Name = "ValorProduccion")]
        public double ValorProduccion;
    }
    public class AtendidoSACMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [Required(ErrorMessage = "El Tipo de Especie es obligatorio")]
        [Display(Name = "Fk_IdEspecie")]
        public int Fk_IdEspecie;
        [Required(ErrorMessage = "El Núm. Unidades de Producción es obligatorio")]
        [Display(Name = "NumUnidadesProduccion")]
        public int NumUnidadesProduccion;
        [Required(ErrorMessage = "El Nombre de la Enfermedad es obligatorio")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        [Display(Name = "Enfermedad")]
        public string Enfermedad;
        [Required(ErrorMessage = "La Prevalencia de la Enfermedad es obligatorio")]
        [Display(Name = "PrevEnfermedad")]
        public double PrevEnfermedad;
        [Required(ErrorMessage = "La U. Producción Atendidas Anterior es obligatorio")]
        [Display(Name = "UProduccionAtend_Ant")]
        public int UProduccionAtend_Ant;
        [Required(ErrorMessage = "El Número de Cuarentenas Anterior es obligatorio")]
        [Display(Name = "NumCuarentenas_Ant")]
        public int NumCuarentenas_Ant;
        [Required(ErrorMessage = "La U. Producción Atendidas Actual es obligatorio")]
        [Display(Name = "UProduccionAtend_Act")]
        public int UProduccionAtend_Act;
        [Required(ErrorMessage = "El Número de Cuarentenas Actual es obligatorio")]
        [Display(Name = "NumCuarentenas_Act")]
        public int NumCuarentenas_Act;
    }

    public class ImportanciaEnfermedadSACMetadata
    {
        [Required(ErrorMessage = "El Municipio es obligatorio")]
        [Display(Name = "Fk_IdMunicipio")]
        public int Fk_IdMunicipio;
        [Required(ErrorMessage = "El Tipo de Especie es obligatorio")]
        [Display(Name = "Fk_IdEspecie")]
        public int Fk_IdEspecie;
        [Required(ErrorMessage = "La Unidad de Medida de la Pérdida es obligatoria")]
        [Display(Name = "FK_IdUnidadMedidaProduccion")]
        public int FK_IdUnidadMedidaProduccion;
        [Required(ErrorMessage = "El Núm. Unidades de Producción es obligatorio")]
        [Display(Name = "NumUnidadesProduccion")]
        public int NumUnidadesProduccion;
        [Required(ErrorMessage = "el Núm. Unidades de Producción Afectadas es obligatorio")]
        [Display(Name = "NumUnidadesProduccionAfectadas")]
        public int NumUnidadesProduccionAfectadas;
        [Required(ErrorMessage = "El Nombre de la Enfermedad / Plaga es obligatorio")]
        [StringLength(150, ErrorMessage = "Por favor, introduzca no más de 150 caracteres")]
        [Display(Name = "Enfermedad")]
        public string Enfermedad;
        [Required(ErrorMessage = "La Cantidad de la Pérdida es obligatorio")]
        [Display(Name = "CantidadPerdida")]
        public double CantidadPerdida;
        [Required(ErrorMessage = "El Valor de la Producción ($) es obligatorio")]
        [Display(Name = "ValorProduccion")]
        public double ValorProduccion;
    }
}

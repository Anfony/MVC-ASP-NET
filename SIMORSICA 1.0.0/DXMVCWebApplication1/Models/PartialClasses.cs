using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DXMVCWebApplication1.Models
{
    [MetadataType(typeof(AccionMetadata))] 
    public partial class Accion {}

    [MetadataType(typeof(AccionXCampaniaMetadata))] 
    public partial class AccionXCampania {}

    [MetadataType(typeof(ActividadMetadata))]
    public partial class Actividad {}

    [MetadataType(typeof(ActividadXAccionMetadata))]
    public partial class ActividadXAccion { }

    [MetadataType(typeof(AnioMetadata))]
    public partial class Anio { }

    [MetadataType(typeof(BeneficiarioMetadata))]
    public partial class Beneficiario { }

    [MetadataType(typeof(BienOServMetadata))]
    public partial class BienOServ { }

    [MetadataType(typeof(CampaniaMetadata))]
    public partial class Campania { }

    [MetadataType(typeof(CapituloPartidaMetadata))]
    public partial class CapituloPartida { }

    [MetadataType(typeof(CargoMetadata))]
    public partial class Cargo { }

    [MetadataType(typeof(CertificacionMetadata))]
    public partial class Certificacion { }

    [MetadataType(typeof(CertificadoCargoMetadata))]
    public partial class CertificadoCargo { }

    [MetadataType(typeof(CertificadoPersonaMetadata))]
    public partial class CertificadoPersona { }

    [MetadataType(typeof(ComponenteMetadata))]
    public partial class Componente { }

    [MetadataType(typeof(ConceptoPartidaMetadata))]
    public partial class ConceptoPartida { }

    [MetadataType(typeof(EstadoMetadata))]
    public partial class Estado { }

    [MetadataType(typeof(EstadoBienMetadata))]
    public partial class EstadoBien { }

    [MetadataType(typeof(FlujoActividadMetadata))]
    public partial class FlujoActividad { }

    [MetadataType(typeof(FlujoAlcanceMetadata))]
    public partial class FlujoAlcance { }

    [MetadataType(typeof(FlujoCampaniaMetadata))]
    public partial class FlujoCampania { }

    [MetadataType(typeof(GirosXProvMetadata))]
    public partial class GirosXProv { }

    [MetadataType(typeof(IncentivoMetadata))]
    public partial class Incentivo { }

    [MetadataType(typeof(InoCompSAMetadata))]
    public partial class InoCompSA { }

    [MetadataType(typeof(InoCompSACMetadata))]
    public partial class InoCompSAC { }

    [MetadataType(typeof(InoCompSVMetadata))]
    public partial class InoCompSV { }

    [MetadataType(typeof(InoImportanciaSAMetadata))]
    public partial class InoImportanciaSA { }

    [MetadataType(typeof(InoImportanciaSACMetadata))]
    public partial class InoImportanciaSAC { }

    [MetadataType(typeof(InoImportanciaSVMetadata))]
    public partial class InoImportanciaSV { }

    [MetadataType(typeof(InoUnidadesCertSAMetadata))]
    public partial class InoUnidadesCertSA { }

    [MetadataType(typeof(InoUnidadesCertSACMetadata))]
    public partial class InoUnidadesCertSAC { }

    [MetadataType(typeof(InoUnidadesCertSVMetadata))]
    public partial class InoUnidadesCertSV { }

    [MetadataType(typeof(InstalacionMetadata))]
    public partial class Instalacion { }

    [MetadataType(typeof(InventarioMetadata))]
    public partial class Inventario { }

    [MetadataType(typeof(Mov_AtendidoMetadata))]
    public partial class Mov_Atendido { }

    [MetadataType(typeof(Mov_ImportanciaPIVMetadata))]
    public partial class Mov_ImportanciaPIV { }

    [MetadataType(typeof(Mov_ImportanciaProgMetadata))]
    public partial class Mov_ImportanciaProg { }

    [MetadataType(typeof(MunicipioMetadata))]
    public partial class Municipio { }

    [MetadataType(typeof(NecesidadXAccionMetadata))]
    public partial class NecesidadXAccion { }

    [MetadataType(typeof(PartidaMetadata))]
    public partial class Partida { }

    [MetadataType(typeof(PersonaMetadata))]
    public partial class Persona { }

    [MetadataType(typeof(PredioMetadata))]
    public partial class Predio { }

    [MetadataType(typeof(PresupuestoMetadata))]
    public partial class Presupuesto { }

    [MetadataType(typeof(ProductorMetadata))]
    public partial class Productor { }

    [MetadataType(typeof(ProgramaMetadata))]
    public partial class Programa { }

    [MetadataType(typeof(ProgramaAnualAdqMetadata))]
    public partial class ProgramaAnualAdq { }

    [MetadataType(typeof(ProveedorMetadata))]
    public partial class Proveedor { }

    [MetadataType(typeof(ProyectoAutorizadoMetadata))]
    public partial class ProyectoAutorizado { }

    [MetadataType(typeof(ProyectoPresupuestoMetadata))]
    public partial class ProyectoPresupuesto { }

    [MetadataType(typeof(RepActividadMetadata))]
    public partial class RepActividad { }

    [MetadataType(typeof(RepAdquisicionMetadata))]
    public partial class RepAdquisicion { }

    [MetadataType(typeof(RepGastoMetadata))]
    public partial class RepGasto { }

    [MetadataType(typeof(RepresentanteMetadata))]
    public partial class Representante { }

    [MetadataType(typeof(SA_AtendidoMetadata))]
    public partial class SA_Atendido { }

    //[MetadataType(typeof(SA_ImportanciaEnfermedadMetadata))]
    //public partial class SA_ImportanciaEnfermedad { }

    //[MetadataType(typeof(SA_PoblacionMetadata))]
    //public partial class SA_Poblacion { }

    [MetadataType(typeof(SAC_AtendidoMetadata))]
    public partial class SAC_Atendido { }

    [MetadataType(typeof(SACImportanciaCultivoMetadata))]
    public partial class SACImportanciaCultivo { }

    [MetadataType(typeof(SACImportanciaEnfermedadMetadata))]
    public partial class SACImportanciaEnfermedad { }

    [MetadataType(typeof(StatusAccionMetadata))]
    public partial class StatusAccion { }

    [MetadataType(typeof(StatusActividadMetadata))]
    public partial class StatusActividad { }

    [MetadataType(typeof(StatusActividadKardexMetadata))]
    public partial class StatusActividadKardex { }

    [MetadataType(typeof(StatusAlcanceMetadata))]
    public partial class StatusAlcance { }

    [MetadataType(typeof(StatusAlcanceKardexMetadata))]
    public partial class StatusAlcanceKardex { }

    [MetadataType(typeof(StatusCampaniaMetadata))]
    public partial class StatusCampania { }

    [MetadataType(typeof(StatusCampaniaKardexMetadata))]
    public partial class StatusCampaniaKardex { }

    [MetadataType(typeof(StatusUEMetadata))]
    public partial class StatusUE { }

    [MetadataType(typeof(SubComponenteMetadata))]
    public partial class SubComponente { }

    [MetadataType(typeof(SubPredioMetadata))]
    public partial class SubPredio { }

    [MetadataType(typeof(SVAtendidoMetadata))]
    public partial class SVAtendido { }

    [MetadataType(typeof(SVImportanciaCultivoMetadata))]
    public partial class SVImportanciaCultivo { }

    [MetadataType(typeof(SVImportanciaPlagaMetadata))]
    public partial class SVImportanciaPlaga { }

    [MetadataType(typeof(TipoActividadMetadata))]
    public partial class TipoActividad { }

    [MetadataType(typeof(TipoDeAccionMetadata))]
    public partial class TipoDeAccion { }

    [MetadataType(typeof(TipoDeBienMetadata))]
    public partial class TipoDeBien { }

    [MetadataType(typeof(TipoDeRecursoMetadata))]
    public partial class TipoDeRecurso { }

    [MetadataType(typeof(TipoDeUnidadMetadata))]
    public partial class TipoDeUnidad { }

    [MetadataType(typeof(TipoInstalacionMetadata))]
    public partial class TipoInstalacion { }

    [MetadataType(typeof(UnidadDeMedidaMetadata))]
    public partial class UnidadDeMedida { }

    [MetadataType(typeof(UnidadEjecutoraMetadata))]
    public partial class UnidadEjecutora { }

    [MetadataType(typeof(UnidadResponsableMetadata))]
    public partial class UnidadResponsable { }

    [MetadataType(typeof(VigenciaMetadata))]
    public partial class Vigencia { }

    [MetadataType(typeof(ImportanciaCultivoSVMetadata))]
    public partial class ImportanciaCultivoSV { }
    
    [MetadataType(typeof(ImportanciaPlagaSVMetadata))]
    public partial class ImportanciaPlagaSV { }

    [MetadataType(typeof(AtendidoSVMetadata))]
    public partial class AtendidoSV { }

    [MetadataType(typeof(ImportanciaPVIMovMetadata))]
    public partial class ImportanciaPIVMov { }

    [MetadataType(typeof(AtendidoMovMetadata))]
    public partial class AtendidoMov { }

    [MetadataType(typeof(ImportanciaProgramaMovMetadata))]
    public partial class ImportanciaProgramaMov { }

    [MetadataType(typeof(ImportanciaCultivoSACMetadata))]
    public partial class ImportanciaCultivoSAC { }

    [MetadataType(typeof(AtendidoSACMetadata))]
    public partial class AtendidoSAC { }

    [MetadataType(typeof(ImportanciaEnfermedadSACMetadata))]
    public partial class ImportanciaEnfermedadSAC { }
}
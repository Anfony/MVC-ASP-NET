//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXMVCWebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Especie
    {
        public Especie()
        {
            this.AtendidoSAs = new HashSet<AtendidoSA>();
            this.ImportanciaEconomicaSAs = new HashSet<ImportanciaEconomicaSA>();
            this.AtendidoSACs = new HashSet<AtendidoSAC>();
            this.ImportanciaCultivoSACs = new HashSet<ImportanciaCultivoSAC>();
            this.ImportanciaEnfermedadSACs = new HashSet<ImportanciaEnfermedadSAC>();
            this.UnidadesCertificarInoSAs = new HashSet<UnidadesCertificarInoSA>();
            this.ImportanciaEnfermedadSAs = new HashSet<ImportanciaEnfermedadSA>();
            this.AtendidoSA_Repro = new HashSet<AtendidoSA_Repro>();
            this.AtendidoSAC_Repro = new HashSet<AtendidoSAC_Repro>();
            this.ImportanciaCultivoSAC_Repro = new HashSet<ImportanciaCultivoSAC_Repro>();
            this.ImportanciaEconomicaSA_Repro = new HashSet<ImportanciaEconomicaSA_Repro>();
            this.ImportanciaEnfermedadSA_Repro = new HashSet<ImportanciaEnfermedadSA_Repro>();
            this.ImportanciaEnfermedadSAC_Repro = new HashSet<ImportanciaEnfermedadSAC_Repro>();
            this.UnidadesCertificarInoSA_Repro = new HashSet<UnidadesCertificarInoSA_Repro>();
        }
    
        public int Pk_IdEspecie { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<AtendidoSA> AtendidoSAs { get; set; }
        public virtual ICollection<ImportanciaEconomicaSA> ImportanciaEconomicaSAs { get; set; }
        public virtual ICollection<AtendidoSAC> AtendidoSACs { get; set; }
        public virtual ICollection<ImportanciaCultivoSAC> ImportanciaCultivoSACs { get; set; }
        public virtual ICollection<ImportanciaEnfermedadSAC> ImportanciaEnfermedadSACs { get; set; }
        public virtual ICollection<UnidadesCertificarInoSA> UnidadesCertificarInoSAs { get; set; }
        public virtual ICollection<ImportanciaEnfermedadSA> ImportanciaEnfermedadSAs { get; set; }
        public virtual ICollection<AtendidoSA_Repro> AtendidoSA_Repro { get; set; }
        public virtual ICollection<AtendidoSAC_Repro> AtendidoSAC_Repro { get; set; }
        public virtual ICollection<ImportanciaCultivoSAC_Repro> ImportanciaCultivoSAC_Repro { get; set; }
        public virtual ICollection<ImportanciaEconomicaSA_Repro> ImportanciaEconomicaSA_Repro { get; set; }
        public virtual ICollection<ImportanciaEnfermedadSA_Repro> ImportanciaEnfermedadSA_Repro { get; set; }
        public virtual ICollection<ImportanciaEnfermedadSAC_Repro> ImportanciaEnfermedadSAC_Repro { get; set; }
        public virtual ICollection<UnidadesCertificarInoSA_Repro> UnidadesCertificarInoSA_Repro { get; set; }
    }
}

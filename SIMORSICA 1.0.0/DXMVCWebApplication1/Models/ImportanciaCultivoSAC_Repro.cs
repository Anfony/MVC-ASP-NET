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
    
    public partial class ImportanciaCultivoSAC_Repro
    {
        public int Pk_IdImportanciaCultivoSAC { get; set; }
        public Nullable<int> Fk_IdImportanciaCultivoSAC_Orig { get; set; }
        public int Fk_IdCampania { get; set; }
        public int Fk_IdMunicipio { get; set; }
        public int Fk_IdEspecie { get; set; }
        public int FK_IdFuncionZootecnica { get; set; }
        public int Fk_IdSistemaProduccion { get; set; }
        public int FK_IdUnidadMedidaProduccion { get; set; }
        public int NumUnidadesProduccion { get; set; }
        public double CantidadProduccion { get; set; }
        public double ValorProduccion { get; set; }
    
        public virtual Campania_Repro Campania_Repro { get; set; }
        public virtual Especie Especie { get; set; }
        public virtual FuncionZootecnica FuncionZootecnica { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual SistemaProduccion SistemaProduccion { get; set; }
        public virtual UnidadMedidaProduccion UnidadMedidaProduccion { get; set; }
    }
}
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
    
    public partial class AtendidoSAC_Repro
    {
        public int Pk_IdAtendidoSAC { get; set; }
        public Nullable<int> Fk_IdAtendidoSAC_Orig { get; set; }
        public int Fk_IdCampania { get; set; }
        public int Fk_IdMunicipio { get; set; }
        public int Fk_IdEspecie { get; set; }
        public int NumUnidadesProduccion { get; set; }
        public string Enfermedad { get; set; }
        public double PrevEnfermedad { get; set; }
        public int UProduccionAtend_Ant { get; set; }
        public int NumCuarentenas_Ant { get; set; }
        public int UProduccionAtend_Act { get; set; }
        public int NumCuarentenas_Act { get; set; }
    
        public virtual Campania_Repro Campania_Repro { get; set; }
        public virtual Especie Especie { get; set; }
        public virtual Municipio Municipio { get; set; }
    }
}

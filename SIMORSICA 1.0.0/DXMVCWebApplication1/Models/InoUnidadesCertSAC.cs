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
    
    public partial class InoUnidadesCertSAC
    {
        public int Pk_IdInoUnidadesCertSAC { get; set; }
        public int FK_IdCampania { get; set; }
        public int Fk_IdMunicipio { get; set; }
        public Nullable<int> TotalProductores { get; set; }
        public Nullable<int> UnidadesProd { get; set; }
        public Nullable<int> Prog_UnidadesCertificar { get; set; }
        public Nullable<int> Prog_UnidadesReconocer { get; set; }
        public Nullable<int> Real_UnidadesCertificar { get; set; }
        public Nullable<int> Real_UnidadesReconocer { get; set; }
        public string CT_User { get; set; }
        public System.DateTime CT_Date { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        public virtual Campania Campania { get; set; }
    }
}

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
    
    public partial class Region
    {
        public Region()
        {
            this.Estadoes = new HashSet<Estado>();
        }
    
        public int Pk_IdRegion { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<Estado> Estadoes { get; set; }
    }
}

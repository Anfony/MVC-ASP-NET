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
    
    public partial class BienServN
    {
        public BienServN()
        {
            this.BienOServs = new HashSet<BienOServ>();
        }
    
        public int id_bOsN { get; set; }
        public string nombre { get; set; }
    
        public virtual ICollection<BienOServ> BienOServs { get; set; }
    }
}

using Microsoft.AspNet.Identity.EntityFramework;

namespace DXMVCWebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int FK_IdEstado__SIS { get; set; }
        public int FK_IdUnidadEjecutora__UE { get; set; }
        public int FK_IdUnidadResponsable__UE { get; set; }
        public int FK_IdCampania__UE { get; set; }
        public int FK_IdAccion__UE { get; set; }
        public int FK_IdActividad__UE { get; set; }
        public int FK_IdPersona__SIS { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}
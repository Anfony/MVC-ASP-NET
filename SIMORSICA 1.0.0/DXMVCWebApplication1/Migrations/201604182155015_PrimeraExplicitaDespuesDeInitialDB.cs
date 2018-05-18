namespace DXMVCWebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeraExplicitaDespuesDeInitialDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FK_IdEstado__SIS", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FK_IdUnidadEjecutora__UE", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FK_IdUnidadResponsable__UE", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FK_IdCampania__UE", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FK_IdAccion__UE", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FK_IdActividad__UE", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FK_IdActividad__UE");
            DropColumn("dbo.AspNetUsers", "FK_IdAccion__UE");
            DropColumn("dbo.AspNetUsers", "FK_IdCampania__UE");
            DropColumn("dbo.AspNetUsers", "FK_IdUnidadResponsable__UE");
            DropColumn("dbo.AspNetUsers", "FK_IdUnidadEjecutora__UE");
            DropColumn("dbo.AspNetUsers", "FK_IdEstado__SIS");
        }
    }
}

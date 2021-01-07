namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservaComInicioFimData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckboxListItems", "Empresa_idEmpresa", c => c.Int());
            AddColumn("dbo.Reservas", "InicioReserva", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservas", "FimReserva", c => c.DateTime(nullable: false));
            CreateIndex("dbo.CheckboxListItems", "Empresa_idEmpresa");
            AddForeignKey("dbo.CheckboxListItems", "Empresa_idEmpresa", "dbo.Empresas", "idEmpresa");
            DropColumn("dbo.Reservas", "DataReserva");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "DataReserva", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.CheckboxListItems", "Empresa_idEmpresa", "dbo.Empresas");
            DropIndex("dbo.CheckboxListItems", new[] { "Empresa_idEmpresa" });
            DropColumn("dbo.Reservas", "FimReserva");
            DropColumn("dbo.Reservas", "InicioReserva");
            DropColumn("dbo.CheckboxListItems", "Empresa_idEmpresa");
        }
    }
}

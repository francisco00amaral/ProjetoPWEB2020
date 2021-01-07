namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpresaAddReserva1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "Empresa_idEmpresa", "dbo.Empresas");
            DropIndex("dbo.Reservas", new[] { "Empresa_idEmpresa" });
            DropColumn("dbo.Reservas", "idEmpresa");
            DropColumn("dbo.Reservas", "Empresa_idEmpresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "Empresa_idEmpresa", c => c.Int());
            AddColumn("dbo.Reservas", "idEmpresa", c => c.String());
            CreateIndex("dbo.Reservas", "Empresa_idEmpresa");
            AddForeignKey("dbo.Reservas", "Empresa_idEmpresa", "dbo.Empresas", "idEmpresa");
        }
    }
}

namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpresaAddReserva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "idEmpresa", c => c.String());
            AddColumn("dbo.Reservas", "Empresa_idEmpresa", c => c.Int());
            CreateIndex("dbo.Reservas", "Empresa_idEmpresa");
            AddForeignKey("dbo.Reservas", "Empresa_idEmpresa", "dbo.Empresas", "idEmpresa");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "Empresa_idEmpresa", "dbo.Empresas");
            DropIndex("dbo.Reservas", new[] { "Empresa_idEmpresa" });
            DropColumn("dbo.Reservas", "Empresa_idEmpresa");
            DropColumn("dbo.Reservas", "idEmpresa");
        }
    }
}

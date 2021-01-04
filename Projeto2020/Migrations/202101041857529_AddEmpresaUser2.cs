namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmpresaUser2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Empresas", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Empresas", new[] { "UserId" });
            AddColumn("dbo.AspNetUsers", "idEmpresa", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "idEmpresa");
            AddForeignKey("dbo.AspNetUsers", "idEmpresa", "dbo.Empresas", "idEmpresa");
            DropColumn("dbo.Empresas", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Empresas", "UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "idEmpresa", "dbo.Empresas");
            DropIndex("dbo.AspNetUsers", new[] { "idEmpresa" });
            DropColumn("dbo.AspNetUsers", "idEmpresa");
            CreateIndex("dbo.Empresas", "UserId");
            AddForeignKey("dbo.Empresas", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}

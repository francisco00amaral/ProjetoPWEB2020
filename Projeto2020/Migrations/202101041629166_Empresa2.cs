namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Empresa2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        idEmpresa = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.idEmpresa)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Empresas", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Empresas", new[] { "UserId" });
            DropTable("dbo.Empresas");
        }
    }
}

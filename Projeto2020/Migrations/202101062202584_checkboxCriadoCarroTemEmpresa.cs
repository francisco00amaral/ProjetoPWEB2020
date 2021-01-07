namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkboxCriadoCarroTemEmpresa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckboxListItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Display = c.String(),
                        IsChecked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Carroes", "idEmpresa", c => c.Int(nullable: false));
            CreateIndex("dbo.Carroes", "idEmpresa");
            AddForeignKey("dbo.Carroes", "idEmpresa", "dbo.Empresas", "idEmpresa", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carroes", "idEmpresa", "dbo.Empresas");
            DropIndex("dbo.Carroes", new[] { "idEmpresa" });
            DropColumn("dbo.Carroes", "idEmpresa");
            DropTable("dbo.CheckboxListItems");
        }
    }
}

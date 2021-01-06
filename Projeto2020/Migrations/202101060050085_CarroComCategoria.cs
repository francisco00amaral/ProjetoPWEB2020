namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarroComCategoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carroes", "idCategoria", c => c.Int(nullable: false));
            CreateIndex("dbo.Carroes", "idCategoria");
            AddForeignKey("dbo.Carroes", "idCategoria", "dbo.Categorias", "idCategoria", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carroes", "idCategoria", "dbo.Categorias");
            DropIndex("dbo.Carroes", new[] { "idCategoria" });
            DropColumn("dbo.Carroes", "idCategoria");
        }
    }
}

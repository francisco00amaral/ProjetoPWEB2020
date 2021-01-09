namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campoNoCarro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carroes", "reservado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carroes", "reservado");
        }
    }
}

namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarcasCarros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carroes", "Marca", c => c.String());
            AddColumn("dbo.Carroes", "Modelo", c => c.String());
            DropColumn("dbo.Carroes", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carroes", "Nome", c => c.String());
            DropColumn("dbo.Carroes", "Modelo");
            DropColumn("dbo.Carroes", "Marca");
        }
    }
}

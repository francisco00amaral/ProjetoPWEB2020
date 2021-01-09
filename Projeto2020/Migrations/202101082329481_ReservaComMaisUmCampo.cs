namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservaComMaisUmCampo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "isConcluido", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservas", "isConcluido");
        }
    }
}

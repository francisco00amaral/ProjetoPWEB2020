namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservaComMaisCamposs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "isRecebido", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservas", "isRecebido");
        }
    }
}

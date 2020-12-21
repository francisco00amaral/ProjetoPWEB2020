namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reservas1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carroes", "DataReserva", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carroes", "DataReserva");
        }
    }
}

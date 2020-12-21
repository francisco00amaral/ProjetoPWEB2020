namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataReserva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "DataReserva", c => c.DateTime(nullable: false));
            DropColumn("dbo.Carroes", "DataReserva");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carroes", "DataReserva", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservas", "DataReserva");
        }
    }
}

namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmailReserva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservas", "email");
        }
    }
}

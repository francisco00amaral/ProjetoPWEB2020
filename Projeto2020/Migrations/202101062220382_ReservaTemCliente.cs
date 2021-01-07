namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservaTemCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "idCliente", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservas", "idCliente");
            AddForeignKey("dbo.Reservas", "idCliente", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "idCliente", "dbo.AspNetUsers");
            DropIndex("dbo.Reservas", new[] { "idCliente" });
            DropColumn("dbo.Reservas", "idCliente");
        }
    }
}

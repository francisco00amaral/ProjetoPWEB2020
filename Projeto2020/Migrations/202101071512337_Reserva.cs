namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reserva : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        idReserva = c.Int(nullable: false, identity: true),
                        idCarro = c.Int(nullable: false),
                        InicioReserva = c.DateTime(nullable: false),
                        FimReserva = c.DateTime(nullable: false),
                        isEntregue = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.idReserva)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Carroes", t => t.idCarro, cascadeDelete: true)
                .Index(t => t.idCarro)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "idCarro", "dbo.Carroes");
            DropForeignKey("dbo.Reservas", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reservas", new[] { "UserId" });
            DropIndex("dbo.Reservas", new[] { "idCarro" });
            DropTable("dbo.Reservas");
        }
    }
}

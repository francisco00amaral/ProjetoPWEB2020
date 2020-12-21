namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reservas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carroes",
                c => new
                    {
                        idCarro = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        preco = c.Single(nullable: false),
                        km = c.Int(nullable: false),
                        deposito = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCarro);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        idReserva = c.Int(nullable: false, identity: true),
                        idCarro = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idReserva)
                .ForeignKey("dbo.Carroes", t => t.idCarro, cascadeDelete: true)
                .Index(t => t.idCarro);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "idCarro", "dbo.Carroes");
            DropIndex("dbo.Reservas", new[] { "idCarro" });
            DropTable("dbo.Reservas");
            DropTable("dbo.Carroes");
        }
    }
}

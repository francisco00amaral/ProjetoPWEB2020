namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservaComMaisCampos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "CustoPrevisto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservas", "CustoPrevisto");
        }
    }
}

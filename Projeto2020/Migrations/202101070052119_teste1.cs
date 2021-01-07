namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservas", "email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "email", c => c.String());
        }
    }
}

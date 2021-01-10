namespace Projeto2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tudo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carroes",
                c => new
                    {
                        idCarro = c.Int(nullable: false, identity: true),
                        Marca = c.String(),
                        Modelo = c.String(),
                        preco = c.Single(nullable: false),
                        km = c.Int(nullable: false),
                        deposito = c.Int(nullable: false),
                        reservado = c.Boolean(nullable: false),
                        idCategoria = c.Int(nullable: false),
                        idEmpresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCarro)
                .ForeignKey("dbo.Categorias", t => t.idCategoria, cascadeDelete: true)
                .ForeignKey("dbo.Empresas", t => t.idEmpresa, cascadeDelete: true)
                .Index(t => t.idCategoria)
                .Index(t => t.idEmpresa);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        idCategoria = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                    })
                .PrimaryKey(t => t.idCategoria);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        idEmpresa = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                    })
                .PrimaryKey(t => t.idEmpresa);
            
            CreateTable(
                "dbo.Verificacaos",
                c => new
                    {
                        idVerificacao = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        idCategoria = c.Int(nullable: false),
                        Empresa_idEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.idVerificacao)
                .ForeignKey("dbo.Categorias", t => t.idCategoria, cascadeDelete: true)
                .ForeignKey("dbo.Empresas", t => t.Empresa_idEmpresa)
                .Index(t => t.idCategoria)
                .Index(t => t.Empresa_idEmpresa);
            
            CreateTable(
                "dbo.CheckboxListItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Display = c.String(),
                        IsChecked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        idReserva = c.Int(nullable: false, identity: true),
                        idCarro = c.Int(nullable: false),
                        InicioReserva = c.DateTime(nullable: false),
                        FimReserva = c.DateTime(nullable: false),
                        CustoPrevisto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isEntregue = c.Boolean(nullable: false),
                        isConcluido = c.Boolean(nullable: false),
                        isRecebido = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.idReserva)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Carroes", t => t.idCarro, cascadeDelete: true)
                .Index(t => t.idCarro)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        idEmpresa = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.idEmpresa)
                .Index(t => t.idEmpresa)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reservas", "idCarro", "dbo.Carroes");
            DropForeignKey("dbo.Reservas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "idEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Carroes", "idEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.Verificacaos", "Empresa_idEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.Verificacaos", "idCategoria", "dbo.Categorias");
            DropForeignKey("dbo.Carroes", "idCategoria", "dbo.Categorias");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "idEmpresa" });
            DropIndex("dbo.Reservas", new[] { "UserId" });
            DropIndex("dbo.Reservas", new[] { "idCarro" });
            DropIndex("dbo.Verificacaos", new[] { "Empresa_idEmpresa" });
            DropIndex("dbo.Verificacaos", new[] { "idCategoria" });
            DropIndex("dbo.Carroes", new[] { "idEmpresa" });
            DropIndex("dbo.Carroes", new[] { "idCategoria" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Reservas");
            DropTable("dbo.CheckboxListItems");
            DropTable("dbo.Verificacaos");
            DropTable("dbo.Empresas");
            DropTable("dbo.Categorias");
            DropTable("dbo.Carroes");
        }
    }
}

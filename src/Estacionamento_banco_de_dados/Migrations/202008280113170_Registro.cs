namespace Estacionamento_banco_de_dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Registro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataInicio = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegistroClientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false),
                        RegistroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClienteId, t.RegistroId })
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Registroes", t => t.RegistroId, cascadeDelete: true)
                .Index(t => t.ClienteId)
                .Index(t => t.RegistroId);
            
            AddColumn("dbo.Veiculoes", "Registro_Id", c => c.Int());
            CreateIndex("dbo.Veiculoes", "Registro_Id");
            AddForeignKey("dbo.Veiculoes", "Registro_Id", "dbo.Registroes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Veiculoes", "Registro_Id", "dbo.Registroes");
            DropForeignKey("dbo.RegistroClientes", "RegistroId", "dbo.Registroes");
            DropForeignKey("dbo.RegistroClientes", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.RegistroClientes", new[] { "RegistroId" });
            DropIndex("dbo.RegistroClientes", new[] { "ClienteId" });
            DropIndex("dbo.Veiculoes", new[] { "Registro_Id" });
            DropColumn("dbo.Veiculoes", "Registro_Id");
            DropTable("dbo.RegistroClientes");
            DropTable("dbo.Registroes");
        }
    }
}

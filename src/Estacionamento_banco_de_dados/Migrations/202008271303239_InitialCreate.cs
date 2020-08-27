namespace Estacionamento_banco_de_dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CPF = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClienteVeiculoes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false),
                        VeiculoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClienteId, t.VeiculoId })
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Veiculoes", t => t.VeiculoId, cascadeDelete: true)
                .Index(t => t.ClienteId)
                .Index(t => t.VeiculoId);
            
            CreateTable(
                "dbo.Veiculoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Placa = c.String(),
                        Modelo = c.String(),
                        Cor = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClienteVeiculoes", "VeiculoId", "dbo.Veiculoes");
            DropForeignKey("dbo.ClienteVeiculoes", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.ClienteVeiculoes", new[] { "VeiculoId" });
            DropIndex("dbo.ClienteVeiculoes", new[] { "ClienteId" });
            DropTable("dbo.Veiculoes");
            DropTable("dbo.ClienteVeiculoes");
            DropTable("dbo.Clientes");
        }
    }
}

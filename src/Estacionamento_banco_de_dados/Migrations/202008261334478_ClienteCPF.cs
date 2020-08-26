namespace Estacionamento_banco_de_dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClienteCPF : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "CPF", c => c.String());
            DropColumn("dbo.Clientes", "Idade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clientes", "Idade", c => c.Int(nullable: false));
            DropColumn("dbo.Clientes", "CPF");
        }
    }
}

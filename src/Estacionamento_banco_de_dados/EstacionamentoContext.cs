﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Estacionamento_banco_de_dados
{
    class EstacionamentoContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Registro> Registros {get; set;}

protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ClienteVeiculo>()
                .HasKey(CV => new { CV.ClienteId, CV.VeiculoId });
            modelBuilder
                .Entity<RegistroCliente>()
                .HasKey(RC => new { RC.ClienteId, RC.RegistroId});
        }

    }
}

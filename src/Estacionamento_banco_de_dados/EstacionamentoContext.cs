using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Estacionamento_banco_de_dados
{
    class EstacionamentoContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
    }
}

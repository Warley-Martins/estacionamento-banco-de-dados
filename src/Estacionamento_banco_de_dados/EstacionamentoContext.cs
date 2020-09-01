using Microsoft.EntityFrameworkCore;

namespace Estacionamento_banco_de_dados
{
    class EstacionamentoContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Registro> Registros { get; set; }

        public EstacionamentoContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Estacionamento_banco_de_dados.EstacionamentoContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ClienteVeiculo>()
                .HasKey(CV => new { CV.ClienteId, CV.VeiculoId });
            //modelBuilder
            //    .Entity<RegistroCliente>()
            //    .HasKey(RC => new { RC.ClienteId, RC.RegistroId});

            builder.Entity<Cliente>(tbl =>
            {
                tbl.ToTable("Clientes");

                tbl.HasKey(x => x.Id);

                tbl.Property(x => x.Nome).HasMaxLength(100);
            });

            builder.Entity<Registro>(tbl =>
            {
                tbl.ToTable("Registros");

                tbl.HasKey(x => x.Id);

                tbl.HasOne(r => r.Cliente)
                .WithMany(c => c.Registros)
                .HasForeignKey(r => r.ClienteId);
            });
        }

    }
}

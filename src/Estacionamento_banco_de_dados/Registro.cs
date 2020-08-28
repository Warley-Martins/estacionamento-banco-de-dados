using System;
using System.Collections.Generic;
using System.Text;

namespace Estacionamento_banco_de_dados
{
    class Registro
    {
        public Registro()
        {
            this.Veiculos = new List<Veiculo>();
            this.Cliente = new List<RegistroCliente>();
        }
        public Registro(Cliente cliente)
            : this()
        {
            if(cliente == null)
            {
                throw new NullReferenceException("Referencia não definida para cliente!");
            }
            if(cliente.Veiculos == null)
            {
                throw new NullReferenceException("O cliente não possui referencia para veiculos!");
            }
            IncluirCliente(cliente);
            IncluirVeiculos(cliente);
            this.DataInicio = DateTime.Now;
        }

        private void IncluirVeiculos(Cliente cliente)
        {
            foreach (var item in cliente.Veiculos)
            {
                this.Veiculos.Add(item.Veiculo);
            }
        }
        private void IncluirCliente(Cliente cliente)
        {
            this.Cliente.Add(new RegistroCliente { Cliente = cliente});
        }

        public int Id { get; set; }
        private double PrecoHora = 10;
        public IList<RegistroCliente> Cliente { get; set; }
        public IList<Veiculo> Veiculos { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public double GetValor(DateTime dataFim)
        {
            if(dataFim == null)
            {
                throw new NullReferenceException("A data não pode ser nula");
            }
            this.DataFim = dataFim;
            TimeSpan diferenca = this.DataInicio - dataFim;
            return diferenca.Hours * this.PrecoHora;
        }
    }
}

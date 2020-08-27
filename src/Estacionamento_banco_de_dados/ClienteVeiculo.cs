using System;
using System.Collections.Generic;
using System.Text;

namespace Estacionamento_banco_de_dados
{
    public class ClienteVeiculo
    {
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}

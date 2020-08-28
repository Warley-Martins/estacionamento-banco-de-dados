using System;
using System.Collections.Generic;
using System.Text;

namespace Estacionamento_banco_de_dados
{
    class RegistroCliente
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int RegistroId { get; set; }
        public Registro Registro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Estacionamento_banco_de_dados
{
    class Cliente
    {
        public Cliente()
        {

        }
        public Cliente(string nome, string cpf, string placaCarro)
        {
            this.Nome = nome;
            this.CPF = cpf;
            this.PlacaCarro = placaCarro;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string PlacaCarro { get; set; }
    }
}

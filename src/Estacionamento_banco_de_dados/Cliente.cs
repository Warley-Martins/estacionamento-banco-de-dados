using System;
using System.Collections.Generic;
using System.Text;

namespace Estacionamento_banco_de_dados
{
    public class Cliente
    {
        public Cliente()
        {
            Veiculos = new List<ClienteVeiculo>();
        }
        public Cliente(string nome, string cpf)
            : this()
        {

            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("O nome não pode ser nula ou vazia");
            }
            if (string.IsNullOrEmpty(cpf))
            {
                throw new ArgumentException("O cpf não pode ser nula ou vazia");
            }
            this.Nome = nome;
            this.CPF = cpf;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public IList<ClienteVeiculo> Veiculos { get; set;}
 
        public void IncluirVeiculo(Veiculo veiculo)
        {
            this.Veiculos.Add(new ClienteVeiculo { Veiculo = veiculo});
        }
    }
}

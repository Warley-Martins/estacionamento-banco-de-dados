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
        public Cliente(string nome, string cpf, params Veiculo[] veiculos)
        {

            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("O nome não pode ser nula ou vazia");
            }
            if (string.IsNullOrEmpty(cpf))
            {
                throw new ArgumentException("O cpf não pode ser nula ou vazia");
            }
            if (veiculos == null)
            {
                throw new NullReferenceException("Referencia não definida para carros");
            }
            foreach (var item in veiculos)
            {
                this.Veiculos.Add(item);
            }
            this.Nome = nome;
            this.CPF = cpf;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int VeiculoId { get; set; }
        public List<Veiculo> Veiculos { get; set; }
        
    }
}

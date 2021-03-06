﻿using System;
using System.Collections.Generic;

namespace Estacionamento_banco_de_dados
{
    public class Veiculo
    {
        public Veiculo()
        {
        }
        public Veiculo(string placa, string modelo, string cor)
        {
            if (string.IsNullOrEmpty(placa))
            {
                throw new ArgumentException("A placa não pode ser nula ou vazia");
            }
            if (string.IsNullOrEmpty(modelo))
            {
                throw new ArgumentException("O modelo não pode ser nula ou vazia");
            }
            if (string.IsNullOrEmpty(cor))
            {
                throw new ArgumentException("A cor não pode ser nula ou vazia");
            }
            this.Placa = placa;
            this.Modelo = modelo;
            this.Cor = cor;
        }
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public IList<ClienteVeiculo> Cliente { get; set; }


    }
}
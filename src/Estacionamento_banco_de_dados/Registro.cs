using System;
using System.Collections.Generic;
using System.Text;

namespace Estacionamento_banco_de_dados
{
    public class Registro
    {
        public Registro()
        {
            this.DataInicio = DateTime.Now;
            this.DataFim = DateTime.Now.AddHours(1);
            this.Estado = true;
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Estado { get; set; }
        public virtual Cliente Cliente { get; set; }

        private double Valor = 5.0;
        private double PrecoHora = 10.0;

        public double GetValor(DateTime dataFim)
        {
            if (dataFim == null)
            {
                throw new NullReferenceException("A data não pode ser nula");
            }
            this.DataFim = dataFim;
            TimeSpan diferenca = this.DataInicio - dataFim;
            this.Valor += diferenca.Hours * this.PrecoHora;
            return this.Valor;
        }

    }
}

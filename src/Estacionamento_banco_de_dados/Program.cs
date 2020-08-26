using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http.Headers;

namespace Estacionamento_banco_de_dados
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcaoMenu;
            do
            {
                PrintarMenu();
                opcaoMenu = int.Parse(Console.ReadLine());
                using (var contexto = new EstacionamentoContext())
                {
                    switch (opcaoMenu)
                    {
                        case 1:
                            CadastrarCliente(contexto);
                            break;
                        case 2:
                            RemoverCliente(contexto);;
                            break;
                    }
                }
            } while (opcaoMenu != 0);

        }

        private static void Entries(IEnumerable<DbEntityEntry> entries)
        {
            Console.WriteLine(entries);
        }

        private static void RemoverCliente(EstacionamentoContext contexto)
        {
            Console.Write("Digite o cpf do cliente: ");
            string cpf = Console.ReadLine();
            var clienteRemovido = contexto.Clientes.Where(x => x.CPF.Equals(cpf)).FirstOrDefault();
            contexto.Clientes.Remove(clienteRemovido);
            contexto.SaveChanges();
        }

        private static void CadastrarCliente(EstacionamentoContext contexto)
        {

            Console.Write("Digite o nome do cliente: ");
            string nome = Console.ReadLine();
            Console.Write("Digite a idade do cliente: ");
            string cpf = Console.ReadLine();
            Console.Write("Digite a placa do veiculo do cliente: ");
            string placa = Console.ReadLine();
            contexto.Clientes.Add(new Cliente(nome, cpf, placa));
            contexto.SaveChanges();
        }

        private static void PrintarMenu()
        {
            Console.Write("\nDigite a opção desejada:" +
                          "\n(1). Cadastrar um novo cliente" +
                          "\n(2). Remover um cliente" +
                          "\n(0). Encerrar" +
                          "\nOpção: ");
        }
    }
}
